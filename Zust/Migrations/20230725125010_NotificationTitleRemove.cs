using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zust.Web.Migrations
{
    public partial class NotificationTitleRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Notifications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
