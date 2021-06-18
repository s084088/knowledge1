using Microsoft.EntityFrameworkCore.Migrations;

namespace Knowledge.Migrations
{
    public partial class a9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Final",
                table: "KL_Point");

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

            migrationBuilder.DropColumn(
                name: "Final",
                table: "KL_Node");

            migrationBuilder.DropColumn(
                name: "Type00",
                table: "KL_Node");

            migrationBuilder.DropColumn(
                name: "Type01",
                table: "KL_Node");

            migrationBuilder.DropColumn(
                name: "Type10",
                table: "KL_Node");

            migrationBuilder.DropColumn(
                name: "Type11",
                table: "KL_Node");

            migrationBuilder.DropColumn(
                name: "Value0",
                table: "KL_Node");

            migrationBuilder.DropColumn(
                name: "Value1",
                table: "KL_Node");

            migrationBuilder.DropColumn(
                name: "Value2",
                table: "KL_Node");

            migrationBuilder.DropColumn(
                name: "Value3",
                table: "KL_Node");

            migrationBuilder.AddColumn<string>(
                name: "LevelID",
                table: "KL_Point",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcessID",
                table: "KL_Point",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeID",
                table: "KL_Point",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LevelID",
                table: "KL_Node",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcessID",
                table: "KL_Node",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeID",
                table: "KL_Node",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KL_Point_LevelID",
                table: "KL_Point",
                column: "LevelID");

            migrationBuilder.CreateIndex(
                name: "IX_KL_Point_ProcessID",
                table: "KL_Point",
                column: "ProcessID");

            migrationBuilder.CreateIndex(
                name: "IX_KL_Point_TypeID",
                table: "KL_Point",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_KL_Node_LevelID",
                table: "KL_Node",
                column: "LevelID");

            migrationBuilder.CreateIndex(
                name: "IX_KL_Node_ProcessID",
                table: "KL_Node",
                column: "ProcessID");

            migrationBuilder.CreateIndex(
                name: "IX_KL_Node_TypeID",
                table: "KL_Node",
                column: "TypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_KL_Node_KL_Level_LevelID",
                table: "KL_Node",
                column: "LevelID",
                principalTable: "KL_Level",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KL_Node_KL_Level_ProcessID",
                table: "KL_Node",
                column: "ProcessID",
                principalTable: "KL_Level",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KL_Node_KL_Level_TypeID",
                table: "KL_Node",
                column: "TypeID",
                principalTable: "KL_Level",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KL_Point_KL_Level_LevelID",
                table: "KL_Point",
                column: "LevelID",
                principalTable: "KL_Level",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KL_Point_KL_Level_ProcessID",
                table: "KL_Point",
                column: "ProcessID",
                principalTable: "KL_Level",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KL_Point_KL_Level_TypeID",
                table: "KL_Point",
                column: "TypeID",
                principalTable: "KL_Level",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KL_Node_KL_Level_LevelID",
                table: "KL_Node");

            migrationBuilder.DropForeignKey(
                name: "FK_KL_Node_KL_Level_ProcessID",
                table: "KL_Node");

            migrationBuilder.DropForeignKey(
                name: "FK_KL_Node_KL_Level_TypeID",
                table: "KL_Node");

            migrationBuilder.DropForeignKey(
                name: "FK_KL_Point_KL_Level_LevelID",
                table: "KL_Point");

            migrationBuilder.DropForeignKey(
                name: "FK_KL_Point_KL_Level_ProcessID",
                table: "KL_Point");

            migrationBuilder.DropForeignKey(
                name: "FK_KL_Point_KL_Level_TypeID",
                table: "KL_Point");

            migrationBuilder.DropIndex(
                name: "IX_KL_Point_LevelID",
                table: "KL_Point");

            migrationBuilder.DropIndex(
                name: "IX_KL_Point_ProcessID",
                table: "KL_Point");

            migrationBuilder.DropIndex(
                name: "IX_KL_Point_TypeID",
                table: "KL_Point");

            migrationBuilder.DropIndex(
                name: "IX_KL_Node_LevelID",
                table: "KL_Node");

            migrationBuilder.DropIndex(
                name: "IX_KL_Node_ProcessID",
                table: "KL_Node");

            migrationBuilder.DropIndex(
                name: "IX_KL_Node_TypeID",
                table: "KL_Node");

            migrationBuilder.DropColumn(
                name: "LevelID",
                table: "KL_Point");

            migrationBuilder.DropColumn(
                name: "ProcessID",
                table: "KL_Point");

            migrationBuilder.DropColumn(
                name: "TypeID",
                table: "KL_Point");

            migrationBuilder.DropColumn(
                name: "LevelID",
                table: "KL_Node");

            migrationBuilder.DropColumn(
                name: "ProcessID",
                table: "KL_Node");

            migrationBuilder.DropColumn(
                name: "TypeID",
                table: "KL_Node");

            migrationBuilder.AddColumn<int>(
                name: "Final",
                table: "KL_Point",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type00",
                table: "KL_Point",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type01",
                table: "KL_Point",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type10",
                table: "KL_Point",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type11",
                table: "KL_Point",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Value0",
                table: "KL_Point",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Value1",
                table: "KL_Point",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Value2",
                table: "KL_Point",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Value3",
                table: "KL_Point",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Final",
                table: "KL_Node",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type00",
                table: "KL_Node",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type01",
                table: "KL_Node",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type10",
                table: "KL_Node",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type11",
                table: "KL_Node",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Value0",
                table: "KL_Node",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Value1",
                table: "KL_Node",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Value2",
                table: "KL_Node",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Value3",
                table: "KL_Node",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
