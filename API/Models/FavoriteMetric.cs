using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class FavoriteMetric
{
    [Required]
    public int FavoriteQueryId { get; set; }

    [Required] 
    public string UserId { get; set; } = null!;
    
    [Required]
    public long BarId { get; set; }
}