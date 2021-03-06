/****** Object:  Database [aspnet-uhps]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE DATABASE [aspnet-uhps]  (EDITION = 'Basic', SERVICE_OBJECTIVE = 'Basic', MAXSIZE = 2 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS;
GO
ALTER DATABASE [aspnet-uhps] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [aspnet-uhps] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [aspnet-uhps] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [aspnet-uhps] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [aspnet-uhps] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [aspnet-uhps] SET ARITHABORT OFF 
GO
ALTER DATABASE [aspnet-uhps] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [aspnet-uhps] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [aspnet-uhps] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [aspnet-uhps] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [aspnet-uhps] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [aspnet-uhps] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [aspnet-uhps] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [aspnet-uhps] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [aspnet-uhps] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [aspnet-uhps] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [aspnet-uhps] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [aspnet-uhps] SET  MULTI_USER 
GO
ALTER DATABASE [aspnet-uhps] SET ENCRYPTION ON
GO
ALTER DATABASE [aspnet-uhps] SET QUERY_STORE = ON
GO
ALTER DATABASE [aspnet-uhps] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 7), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 10, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 4/25/2022 7:44:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 4/25/2022 7:44:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StreetAddress] [nvarchar](30) NOT NULL,
	[City] [nvarchar](30) NOT NULL,
	[State] [nvarchar](2) NOT NULL,
	[Zipcode] [nvarchar](5) NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 4/25/2022 7:44:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[Email] [nvarchar](450) NOT NULL,
	[Password] [nvarchar](max) NULL,
	[AddressID] [int] NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 4/25/2022 7:44:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](450) NOT NULL,
	[Email] [nvarchar](450) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[AddressID] [int] NOT NULL,
	[StoreID] [int] NULL,
	[Role] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Packages]    Script Date: 4/25/2022 7:44:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Packages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SenderID] [int] NULL,
	[ReceiverID] [int] NULL,
	[AddressID] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[Weight] [real] NOT NULL,
	[Width] [real] NOT NULL,
	[Height] [real] NOT NULL,
	[Depth] [real] NOT NULL,
	[Express] [bit] NOT NULL,
	[ClassID] [int] NOT NULL,
	[ShipCost] [real] NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Packages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 4/25/2022 7:44:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Desc] [nvarchar](max) NOT NULL,
	[UnitCost] [real] NOT NULL,
	[Stock] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 4/25/2022 7:44:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[Quantity] [int] NOT NULL,
	[PurchaseDate] [datetime2](7) NOT NULL,
	[BuyerID] [int] NULL,
	[Total] [real] NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShipmentClasses]    Script Date: 4/25/2022 7:44:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipmentClasses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Desc] [nvarchar](max) NOT NULL,
	[MaxLength] [real] NOT NULL,
	[MaxHeight] [real] NOT NULL,
	[MaxWidth] [real] NOT NULL,
	[GroundCost] [real] NOT NULL,
	[ExpressCost] [real] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_ShipmentClasses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stores]    Script Date: 4/25/2022 7:44:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stores](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[SupID] [int] NULL,
	[AddressID] [int] NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Stores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrackingRecords]    Script Date: 4/25/2022 7:44:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrackingRecords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NULL,
	[TrackNum] [int] NOT NULL,
	[StoreId] [int] NOT NULL,
	[TimeIn] [datetime2](7) NULL,
	[TimeOut] [datetime2](7) NULL,
	[Destination] [int] NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_TrackingRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Addresses_StreetAddress_City_State_Zipcode]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Addresses_StreetAddress_City_State_Zipcode] ON [dbo].[Addresses]
(
	[StreetAddress] ASC,
	[City] ASC,
	[State] ASC,
	[Zipcode] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Customers_AddressID]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_Customers_AddressID] ON [dbo].[Customers]
(
	[AddressID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Customers_Email]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Customers_Email] ON [dbo].[Customers]
(
	[Email] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Employees_AddressID]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_Employees_AddressID] ON [dbo].[Employees]
(
	[AddressID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Employees_Email]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Employees_Email] ON [dbo].[Employees]
(
	[Email] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Employees_PhoneNumber]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Employees_PhoneNumber] ON [dbo].[Employees]
(
	[PhoneNumber] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Employees_StoreID]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_Employees_StoreID] ON [dbo].[Employees]
(
	[StoreID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Packages_AddressID]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_Packages_AddressID] ON [dbo].[Packages]
(
	[AddressID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Packages_ClassID]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_Packages_ClassID] ON [dbo].[Packages]
(
	[ClassID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Packages_ReceiverID]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_Packages_ReceiverID] ON [dbo].[Packages]
(
	[ReceiverID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Packages_SenderID]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_Packages_SenderID] ON [dbo].[Packages]
(
	[SenderID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Sales_BuyerID]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_Sales_BuyerID] ON [dbo].[Sales]
(
	[BuyerID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Sales_ProductID]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_Sales_ProductID] ON [dbo].[Sales]
(
	[ProductID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Stores_AddressID]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Stores_AddressID] ON [dbo].[Stores]
(
	[AddressID] ASC
)
WHERE ([AddressID] IS NOT NULL)
WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Stores_SupID]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_Stores_SupID] ON [dbo].[Stores]
(
	[SupID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TrackingRecords_Destination]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_TrackingRecords_Destination] ON [dbo].[TrackingRecords]
(
	[Destination] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TrackingRecords_EmployeeId]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_TrackingRecords_EmployeeId] ON [dbo].[TrackingRecords]
(
	[EmployeeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TrackingRecords_StoreId]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_TrackingRecords_StoreId] ON [dbo].[TrackingRecords]
(
	[StoreId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TrackingRecords_TrackNum]    Script Date: 4/25/2022 7:44:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_TrackingRecords_TrackNum] ON [dbo].[TrackingRecords]
(
	[TrackNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Addresses] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Deleted]
GO
ALTER TABLE [dbo].[Customers] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Deleted]
GO
ALTER TABLE [dbo].[Employees] ADD  DEFAULT ((2)) FOR [Role]
GO
ALTER TABLE [dbo].[Employees] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Deleted]
GO
ALTER TABLE [dbo].[Packages] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Deleted]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Deleted]
GO
ALTER TABLE [dbo].[Sales] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Deleted]
GO
ALTER TABLE [dbo].[ShipmentClasses] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Deleted]
GO
ALTER TABLE [dbo].[Stores] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Deleted]
GO
ALTER TABLE [dbo].[TrackingRecords] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Deleted]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Addresses_AddressID] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Addresses] ([Id])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Addresses_AddressID]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Addresses_AddressID] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Addresses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Addresses_AddressID]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Stores_StoreID] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Stores] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Stores_StoreID]
GO
ALTER TABLE [dbo].[Packages]  WITH CHECK ADD  CONSTRAINT [FK_Packages_Addresses_AddressID] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Addresses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Packages] CHECK CONSTRAINT [FK_Packages_Addresses_AddressID]
GO
ALTER TABLE [dbo].[Packages]  WITH CHECK ADD  CONSTRAINT [FK_Packages_Customers_ReceiverID] FOREIGN KEY([ReceiverID])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Packages] CHECK CONSTRAINT [FK_Packages_Customers_ReceiverID]
GO
ALTER TABLE [dbo].[Packages]  WITH CHECK ADD  CONSTRAINT [FK_Packages_Customers_SenderID] FOREIGN KEY([SenderID])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Packages] CHECK CONSTRAINT [FK_Packages_Customers_SenderID]
GO
ALTER TABLE [dbo].[Packages]  WITH CHECK ADD  CONSTRAINT [FK_Packages_ShipmentClasses_ClassID] FOREIGN KEY([ClassID])
REFERENCES [dbo].[ShipmentClasses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Packages] CHECK CONSTRAINT [FK_Packages_ShipmentClasses_ClassID]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Customers_BuyerID] FOREIGN KEY([BuyerID])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Customers_BuyerID]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Products_ProductID] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Products_ProductID]
GO
ALTER TABLE [dbo].[Stores]  WITH CHECK ADD  CONSTRAINT [FK_Stores_Addresses_AddressID] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Addresses] ([Id])
GO
ALTER TABLE [dbo].[Stores] CHECK CONSTRAINT [FK_Stores_Addresses_AddressID]
GO
ALTER TABLE [dbo].[Stores]  WITH CHECK ADD  CONSTRAINT [FK_Stores_Employees_SupID] FOREIGN KEY([SupID])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[Stores] CHECK CONSTRAINT [FK_Stores_Employees_SupID]
GO
ALTER TABLE [dbo].[TrackingRecords]  WITH CHECK ADD  CONSTRAINT [FK_TrackingRecords_Addresses_Destination] FOREIGN KEY([Destination])
REFERENCES [dbo].[Addresses] ([Id])
GO
ALTER TABLE [dbo].[TrackingRecords] CHECK CONSTRAINT [FK_TrackingRecords_Addresses_Destination]
GO
ALTER TABLE [dbo].[TrackingRecords]  WITH CHECK ADD  CONSTRAINT [FK_TrackingRecords_Employees_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[TrackingRecords] CHECK CONSTRAINT [FK_TrackingRecords_Employees_EmployeeId]
GO
ALTER TABLE [dbo].[TrackingRecords]  WITH CHECK ADD  CONSTRAINT [FK_TrackingRecords_Packages_TrackNum] FOREIGN KEY([TrackNum])
REFERENCES [dbo].[Packages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrackingRecords] CHECK CONSTRAINT [FK_TrackingRecords_Packages_TrackNum]
GO
ALTER TABLE [dbo].[TrackingRecords]  WITH CHECK ADD  CONSTRAINT [FK_TrackingRecords_Stores_StoreId] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Stores] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrackingRecords] CHECK CONSTRAINT [FK_TrackingRecords_Stores_StoreId]
GO
ALTER DATABASE [aspnet-uhps] SET  READ_WRITE 
GO
