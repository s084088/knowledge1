using Microsoft.EntityFrameworkCore.Migrations;

namespace Knowledge.Migrations
{
    public partial class a6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type00",
                table: "KL_Point",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type01",
                table: "KL_Point",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type10",
                table: "KL_Point",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type11",
                table: "KL_Point",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type00",
                table: "KL_Point");

            migrationBuilder.DropColumn(
                name: "Type01",
                table: "KL_Point");

            migrationBuilder.DropColumn(
                name: "Type10",
                table: "KL_Point");

            migrationBuilder.DropColumn(
                name: "Type11",
                table: "KL_Point");
        }
    }
}
