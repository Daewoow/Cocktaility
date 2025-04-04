using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BarsController : ControllerBase
{
    internal static readonly List<Bar> _bars = new()
    {
        new Bar
        {
            Id = 1, 
            Name = "Бар 1", 
            Tags = new List<string> { "#музыка", "#андерграунд", "#камерный", "#настойки", "#безопасно" }
        },
        new Bar
        {
            Id = 2, 
            Name = "Бар 2", 
            Tags = new List<string> { "#ретро", "#настойки", "#безопасно", "#настолки", "#безалкогольный" }
        },
        new Bar
        {
            Id = 3, 
            Name = "Бар 3", 
            Tags = new List<string> { "#музыка", "#андерграунд", "#красивый_туалет", "#настолки", "#безалкогольный" }
        }
    };

    [HttpGet("{id}")]
    public ActionResult<Bar> GetById(int id)
    {
        var bar = _bars.FirstOrDefault(b => b.Id == id);
        
        if (bar == null)
        {
            return NotFound();
        }
        
        return Ok(bar);
    }
}