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
        var tagIds = await _context.Tags
            .Where(t => tagNames.Contains(t.Name))
            .Select(t => t.Id)
            .ToListAsync();

        if (tagIds.Count == 0)
            return new List<Bar>();

        var bars = await _context.Bars
            .AsNoTracking()
            .Include(x => x.Tags)
            .Where(b => b.Tags.Any(bt => tagIds.Contains(bt.Id)))
            .Select(b => new
            {
                Bar = b,
                MatchingTagsCount = b.Tags.Count(bt => tagIds.Contains(bt.Id))
            })
            .OrderByDescending(x => x.MatchingTagsCount)
            .ThenBy(x => x.Bar.Name)
            .Select(x => x.Bar)
            .ToListAsync();

        return bars;
    }

    public async Task<Bar?> GetBarById(int id) 
        => await _context.Bars.FirstOrDefaultAsync(b => b.Id == id);

    public async Task<IEnumerable<string>> GetAllTags()
    {
        return await _context.Tags.Select(t => t.Name).ToListAsync();
    }
}