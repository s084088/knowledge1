using Microsoft.EntityFrameworkCore.Migrations;

namespace Knowledge.Migrations
{
    public partial class a2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KL_Node_Point_PointID",
                table: "KL_Node");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Point",
                table: "Point");

            migrationBuilder.RenameTable(
                name: "Point",
                newName: "KL_Point");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KL_Point",
                table: "KL_Point",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_KL_Node_KL_Point_PointID",
                table: "KL_Node",
                column: "PointID",
                principalTable: "KL_Point",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KL_Node_KL_Point_PointID",
                table: "KL_Node");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KL_Point",
                table: "KL_Point");

            migrationBuilder.RenameTable(
                name: "KL_Point",
                newName: "Point");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Point",
                table: "Point",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_KL_Node_Point_PointID",
                table: "KL_Node",
                column: "PointID",
                principalTable: "Point",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
