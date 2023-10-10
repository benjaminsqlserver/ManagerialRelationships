﻿USE [master]
GO
/****** Object:  Database [EmployeesDB]    Script Date: 10/10/2023 03:42:54 ******/
CREATE DATABASE [EmployeesDB]
 
GO
ALTER DATABASE [EmployeesDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EmployeesDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EmployeesDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EmployeesDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EmployeesDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EmployeesDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EmployeesDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [EmployeesDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EmployeesDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EmployeesDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EmployeesDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EmployeesDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EmployeesDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EmployeesDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EmployeesDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EmployeesDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EmployeesDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EmployeesDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EmployeesDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EmployeesDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EmployeesDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EmployeesDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EmployeesDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EmployeesDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EmployeesDB] SET RECOVERY FULL 
GO
ALTER DATABASE [EmployeesDB] SET  MULTI_USER 
GO
ALTER DATABASE [EmployeesDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EmployeesDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EmployeesDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EmployeesDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EmployeesDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EmployeesDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'EmployeesDB', N'ON'
GO
ALTER DATABASE [EmployeesDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [EmployeesDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [EmployeesDB]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 10/10/2023 03:42:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 10/10/2023 03:42:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeID] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[ManagerID] [bigint] NULL,
	[EmployeePhoto] [nvarchar](max) NULL,
	[GenderID] [int] NOT NULL,
	[DepartmentID] [int] NOT NULL,
 CONSTRAINT [PK__Employee__7AD04FF19D75CB9F] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genders]    Script Date: 10/10/2023 03:42:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genders](
	[GenderID] [int] IDENTITY(1,1) NOT NULL,
	[GenderName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[GenderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Departments] ON 
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (22, N'Business Development')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (29, N'Compliance')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (21, N'Content Management')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (25, N'Corporate Communications')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (30, N'Corporate Social Responsibility')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (17, N'Customer Experience')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (42, N'Customer Insights')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (6, N'Customer Service')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (44, N'Customer Success')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (14, N'Data Analytics')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (40, N'Data Science')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (16, N'Design')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (48, N'Digital Marketing')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (41, N'E-commerce')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (13, N'Engineering')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (26, N'Environmental Sustainability')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (31, N'Facilities Management')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (4, N'Finance')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (39, N'Financial Planning and Analysis')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (24, N'Health and Safety')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (1, N'Human Resources')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (34, N'Information Security')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (2, N'Information Technology')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (27, N'Internal Audit')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (46, N'Inventory Management')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (12, N'Legal')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (43, N'Legal Affairs')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (18, N'Logistics')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (38, N'Market Research')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (3, N'Marketing')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (5, N'Operations')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (20, N'Procurement')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (36, N'Product Design')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (10, N'Product Management')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (45, N'Public Affairs')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (15, N'Public Relations')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (11, N'Quality Assurance')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (7, N'Research and Development')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (35, N'Risk Management')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (9, N'Sales')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (23, N'Security')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (33, N'Strategic Planning')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (8, N'Supply Chain')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (32, N'Training and Development')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (47, N'Transportation')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (37, N'User Experience')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (28, N'Vendor Management')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (19, N'Warehousing')
GO
SET IDENTITY_INSERT [dbo].[Departments] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [MiddleName], [LastName], [ManagerID], [EmployeePhoto], [GenderID], [DepartmentID]) VALUES (1, N'John', N'Michael', N'Smith', NULL, N'JohnPhoto.jpg', 1, 1)
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [MiddleName], [LastName], [ManagerID], [EmployeePhoto], [GenderID], [DepartmentID]) VALUES (2, N'Jane', N'Elizabeth', N'Doe', 1, N'JanePhoto.jpg', 2, 2)
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [MiddleName], [LastName], [ManagerID], [EmployeePhoto], [GenderID], [DepartmentID]) VALUES (3, N'Robert', N'Lee', N'Johnson', 1, N'RobertPhoto.jpg', 1, 3)
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [MiddleName], [LastName], [ManagerID], [EmployeePhoto], [GenderID], [DepartmentID]) VALUES (4, N'Emily', N'Grace', N'Brown', 2, N'EmilyPhoto.jpg', 2, 4)
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [MiddleName], [LastName], [ManagerID], [EmployeePhoto], [GenderID], [DepartmentID]) VALUES (5, N'William', N'James', N'Wilson', 2, N'WilliamPhoto.jpg', 1, 5)
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [MiddleName], [LastName], [ManagerID], [EmployeePhoto], [GenderID], [DepartmentID]) VALUES (6, N'Alice', N'Marie', N'Davis', 3, N'AlicePhoto.jpg', 2, 6)
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [MiddleName], [LastName], [ManagerID], [EmployeePhoto], [GenderID], [DepartmentID]) VALUES (7, N'Daniel', N'Joseph', N'White', 3, N'DanielPhoto.jpg', 1, 7)
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [MiddleName], [LastName], [ManagerID], [EmployeePhoto], [GenderID], [DepartmentID]) VALUES (8, N'Olivia', N'Sophia', N'Miller', 4, N'OliviaPhoto.jpg', 2, 8)
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [MiddleName], [LastName], [ManagerID], [EmployeePhoto], [GenderID], [DepartmentID]) VALUES (9, N'David', N'Benjamin', N'Garcia', 4, N'DavidPhoto.jpg', 1, 9)
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [MiddleName], [LastName], [ManagerID], [EmployeePhoto], [GenderID], [DepartmentID]) VALUES (10, N'Sophia', N'Ava', N'Jones', 5, N'SophiaPhoto.jpg', 2, 10)
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [MiddleName], [LastName], [ManagerID], [EmployeePhoto], [GenderID], [DepartmentID]) VALUES (11, N'John', NULL, N'Bosco', 2, N'gggg', 1, 14)
GO
INSERT [dbo].[Employees] ([EmployeeID], [FirstName], [MiddleName], [LastName], [ManagerID], [EmployeePhoto], [GenderID], [DepartmentID]) VALUES (10011, N'Roger', NULL, N'Bacon', 9, NULL, 1, 42)
GO
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Genders] ON 
GO
INSERT [dbo].[Genders] ([GenderID], [GenderName]) VALUES (2, N'Female')
GO
INSERT [dbo].[Genders] ([GenderID], [GenderName]) VALUES (1, N'Male')
GO
SET IDENTITY_INSERT [dbo].[Genders] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Departme__D949CC34189DE503]    Script Date: 10/10/2023 03:42:54 ******/
ALTER TABLE [dbo].[Departments] ADD UNIQUE NONCLUSTERED 
(
	[DepartmentName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Genders__F7C177158FB72DF3]    Script Date: 10/10/2023 03:42:54 ******/
ALTER TABLE [dbo].[Genders] ADD UNIQUE NONCLUSTERED 
(
	[GenderName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Departments] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Departments] ([DepartmentID])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Departments]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Employees] FOREIGN KEY([ManagerID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Employees]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Genders] FOREIGN KEY([GenderID])
REFERENCES [dbo].[Genders] ([GenderID])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Genders]
GO
USE [master]
GO
ALTER DATABASE [EmployeesDB] SET  READ_WRITE 
GO
