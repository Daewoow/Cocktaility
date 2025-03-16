using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Database;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } // Или лучше Guid
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
}