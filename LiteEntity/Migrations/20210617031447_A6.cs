using Microsoft.EntityFrameworkCore.Migrations;

namespace LiteEntity.Migrations
{
    public partial class A6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Subject",
                table: "KPR",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Subject",
                table: "KP",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "KPR");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "KP");
        }
    }
}
