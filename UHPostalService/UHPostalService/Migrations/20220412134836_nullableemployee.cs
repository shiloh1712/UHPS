using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UHPostalService.Migrations
{
    public partial class nullableemployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stores_SupID",
                table: "Stores");

            migrationBuilder.AlterColumn<int>(
                name: "SupID",
                table: "Stores",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_SupID",
                table: "Stores",
                column: "SupID",
                //unique: true,
                filter: "[SupID] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stores_SupID",
                table: "Stores");

            migrationBuilder.AlterColumn<int>(
                name: "SupID",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stores_SupID",
                table: "Stores",
                column: "SupID",
                unique: true);
        }
    }
}
