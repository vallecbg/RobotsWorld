using Microsoft.EntityFrameworkCore.Migrations;

namespace RobotsWorld.Data.Migrations
{
    public partial class AddedTransportTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransportType",
                table: "Deliveries",
                newName: "TransportTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "TransportTypeId",
                table: "Deliveries",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TransportTypes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_TransportTypeId",
                table: "Deliveries",
                column: "TransportTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_TransportTypes_TransportTypeId",
                table: "Deliveries",
                column: "TransportTypeId",
                principalTable: "TransportTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_TransportTypes_TransportTypeId",
                table: "Deliveries");

            migrationBuilder.DropTable(
                name: "TransportTypes");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_TransportTypeId",
                table: "Deliveries");

            migrationBuilder.RenameColumn(
                name: "TransportTypeId",
                table: "Deliveries",
                newName: "TransportType");

            migrationBuilder.AlterColumn<string>(
                name: "TransportType",
                table: "Deliveries",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
