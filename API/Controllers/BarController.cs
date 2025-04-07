using API.Models;
using API.Requests;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/")]
public class BarsController(BarService barService) : ControllerBase
{
    [HttpGet("search")]
    public async Task<IActionResult> GetBars()
    {
        var page = new PageBuilder
        {
            Title = "Поиск"
        }
        .AddLayout("src/components/mainLayout.html")
        .AddBody("src/components/search.html")
        .AddStyles("src/styles/search.css", "src/styles/card.css")
        .AddScripts("src/scripts/search.js")
        .Build();
        return Content(page, "text/html");
    }
    
    [HttpPost("search")]
    public async Task<IActionResult> SearchBars([FromBody] SearchRequest request)
    {
        if (request.Tags is null || !request.Tags.Any())
        {
            return BadRequest("Список тегов не может быть пустым.");
        }

        var bars = await barService.SearchBarsAsync(request.Tags);

        return Ok(bars.Select(b => new BarViewModel(b)));
    }

    [HttpGet("/bars/{id:int}")]
    public async Task<IActionResult> GetBarById(int id)
    {
        var bar = await barService.GetBarById(id);
        if (bar is null)
            return NotFound();
        return Ok(bar);
    }

    [HttpGet("tags")]
    public async Task<IActionResult> GetAllTags()
    {
        var tags = await barService.GetAllTags();
        return Ok(tags);
    }
}