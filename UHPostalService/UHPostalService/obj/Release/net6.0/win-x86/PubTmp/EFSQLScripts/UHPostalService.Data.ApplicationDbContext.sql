IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE TABLE [Addresses] (
        [Id] int NOT NULL IDENTITY,
        [StreetAddress] nvarchar(30) NOT NULL,
        [City] nvarchar(10) NOT NULL,
        [State] nvarchar(2) NOT NULL,
        [Zipcode] nvarchar(5) NOT NULL,
        CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    INSERT INTO Addresses (StreetAddress, City, State, Zipcode) VALUES ('4800 Calhoun Rd', 'Houston', 'TX', '77024')
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE TABLE [Products] (
        [Id] int NOT NULL IDENTITY,
        [Desc] nvarchar(max) NOT NULL,
        [UnitCost] real NOT NULL,
        [Stock] int NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE TABLE [ShipmentClasses] (
        [Id] int NOT NULL IDENTITY,
        [Desc] nvarchar(max) NOT NULL,
        [MaxLength] real NOT NULL,
        [MaxHeight] real NOT NULL,
        [MaxWidth] real NOT NULL,
        [GroundCost] real NOT NULL,
        [ExpressCost] real NOT NULL,
        CONSTRAINT [PK_ShipmentClasses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE TABLE [Customers] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [Email] nvarchar(450) NOT NULL,
        [Password] nvarchar(max) NULL,
        [AddressID] int NULL,
        CONSTRAINT [PK_Customers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Customers_Addresses_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [Addresses] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE TABLE [Sales] (
        [ID] int NOT NULL IDENTITY,
        [ProductID] int NOT NULL,
        [Quantity] int NOT NULL,
        [PurchaseDate] datetime2 NOT NULL,
        [Total] real NULL,
        CONSTRAINT [PK_Sales] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Sales_Products_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE TABLE [Packages] (
        [Id] int NOT NULL IDENTITY,
        [SenderID] int NOT NULL,
        [ReceiverID] int NOT NULL,
        [AddressID] int NOT NULL,
        [AddrToID] int NOT NULL,
        [Description] nvarchar(max) NULL,
        [Status] int NOT NULL,
        [Weight] real NOT NULL,
        [Express] bit NOT NULL,
        [ShipCost] real NULL,
        CONSTRAINT [PK_Packages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Packages_Addresses_AddrToID] FOREIGN KEY ([AddressID]) REFERENCES [Addresses] ([Id]),
        CONSTRAINT [FK_Packages_Customers_ReceiverID] FOREIGN KEY ([ReceiverID]) REFERENCES [Customers] ([Id]),
        CONSTRAINT [FK_Packages_Customers_SenderID] FOREIGN KEY ([SenderID]) REFERENCES [Customers] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE TABLE [Employees] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(450) NOT NULL,
        [Email] nvarchar(450) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [AddressID] int NOT NULL,
        [StoreID] int NULL,
        [Role] int NOT NULL,
        CONSTRAINT [PK_Employees] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Employees_Addresses_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [Addresses] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    INSERT INTO Employees (Name, PhoneNumber, Email, Password, AddressID, Role)
                                        VALUES ('Admin', '7777777777', 'admin@uhps.com', 'password', (SELECT TOP 1 Id FROM Addresses),0)
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE TABLE [Stores] (
        [Id] int NOT NULL IDENTITY,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [SupID] int NOT NULL,
        [AddressID] int NOT NULL,
        CONSTRAINT [PK_Stores] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Stores_Addresses_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [Addresses] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Stores_Employees_SupID] FOREIGN KEY ([SupID]) REFERENCES [Employees] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    INSERT INTO Stores (PhoneNumber, SupID, AddressID) VALUES('1234567890', (SELECT Top 1 Id FROM Employees), 1)
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE TABLE [TrackingRecords] (
        [Id] int NOT NULL IDENTITY,
        [EmployeeId] int NULL,
        [TrackNum] int NOT NULL,
        [StoreId] int NOT NULL,
        [TimeIn] datetime2 NULL,
        [TimeOut] datetime2 NULL,
        [Destination] int NULL,
        CONSTRAINT [PK_TrackingRecords] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TrackingRecords_Addresses_Destination] FOREIGN KEY ([Destination]) REFERENCES [Addresses] ([Id]),
        CONSTRAINT [FK_TrackingRecords_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]),
        CONSTRAINT [FK_TrackingRecords_Packages_TrackNum] FOREIGN KEY ([TrackNum]) REFERENCES [Packages] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TrackingRecords_Stores_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Stores] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE UNIQUE INDEX [IX_Addresses_StreetAddress_City_State_Zipcode] ON [Addresses] ([StreetAddress], [City], [State], [Zipcode]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE INDEX [IX_Customers_AddressID] ON [Customers] ([AddressID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE UNIQUE INDEX [IX_Customers_Email] ON [Customers] ([Email]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE INDEX [IX_Employees_AddressID] ON [Employees] ([AddressID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE UNIQUE INDEX [IX_Employees_Email] ON [Employees] ([Email]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE UNIQUE INDEX [IX_Employees_PhoneNumber] ON [Employees] ([PhoneNumber]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE INDEX [IX_Employees_StoreID] ON [Employees] ([StoreID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE INDEX [IX_Packages_AddrToID] ON [Packages] ([AddrToID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE INDEX [IX_Packages_ReceiverID] ON [Packages] ([ReceiverID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE INDEX [IX_Packages_SenderID] ON [Packages] ([SenderID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE INDEX [IX_Sales_ProductID] ON [Sales] ([ProductID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE INDEX [IX_Stores_AddressID] ON [Stores] ([AddressID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE UNIQUE INDEX [IX_Stores_SupID] ON [Stores] ([SupID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE INDEX [IX_TrackingRecords_Destination] ON [TrackingRecords] ([Destination]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE INDEX [IX_TrackingRecords_EmployeeId] ON [TrackingRecords] ([EmployeeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE INDEX [IX_TrackingRecords_StoreId] ON [TrackingRecords] ([StoreId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    CREATE INDEX [IX_TrackingRecords_TrackNum] ON [TrackingRecords] ([TrackNum]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    ALTER TABLE [Employees] ADD CONSTRAINT [FK_Employees_Stores_StoreID] FOREIGN KEY ([StoreID]) REFERENCES [Stores] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405033412_firstmigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220405033412_firstmigration', N'6.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405051521_editfk')
BEGIN
    ALTER TABLE [Packages] DROP CONSTRAINT [FK_Packages_Addresses_AddrToID];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405051521_editfk')
BEGIN
    DROP INDEX [IX_Packages_AddrToID] ON [Packages];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405051521_editfk')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Packages]') AND [c].[name] = N'AddrToID');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Packages] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Packages] DROP COLUMN [AddrToID];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405051521_editfk')
BEGIN
    CREATE INDEX [IX_Packages_AddressID] ON [Packages] ([AddressID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405051521_editfk')
BEGIN
    ALTER TABLE [Packages] ADD CONSTRAINT [FK_Packages_Addresses_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [Addresses] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405051521_editfk')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220405051521_editfk', N'6.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405062607_some')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220405062607_some', N'6.0.3');
END;
GO

COMMIT;
GO

