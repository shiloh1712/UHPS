use [aspnet-uhps-db]
--trigger: if package is sent to destination instead of another store, package status is updated to out-for-delivery
drop trigger if exists autostatus
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
            select @stat = 0
        else
            begin
                if @out = @dest
                    select @stat = 2
                else
                    select @stat = 1
            end
        update Packages set status = @stat where Id=@tracknum
end
--trigger: auto update timein/timeout of package when check in/out
drop trigger if exists datechange
go
create trigger datechange on trackingrecords
after insert, update
as begin
    declare @tin DateTime;
    declare @tout DateTime;
    declare @ident int;
    select @ident=Id from inserted;
    select @tin=trackingrecords.TimeIn, @tout=trackingrecords.TimeOut from inserted,trackingrecords where trackingrecords.Id=@ident;
    if @tin is NULL
    begin
        update trackingrecords set trackingrecords.TimeIn = getdate() where trackingrecords.Id=@ident;
    end
    else
    begin
        update trackingrecords set trackingrecords.TimeOut = getdate() where trackingrecords.Id=@ident;
    end
end
--trigger: automatically set the cost of a package
drop trigger if exists cost
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