using Microsoft.EntityFrameworkCore.Migrations;

namespace LiteEntity.Migrations
{
    public partial class a2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tightness",
                table: "KPR",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "KPR",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAbstract",
                table: "KP",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tightness",
                table: "KPR");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "KPR");

            migrationBuilder.DropColumn(
                name: "IsAbstract",
                table: "KP");
        }
    }
}
