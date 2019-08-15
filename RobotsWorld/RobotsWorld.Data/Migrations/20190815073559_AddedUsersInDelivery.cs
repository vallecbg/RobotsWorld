using Microsoft.EntityFrameworkCore.Migrations;

namespace RobotsWorld.Data.Migrations
{
    public partial class AddedUsersInDelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceiverId",
                table: "Deliveries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderId",
                table: "Deliveries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_ReceiverId",
                table: "Deliveries",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_SenderId",
                table: "Deliveries",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_AspNetUsers_ReceiverId",
                table: "Deliveries",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_AspNetUsers_SenderId",
                table: "Deliveries",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_AspNetUsers_ReceiverId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_AspNetUsers_SenderId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_ReceiverId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_SenderId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Deliveries");
        }
    }
}
