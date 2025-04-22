using API.Models;

namespace API.ViewModels;

public class BarViewModel(Bar bar)
{
    public int Id { get; set; } = bar.BarId;

    public string Name { get; set; } = bar.Name;

    public string Address { get; set; } = bar.Address;

    public string Photo { get; set; } = bar.Photo;

    public string Menu { get; set; } = bar.Menu;

    public string Site { get; set; } = bar.Site;

    public int Rating { get; set; } = bar.Rating;

    public string TimeOfWork { get; set; } = bar.TimeOfWork;

    public List<TagViewModel> Tags { get;  } = bar.Tags.Select(TagViewModel.CreateWithNoBars).Take(4).ToList(); // Простите --Да норм

    public bool IsFavorite { get; set; }

    public static explicit operator Bar(BarViewModel bar)
    {
        return new Bar
        {
            Address = bar.Address,
            Photo = bar.Photo,
            Menu = bar.Menu,
            Site = bar.Site,
            Rating = bar.Rating,
            TimeOfWork = bar.TimeOfWork,
            Name = bar.Name,
            FavoritedByUsers = new List<Favorite>(),
            Tags = bar.Tags.Select(t => (Tag)t)
        };
    }
}