using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class TagMetric
{
    [Required]
    public int QueryId { get; set; }
    
    [Required] 
    public string Day { get; set; } = null!;
    
    [Required]
    public int TagsCount { get; set; }

    public string Tags { get; set; } = "";
}