﻿using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Entities
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bars",
                columns: table => new
                {
                    BarId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Photo = table.Column<string>(type: "text", nullable: false),
                    Menu = table.Column<string>(type: "text", nullable: false),
                    Site = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    TimeOfWork = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bars", x => x.BarId);
                });

            migrationBuilder.CreateTable(
                name: "QueryMetrics",
                columns: table => new
                {
                    QueryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Day = table.Column<string>(type: "text", nullable: false),
                    TagsCount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryMetrics", x => x.QueryId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BarTag",
                columns: table => new
                {
                    BarsBarId = table.Column<int>(type: "integer", nullable: false),
                    TagsTagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarTag", x => new { x.BarsBarId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_BarTag_Bars_BarsBarId",
                        column: x => x.BarsBarId,
                        principalTable: "Bars",
                        principalColumn: "BarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarTag_Tags_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    FavId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    BarId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.FavId);
                    table.ForeignKey(
                        name: "FK_Favorites_Bars_BarId",
                        column: x => x.BarId,
                        principalTable: "Bars",
                        principalColumn: "BarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bars",
                columns: new[] { "BarId", "Address", "Menu", "Name", "Photo", "Rating", "Site", "TimeOfWork" },
                values: new object[,]
                {
                    { 1, "просп. Ленина, 20А", "https://vk.com/doc792294115_687636926", "Негодяи", "https://img.restoclub.ru/uploads/place/0/9/8/e/098e561454ac4d89aa8c755e0d181c55_w1230_h820--no-cut.webp?v=3", 0, "https://negodyai.com/", "будни: 12.00 - 03.00; выходные: 12.00 - 06.00" },
                    { 2, "ул. Малышева, 21/4", "https://nelsonsauvin.ru/#menu", "Нельсон Совин", "https://img.restoclub.ru/uploads/place/9/6/4/e/964e95990e6af781ce000062ba85b374_w426_h278.jpg", 0, "https://nelsonsauvin.ru/", "будни: 14.00 - 00.00; выходные: 14.00 - 02.00" },
                    { 3, "ул. Хохрякова, 3а", "https://tomesto.ru/ekaterinburg/places/besy", "Бесы", "https://scdn.tomesto.ru/img/place/000/030/109/gastrobar-besy-na-ulitse-hohryakova_df0cf_full-272101.jpg", 0, "https://tomesto.ru/ekaterinburg/places/besy", "будни: 12.00 - 00.00; выходные: 12.00 - 02.00" },
                    { 4, "ул. Вайнера, 9а", "https://killfish.ru/menu.html", "KILLFISH", "https://img.restoclub.ru/uploads/place/e/b/8/1/eb818ba8605995f2aad10136b0c93eec_w1230_h820--no-cut.webp?v=3", 0, "https://killfish.ru/#", "будни и вс: 14.00 - 02.00; пт-сб: 14.00 - 03.00" },
                    { 5, "ул. Малышева, 29", "https://killfish.ru/menu.html", "Мам я в хлам", "https://img.restoclub.ru/uploads/place/a/3/3/a/a33a7fa1db085b661b4c93fb4732884e_w1230_h820--no-cut.webp?v=3", 0, "https://killfish.ru/#", "будни и вс: 11.00 - 00.00; пт-сб: 11.00 - 02.00" },
                    { 6, "ул. 8 Марта, 8Г", "https://klktv91.ru/menu", "Коллектив", "https://img.restoclub.ru/uploads/place/3/4/f/a/34fa1dbea0f0b5d0bc0bede66b723f26_w1230_h820--no-cut.webp?v=3", 0, "https://klktv91.ru/", "будни и вс: 18.00 - 02.00; пт-сб: 18.00 - 04.00" },
                    { 7, "ул. 8 Марта, 31", "https://polki-centr.ru/price/", "Полки LOUNGE", "https://p0.zoon.ru/b/a/5bb5e429a4b0310a5f52870c_669dfb414c3ee6.11034042.jpg", 0, "https://polki-centr.ru/?utm_source=gmb", "все дни 12.00 - 02.00" },
                    { 8, "ул. 8 Марта, 31в", "https://theoutbar.ru/menu", "Караоке THE OUT BAR", "https://img.restoclub.ru/uploads/place/c/e/5/5/ce5550b6d4a2841d57df1255b3043b2e_w1230_h820--no-cut.webp?v=3", 0, "https://theoutbar.ru/", "ср-чт: 19.00 - 02.00, пт-сб: 19.00 - 05.00" },
                    { 9, "ул. Малышева, 29А", "http://samocvet.ekb.tilda.ws/#menu", "Самоцвет", "https://img.restoclub.ru/uploads/place/b/f/d/c/bfdc1a82fe52ec1c1730983f58ae0d0a_w470.jpg", 0, "http://samocvet.ekb.tilda.ws/", "будни: 17.00 - 02.00; выходные: 15.00 - 06.00" },
                    { 10, "ул. Малышева, 19", "https://vk.com/doc1473743_673529194?hash=TVz29uze1pOgq4GJrqZcNw3tz4qefoQvzW09fXHgER0&dl=lJjDd3sWQviO9DpszKzAGB0GL75kTyFpsks90YThXKT", "Здоровье", "https://img.restoclub.ru/uploads/place/5/5/d/a/55dae2173d7cd2b48378d2377f800edc_w470.jpg", 0, "", "будни и вс: 16.00 - 02.00; пт-сб: 16.00 - 04.00" },
                    { 11, "ул. Розы Люксембург, 14", "https://vk.com/bar.stavnikov?z=album-205091375_303719416", "Ставников", "https://img.restoclub.ru/uploads/place/3/f/3/b/3f3ba52891c1577ad7f3f3c06e9d2105_w1230_h820--no-cut.webp?v=3", 0, "https://vk.com/bar.stavnikov", "пн-чт: 12.00 - 02.00; пт: 12.00 - 04.00; сб: 16.00 - 04.00, вс: 16.00 - 02.00" },
                    { 12, "просп. Ленина, 49", "https://grottbar.ru/menu", "Гротт Бар", "https://img.restoclub.ru/uploads/place/d/d/8/c/dd8c1dddd95de76e486d6e19a0f15515_w1230_h820--no-cut.webp?v=3", 0, "https://grottbar.ru/", "будни и вс: 12.00 - 00.00; пт-сб: 12.00 - 03.00" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "TagId", "Name" },
                values: new object[,]
                {
                    { 1, "#красивый_туалет" },
                    { 2, "#настойки" },
                    { 3, "#коктейли" },
                    { 4, "#необычные_названия" },
                    { 5, "#пиво" },
                    { 6, "#кальян" },
                    { 7, "#книги" },
                    { 8, "#караоке" },
                    { 9, "#настолки" }
                });

            migrationBuilder.InsertData(
                table: "BarTag",
                columns: new[] { "BarsBarId", "TagsTagId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 5 },
                    { 2, 9 },
                    { 7, 6 },
                    { 7, 7 },
                    { 8, 8 },
                    { 9, 2 },
                    { 10, 2 },
                    { 11, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BarTag_TagsTagId",
                table: "BarTag",
                column: "TagsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_BarId",
                table: "Favorites",
                column: "BarId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarTag");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "QueryMetrics");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Bars");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
