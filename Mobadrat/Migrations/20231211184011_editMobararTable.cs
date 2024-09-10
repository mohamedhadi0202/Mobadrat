using Microsoft.EntityFrameworkCore.Migrations;

namespace Mobadrat.Migrations
{
    public partial class editMobararTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mobadras_Users_UserId",
                table: "Mobadras");

            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "Mobadras");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Mobadras",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mobadras_DurationTimeID",
                table: "Mobadras",
                column: "DurationTimeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Mobadras_DurationTimes_DurationTimeID",
                table: "Mobadras",
                column: "DurationTimeID",
                principalTable: "DurationTimes",
                principalColumn: "DurationTimeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mobadras_Users_UserId",
                table: "Mobadras",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mobadras_DurationTimes_DurationTimeID",
                table: "Mobadras");

            migrationBuilder.DropForeignKey(
                name: "FK_Mobadras_Users_UserId",
                table: "Mobadras");

            migrationBuilder.DropIndex(
                name: "IX_Mobadras_DurationTimeID",
                table: "Mobadras");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Mobadras",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CreateUser",
                table: "Mobadras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Mobadras_Users_UserId",
                table: "Mobadras",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
