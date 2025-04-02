using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace API.Models;

public class AppUser
{
    [Key]
    public string Id { get; set; }
    [Required]
    public bool IsAdmin { get; set; }
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}