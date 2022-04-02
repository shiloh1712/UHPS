using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UHPostalService.Migrations
{
    public partial class migrationname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Postages",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PKG",
                table: "Postages",
                newName: "PackageID");

            migrationBuilder.AddColumn<string>(
                name: "atr",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "atr",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Postages",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "PackageID",
                table: "Postages",
                newName: "PKG");
        }
    }
}
