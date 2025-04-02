using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API.Controllers;
using API.Models;
using API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[action]")]
public class AccountController(ApplicationContext context, PageBuilder builder) : ControllerBase
{
    [HttpGet]
    public IActionResult Login()
    {
        var page = new PageBuilder
        {
            Title = "Login",
        }
        .AddLayout("src/components/mainLayout.html")
        .AddBody("src/components/login.html")
        .AddScripts("/src/scripts/login.js")
        .Build();
        return Content(page, "text/html");
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await CheckCredentials(request.Email, request.Password);
        if (user is null)
            return Unauthorized(new { success = false, message = "Неверный email или пароль." });
        
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, request.Email),
            new (ClaimTypes.Role, user.IsAdmin ? "Admin" : "User" ) // тут равно =
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
        };
        
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity), authProperties);

        return Ok(new { success = true });
    }

    [HttpGet]
    public IActionResult Register()
    {
        var page = new PageBuilder
            {
                Title = "Register",
            }
            .AddLayout("src/components/mainLayout.html")
            .AddBody("src/components/register.html")
            .AddScripts("/src/scripts/register.js")
            .Build();
        return Content(page, "text/html");
    }
    
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
    
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var success = await TryRegister(request);
        return Ok(new {success = success});
    }

    private async Task<AppUser?> CheckCredentials(string email, string password) 
        => await context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);

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