using API.Database;
using Microsoft.EntityFrameworkCore;
public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();  
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(x => x.Id);
        modelBuilder.Entity<User>().HasData(
            new User { Id=256, Email="test1@jmail.com", Password = "1234", IsAdmin = false }, 
            new User { Id=257, Email="admin@jmail.com", Password = "QwErTy", IsAdmin = true },
            new User { Id=258, Email="test2@gandex.ru", Password = "15", IsAdmin = false }
        );
    }
}