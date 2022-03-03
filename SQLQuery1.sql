/*
CREATE SCHEMA PO;
GO
CREATE TABLE PO.PKG (ID INT, DESCN VARCHAR(20));
GO
*/


--SELECT * FROM PO.PKG;


CREATE TABLE SQLTest (
	ID INT NOT NULL PRIMARY KEY,
	c1 VARCHAR(100) NOT NULL,
	dt1 DATETIME NOT NULL DEFAULT getdate()
)
GO

INSERT INTO SQLTest (ID, c1) VALUES (1, 'test1')
GO

select * from SQLTest;
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

CREATE TABLE EMPLOYEE(
	ID int NOT NULL,
	Employee_Name varchar(15) NOT NULL,
	Phone_Number char(10) NOT NULL,
	Address int NOT NULL, /* Address is highlighted blue, will that be ok? Or does it need a name change*/
	Password varchar(20) NOT NULL, /*dont know how to set parameters for varchar, maybe put input restrictions*/
	Work_Place int NOT NULL

);


