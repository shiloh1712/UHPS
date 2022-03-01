--CREATE SCHEMA
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
