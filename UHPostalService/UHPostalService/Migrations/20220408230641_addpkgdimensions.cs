using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UHPostalService.Migrations
{
    public partial class addpkgdimensions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Depth",
                table: "Packages",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "Packages",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Width",
                table: "Packages",
                type: "real",
                nullable: false,
                defaultValue: 0f);
            //trigger: automatically set the cost of a package
            migrationBuilder.Sql(@"		drop trigger if exists cost
		                            go
		                            create trigger cost on packages
		                            after insert
		                            as begin
			                            declare @total float;
			                            declare @W float;
			                            declare @exp int;
			                            declare @ident int;
										declare @depth float;
										declare @width float;
										declare @height float;
			                            select @ident=Id from inserted;
			                            select @W = packages.Weight, @exp = packages.Express, @depth = packages.Depth, @width = packages.Width, @height = packages.Height from packages where packages.Id =@ident;
										declare @bound int;
										select @bound = Id from ShipmentClasses where ExpressCost >= 0;
											if @exp = 1
											begin
											set @total = (select min(ShipmentClasses.ExpressCost) from ShipmentClasses where @height < ShipmentClasses.MaxHeight and @width < ShipmentClasses.MaxWidth and @depth < ShipmentClasses.MaxLength);

											end

											if @exp = 0
											begin
											set @total = (select min(ShipmentClasses.GroundCost) from ShipmentClasses where @height < ShipmentClasses.MaxHeight and @width < ShipmentClasses.MaxWidth and @depth < ShipmentClasses.MaxLength);
											end
										if @total is null
										begin
											if @exp = 1
											begin
											set @total = (select max(ExpressCost) from ShipmentClasses);
											end
											else
											begin
											set @total = (select max(GroundCost) from ShipmentClasses);
											end
										end
										set @total = (@total * @W);	
			                            update packages set packages.ShipCost = @total where packages.Id = @ident;
		                            end
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Depth",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Packages");
        }
    }
}
