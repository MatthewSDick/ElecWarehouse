using Microsoft.EntityFrameworkCore.Migrations;

namespace ElecWarehouse.Migrations
{
    public partial class ForSomeReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationItem_Items_ItemId",
                table: "LocationItem");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationItem_Locations_LocationId",
                table: "LocationItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationItem",
                table: "LocationItem");

            migrationBuilder.RenameTable(
                name: "LocationItem",
                newName: "LocationsItems");

            migrationBuilder.RenameIndex(
                name: "IX_LocationItem_LocationId",
                table: "LocationsItems",
                newName: "IX_LocationsItems_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_LocationItem_ItemId",
                table: "LocationsItems",
                newName: "IX_LocationsItems_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationsItems",
                table: "LocationsItems",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationsItems_Items_ItemId",
                table: "LocationsItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationsItems_Locations_LocationId",
                table: "LocationsItems",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationsItems_Items_ItemId",
                table: "LocationsItems");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationsItems_Locations_LocationId",
                table: "LocationsItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationsItems",
                table: "LocationsItems");

            migrationBuilder.RenameTable(
                name: "LocationsItems",
                newName: "LocationItem");

            migrationBuilder.RenameIndex(
                name: "IX_LocationsItems_LocationId",
                table: "LocationItem",
                newName: "IX_LocationItem_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_LocationsItems_ItemId",
                table: "LocationItem",
                newName: "IX_LocationItem_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationItem",
                table: "LocationItem",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationItem_Items_ItemId",
                table: "LocationItem",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationItem_Locations_LocationId",
                table: "LocationItem",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
