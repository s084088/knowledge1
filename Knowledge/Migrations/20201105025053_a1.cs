using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Knowledge.Migrations
{
    public partial class a1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Point",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 63, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    DeleteFlag = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 127, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Point", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KL_Node",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 63, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    DeleteFlag = table.Column<int>(nullable: false),
                    PointID = table.Column<string>(nullable: true),
                    Type00 = table.Column<int>(nullable: false),
                    Type01 = table.Column<int>(nullable: false),
                    Type10 = table.Column<int>(nullable: false),
                    Type11 = table.Column<int>(nullable: false),
                    Value0 = table.Column<int>(nullable: false),
                    Value1 = table.Column<int>(nullable: false),
                    Value2 = table.Column<int>(nullable: false),
                    Value3 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KL_Node", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KL_Node_Point_PointID",
                        column: x => x.PointID,
                        principalTable: "Point",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KL_Node_PointID",
                table: "KL_Node",
                column: "PointID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KL_Node");

            migrationBuilder.DropTable(
                name: "Point");
        }
    }
}
