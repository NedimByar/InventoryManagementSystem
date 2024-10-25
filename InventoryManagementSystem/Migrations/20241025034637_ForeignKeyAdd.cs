using Microsoft.EntityFrameworkCore.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeyAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InventoryNameId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_InventoryNameId",
                table: "Products",
                column: "InventoryNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Inventories_InventoryNameId",
                table: "Products",
                column: "InventoryNameId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Inventories_InventoryNameId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_InventoryNameId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InventoryNameId",
                table: "Products");
        }
    }
}
