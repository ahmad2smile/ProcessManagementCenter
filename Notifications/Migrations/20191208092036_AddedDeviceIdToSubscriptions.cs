using Microsoft.EntityFrameworkCore.Migrations;

namespace Notifications.Migrations
{
    public partial class AddedDeviceIdToSubscriptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceId",
                table: "Subscriptions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Subscriptions");
        }
    }
}
