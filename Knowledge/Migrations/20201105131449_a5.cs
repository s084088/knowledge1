using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Knowledge.Migrations
{
    public partial class a5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Final",
                table: "KL_Point",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Final",
                table: "KL_Node",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "KL_Value",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 63, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    DeleteFlag = table.Column<int>(nullable: false),
                    Final = table.Column<int>(nullable: false),
                    Grade = table.Column<int>(nullable: false),
                    Album = table.Column<int>(nullable: false),
                    Unit = table.Column<int>(nullable: false),
                    Lesson = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KL_Value", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KL_Value");

            migrationBuilder.DropColumn(
                name: "Final",
                table: "KL_Point");

            migrationBuilder.DropColumn(
                name: "Final",
                table: "KL_Node");
        }
    }
}
