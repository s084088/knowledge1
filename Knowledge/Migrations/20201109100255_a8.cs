using Microsoft.EntityFrameworkCore.Migrations;

namespace Knowledge.Migrations
{
    public partial class a8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Final",
                table: "KL_Level",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Final",
                table: "KL_Level");
        }
    }
}
