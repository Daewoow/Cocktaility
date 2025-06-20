﻿using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    [HttpGet("check-auth")]
    public IActionResult CheckAuth()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Console.WriteLine(userId);
        return userId == null ? Ok(new {isAuthenticated = false}) : Ok(new { isAuthenticated = true });
    }

    [HttpGet("current-user")]
    public IActionResult CurrentUser()
    {
        if (!User.Identity.IsAuthenticated)
            return Unauthorized();
        return Ok(new {user = User.Identity.Name});
    }
}