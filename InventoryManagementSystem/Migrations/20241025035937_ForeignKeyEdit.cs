using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeyEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Inventories_InventoryNameId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "InventoryNameId",
                table: "Products",
                newName: "InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_InventoryNameId",
                table: "Products",
                newName: "IX_Products_InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Inventories_InventoryId",
                table: "Products",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Inventories_InventoryId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "InventoryId",
                table: "Products",
                newName: "InventoryNameId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_InventoryId",
                table: "Products",
                newName: "IX_Products_InventoryNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Inventories_InventoryNameId",
                table: "Products",
                column: "InventoryNameId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
