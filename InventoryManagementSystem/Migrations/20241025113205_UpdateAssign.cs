using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAssign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assignments_Products_ProductId",
                table: "assignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_assignments",
                table: "assignments");

            migrationBuilder.RenameTable(
                name: "assignments",
                newName: "Assignments");

            migrationBuilder.RenameIndex(
                name: "IX_assignments_ProductId",
                table: "Assignments",
                newName: "IX_Assignments_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Products_ProductId",
                table: "Assignments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Products_ProductId",
                table: "Assignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.RenameTable(
                name: "Assignments",
                newName: "assignments");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_ProductId",
                table: "assignments",
                newName: "IX_assignments_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_assignments",
                table: "assignments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_assignments_Products_ProductId",
                table: "assignments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
