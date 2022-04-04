using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UHPostalService.Migrations
{
    public partial class restart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetAddress = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    City = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    State = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Zipcode = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });
            //HEADQUARTER
            migrationBuilder.Sql("INSERT INTO Addresses (StreetAddress, City, State, Zipcode) VALUES ('4800 Calhoun Rd', 'Houston', 'TX', '77024')");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Desc = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UnitCost = table.Column<float>(type: "real", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PURCHASE_DATE = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentClasses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCR = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LENGTH = table.Column<float>(type: "real", nullable: false),
                    HEIGHT = table.Column<float>(type: "real", nullable: false),
                    WIDTH = table.Column<float>(type: "real", nullable: false),
                    GroundCost = table.Column<float>(type: "real", nullable: false),
                    ExpressCost = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentClasses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TrackingRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    TrackNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Store = table.Column<int>(type: "int", nullable: false),
                    TimeIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Destination = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderID = table.Column<int>(type: "int", nullable: false),
                    ReceiverID = table.Column<int>(type: "int", nullable: false),
                    AddrToID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Express = table.Column<bool>(type: "bit", nullable: false),
                    ShipCost = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packages_Addresses_AddrToID",
                        column: x => x.AddrToID,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Packages_Customers_ReceiverID",
                        column: x => x.ReceiverID,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Packages_Customers_SenderID",
                        column: x => x.SenderID,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressID = table.Column<int>(type: "int", nullable: false),
                    StoreID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            //FOUNDER: ADIMN
            migrationBuilder.Sql(@"INSERT INTO Employees (Name, PhoneNumber, Email, Password, AddressID)
                                    VALUES ('Admin', '7777777777', 'admin@uhps.com', 'password', (SELECT Id FROM Addresses WHERE StreetAddress = '4800 Calhoun Rd'))");

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupID = table.Column<int>(type: "int", nullable: false),
                    AddressID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stores_Employees_SupID",
                        column: x => x.SupID,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });
            //FIRST STORE
            migrationBuilder.Sql(@"INSERT INTO Stores (PhoneNumber, SupID, AddressID)
                                    VALUES ('1234567890', (SELECT Id FROM Employees WHERE Email = 'admin@uhps.com'), 1)");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StreetAddress_City_State_Zipcode",
                table: "Addresses",
                columns: new[] { "StreetAddress", "City", "State", "Zipcode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressID",
                table: "Customers",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AddressID",
                table: "Employees",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PhoneNumber",
                table: "Employees",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StoreID",
                table: "Employees",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_AddrToID",
                table: "Packages",
                column: "AddrToID");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_ReceiverID",
                table: "Packages",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_SenderID",
                table: "Packages",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_AddressID",
                table: "Stores",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_SupID",
                table: "Stores",
                column: "SupID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Stores_StoreID",
                table: "Employees",
                column: "StoreID",
                principalTable: "Stores",
                principalColumn: "Id");

            //trigger: if package is sent to destination instead of another store, package status is updated to out-for-delivery 
            migrationBuilder.Sql(@"drop trigger if exists autostatus
                                    go
                                    create trigger autostatus on trackingrecords
                                    after insert, update
                                    as begin
	                                    declare @tracknum int;
	                                    declare @dest int;
	                                    declare @out int;
	                                    select @tracknum=tracknum, @out=destination, @dest=addrtoid from inserted,Packages where Packages.Id = inserted.TrackNum;
	                                    if @out = @dest
	                                    begin
		                                    update Packages set status = 4 where Id=@tracknum
	                                    end
                                    end"

            );
            migrationBuilder.Sql(@"drop trigger if exists datechange
                                    go
                                    create trigger datechange on trackingrecords
                                    after insert, update
                                    as begin
                                        declare @tin DateTime;
                                        declare @tout DateTime;
                                        select @tin=TimeIn, @tout=TimeOut from trackingrecords;
                                        if @tin is NULL
                                        begin
                                            update trackingrecords set TimeIn = getdate();
                                        end
                                        else
                                        begin
                                            update trackingrecords set TimeOut = getdate();
                                        end
                                    end");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Addresses_AddressID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Addresses_AddressID",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Stores_StoreID",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "ShipmentClasses");

            migrationBuilder.DropTable(
                name: "TrackingRecords");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
