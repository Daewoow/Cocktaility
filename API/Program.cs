using API.Controllers;
using API.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.WebHost.UseUrls("http://+:5184");
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.AccessDeniedPath = "/search";
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => 
        policy.RequireRole("Admin"));
});


builder.Services.AddScoped<TagService>();
builder.Services.AddScoped<BarService>();
builder.Services.AddScoped<PageBuilder>();
builder.Services.AddScoped<PageBuilder>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMetricServer(url: "/metrics");
app.UseHttpMetrics();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// троляля
PageBuilder.SetRoot(app.Environment.WebRootPath);

app.MapGet("/users", [Authorize(Roles = "Admin")]async (ApplicationContext db) => await db.Users.ToListAsync());
//app.MapGet("/Favorite", [Authorize]async (ApplicationContext db) => await db.Favorites.ToListAsync());
app.MapGet("/user/{id:int}", [Authorize]async (int id, ApplicationContext db) =>
    await db.Users.FirstOrDefaultAsync(x => x.Id == id.ToString())); // TODO: id теперь Guid
app.MapGet("/mainpage", context =>
{
    context.Response.Redirect("/src/pages/index.html");
    return Task.CompletedTask;
});
app.MapGet("/", () => Results.Redirect("/search"));

app.Run();