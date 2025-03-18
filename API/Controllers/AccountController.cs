using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API.Controllers;
using API.Database;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]/[action]")]
public class AccountController(ApplicationContext context) : ControllerBase
{
    [HttpGet]
    public IActionResult Login()
    {
        var page = new PageBuilder
        {
            Title = "Login",
        }
        .AddLayout("src/test/mainLayout.html")
        .AddBody("src/test/login.html")
        .AddScripts("/scripts/login.js")
        .Build();
        return Content(page, "text/html");
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var isValid = await CheckCredentials(request.Email, request.Password);

        return isValid
            ? Ok(new { success = true })
            : Unauthorized(new { success = false, message = "Неверный email или пароль." });
    }

    [HttpGet]
    public IActionResult Register()
    {
        var page = new PageBuilder
            {
                Title = "Register",
            }
            .AddLayout("src/test/mainLayout.html")
            .AddBody("src/test/register.html")
            .AddScripts("/scripts/register.js")
            .Build();
        return Content(page, "text/html");
    }
    
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var success = await TryRegister(request);
        return Ok(new {success = success});
    }

    private async Task<bool> CheckCredentials(string email, string password)
    {
        var neededUser = await context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        return neededUser is not null;
    }

    private async Task<bool> TryRegister(RegisterRequest request)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (user != null)
        {
            return false;
        }

        var newUser = new AppUser
        {
            Email = request.Email,
            Password = request.Password,
            IsAdmin = false
        };
        context.Users.Add(newUser);
        return await context.SaveChangesAsync() == 1;
    }
}