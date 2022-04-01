using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UHPostalService.Migrations
{
    public partial class editaddre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Addresses_StreetAddress_City_State_Zipcode",
                table: "Addresses");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StreetAddress",
                table: "Addresses",
                column: "StreetAddress",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Addresses_StreetAddress",
                table: "Addresses");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StreetAddress_City_State_Zipcode",
                table: "Addresses",
                columns: new[] { "StreetAddress", "City", "State", "Zipcode" },
                unique: true);
        }
    }
}
