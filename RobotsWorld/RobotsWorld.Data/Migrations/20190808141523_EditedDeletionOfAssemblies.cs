using Microsoft.EntityFrameworkCore.Migrations;

namespace RobotsWorld.Data.Migrations
{
    public partial class EditedDeletionOfAssemblies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubAssemblies_Assemblies_AssemblyId",
                table: "SubAssemblies");

            migrationBuilder.AddForeignKey(
                name: "FK_SubAssemblies_Assemblies_AssemblyId",
                table: "SubAssemblies",
                column: "AssemblyId",
                principalTable: "Assemblies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubAssemblies_Assemblies_AssemblyId",
                table: "SubAssemblies");

            migrationBuilder.AddForeignKey(
                name: "FK_SubAssemblies_Assemblies_AssemblyId",
                table: "SubAssemblies",
                column: "AssemblyId",
                principalTable: "Assemblies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
