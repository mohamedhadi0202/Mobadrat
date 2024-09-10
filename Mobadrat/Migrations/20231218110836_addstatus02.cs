using Microsoft.EntityFrameworkCore.Migrations;

namespace Mobadrat.Migrations
{
    public partial class addstatus02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MobadraComments_Comments_MobadraCommentID",
                table: "MobadraComments");

            migrationBuilder.RenameColumn(
                name: "MobadraCommentID",
                table: "MobadraComments",
                newName: "StatusID");

            migrationBuilder.RenameIndex(
                name: "IX_MobadraComments_MobadraCommentID",
                table: "MobadraComments",
                newName: "IX_MobadraComments_StatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_MobadraComments_Statuses_StatusID",
                table: "MobadraComments",
                column: "StatusID",
                principalTable: "Statuses",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MobadraComments_Statuses_StatusID",
                table: "MobadraComments");

            migrationBuilder.RenameColumn(
                name: "StatusID",
                table: "MobadraComments",
                newName: "MobadraCommentID");

            migrationBuilder.RenameIndex(
                name: "IX_MobadraComments_StatusID",
                table: "MobadraComments",
                newName: "IX_MobadraComments_MobadraCommentID");

            migrationBuilder.AddForeignKey(
                name: "FK_MobadraComments_Comments_MobadraCommentID",
                table: "MobadraComments",
                column: "MobadraCommentID",
                principalTable: "Comments",
                principalColumn: "MobadraCommentID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
