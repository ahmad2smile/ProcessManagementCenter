using Microsoft.EntityFrameworkCore.Migrations;

namespace Notifications.Migrations
{
    public partial class AddedMineSiteIdToNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Subscriptions");

            migrationBuilder.AddColumn<int>(
                name: "MineSiteId",
                table: "Subscriptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MineSiteId",
                table: "Notifications",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MineSiteId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "MineSiteId",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Subscriptions",
                type: "text",
                nullable: true);
        }
    }
}
