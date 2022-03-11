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

CREATE TABLE SALE(
	ITEMID int NOT NULL
    , QUANTITY int NOT NULL
    , DATE datetime (YYYY-MM-DD) NOT NULL
    , PRICE real (10, 2) NOT NULL
);