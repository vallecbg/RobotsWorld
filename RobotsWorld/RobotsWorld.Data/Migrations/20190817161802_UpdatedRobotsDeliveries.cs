using Microsoft.EntityFrameworkCore.Migrations;

namespace RobotsWorld.Data.Migrations
{
    public partial class UpdatedRobotsDeliveries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Deliveries_RobotId",
                table: "Deliveries");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_RobotId",
                table: "Deliveries",
                column: "RobotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Deliveries_RobotId",
                table: "Deliveries");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_RobotId",
                table: "Deliveries",
                column: "RobotId",
                unique: true,
                filter: "[RobotId] IS NOT NULL");
        }
    }
}
