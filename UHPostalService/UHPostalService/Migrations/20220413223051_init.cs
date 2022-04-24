using System;
using Microsoft.EntityFrameworkCore.Migrations;
using UHPostalService.Models;

#nullable disable

namespace UHPostalService.Migrations
{
    public partial class init : Migration
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
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    State = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Zipcode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
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
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitCost = table.Column<float>(type: "real", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxLength = table.Column<float>(type: "real", nullable: false),
                    MaxHeight = table.Column<float>(type: "real", nullable: false),
                    MaxWidth = table.Column<float>(type: "real", nullable: false),
                    GroundCost = table.Column<float>(type: "real", nullable: false),
                    ExpressCost = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentClasses", x => x.Id);
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
                    SenderID = table.Column<int>(type: "int", nullable: true),
                    ReceiverID = table.Column<int>(type: "int", nullable: true),
                    AddressID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Width = table.Column<float>(type: "real", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    Depth = table.Column<float>(type: "real", nullable: false),
                    Express = table.Column<bool>(type: "bit", nullable: false),
                    ClassID = table.Column<int>(type: "int", nullable: false),
                    ShipCost = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packages_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Packages_Customers_ReceiverID",
                        column: x => x.ReceiverID,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Packages_Customers_SenderID",
                        column: x => x.SenderID,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Packages_ShipmentClasses_ClassID",
                        column: x => x.ClassID,
                        principalTable: "ShipmentClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuyerID = table.Column<int>(type: "int", nullable: true),
                    Total = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sales_Customers_BuyerID",
                        column: x => x.BuyerID,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sales_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id");
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
                    StoreID = table.Column<int>(type: "int", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false, defaultValue: 2)
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
            //FOUNDER:
            migrationBuilder.Sql(@"INSERT INTO Employees (Name, PhoneNumber, Email, Password, AddressID, Role)
                                    VALUES ('Admin', '7777777777', 'admin@uhps.com', 'password', (SELECT TOP 1 Id FROM Addresses)," + $"{(int)Role.Admin})");


            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupID = table.Column<int>(type: "int", nullable: true),
                    AddressID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stores_Employees_SupID",
                        column: x => x.SupID,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });
            //FIRST STORE
            migrationBuilder.Sql(@"INSERT INTO Stores (PhoneNumber, SupID, AddressID) VALUES('1234567890', (SELECT Top 1 Id FROM Employees), 1)");
            migrationBuilder.Sql(@"UPDATE Employees set StoreID =1 where name='admin'");


            migrationBuilder.CreateTable(
                name: "TrackingRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    TrackNum = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    TimeIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Destination = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackingRecords_Addresses_Destination",
                        column: x => x.Destination,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrackingRecords_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrackingRecords_Packages_TrackNum",
                        column: x => x.TrackNum,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackingRecords_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            
            //trigger: if package is sent to destination instead of another store, package status is updated to out-for-delivery
            migrationBuilder.Sql(@"drop trigger if exists autostatus
                                    go
                                    create trigger autostatus on trackingrecords
                                    after insert, update
                                    as begin
                                            declare @tracknum int;
                                            declare @dest int;
                                            declare @out int;
                                            declare @stat int;
                                            select @tracknum=tracknum, @out=destination, @dest=addressid from inserted,Packages where Packages.Id = inserted.TrackNum;
                                            if @out is null
                                                select @stat = " + ((int)Status.InStore).ToString() + @"
                                            else
                                                begin
                                                    if @out = @dest
                                                        select @stat = " + ((int)Status.OutForDelivery).ToString() + @"
                                                    else
                                                        select @stat = " + ((int)Status.InTransit).ToString() + @"
                                                end
                                            update Packages set status = @stat where Id=@tracknum
                                    end"
            );
            //trigger: automatically set the shipment class and cost of a shipment
            migrationBuilder.Sql(@"drop trigger if exists cost
            go
            create trigger cost on packages
            after insert, update
            as begin
                declare @total float;
                declare @W float;
                declare @exp int;
                declare @ident int;
                declare @depth float;
                declare @width float;
                declare @height float;
                declare @shipclass int;
                select @ident=Id from inserted;
                select @W = packages.Weight, @exp = packages.Express, @depth = packages.Depth, @width = packages.Width, @height = packages.Height from packages where packages.Id =@ident;
                declare @bound int;
                select @bound = Id from ShipmentClasses where ExpressCost >= 0;
                select @shipclass = (select top 1 id from ShipmentClasses where @height < ShipmentClasses.MaxHeight and @width < ShipmentClasses.MaxWidth and @depth < ShipmentClasses.MaxLength order by GroundCost)
                if @shipclass is null
                select @shipclass=(select top 1 id from ShipmentClasses order by GroundCost desc)

                    if @exp = 1
                    begin
                    select @total = ShipmentClasses.ExpressCost from ShipmentClasses where id=@shipclass;

                    end

                    if @exp = 0
                    begin
                    select @total = ShipmentClasses.GroundCost from ShipmentClasses where id=@shipclass;
                    end

                set @total = (@total * @W);	
                update packages set packages.ShipCost = @total, packages.classid=@shipclass where packages.Id = @ident;
            end");


            migrationBuilder.Sql(@"drop trigger if exists NewSale
                                    go
                                    create trigger NewSale on sales
                                    after insert
                                    as begin
                                        declare @ident int;
										declare @quant int;
										declare @pid int;
										declare @tot float;
                                        select @ident=Id, @quant = Quantity, @pid = ProductID from inserted;
										select @tot = products.UnitCost from products where products.Id = @pid;
										set @tot = @tot*@quant;
                                        update sales set sales.PurchaseDate = getdate() where sales.ID=@ident;
										update sales set sales.Total = @tot where sales.ID=@ident;
										update products set products.Stock = (products.Stock-@quant) where products.Id = @pid;
                                    end

            ");
            migrationBuilder.Sql(@"drop trigger if exists UpdateSale
							go
							create trigger UpdateSale on sales
							after update
							as begin
								declare @newQuant int;
								declare @oldQuant int;
								declare @newPID int;
								declare @oldPID int;
								declare @ident int;
								declare @tot float;
								declare @cost float;
								select @ident=Id, @newQuant=Quantity, @newPID=ProductID from inserted;
								select @oldPID=ProductID, @oldQuant=Quantity from deleted;
								select @cost=UnitCost from products where products.ID = @newPID;
								if ((@oldQuant != @newQuant) and (@newPID = @oldPID))
									begin
									update products set products.Stock = (products.Stock+(@oldQuant-@newQuant)) where products.Id = @newPID;
									end
								if (@newPID != @oldPID)
									begin
									update products set products.Stock = (products.Stock+@oldQuant) where products.Id = @oldPID;
									update products set products.Stock = (products.Stock-@newQuant) where products.Id = @newPID;
									end
								set @tot = @newQuant*@cost;
								update sales set sales.Total = @tot where sales.ID = @ident;
								update sales set sales.PurchaseDate=getdate() where sales.ID = @ident;

							end
            ");
            
            


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
                name: "IX_Packages_AddressID",
                table: "Packages",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_ClassID",
                table: "Packages",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_ReceiverID",
                table: "Packages",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_SenderID",
                table: "Packages",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_BuyerID",
                table: "Sales",
                column: "BuyerID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProductID",
                table: "Sales",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_AddressID",
                table: "Stores",
                column: "AddressID",
                unique: true,
                filter: "[AddressID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_SupID",
                table: "Stores",
                column: "SupID");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingRecords_Destination",
                table: "TrackingRecords",
                column: "Destination");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingRecords_EmployeeId",
                table: "TrackingRecords",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingRecords_StoreId",
                table: "TrackingRecords",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingRecords_TrackNum",
                table: "TrackingRecords",
                column: "TrackNum");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Stores_StoreID",
                table: "Employees",
                column: "StoreID",
                principalTable: "Stores",
                principalColumn: "Id");
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
                name: "Sales");

            migrationBuilder.DropTable(
                name: "TrackingRecords");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "ShipmentClasses");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
