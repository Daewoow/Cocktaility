using System.ComponentModel.DataAnnotations;

namespace API.Database;

public class Tag
{
    [Required]
    public int TagId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public IEnumerable<Bar> Bars { get; set; }
}