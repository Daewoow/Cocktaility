using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Entities
{
    /// <inheritdoc />
    public partial class PleaseFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BarTag",
                keyColumns: new[] { "BarsBarId", "TagsTagId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "BarTag",
                keyColumns: new[] { "BarsBarId", "TagsTagId" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "BarTag",
                keyColumns: new[] { "BarsBarId", "TagsTagId" },
                keyValues: new object[] { 7, 6 });

            migrationBuilder.DeleteData(
                table: "BarTag",
                keyColumns: new[] { "BarsBarId", "TagsTagId" },
                keyValues: new object[] { 7, 7 });

            migrationBuilder.DeleteData(
                table: "BarTag",
                keyColumns: new[] { "BarsBarId", "TagsTagId" },
                keyValues: new object[] { 8, 8 });

            migrationBuilder.DeleteData(
                table: "BarTag",
                keyColumns: new[] { "BarsBarId", "TagsTagId" },
                keyValues: new object[] { 9, 2 });

            migrationBuilder.DeleteData(
                table: "BarTag",
                keyColumns: new[] { "BarsBarId", "TagsTagId" },
                keyValues: new object[] { 10, 2 });

            migrationBuilder.DeleteData(
                table: "BarTag",
                keyColumns: new[] { "BarsBarId", "TagsTagId" },
                keyValues: new object[] { 11, 2 });

            migrationBuilder.DeleteData(
                table: "Bars",
                keyColumn: "BarId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bars",
                keyColumn: "BarId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Bars",
                keyColumn: "BarId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Bars",
                keyColumn: "BarId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Bars",
                keyColumn: "BarId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Bars",
                keyColumn: "BarId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bars",
                keyColumn: "BarId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Bars",
                keyColumn: "BarId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Bars",
                keyColumn: "BarId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Bars",
                keyColumn: "BarId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Bars",
                keyColumn: "BarId",
                keyValue: 11);

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "BarId");
        }
    }
}
