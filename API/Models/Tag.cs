using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Tag
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public ICollection<Bar> Bars { get;}
}