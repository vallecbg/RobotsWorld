using Microsoft.EntityFrameworkCore.Migrations;

namespace RobotsWorld.Data.Migrations
{
    public partial class UpdatedDeleteBehaviors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_SubAssemblies_SubAssemblyId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_SubAssemblies_Assemblies_AssemblyId",
                table: "SubAssemblies");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_SubAssemblies_SubAssemblyId",
                table: "Parts",
                column: "SubAssemblyId",
                principalTable: "SubAssemblies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubAssemblies_Assemblies_AssemblyId",
                table: "SubAssemblies",
                column: "AssemblyId",
                principalTable: "Assemblies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_SubAssemblies_SubAssemblyId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_SubAssemblies_Assemblies_AssemblyId",
                table: "SubAssemblies");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_SubAssemblies_SubAssemblyId",
                table: "Parts",
                column: "SubAssemblyId",
                principalTable: "SubAssemblies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubAssemblies_Assemblies_AssemblyId",
                table: "SubAssemblies",
                column: "AssemblyId",
                principalTable: "Assemblies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
