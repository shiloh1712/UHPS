using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UHPostalService.Migrations
{
    public partial class SetNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Customers_SenderID",
                table: "Packages");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Customers_SenderID",
                table: "Packages",
                column: "SenderID",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Customers_SenderID",
                table: "Packages");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Customers_SenderID",
                table: "Packages",
                column: "SenderID",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
