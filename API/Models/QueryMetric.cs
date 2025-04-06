using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class QueryMetric
{
    [Required]
    public int QueryId { get; set; }

    [Required] 
    public string Day { get; set; } = null!;
    
    [Required]
    public long TagsCount { get; set; }
}