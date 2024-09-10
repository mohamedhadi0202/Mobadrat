using Microsoft.EntityFrameworkCore.Migrations;

namespace Mobadrat.Migrations
{
    public partial class addCurrentSpot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mobadras_CurrentSpot_CurrentSpotID",
                table: "Mobadras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrentSpot",
                table: "CurrentSpot");

            migrationBuilder.RenameTable(
                name: "CurrentSpot",
                newName: "CurrentSpots");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrentSpots",
                table: "CurrentSpots",
                column: "CurrentSpotID");

            migrationBuilder.AddForeignKey(
                name: "FK_Mobadras_CurrentSpots_CurrentSpotID",
                table: "Mobadras",
                column: "CurrentSpotID",
                principalTable: "CurrentSpots",
                principalColumn: "CurrentSpotID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mobadras_CurrentSpots_CurrentSpotID",
                table: "Mobadras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrentSpots",
                table: "CurrentSpots");

            migrationBuilder.RenameTable(
                name: "CurrentSpots",
                newName: "CurrentSpot");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrentSpot",
                table: "CurrentSpot",
                column: "CurrentSpotID");

            migrationBuilder.AddForeignKey(
                name: "FK_Mobadras_CurrentSpot_CurrentSpotID",
                table: "Mobadras",
                column: "CurrentSpotID",
                principalTable: "CurrentSpot",
                principalColumn: "CurrentSpotID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
