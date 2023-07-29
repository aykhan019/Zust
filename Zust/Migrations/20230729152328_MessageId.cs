using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zust.Web.Migrations
{
    public partial class MessageId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceiverUserId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderUserId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverUserId",
                table: "Messages",
                column: "ReceiverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderUserId",
                table: "Messages",
                column: "SenderUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ReceiverUserId",
                table: "Messages",
                column: "ReceiverUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SenderUserId",
                table: "Messages",
                column: "SenderUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ReceiverUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SenderUserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ReceiverUserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SenderUserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ReceiverUserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderUserId",
                table: "Messages");
        }
    }
}
