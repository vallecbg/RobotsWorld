using Microsoft.EntityFrameworkCore.Migrations;

namespace RobotsWorld.Data.Migrations
{
    public partial class AddedVendors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "SubAssemblies",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "VendorId",
                table: "Parts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parts_VendorId",
                table: "Parts",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Vendors_VendorId",
                table: "Parts",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Vendors_VendorId",
                table: "Parts");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Parts_VendorId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "SubAssemblies");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Parts");
        }
    }
}
