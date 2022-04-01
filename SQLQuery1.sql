/*
CREATE SCHEMA PO;
GO
CREATE TABLE PO.PKG (ID INT, DESCN VARCHAR(20));
GO
*/


--SELECT * FROM PO.PKG;



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

