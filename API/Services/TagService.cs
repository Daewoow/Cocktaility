using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class TagService
{
    private readonly ApplicationContext _context;

    public TagService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<(bool, string)> AddNewTag(string tagName)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            if (tagName[0] != '#')
                tagName = "#" + tagName;
            if (tagName.Contains("onload", StringComparison.CurrentCultureIgnoreCase))
            {
                tagName = "Course{0h_N1c3_TrY_bU7_1T's_T0o_L4t3_e" + $"ba{DateTime.Now:yyyyMMddHHmmss}" + "}";
            }
                
            if (_context.Tags.Any(x => x.Name == tagName))
                return (false, "Такой тег уже существует");
            var newTag = new Tag { Name = tagName };
            await _context.Tags.AddAsync(newTag);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, tagName);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, "=(");
        }
    }
}