using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Entities
{
    /// <inheritdoc />
    public partial class Beauty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BarTag",
                columns: new[] { "BarsBarId", "TagsTagId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 12, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BarTag",
                keyColumns: new[] { "BarsBarId", "TagsTagId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BarTag",
                keyColumns: new[] { "BarsBarId", "TagsTagId" },
                keyValues: new object[] { 12, 1 });
        }
    }
}
