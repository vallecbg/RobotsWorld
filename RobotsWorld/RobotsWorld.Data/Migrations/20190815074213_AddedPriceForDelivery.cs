using Microsoft.EntityFrameworkCore.Migrations;

namespace RobotsWorld.Data.Migrations
{
    public partial class AddedPriceForDelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Deliveries",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Deliveries");
        }
    }
}
