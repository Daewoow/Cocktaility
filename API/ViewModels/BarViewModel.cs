using API.Models;

namespace API.ViewModels;

public class BarViewModel
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public string Photo { get; set; }
    
    public string Menu { get; set; }
    
    public string Site { get; set; }
    
    public int Rating { get; set; }
    
    public string TimeOfWork { get; set; }
    
    public List<TagViewModel> Tags { get;  }
    
    public List<TagViewModel> FavoritedByUsers { get; set; }

    public BarViewModel(Bar bar)
    {
        Id = bar.BarId;
        Name = bar.Name;
        Address = bar.Address;
        Photo = bar.Photo;
        Menu = bar.Menu;
        Site = bar.Site;
        Rating = bar.Rating;
        TimeOfWork = bar.TimeOfWork;
        Tags = bar.Tags.Select(TagViewModel.CreateWithNoBars).Take(4).ToList(); // Простите
    }
}