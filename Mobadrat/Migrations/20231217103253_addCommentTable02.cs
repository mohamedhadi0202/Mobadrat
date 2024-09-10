using Microsoft.EntityFrameworkCore.Migrations;

namespace Mobadrat.Migrations
{
    public partial class addCommentTable02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MobadraComments_MobadraID",
                table: "MobadraComments",
                column: "MobadraID");

            migrationBuilder.AddForeignKey(
                name: "FK_MobadraComments_Mobadras_MobadraID",
                table: "MobadraComments",
                column: "MobadraID",
                principalTable: "Mobadras",
                principalColumn: "MobadraID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MobadraComments_Mobadras_MobadraID",
                table: "MobadraComments");

            migrationBuilder.DropIndex(
                name: "IX_MobadraComments_MobadraID",
                table: "MobadraComments");
        }
    }
}
