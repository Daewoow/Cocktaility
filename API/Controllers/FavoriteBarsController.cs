using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.ViewModels;

namespace API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class FavoriteBarsController(BarService barService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Bar>>> GetFavoriteBars()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        try
        {
            var bars = await barService.GetFavoriteBars(userId);
            return Ok(bars.Select(bar => new BarViewModel(bar)).ToList());
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса");
        }
    }

    [HttpPost("{barId}")]
    public async Task<IActionResult> AddToFavorites(int barId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        try
        {
            var success = await barService.AddToFavorites(userId, barId);
            if (success)
                return Ok();
            return StatusCode(500, "Не найден пользователь или бар");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса");
        }
    }

    [HttpDelete("{barId}")]
    public async Task<IActionResult> RemoveFromFavorites(int barId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        try
        {
            var success  = await barService.RemoveFromFavorites(userId, barId);
            if (success)
                return Ok();
            return StatusCode(500, "Не найден пользователь или бар");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса");
        }
    }
}