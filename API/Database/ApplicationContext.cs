using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

public class ApplicationContext : DbContext
{
    public new DbSet<AppUser> Users { get; set; } = null!;
    public DbSet<Bar> Bars { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<Favorite> Favorites { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<AppUser>().ToTable("Users");

        modelBuilder.Entity<Bar>(entity =>
        {
            entity.HasKey(x => x.BarId);
            entity.Property(x => x.BarId).ValueGeneratedOnAdd();
            entity.Property(x => x.Name).IsRequired();
            entity.Property(x => x.Address).IsRequired();
            entity.Property(x => x.Photo).IsRequired();
            entity.Property(x => x.Menu).IsRequired();
            entity.Property(x => x.Site).IsRequired();
            entity.Property(x => x.Rating);
            entity.Property(x => x.TimeOfWork);
        });
        modelBuilder.Entity<Bar>().HasData(
            new Bar
            {
                BarId = 1, Name = "Негодяи", Address = "просп. Ленина, 20А",
                Menu = "https://vk.com/doc792294115_687636926",
                TimeOfWork = "будни: 12.00 - 03.00; выходные: 12.00 - 06.00", Site = "https://negodyai.com/",
                Photo =
                    "https://img.restoclub.ru/uploads/place/0/9/8/e/098e561454ac4d89aa8c755e0d181c55_w1230_h820--no-cut.webp?v=3"
            },
            new Bar
            {
                BarId = 2, Name = "Нельсон Совин", Address = "ул. Малышева, 21/4",
                Menu = "https://nelsonsauvin.ru/#menu",
                TimeOfWork = "будни: 14.00 - 00.00; выходные: 14.00 - 02.00", Site = "https://nelsonsauvin.ru/",
                Photo = "https://img.restoclub.ru/uploads/place/9/6/4/e/964e95990e6af781ce000062ba85b374_w426_h278.jpg"
            },
            new Bar
            {
                BarId = 3, Name = "Бесы", Address = "ул. Хохрякова, 3а",
                Menu = "https://tomesto.ru/ekaterinburg/places/besy",
                TimeOfWork = "будни: 12.00 - 00.00; выходные: 12.00 - 02.00", Site = "https://tomesto.ru/ekaterinburg/places/besy",
                Photo = "https://scdn.tomesto.ru/img/place/000/030/109/gastrobar-besy-na-ulitse-hohryakova_df0cf_full-272101.jpg"
            },
            new Bar
            {
                BarId = 4, Name = "KILLFISH", Address = "ул. Вайнера, 9а",
                Menu = "https://killfish.ru/menu.html",
                TimeOfWork = "будни и вс: 14.00 - 02.00; пт-сб: 14.00 - 03.00", Site = "https://killfish.ru/#",
                Photo = "https://img.restoclub.ru/uploads/place/e/b/8/1/eb818ba8605995f2aad10136b0c93eec_w1230_h820--no-cut.webp?v=3"
            },
            new Bar
            {
                BarId = 5, Name = "Мам я в хлам", Address = "ул. Малышева, 29",
                Menu = "https://killfish.ru/menu.html",
                TimeOfWork = "будни и вс: 11.00 - 00.00; пт-сб: 11.00 - 02.00", Site = "https://killfish.ru/#",
                Photo = "https://img.restoclub.ru/uploads/place/a/3/3/a/a33a7fa1db085b661b4c93fb4732884e_w1230_h820--no-cut.webp?v=3"
            },
            new Bar
            {
                BarId = 6, Name = "Коллектив", Address = "ул. 8 Марта, 8Г",
                Menu = "https://klktv91.ru/menu",
                TimeOfWork = "будни и вс: 18.00 - 02.00; пт-сб: 18.00 - 04.00", Site = "https://klktv91.ru/",
                Photo = "https://img.restoclub.ru/uploads/place/3/4/f/a/34fa1dbea0f0b5d0bc0bede66b723f26_w1230_h820--no-cut.webp?v=3"
            },
            new Bar
            {
                BarId = 7, Name = "Полки LOUNGE", Address = "ул. 8 Марта, 31",
                Menu = "https://polki-centr.ru/price/",
                TimeOfWork = "все дни 12.00 - 02.00", Site = "https://polki-centr.ru/?utm_source=gmb",
                Photo = "https://p0.zoon.ru/b/a/5bb5e429a4b0310a5f52870c_669dfb414c3ee6.11034042.jpg"
            },
            new Bar
            {
                BarId = 8, Name = "Караоке THE OUT BAR", Address = "ул. 8 Марта, 31в",
                Menu = "https://theoutbar.ru/menu",
                TimeOfWork = "ср-чт: 19.00 - 02.00, пт-сб: 19.00 - 05.00", Site = "https://theoutbar.ru/",
                Photo = "https://img.restoclub.ru/uploads/place/c/e/5/5/ce5550b6d4a2841d57df1255b3043b2e_w1230_h820--no-cut.webp?v=3"
            },
            new Bar
            {
                BarId = 9, Name = "Самоцвет", Address = "ул. Малышева, 29А",
                Menu = "http://samocvet.ekb.tilda.ws/#menu",
                TimeOfWork = "будни: 17.00 - 02.00; выходные: 15.00 - 06.00", Site = "http://samocvet.ekb.tilda.ws/",
                Photo = "https://img.restoclub.ru/uploads/place/b/f/d/c/bfdc1a82fe52ec1c1730983f58ae0d0a_w470.jpg"
            },
            new Bar
            {
                BarId = 10, Name = "Здоровье", Address = "ул. Малышева, 19",
                Menu = "https://vk.com/doc1473743_673529194?hash=TVz29uze1pOgq4GJrqZcNw3tz4qefoQvzW09fXHgER0&dl=lJjDd3sWQviO9DpszKzAGB0GL75kTyFpsks90YThXKT",
                TimeOfWork = "будни и вс: 16.00 - 02.00; пт-сб: 16.00 - 04.00", Site = "",
                Photo = "https://img.restoclub.ru/uploads/place/5/5/d/a/55dae2173d7cd2b48378d2377f800edc_w470.jpg"
            },
            new Bar
            {
                BarId = 11, Name = "Ставников", Address = "ул. Розы Люксембург, 14",
                Menu = "https://vk.com/bar.stavnikov?z=album-205091375_303719416",
                TimeOfWork = "пн-чт: 12.00 - 02.00; пт: 12.00 - 04.00; сб: 16.00 - 04.00, вс: 16.00 - 02.00", Site = "https://vk.com/bar.stavnikov",
                Photo = "https://img.restoclub.ru/uploads/place/3/f/3/b/3f3ba52891c1577ad7f3f3c06e9d2105_w1230_h820--no-cut.webp?v=3"
            },
            new Bar
            {
                BarId = 12, Name = "Гротт Бар", Address = "просп. Ленина, 49",
                Menu = "https://grottbar.ru/menu",
                TimeOfWork = "будни и вс: 12.00 - 00.00; пт-сб: 12.00 - 03.00", Site = "https://grottbar.ru/",
                Photo = "https://img.restoclub.ru/uploads/place/d/d/8/c/dd8c1dddd95de76e486d6e19a0f15515_w1230_h820--no-cut.webp?v=3"
            }
        );
        modelBuilder.Entity<Bar>().ToTable("Bars");

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(x => x.TagId);
            entity.Property(x => x.TagId).ValueGeneratedOnAdd();
            entity.Property(x => x.Name).IsRequired();
        });
        modelBuilder.Entity<Tag>().ToTable("Tags");
        
        modelBuilder.Entity<Bar>()
            .HasMany(b => b.Tags)
            .WithMany(t => t.Bars)
            .UsingEntity(j => j.HasData(
                new {BarsBarId = 1, TagsTagId = 2},
                new {BarsBarId = 1, TagsTagId = 3},
                new {BarsBarId = 1, TagsTagId = 4},
                new {BarsBarId = 2, TagsTagId = 5},
                new {BarsBarId = 2, TagsTagId = 9},
                new {BarsBarId = 7, TagsTagId = 6},
                new {BarsBarId = 7, TagsTagId = 7},
                new {BarsBarId = 8, TagsTagId = 8},
                new {BarsBarId = 9, TagsTagId = 2},
                new {BarsBarId = 10, TagsTagId = 2},
                new {BarsBarId = 11, TagsTagId = 2})
            );
        
        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(x => x.FavId);

            entity.HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(f => f.Bar)
                .WithMany(b => b.FavoritedByUsers)
                .HasForeignKey(f => f.BarId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Favorite>().ToTable("Favorites");
    }
}