using Microsoft.EntityFrameworkCore.Migrations;

namespace Mobadrat.Migrations
{
    public partial class gehaCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mobadras_Geha_Traget_Geha_TragetID",
                table: "Mobadras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Geha_Traget",
                table: "Geha_Traget");

            migrationBuilder.RenameTable(
                name: "Geha_Traget",
                newName: "geha_Tragets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_geha_Tragets",
                table: "geha_Tragets",
                column: "Geha_TragetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Mobadras_geha_Tragets_Geha_TragetID",
                table: "Mobadras",
                column: "Geha_TragetID",
                principalTable: "geha_Tragets",
                principalColumn: "Geha_TragetID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mobadras_geha_Tragets_Geha_TragetID",
                table: "Mobadras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_geha_Tragets",
                table: "geha_Tragets");

            migrationBuilder.RenameTable(
                name: "geha_Tragets",
                newName: "Geha_Traget");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Geha_Traget",
                table: "Geha_Traget",
                column: "Geha_TragetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Mobadras_Geha_Traget_Geha_TragetID",
                table: "Mobadras",
                column: "Geha_TragetID",
                principalTable: "Geha_Traget",
                principalColumn: "Geha_TragetID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
