--Son begin
														--1
CREATE SCHEMA PO;

--CREATE ENTITY ADDRESS									--2
CREATE TABLE ADDRESS(
	AddrID INT PRIMARY KEY IDENTITY(1,1),
	StreetAddress VARCHAR(30),
	City VARCHAR(10),
	StateName CHAR(2),
	Zipcode CHAR(5)
)

--TRY INSERTING A PKG
INSERT INTO ADDRESS (StreetAddress, City, StateName, Zipcode) VALUES ('4800 Calhoun Rd', 'Houston', 'TX', 77024)

--DISPLAY ALL PKG
select * from ADDRESS


--order of create table: address, customer, package, STORE, EMPLOYEE (ADD REFERENCE FROM STORE), TRACKING_RECORD
--add foreign key for store								--7
ALTER TABLE STORE
ADD FOREIGN KEY (Supervisor) REFERENCES	EMPLOYEE(ID)

--CREATE ENTITY PACKAGE									--4
CREATE TABLE PACKAGE(
	TrackingNumber char(12) NOT NULL PRIMARY KEY,
	Sender int NOT NULL, --reference customer
	Recipient INT NOT NULL,	--reference customer
	ToAddress int NOT NULL,	--reference address
	Descrp varchar(60),	
	Stat tinyint DEFAULT 0,
	TimeDelivered datetime,
	CHECK (Stat <= 5 and Stat >=0)
)

--TRY INSERTING A PKG
INSERT INTO PACKAGE (TrackingNumber,Sender,	Recipient,ToAddress,Descrp) VALUES (123456789012, 1, 2, 3, 'SOME STUFF' )

--DISPLAY ALL PKG
select * from PACKAGE



--Son end

--Andy begin											--6
CREATE TABLE EMPLOYEE(
	ID int NOT NULL, 
	Employee_Name varchar(15) NOT NULL,
	Phone_Number char(10) NOT NULL,
	Address int NOT NULL, /* Address is highlighted blue, will that be ok? Or does it need a name change*/
	Password varchar(20) NOT NULL, /*dont know how to set parameters for varchar, maybe put input restrictions*/
	Work_Place int NOT NULL
	--missing PK; 
	--use other attribute names for address+password; 
	--Work_place,Address need to reference STORE, ADDRESS tables
);

--Morrison begin										--8
CREATE TABLE TRACKING_RECORD(
	ID INT NOT NULL,
	Employee_ID INT NOT NULL,
	Tracking_Number CHAR(12) NOT NULL,
	Store_ID INT NOT NULL,
	Time_In DATETIME NOT NULL,
	Time_Out DATETIME,
	Address_ID INT NOT NULL,
	PRIMARY KEY (ID), 
	FOREIGN KEY (Address_ID) REFERENCES PACKAGE(ToAddress) 
	/*
	address_id actually reference address table, not package
	Employee_ID, Tracking_Number, Store_ID need to reference EMPLOYEE, PACKAGE, STORE table
	*/
 );


--Bader begin											--5
CREATE TABLE STORE(
	storeID int NOT NULL  
	, Supervisor int NOT NULL
	, Address int NOT NULL
	, Register ()
	/*
	missing PK
	Supervisor (Son did this), Address need to reference EMPLOYEE, ADDRESS tables
	Register ()?
	*/
);

--Josh Begin											--3
CREATE TABLE CUSTOMER(
	ID int PRIMARY KEY NOT NULL,
	custName varchar(15) NOT NULL,
	phoneNumber char(10) NOT NULL,
	email varchar(20) NOT NULL,
	pswrd varchar(20) CONSTRAINT CK_Users_Pswrd CHECK (LEN(Pswrd) >= 8),
	addr int NOT NULL,
	/*
	addr need to refenrece ADDRESS TABLE
	*/
	)
