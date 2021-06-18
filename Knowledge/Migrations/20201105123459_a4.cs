using Microsoft.EntityFrameworkCore.Migrations;

namespace Knowledge.Migrations
{
    public partial class a4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Value0",
                table: "KL_Point",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Value1",
                table: "KL_Point",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Value2",
                table: "KL_Point",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Value3",
                table: "KL_Point",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value0",
                table: "KL_Point");

            migrationBuilder.DropColumn(
                name: "Value1",
                table: "KL_Point");

            migrationBuilder.DropColumn(
                name: "Value2",
                table: "KL_Point");

            migrationBuilder.DropColumn(
                name: "Value3",
                table: "KL_Point");
        }
    }
}
