using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UHPostalService.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Store",
                table: "TrackingRecords",
                newName: "AddressId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeOut",
                table: "TrackingRecords",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "TrackingRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Destination",
                table: "TrackingRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "TrackingRecords",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrackingRecords_AddressId",
                table: "TrackingRecords",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingRecords_EmployeeId",
                table: "TrackingRecords",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingRecords_StoreId",
                table: "TrackingRecords",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackingRecords_Addresses_AddressId",
                table: "TrackingRecords",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrackingRecords_Employees_EmployeeId",
                table: "TrackingRecords",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackingRecords_Stores_StoreId",
                table: "TrackingRecords",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackingRecords_Addresses_AddressId",
                table: "TrackingRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackingRecords_Employees_EmployeeId",
                table: "TrackingRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackingRecords_Stores_StoreId",
                table: "TrackingRecords");

            migrationBuilder.DropIndex(
                name: "IX_TrackingRecords_AddressId",
                table: "TrackingRecords");

            migrationBuilder.DropIndex(
                name: "IX_TrackingRecords_EmployeeId",
                table: "TrackingRecords");

            migrationBuilder.DropIndex(
                name: "IX_TrackingRecords_StoreId",
                table: "TrackingRecords");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "TrackingRecords");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "TrackingRecords",
                newName: "Store");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeOut",
                table: "TrackingRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "TrackingRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Destination",
                table: "TrackingRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
