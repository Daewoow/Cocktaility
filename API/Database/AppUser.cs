using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace API.Database;

public class AppUser : IdentityUser
{
    [Required]
    public bool IsAdmin { get; set; }
    [Required]
    public string Password { get; set; }
}