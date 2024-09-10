using Microsoft.EntityFrameworkCore.Migrations;

namespace Mobadrat.Migrations
{
    public partial class addVlonteer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mobadras_Volunteer_VolunteerID",
                table: "Mobadras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Volunteer",
                table: "Volunteer");

            migrationBuilder.RenameTable(
                name: "Volunteer",
                newName: "volunteers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_volunteers",
                table: "volunteers",
                column: "VolunteerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Mobadras_volunteers_VolunteerID",
                table: "Mobadras",
                column: "VolunteerID",
                principalTable: "volunteers",
                principalColumn: "VolunteerID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mobadras_volunteers_VolunteerID",
                table: "Mobadras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_volunteers",
                table: "volunteers");

            migrationBuilder.RenameTable(
                name: "volunteers",
                newName: "Volunteer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Volunteer",
                table: "Volunteer",
                column: "VolunteerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Mobadras_Volunteer_VolunteerID",
                table: "Mobadras",
                column: "VolunteerID",
                principalTable: "Volunteer",
                principalColumn: "VolunteerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
