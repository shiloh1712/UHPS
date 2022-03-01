--Son begin
CREATE SCHEMA PO;
GO
CREATE TABLE PO.PKG (ID INT, DESCN VARCHAR(20));
GO

CREATE TABLE PACKAGE(
	TrackingNumber char(12) NOT NULL PRIMARY KEY
	, Sender int NOT NULL
	, Recipient INT NOT NULL
	, ToAddress int 
	, Descrp varchar(60)
	, Stat int (options: in store, in transit, out for delivery, delivered, lost, returned)
	, TimeDelivered datetime
)
GO
--Son end

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


--Bader
CREATE TABLE STORE();
