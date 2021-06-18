using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Knowledge.Migrations
{
    public partial class a7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KL_Value");

            migrationBuilder.CreateTable(
                name: "KL_Level",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 63, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    DeleteFlag = table.Column<int>(nullable: false),
                    Key = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 2047, nullable: true),
                    Value = table.Column<int>(nullable: false),
                    ParentID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KL_Level", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KL_Level_KL_Level_ParentID",
                        column: x => x.ParentID,
                        principalTable: "KL_Level",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KL_Level_ParentID",
                table: "KL_Level",
                column: "ParentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KL_Level");

            migrationBuilder.CreateTable(
                name: "KL_Value",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(63) CHARACTER SET utf8mb4", maxLength: 63, nullable: false),
                    Album = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeleteFlag = table.Column<int>(type: "int", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Final = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    Lesson = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KL_Value", x => x.ID);
                });
        }
    }
}
