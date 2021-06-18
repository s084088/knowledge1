using Microsoft.EntityFrameworkCore.Migrations;

namespace LiteEntity.Migrations
{
    public partial class a4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Text",
                table: "KPR",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "KPR");
        }
    }
}
