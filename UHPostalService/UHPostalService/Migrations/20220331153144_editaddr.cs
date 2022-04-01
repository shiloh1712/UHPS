using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UHPostalService.Migrations
{
    public partial class editaddr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StreetAddress_City_State_Zipcode",
                table: "Addresses",
                columns: new[] { "StreetAddress", "City", "State", "Zipcode" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Addresses_StreetAddress_City_State_Zipcode",
                table: "Addresses");
        }
    }
}
