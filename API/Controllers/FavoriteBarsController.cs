using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Models;

namespace API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class FavoriteBarsController(BarService barService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Bar>>> GetAllBars()
    {
        var userId = User.Identity.Name;
        
        try
        {
            var bars = await barService.GetFavoriteBars(userId);
            return Ok(bars);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Произошла ошибка при обработке запроса");
        }
    }

    [HttpPost("{barId}")]
    public async Task<IActionResult> AddToFavorites(int barId)
    {
        var userId = User.Identity.Name;
        
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
        var userId = User.Identity.Name;
        
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