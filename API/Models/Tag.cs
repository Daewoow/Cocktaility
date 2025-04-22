using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Tag
{
    [Key]
    public int TagId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public IEnumerable<Bar> Bars { get; internal set; }
}