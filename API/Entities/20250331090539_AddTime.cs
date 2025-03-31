using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Entities
{
    /// <inheritdoc />
    public partial class AddTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TimeOfWork",
                table: "Bars",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeOfWork",
                table: "Bars");
        }
    }
}
