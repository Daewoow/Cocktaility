using System.ComponentModel.DataAnnotations;

namespace API.Database;

public class Favorite
{
    [Required]
    public int FavId { get; set; }

    [Required]
    public string UserId { get; set; }

    public AppUser User { get; set; } = null!;
    
    [Required]
    public int BarId { get; set; }
    public Bar Bar { get; set; } = null!;
}