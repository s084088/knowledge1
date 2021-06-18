using Microsoft.EntityFrameworkCore.Migrations;

namespace LiteEntity.Migrations
{
    public partial class a3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "KP",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Importance",
                table: "KP",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "KP",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "KP");

            migrationBuilder.DropColumn(
                name: "Importance",
                table: "KP");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "KP");
        }
    }
}
