using System.Net.Quic;
using API.Database;
using API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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
var pagesFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "CockUI", "src", "pages");
var imagesFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "CockUI", "public", "images");
var stylesFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "CockUI", "src", "styles");
var scriptsFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "CockUI", "src", "scripts");

app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(pagesFilesPath),
    RequestPath = ""
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesFilesPath),
    RequestPath = "/public/images"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(stylesFilesPath),
    RequestPath = "/styles"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(scriptsFilesPath),
    RequestPath = "/scripts"
});

app.UseRouting();
app.MapControllers();

app.MapGet("/users", async (ApplicationContext db) => await db.Users.ToListAsync());
app.MapGet("/user/{id:int}", async (int id, ApplicationContext db) =>
    await db.Users.FirstOrDefaultAsync(x => x.Id == id));
app.MapGet("/mainpage", context =>
{
    context.Response.Redirect("/index.html");
    return Task.CompletedTask;
});

app.Run();