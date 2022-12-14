USE [master]
GO
/****** Object:  Database [HeleniumDb]    Script Date: 19/11/2022 16:39:20 ******/
CREATE DATABASE [HeleniumDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProductsDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ProductsDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProductsDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ProductsDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [HeleniumDb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HeleniumDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HeleniumDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HeleniumDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HeleniumDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HeleniumDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HeleniumDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [HeleniumDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [HeleniumDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HeleniumDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HeleniumDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HeleniumDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HeleniumDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HeleniumDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HeleniumDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HeleniumDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HeleniumDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [HeleniumDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HeleniumDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HeleniumDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HeleniumDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HeleniumDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HeleniumDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HeleniumDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HeleniumDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HeleniumDb] SET  MULTI_USER 
GO
ALTER DATABASE [HeleniumDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HeleniumDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HeleniumDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HeleniumDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HeleniumDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HeleniumDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [HeleniumDb] SET QUERY_STORE = OFF
GO
USE [HeleniumDb]
GO
/****** Object:  Table [dbo].[PaymentMethods]    Script Date: 19/11/2022 16:39:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethods](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[methodName] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 19/11/2022 16:39:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Product_Id] [int] NOT NULL,
	[Product_Name] [nvarchar](50) NOT NULL,
	[Product_Category] [nvarchar](50) NOT NULL,
	[Product_Quantity] [int] NOT NULL,
	[Product_Price] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Product_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products_Categories]    Script Date: 19/11/2022 16:39:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products_Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Request]    Script Date: 19/11/2022 16:39:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Request](
	[RequestId] [int] NOT NULL,
	[RequestName] [nvarchar](50) NULL,
	[RequestQuantity] [int] NULL,
	[RequestPrice] [decimal](10, 2) NULL,
	[RequestTotal]  AS ([RequestQuantity]*[RequestPrice])
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockControl]    Script Date: 19/11/2022 16:39:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockControl](
	[id] [int] NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[StartQuantity] [int] NOT NULL,
	[EndQuantity] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 19/11/2022 16:39:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userName] [nvarchar](50) NOT NULL,
	[totalSellings] [int] NULL,
	[cancelledSellings] [int] NULL,
	[pixSelling] [decimal](10, 2) NULL,
	[debitSelling] [decimal](10, 2) NULL,
	[creditSelling] [decimal](10, 2) NULL,
	[moneySelling] [decimal](10, 2) NULL,
	[valueSold]  AS ((([pixSelling]+[debitSelling])+[creditSelling])+[moneySelling]),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersCredentials]    Script Date: 19/11/2022 16:39:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersCredentials](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[PasswordHash] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[PaymentMethods] ON 

INSERT [dbo].[PaymentMethods] ([id], [methodName]) VALUES (1, N'Credit')
INSERT [dbo].[PaymentMethods] ([id], [methodName]) VALUES (2, N'Debit')
INSERT [dbo].[PaymentMethods] ([id], [methodName]) VALUES (3, N'Money')
INSERT [dbo].[PaymentMethods] ([id], [methodName]) VALUES (4, N'Pix')
SET IDENTITY_INSERT [dbo].[PaymentMethods] OFF
GO
INSERT [dbo].[Products] ([Product_Id], [Product_Name], [Product_Category], [Product_Quantity], [Product_Price]) VALUES (11211, N'Mamão', N'Frutas', 12, CAST(12.00 AS Decimal(10, 2)))
INSERT [dbo].[Products] ([Product_Id], [Product_Name], [Product_Category], [Product_Quantity], [Product_Price]) VALUES (223322, N'Agua Sanitaria', N'Utilidades Domésticas', 2, CAST(22.00 AS Decimal(10, 2)))
INSERT [dbo].[Products] ([Product_Id], [Product_Name], [Product_Category], [Product_Quantity], [Product_Price]) VALUES (324498, N'Pipos Vitaminado 50g', N'Salgadinhos', 14, CAST(5.50 AS Decimal(10, 2)))
INSERT [dbo].[Products] ([Product_Id], [Product_Name], [Product_Category], [Product_Quantity], [Product_Price]) VALUES (656678, N'Banana Und', N'Frutas', 15, CAST(2.75 AS Decimal(10, 2)))
INSERT [dbo].[Products] ([Product_Id], [Product_Name], [Product_Category], [Product_Quantity], [Product_Price]) VALUES (912820, N'Ruffles 50g', N'Salgadinhos', 15, CAST(5.00 AS Decimal(10, 2)))
INSERT [dbo].[Products] ([Product_Id], [Product_Name], [Product_Category], [Product_Quantity], [Product_Price]) VALUES (1550669, N'Vinho Suave 1L', N'Bebidas', 10, CAST(12.00 AS Decimal(10, 2)))
INSERT [dbo].[Products] ([Product_Id], [Product_Name], [Product_Category], [Product_Quantity], [Product_Price]) VALUES (4923895, N'copo', N'Utilidades Domésticas', 11, CAST(30.00 AS Decimal(10, 2)))
INSERT [dbo].[Products] ([Product_Id], [Product_Name], [Product_Category], [Product_Quantity], [Product_Price]) VALUES (9990099, N'Pipos Presunto 30g', N'Salgadinhos', 12, CAST(3.50 AS Decimal(10, 2)))
INSERT [dbo].[Products] ([Product_Id], [Product_Name], [Product_Category], [Product_Quantity], [Product_Price]) VALUES (42399844, N'Desodorante Suave', N'Utilidades Domésticas', 15, CAST(12.00 AS Decimal(10, 2)))
INSERT [dbo].[Products] ([Product_Id], [Product_Name], [Product_Category], [Product_Quantity], [Product_Price]) VALUES (43242378, N'Trembolona 150ml', N'Bebidas', 10, CAST(199.99 AS Decimal(10, 2)))
INSERT [dbo].[Products] ([Product_Id], [Product_Name], [Product_Category], [Product_Quantity], [Product_Price]) VALUES (59399854, N'tramontina Faca', N'Utilidades Domésticas', 13, CAST(12.50 AS Decimal(10, 2)))
INSERT [dbo].[Products] ([Product_Id], [Product_Name], [Product_Category], [Product_Quantity], [Product_Price]) VALUES (66969789, N'trakinas 25g', N'Biscoitos', 10, CAST(3.50 AS Decimal(10, 2)))
INSERT [dbo].[Products] ([Product_Id], [Product_Name], [Product_Category], [Product_Quantity], [Product_Price]) VALUES (95959595, N'Laranja Cravo', N'Frutas', 12, CAST(5.00 AS Decimal(10, 2)))
INSERT [dbo].[Products] ([Product_Id], [Product_Name], [Product_Category], [Product_Quantity], [Product_Price]) VALUES (556789900, N'Sensações 45g', N'Salgadinhos', 7, CAST(6.50 AS Decimal(10, 2)))
INSERT [dbo].[Products] ([Product_Id], [Product_Name], [Product_Category], [Product_Quantity], [Product_Price]) VALUES (982191897, N'Treloso 25g', N'Biscoitos', 12, CAST(3.50 AS Decimal(10, 2)))
GO
SET IDENTITY_INSERT [dbo].[Products_Categories] ON 

INSERT [dbo].[Products_Categories] ([CategoryId], [CategoryName]) VALUES (4, N'Bebidas')
INSERT [dbo].[Products_Categories] ([CategoryId], [CategoryName]) VALUES (2, N'Biscoitos')
INSERT [dbo].[Products_Categories] ([CategoryId], [CategoryName]) VALUES (1, N'Frutas')
INSERT [dbo].[Products_Categories] ([CategoryId], [CategoryName]) VALUES (3, N'Salgadinhos')
INSERT [dbo].[Products_Categories] ([CategoryId], [CategoryName]) VALUES (5, N'Utilidades Domésticas')
SET IDENTITY_INSERT [dbo].[Products_Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([id], [userName], [totalSellings], [cancelledSellings], [pixSelling], [debitSelling], [creditSelling], [moneySelling]) VALUES (1, N'Fellips', 0, 0, CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Users] ([id], [userName], [totalSellings], [cancelledSellings], [pixSelling], [debitSelling], [creditSelling], [moneySelling]) VALUES (2, N'Alanis', 0, 0, CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[UsersCredentials] ON 

INSERT [dbo].[UsersCredentials] ([UserID], [Username], [PasswordHash]) VALUES (1, N'Fellips', N'E10ADC3949BA59ABBE56E057F20F883E')
INSERT [dbo].[UsersCredentials] ([UserID], [Username], [PasswordHash]) VALUES (2, N'Alanis', N'3FE4C917F62E537E040EB6424EF3D304')
SET IDENTITY_INSERT [dbo].[UsersCredentials] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Products__8517B2E036C52CAB]    Script Date: 19/11/2022 16:39:21 ******/
ALTER TABLE [dbo].[Products_Categories] ADD UNIQUE NONCLUSTERED 
(
	[CategoryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([Product_Category])
REFERENCES [dbo].[Products_Categories] ([CategoryName])
GO
ALTER TABLE [dbo].[Request]  WITH CHECK ADD FOREIGN KEY([RequestId])
REFERENCES [dbo].[Products] ([Product_Id])
GO
USE [master]
GO
ALTER DATABASE [HeleniumDb] SET  READ_WRITE 
GO
