using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API.Controllers;
using API.Models;
using API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
        .LoadEntirePage("src/pages/sign.html");
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
            new (ClaimTypes.NameIdentifier, user.Id),
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
            .LoadEntirePage("src/pages/register.html");
        return Content(page, "text/html");
    }
    
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("search");
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

    [HttpGet]
    public async Task<IActionResult> Favorite()
    {
        var page = new PageBuilder
            {
                Title = "Поиск",
                IsAuthenticated = User.Identity is { IsAuthenticated: true },
            }
            .AddLayout("src/components/mainLayout.html")
            .AddBody("src/components/favorite.html")
            .AddStyles("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css",
                "src/styles/search.css", 
                "src/styles/card.css",
                "src/styles/detailsPanel.css"
            )
            .AddScripts(false,
                "https://api-maps.yandex.ru/2.1/?apikey=c34675db-5522-4f61-b4ee-9eda5adca08e&lang=ru_RU",
                "src/scripts/utils.js",
                "src/scripts/search.js",
                "src/scripts/search_ymaps.js",
                "src/scripts/favorite.js"
            )
            .Build();
        page = page.Replace("<button class=\"btn\" onclick=\"window.location.href='/Favorite'\">Избранное</button>",
            "<button class=\"btn\" onclick=\"window.location.href='/Search'\">Поиск</button>");
        return Content(page, "text/html");
    }
}