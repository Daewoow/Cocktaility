using System.Security.Claims;
using API.Models;
using API.Requests;
using API.Services;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/")]
public class BarsController(BarService barService, TagService tagService) : ControllerBase
{
    [HttpGet("search")]
    public async Task<IActionResult> GetBars()
    {
        var page = new PageBuilder
        {
            Title = "Поиск",
            IsAuthenticated = User.Identity is { IsAuthenticated: true },
        }
        .AddLayout("src/components/mainLayout.html")
        .AddBody("src/components/search.html")
        .AddStyles("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css",
            "src/styles/search.css", 
            "src/styles/card.css",
            "src/styles/detailsPanel.css"
            )
        .AddScripts(false,
			"https://api-maps.yandex.ru/2.1/?apikey=c34675db-5522-4f61-b4ee-9eda5adca08e&lang=ru_RU",
            "src/scripts/search.js",
            "src/scripts/search_ymaps.js"
            )
        // .AddScripts(true,
        //     "https://api-maps.yandex.ru/v2.1/?apikey=c34675db-5522-4f61-b4ee-9eda5adca08e&lang=ru_RU\""
        //     )
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
        var barsViewModels = bars.Select(b =>
        {
            var barViewModel = new BarViewModel(b);
            return barViewModel;
        });
        return Ok(barsViewModels);
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

    [HttpPost("tags/newTag")]
    public async Task<IActionResult> AddNewTag([FromBody] string tagName)
    {
        var result = await tagService.AddNewTag(tagName);
        return result ? Ok() : BadRequest("Bad tag");
    }
    
    [HttpPost("bars/newBar")]
    public async Task<IActionResult> AddNewBar([FromBody] BarViewModel newBar)
    {
        var result = await barService.AddNewBarAsync((Bar)newBar);
        return result ? Ok() : BadRequest("Bad bar");
    }
    
    [HttpPost("bars/Favorite")]
    public async Task<IActionResult> AddFavorite([FromBody] Favorite favorite)
    {
        return await barService.AddToFavorites(ClaimTypes.NameIdentifier, favorite.BarId) 
            ? Ok()
            : BadRequest("Bad favorite");
    }
}