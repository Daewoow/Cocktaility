using API.Models;

namespace API.ViewModels;

public class TagViewModel
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public IEnumerable<BarViewModel> Bars { get;}

    public static TagViewModel CreateWithNoBars(Tag tag)
    {
        return new TagViewModel
        {
            Id = tag.TagId,
            Name = tag.Name,
        };
    }

    public static explicit operator Tag(TagViewModel tag)
    {
        return new Tag
        {
            Name = tag.Name,
            Bars = tag.Bars.Select(b => (Bar)b)
        };
    }
}