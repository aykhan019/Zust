using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zust.Web.Migrations
{
    public partial class NotificationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Notifications",
                newName: "ToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                newName: "IX_Notifications_ToUserId");

            migrationBuilder.AddColumn<string>(
                name: "FromUserId",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_FromUserId",
                table: "Notifications",
                column: "FromUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_FromUserId",
                table: "Notifications",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_ToUserId",
                table: "Notifications",
                column: "ToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_FromUserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_ToUserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_FromUserId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "FromUserId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "ToUserId",
                table: "Notifications",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_ToUserId",
                table: "Notifications",
                newName: "IX_Notifications_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
