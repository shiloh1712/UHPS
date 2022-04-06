use [aspnet-UHPS]
select * from stores
select * from employees

drop trigger if exists testtrigger
go
create trigger testtrigger
on packages
after insert
as begin
	declare @tracknum int;
	declare @employeeid int;
	declare @store int;
	declare @time datetime;
	select @tracknum = Id, @employeeid=1, @store=(select Id from Employees where name='Admin'), @time=GETDATE()
	from inserted;
	insert into TrackingRecords (EmployeeId,TrackNum,Store, TimeIn, TimeOut, Destination) values (@employeeid,@tracknum,@store,GETDATE(), @time, 1);
end

insert into Customers (Name, PHONENUMBER, email, Password, AddressID) VALUES
('Snuffles', '0987654321', 'snuffles@gmail.com', 'password1', 1),
('JOSH', '2345678901', 'JOSH@gmail.com', 'password2', 1)

insert into packages (senderid, receiverid, addrtoid, description, weight, express, shipcost, status) values(1,2,1,'valuable',4.8,1,10.5,2)

select * from TrackingRecords

drop trigger if exists cusresidestore
go
create trigger cusresidestore
before packages
after insert
as begin
	declare @tracknum int;
	declare @employeeid int;
	declare @store int;
	declare @time datetime;
	select @tracknum = Id, @employeeid=1, @store=(select Id from Employees where name='Admin'), @time=GETDATE()
	from inserted;
	insert into TrackingRecords (EmployeeId,TrackNum,Store, TimeIn, TimeOut, Destination) values (@employeeid,@tracknum,@store,GETDATE(), @time, 1);
end

drop trigger if exists autostatus
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
end
	
select * from Packages
update TrackingRecords set Destination=1 where TrackNum=2
