using Microsoft.EntityFrameworkCore.Migrations;

namespace RobotsWorld.Data.Migrations
{
    public partial class UpdatedDeleteBehaviors2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assemblies_Robots_RobotId",
                table: "Assemblies");

            migrationBuilder.AddForeignKey(
                name: "FK_Assemblies_Robots_RobotId",
                table: "Assemblies",
                column: "RobotId",
                principalTable: "Robots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assemblies_Robots_RobotId",
                table: "Assemblies");

            migrationBuilder.AddForeignKey(
                name: "FK_Assemblies_Robots_RobotId",
                table: "Assemblies",
                column: "RobotId",
                principalTable: "Robots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
