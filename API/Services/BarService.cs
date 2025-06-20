﻿using API.Models;
using Microsoft.EntityFrameworkCore;
using Prometheus;

public class BarService
{
    private readonly ApplicationContext _context;
    
    private static readonly Counter _queriesCounter = Metrics.CreateCounter("bar_queries_total", "Total number of queries.");

    public BarService(ApplicationContext context)
    {
        _context = context;
    }

    // Сортриует 1) по количеству совпавших тегов; 2) по алфавиту по убыванию
    public async Task<List<Bar>> SearchBarsAsync(IEnumerable<string> tagNames, string ip="undefined")
    {
        var tagNamesList = tagNames.ToList();
        var day = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + ip;
        _queriesCounter.Inc();
        _context.QueryMetrics.Add(new QueryMetric
        {
            Day = day,
            TagsCount = tagNamesList.Count,
        });
        _context.TagsMetrics.Add(new TagMetric()
        {
            Day = day,
            TagsCount = tagNamesList.Count,
            Tags = string.Join(", ", tagNamesList)
        });
		await _context.SaveChangesAsync();
        var tagIds = await _context.Tags
            .Where(t => tagNamesList.Contains(t.Name))
            .Select(t => t.TagId)
            .ToListAsync();

        if (tagIds.Count == 0)
            return new List<Bar>();

        var bars = await _context.Bars
            .AsNoTracking()
            .Include(x => x.Tags)
            .Where(b => b.Tags.Any(bt => tagIds.Contains(bt.TagId)))
            .Select(b => new
            {
                Bar = b,
                MatchingTagsCount = b.Tags.Count(bt => tagIds.Contains(bt.TagId))
            })
            .OrderByDescending(x => x.MatchingTagsCount)
            .ThenBy(x => x.Bar.Name)
            .Select(x => x.Bar)
            .ToListAsync();

        return bars;
    }

    public async Task<Bar?> GetBarById(int id) 
        => await _context.Bars.Include(bar => bar.Tags).FirstOrDefaultAsync(b => b.BarId == id);

    public async Task<IEnumerable<string>> GetAllTags()
    {
        return await _context.Tags.Select(t => t.Name).ToListAsync();
    }

    public async Task<List<Bar>> GetFavoriteBars(string userId)
    {
        var favoriteBars = await _context.Favorites
            .Where(f => f.UserId == userId)
            .Join(
                _context.Bars,
                f => f.BarId,
                b => b.BarId,
                (favorite, bar) => bar)
            .OrderBy(b => b.BarId)
            .ToListAsync();

        return favoriteBars;
    }

    public async Task<bool> AddNewBarAsync(Bar bar)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var existingBar = await _context.Bars.FirstOrDefaultAsync(b => b.Name == bar.Name);
            if (existingBar is not null)
            {
                Console.WriteLine(existingBar);
                return false;
            }

            if (bar.Name.Contains("onload", StringComparison.CurrentCultureIgnoreCase) 
                || bar.Photo.Contains("onload", StringComparison.CurrentCultureIgnoreCase) 
                || bar.Menu.Contains("onload", StringComparison.CurrentCultureIgnoreCase))
            {
                bar.Name = "Course{0h_T0o_L4t3}";
                bar.Photo = "https://i.yapx.cc/Ywk1y.png";
                bar.Menu = "https://tuttalavita.ru/images/blog/kids_menu18_3_2.jpg";
            }

            var newBar = new Bar
            {
                Address = bar.Address,
                Menu = bar.Menu,
                Name = bar.Name,
                Photo = bar.Photo,
                Site = bar.Site,
                TimeOfWork = bar.TimeOfWork,
            };
            await _context.Bars.AddAsync(newBar);
            await _context.SaveChangesAsync();

            if (bar.Tags is not null && bar.Tags.Any())
            {
                foreach (var tag in bar.Tags)
                {
                    var normalizedTagName = tag.Name.StartsWith('#') ? tag.Name : $"#{tag.Name}";
                
                    var existingTag = await _context.Tags
                        .FirstOrDefaultAsync(t => t.Name == normalizedTagName);
                    
                    if (existingTag == null)
                    {
                        existingTag = new Tag { Name = normalizedTagName };
                        await _context.Tags.AddAsync(existingTag);
                        await _context.SaveChangesAsync();
                    }

                    await _context.Database.ExecuteSqlInterpolatedAsync(
                        $"""
                         INSERT INTO public."BarTag" ("BarsBarId", "TagsTagId") 
                                             VALUES ({newBar.BarId}, {existingTag.TagId})
                         """);
                }
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> AddToFavorites(string userId, int barId)
    {
        _context.FavoriteMetrics.Add(new FavoriteMetric
        {
            UserId = userId,
            BarId = barId,
        });
        await _context.SaveChangesAsync();
        
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null)
        {
            return false;
        }
        
        var bar = await _context.Bars.FirstOrDefaultAsync(x => x.BarId == barId);
        if (bar == null)
        {
            return false;
        }

        var alreadyFavorite = await _context.Favorites
            .AnyAsync(f => f.BarId == barId && f.UserId == userId);
        
        if (alreadyFavorite)
        {
            return true;
        }
        
        var favoriteBar = new Favorite
        {
            UserId = userId,
            User = user,
            BarId = barId,
            Bar = bar
        };

        _context.Favorites.Add(favoriteBar);
        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<bool> RemoveFromFavorites(string userId, int barId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null)
        {
            return false;
        }
        
        var bar = await _context.Bars.FirstOrDefaultAsync(x => x.BarId == barId);
        if (bar == null)
        {
            return false;
        }
        
        var favoriteBar = await _context.Favorites
            .FirstOrDefaultAsync(f => f.BarId == barId && f.UserId == userId);
        
        if (favoriteBar == null)
        {
            return true;
        }

        _context.Favorites.Remove(favoriteBar);
        await _context.SaveChangesAsync();

        return true;
    }
}