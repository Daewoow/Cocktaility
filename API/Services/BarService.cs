using API.Database;
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
            .Select(t => t.TagId)
            .ToListAsync();

        if (tagIds.Count == 0)
            return new List<Bar>();

        var bars = await _context.Bars
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
}