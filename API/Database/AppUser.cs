using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace API.Database;

public class AppUser
{
    [Required]
    public string Id { get; set; }
    [Required]
    public bool IsAdmin { get; set; }
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    public IEnumerable<Favorite> Favorites { get; set; } = new List<Favorite>();
}