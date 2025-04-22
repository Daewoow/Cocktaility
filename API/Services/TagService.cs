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

    public async Task<bool> AddNewTag(Tag tag)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var name = tag.Name;
            if (name[0] != '#')
                name = "#" + tag.Name;
            var newTag = new Tag { Name = name };
            await _context.Tags.AddAsync(newTag);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"TagError: {ex.Message}");
            return false;
        }
    }
}