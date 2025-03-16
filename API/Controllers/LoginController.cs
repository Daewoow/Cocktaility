using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class LoginController(ApplicationContext context) : ControllerBase
{
    [HttpGet]
    public IActionResult Login()
    {
        return Redirect("/sign.html");
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var isValid = await CheckCredentials(request.Email, request.Password);

        return isValid
            ? Ok(new { success = true })
            : Unauthorized(new { success = false, message = "Неверный email или пароль." });
    }

    private async Task<bool> CheckCredentials(string email, string password)
    {
        var neededUser = await context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        return neededUser is not null;
    }
}