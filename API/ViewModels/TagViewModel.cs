using API.Models;

namespace API.ViewModels;

public class TagViewModel
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public ICollection<BarViewModel> Bars { get;}

    public static TagViewModel CreateWithNoBars(Tag tag)
    {
        return new TagViewModel
        {
            Id = tag.TagId,
            Name = tag.Name,
        };
    }
}