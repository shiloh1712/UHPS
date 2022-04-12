using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UHPostalService.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Stores_StoreID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Employees_SupID",
                table: "Stores");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Stores_StoreID",
                table: "Employees",
                column: "StoreID",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Employees_SupID",
                table: "Stores",
                column: "SupID",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.Sql(@"drop trigger if exists adminreplacesup
                                    go
                                    create trigger adminreplacesup on employees
                                    for delete
                                    as begin
                                            declare @employeeid int;
                                            declare @storeid int;
                                            select @employeeid = id from deleted
                                            select @storeid = id from stores where supid=@employeeid
                                            if @storeid is not null
                                            begin
                                                update stores set supid = (select top 1 id from employees) where id = @storeid
                                                update trackingrecords set employeeid = (select top 1 id from employees) where employeeid = @employeeid                                                
                                            end
                                            delete from employees where id = @employeeid
                                    end"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Stores_StoreID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Employees_SupID",
                table: "Stores");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Stores_StoreID",
                table: "Employees",
                column: "StoreID",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Employees_SupID",
                table: "Stores",
                column: "SupID",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
