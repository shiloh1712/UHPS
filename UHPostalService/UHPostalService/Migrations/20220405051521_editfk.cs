using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UHPostalService.Migrations
{
    public partial class editfk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Addresses_AddrToID",
                table: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Packages_AddrToID",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "AddrToID",
                table: "Packages");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_AddressID",
                table: "Packages",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Addresses_AddressID",
                table: "Packages",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Addresses_AddressID",
                table: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Packages_AddressID",
                table: "Packages");

            migrationBuilder.AddColumn<int>(
                name: "AddrToID",
                table: "Packages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Packages_AddrToID",
                table: "Packages",
                column: "AddrToID");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Addresses_AddrToID",
                table: "Packages",
                column: "AddrToID",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
