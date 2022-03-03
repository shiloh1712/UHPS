--Son begin
CREATE SCHEMA PO;
GO

--CREATE ENTITY PACKAGE
CREATE TABLE PACKAGE(
	TrackingNumber char(12) NOT NULL PRIMARY KEY,
	Sender int NOT NULL,
	Recipient INT NOT NULL,
	ToAddress int NOT NULL,
	Descrp varchar(60),
	Stat tinyint DEFAULT 0,
	TimeDelivered datetime,
	CHECK (Stat <= 5 and Stat >=0)
)
GO
--TRY INSERTING A PKG
INSERT INTO PACKAGE (TrackingNumber,Sender,	Recipient,ToAddress,Descrp) VALUES (123456789012, 1, 2, 3, 'SOME STUFF' )
GO
--DISPLAY ALL PKG
select * from PACKAGE
GO
--Son end

--Andy begin
CREATE TABLE EMPLOYEE(
	ID int NOT NULL,
	Employee_Name varchar(15) NOT NULL,
	Phone_Number char(10) NOT NULL,
	Address int NOT NULL, /* Address is highlighted blue, will that be ok? Or does it need a name change*/
	Password varchar(20) NOT NULL, /*dont know how to set parameters for varchar, maybe put input restrictions*/
	Work_Place int NOT NULL

);

--Morrison begin
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
 );


--Bader begin
CREATE TABLE STORE(
	storeID int NOT NULL  
	, Supervisor int NOT NULL
	, Address int NOT NULL
	, Register ()
);

--Josh Begin
CREATE TABLE CUSTOMER(
	ID int PRIMARY KEY NOT NULL,
	custName varchar(15) NOT NULL,
	phoneNumber char(10) NOT NULL,
	email varchar(20) NOT NULL,
	pswrd varchar(20) CONSTRAINT CK_Users_Pswrd CHECK (LEN(Pswrd) >= 8),
	addr int NOT NULL,
	)
