using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Entities
{
    /// <inheritdoc />
    public partial class AddFirstBar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarTags_Bars_BarsBarId",
                table: "BarTags");

            migrationBuilder.DropForeignKey(
                name: "FK_BarTags_Tags_TagsTagId",
                table: "BarTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarTags",
                table: "BarTags");

            migrationBuilder.RenameTable(
                name: "BarTags",
                newName: "BarTag");

            migrationBuilder.RenameIndex(
                name: "IX_BarTags_TagsTagId",
                table: "BarTag",
                newName: "IX_BarTag_TagsTagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarTag",
                table: "BarTag",
                columns: new[] { "BarsBarId", "TagsTagId" });

            migrationBuilder.InsertData(
                table: "Bars",
                columns: new[] { "BarId", "Address", "Menu", "Name", "Photo", "Rating", "Site", "TimeOfWork" },
                values: new object[] { 1, "просп. Ленина, 20А", "https://vk.com/doc792294115_687636926", "Негодяи", "https://img.restoclub.ru/uploads/place/0/9/8/e/098e561454ac4d89aa8c755e0d181c55_w1230_h820--no-cut.webp?v=3", 0, "https://negodyai.com/", "будни: 12.00 - 03.00; выходные: 12.00 - 06.00" });

            migrationBuilder.InsertData(
                table: "BarTag",
                columns: new[] { "BarsBarId", "TagsTagId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BarTag_Bars_BarsBarId",
                table: "BarTag",
                column: "BarsBarId",
                principalTable: "Bars",
                principalColumn: "BarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarTag_Tags_TagsTagId",
                table: "BarTag",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarTag_Bars_BarsBarId",
                table: "BarTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BarTag_Tags_TagsTagId",
                table: "BarTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarTag",
                table: "BarTag");

            migrationBuilder.DeleteData(
                table: "BarTag",
                keyColumns: new[] { "BarsBarId", "TagsTagId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "BarTag",
                keyColumns: new[] { "BarsBarId", "TagsTagId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "BarTag",
                keyColumns: new[] { "BarsBarId", "TagsTagId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "Bars",
                keyColumn: "BarId",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "BarTag",
                newName: "BarTags");

            migrationBuilder.RenameIndex(
                name: "IX_BarTag_TagsTagId",
                table: "BarTags",
                newName: "IX_BarTags_TagsTagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarTags",
                table: "BarTags",
                columns: new[] { "BarsBarId", "TagsTagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BarTags_Bars_BarsBarId",
                table: "BarTags",
                column: "BarsBarId",
                principalTable: "Bars",
                principalColumn: "BarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarTags_Tags_TagsTagId",
                table: "BarTags",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
