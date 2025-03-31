using API.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BarsController : ControllerBase
{
    private readonly BarService _barService;

    public BarsController(BarService barService)
    {
        _barService = barService;
    }

    [HttpPost("search")]
    public async Task<IActionResult> SearchBars([FromBody] SearchRequest request)
    {
        if (request.Tags is null || !request.Tags.Any())
        {
            return BadRequest("Список тегов не может быть пустым.");
        }

        var bars = await _barService.SearchBarsAsync(request.Tags);

        return Ok(bars);
    }

    [HttpGet("/bars/{id:int}")]
    public async Task<IActionResult> GetBarById(int id)
    {
        var bar = await _barService.GetBarById(id);
        if (bar is null)
            return NotFound();
        return Ok(bar);
    }
}