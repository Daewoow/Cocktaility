using System.ComponentModel.DataAnnotations;
using API.Models;

namespace API.Requests;

public class BarRequest
{
    [Required]
    public string Name { get; set; }

    [Required] public string Address { get; set; } = "ул. Чапаева, 17";

    public string Photo { get; set; } = "https://avatars.mds.yandex.net/get-altay/14185024/2a000001955181176ff4baf3d9141e3c34aa/XXXL";

    public string Menu { get; set; } = "https://tuttalavita.ru/images/blog/kids_menu18_3_2.jpg";

    public string Site { get; set; } = "https://sergorn.orb.ru/presscenter/news/125457/";

    public int Rating { get; set; } = 0;

    public string TimeOfWork { get; set; } = "Понедельник-Воскресенье: 12:00-00:00";
    
    public IEnumerable<string> Tags { get; set; } = new List<string>();
    
    public static explicit operator Bar(BarRequest bar)
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
            Tags = bar.Tags.Select(t => new Tag { Name = t })
        };
    }
}