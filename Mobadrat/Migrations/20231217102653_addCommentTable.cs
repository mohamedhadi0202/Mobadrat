using Microsoft.EntityFrameworkCore.Migrations;

namespace Mobadrat.Migrations
{
    public partial class addCommentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobadratStatus",
                table: "MobadraComments");

            migrationBuilder.AddColumn<int>(
                name: "MobadraCommentID",
                table: "MobadraComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    MobadraCommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobadraCommentName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.MobadraCommentID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MobadraComments_MobadraCommentID",
                table: "MobadraComments",
                column: "MobadraCommentID");

            migrationBuilder.AddForeignKey(
                name: "FK_MobadraComments_Comments_MobadraCommentID",
                table: "MobadraComments",
                column: "MobadraCommentID",
                principalTable: "Comments",
                principalColumn: "MobadraCommentID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MobadraComments_Comments_MobadraCommentID",
                table: "MobadraComments");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_MobadraComments_MobadraCommentID",
                table: "MobadraComments");

            migrationBuilder.DropColumn(
                name: "MobadraCommentID",
                table: "MobadraComments");

            migrationBuilder.AddColumn<string>(
                name: "MobadratStatus",
                table: "MobadraComments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
