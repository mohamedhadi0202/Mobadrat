using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mobadrat.Migrations
{
    public partial class adduserupload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateUser",
                table: "MobadraUploadfiles",
                newName: "UserId");

            migrationBuilder.CreateTable(
                name: "MobadraComments",
                columns: table => new
                {
                    RecordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobadraCommet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobadraID = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MobadratStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobadraComments", x => x.RecordID);
                    table.ForeignKey(
                        name: "FK_MobadraComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MobadraUploadfiles_UserId",
                table: "MobadraUploadfiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MobadraComments_UserId",
                table: "MobadraComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MobadraUploadfiles_Users_UserId",
                table: "MobadraUploadfiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MobadraUploadfiles_Users_UserId",
                table: "MobadraUploadfiles");

            migrationBuilder.DropTable(
                name: "MobadraComments");

            migrationBuilder.DropIndex(
                name: "IX_MobadraUploadfiles_UserId",
                table: "MobadraUploadfiles");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "MobadraUploadfiles",
                newName: "CreateUser");
        }
    }
}
