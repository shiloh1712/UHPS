-- create db+schema
CREATE DATABASE PO
CREATE SCHEMA PO
USE PO

SELECT * FROM SYS.TABLES

--CREATE tables	AND INSERT SAMPLE DATA							
CREATE TABLE ADDRESS(
	ID INT PRIMARY KEY IDENTITY(1,1),
	StreetAddress VARCHAR(30),
	City VARCHAR(10),
	StateName CHAR(2),
	Zipcode CHAR(5)
)
INSERT INTO ADDRESS (StreetAddress, City, StateName, Zipcode) VALUES 
('4800 Calhoun Rd', 'Houston', 'TX', 77024),
('4500 University Dr', 'Houston', 'TX', 77004),
('4373 Cougar Village Dr', 'Houston', 'TX', 77204),
('UH Entrance 14', 'Houston', 'TX', 77004),
('4455 University Dr', 'Houston', 'TX', 77204)

CREATE TABLE PACKAGE(
	TrackingNumber char(6) NOT NULL PRIMARY KEY,
	Sender int NOT NULL, 
	Recipient INT NOT NULL,	
	ToAddress int NOT NULL,	
	Descrp varchar(60),	
	Stat tinyint DEFAULT 0,
	TimeDelivered datetime,
	CHECK (Stat <= 5 and Stat >=0)
)
INSERT INTO PACKAGE (TrackingNumber,Sender,	Recipient,ToAddress,Descrp) VALUES (123456, 1, 2, 3, 'SOME STUFF' )

CREATE TABLE SERVICES(
	ID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	Name varchar(15) NOT NULL,
	UnitCost int NOT NULL,
	Quantity int NOT NULL
)

CREATE TABLE EMPLOYEE(
	ID int NOT NULL PRIMARY KEY IDENTITY(1,1), 
	Name varchar(15) NOT NULL,
	PhoneNumber char(10) NOT NULL,
	Address int NOT NULL, 
	Password varchar(20) CONSTRAINT PasswordCheck CHECK (LEN(Password) >= 8) NOT NULL, 
	WorkPlace int NOT NULL 
)
INSERT INTO EMPLOYEE (Name, PhoneNumber, Address,Password,WorkPlace) VALUES ('First Employee', 1234567890, 1, 'idunnowhatpassword', 1)

CREATE TABLE TRACKING_RECORD(
	ID INT NOT NULL IDENTITY(1,1),
	EmployeeID INT NOT NULL,
	TrackingNumber CHAR(6) NOT NULL,
	StoreID INT NOT NULL,
	TimeIn DATETIME NOT NULL DEFAULT GETDATE(),
	TimeOut DATETIME,
	AddressID INT NOT NULL,
	PRIMARY KEY (ID)
) 
INSERT INTO TRACKING_RECORD (EmployeeID, TrackingNumber, StoreID, AddressID) VALUES (1, 123456, 1, 2)

CREATE TABLE SHIPMENT_TYPE(
	ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Weight decimal(3,2) NOT NULL, 
	Length decimal(3,2) NOT NULL, 
	Height decimal(3,2) NOT NULL,
	Girth decimal(2,2) NOT NULL,
	Priority varchar(3) NOT NULL,
	Letter varchar(3) NOT NULL,
	Package varchar(3) NOT NULL 
)
//weight has to be lower than 150 lbs
//length has to be under 108 inches
//last three possibly yes or no
//Package as in not a letter, anything higher than 15 length, 12 height, .75 girth


INSERT INTO SHIPMENT_TYPE (ID, Weight, Length, Height, Girth, Priority, Letter, Package) VALUES (4.2, 12.16, 13.62, 0.25, Yes, Yes, No)

CREATE TABLE STORE(
	ID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	Supervisor int NOT NULL,
	Address int NOT NULL,
	RegisterCode char(6) NOT NULL,
	PhoneNum char(10) NOT NULL
)
INSERT INTO STORE (Supervisor,Address,RegisterCode) VALUES (1,2,654321)

CREATE TABLE CUSTOMER(
	ID int PRIMARY KEY NOT NULL IDENTITY(1,1),
	Name varchar(15) NOT NULL,
	phoneNumber char(10) NOT NULL,
	email varchar(20) NOT NULL,
	pswrd varchar(20) CONSTRAINT CK_Users_Pswrd CHECK (LEN(Pswrd) >= 8),
	addr int NOT NULL
)

CREATE TABLE PKGSTATUS(
	StatusID int PRIMARY KEY NOT NULL,
	details varchar(20) NOT NULL
)
INSERT INTO PKGSTATUS (StatusID, details) VALUES
(0, 'In Store'),
(1, 'In Transit'),
(2, 'Out For Delivery'),
(3, 'Delivered'),
(4, 'Lost'),
(5, 'Returned')


INSERT INTO CUSTOMER (Name, PHONENUMBER, email, pswrd, addr) VALUES
('Snuffles', '0987654321', 'snuffles@gmail.com', 'password1', 4),
('JOSH', '2345678901', 'JOSH@gmail.com', 'password2', 5)

CREATE TABLE 

--add foreign keys
ALTER TABLE STORE
ADD
CONSTRAINT EMP_REF FOREIGN KEY (Supervisor) REFERENCES	EMPLOYEE(ID),
CONSTRAINT STORE_ADDR FOREIGN KEY (Address) REFERENCES ADDRESS(ID)

ALTER TABLE EMPLOYEE
ADD 
CONSTRAINT EMP_STORE FOREIGN KEY (WorkPlace) REFERENCES STORE(ID),
CONSTRAINT EMP_ADDR FOREIGN KEY (Address) REFERENCES ADDRESS(ID)

ALTER TABLE PACKAGE ADD
CONSTRAINT PKG_SENDER FOREIGN KEY (Sender) REFERENCES CUSTOMER(ID),
CONSTRAINT PKG_RCVER FOREIGN KEY (Recipient) REFERENCES CUSTOMER(ID),
CONSTRAINT PKG_TO FOREIGN KEY (ToAddress) REFERENCES ADDRESS(ID)

ALTER TABLE TRACKING_RECORD ADD
CONSTRAINT WHERE_AT FOREIGN KEY (AddressID) REFERENCES ADDRESS(ID),
CONSTRAINT TRCK_PKG FOREIGN KEY (TrackingNumber) REFERENCES PACKAGE(TrackingNumber),
CONSTRAINT CHECKER FOREIGN KEY (EMPLOYEEID) REFERENCES EMPLOYEE(ID),
CONSTRAINT TRCK_STORE FOREIGN KEY (STOREID) REFERENCES STORE(ID)

ALTER TABLE CUSTOMER ADD
CONSTRAINT CUST_ADDR FOREIGN KEY (ADDR) REFERENCES ADDRESS(ID)

ALTER TABLE PACKAGE ADD
CONSTRAINT pkg_type FOREIGN KEY (PkgType) REFERENCES SHIPMENT_TYPE(ID)



--for testing
USE MASTER
DROP DATABASE PO

drop table ADDRESS,CUSTOMER,PACKAGE,STORE,EMPLOYEE,TRACKING_RECORD, TRACKSTATUS
