using API.Models;
using Microsoft.EntityFrameworkCore;

public class BarService
{
    private readonly ApplicationContext _context;

    public BarService(ApplicationContext context)
    {
        _context = context;
    }

    // Сортриует 1) по количеству совпавших тегов; 2) по алфавиту по убыванию
    public async Task<List<Bar>> SearchBarsAsync(IEnumerable<string> tagNames)
    {
        var tagNamesList = tagNames.ToList();
        _context.QueryMetrics.Add(new QueryMetric
        {
            Day = DateTime.Today.ToString("yyyy-MM-dd"),
            TagsCount = tagNamesList.Count,
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
        => await _context.Bars.FirstOrDefaultAsync(b => b.BarId == id);

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

    public async Task<bool> AddToFavorites(string userId, int barId)
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