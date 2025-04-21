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
}