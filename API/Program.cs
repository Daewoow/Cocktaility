using System.Net.Quic;
using API.Database;
using API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.WebHost.UseUrls("http://+:5184");
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.AccessDeniedPath = "/mainpage";
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => 
        policy.RequireRole("Admin"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/users", [Authorize(Roles = "Admin")]async (ApplicationContext db) => await db.Users.ToListAsync());
app.MapGet("/user/{id:int}", [Authorize]async (int id, ApplicationContext db) =>
    await db.Users.FirstOrDefaultAsync(x => x.Id == id.ToString())); // TODO: id теперь Guid
app.MapGet("/negr", context =>
{
    context.Response.Redirect("/src/pages/index.html");
    return Task.CompletedTask;
});
app.MapGet("/mainpage", context =>
{
    context.Response.Redirect("/src/pages/index.html");
    return Task.CompletedTask;
});

app.Run();