using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Knowledge.Migrations
{
    public partial class A10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "N_K_Node",
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
                    table.PrimaryKey("PK_N_K_Node", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "N_K_Line",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 63, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    DeleteFlag = table.Column<int>(nullable: false),
                    SourceID = table.Column<string>(nullable: true),
                    TargetID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_N_K_Line", x => x.ID);
                    table.ForeignKey(
                        name: "FK_N_K_Line_N_K_Node_SourceID",
                        column: x => x.SourceID,
                        principalTable: "N_K_Node",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_N_K_Line_N_K_Node_TargetID",
                        column: x => x.TargetID,
                        principalTable: "N_K_Node",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_N_K_Line_SourceID",
                table: "N_K_Line",
                column: "SourceID");

            migrationBuilder.CreateIndex(
                name: "IX_N_K_Line_TargetID",
                table: "N_K_Line",
                column: "TargetID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "N_K_Line");

            migrationBuilder.DropTable(
                name: "N_K_Node");
        }
    }
}
