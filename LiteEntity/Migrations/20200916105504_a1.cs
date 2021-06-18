using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LiteEntity.Migrations
{
    public partial class a1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entity",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 63, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    DeleteFlag = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 63, nullable: true),
                    Text = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KP",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 63, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    DeleteFlag = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 63, nullable: true),
                    Text = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KP", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EKP",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 63, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    DeleteFlag = table.Column<int>(nullable: false),
                    EntityID = table.Column<string>(nullable: true),
                    KPID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EKP", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EKP_Entity_EntityID",
                        column: x => x.EntityID,
                        principalTable: "Entity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EKP_KP_KPID",
                        column: x => x.KPID,
                        principalTable: "KP",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KPR",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 63, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    DeleteFlag = table.Column<int>(nullable: false),
                    OriginID = table.Column<string>(nullable: true),
                    TargetID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPR", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KPR_KP_OriginID",
                        column: x => x.OriginID,
                        principalTable: "KP",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KPR_KP_TargetID",
                        column: x => x.TargetID,
                        principalTable: "KP",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EKP_EntityID",
                table: "EKP",
                column: "EntityID");

            migrationBuilder.CreateIndex(
                name: "IX_EKP_KPID",
                table: "EKP",
                column: "KPID");

            migrationBuilder.CreateIndex(
                name: "IX_KPR_OriginID",
                table: "KPR",
                column: "OriginID");

            migrationBuilder.CreateIndex(
                name: "IX_KPR_TargetID",
                table: "KPR",
                column: "TargetID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EKP");

            migrationBuilder.DropTable(
                name: "KPR");

            migrationBuilder.DropTable(
                name: "Entity");

            migrationBuilder.DropTable(
                name: "KP");
        }
    }
}
