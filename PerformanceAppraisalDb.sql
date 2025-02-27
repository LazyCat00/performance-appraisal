USE [master]
GO
/****** Object:  Database [PerformanceAppraisalDb]    Script Date: 2025/2/24 14:16:02 ******/
CREATE DATABASE [PerformanceAppraisalDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PerformanceAppraisalDb', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\PerformanceAppraisalDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PerformanceAppraisalDb_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\PerformanceAppraisalDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PerformanceAppraisalDb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PerformanceAppraisalDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PerformanceAppraisalDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET RECOVERY FULL 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET  MULTI_USER 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PerformanceAppraisalDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PerformanceAppraisalDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PerformanceAppraisalDb', N'ON'
GO
ALTER DATABASE [PerformanceAppraisalDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [PerformanceAppraisalDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [PerformanceAppraisalDb]
GO
/****** Object:  Table [dbo].[AppraisalBases]    Script Date: 2025/2/24 14:16:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppraisalBases](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BaseType] [nvarchar](255) NULL,
	[AppraisalBase] [int] NULL,
	[IsDel] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppraisalCoefficients]    Script Date: 2025/2/24 14:16:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppraisalCoefficients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AppraisalType] [nvarchar](255) NULL,
	[AppraisalCoefficient] [decimal](18, 2) NULL,
	[CalculationMethod] [nvarchar](255) NULL,
	[IsDel] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAppraisals]    Script Date: 2025/2/24 14:16:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAppraisals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CoefficientId] [int] NOT NULL,
	[Count] [int] NULL,
	[AssessmentYear] [int] NULL,
	[IsDel] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2025/2/24 14:16:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[Sex] [nvarchar](10) NULL,
	[Password] [nvarchar](255) NOT NULL,
	[BaseTypeId] [int] NULL,
	[IsDel] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AppraisalBases] ON 

INSERT [dbo].[AppraisalBases] ([Id], [BaseType], [AppraisalBase], [IsDel]) VALUES (1, N'政法编制', 20000, 0)
INSERT [dbo].[AppraisalBases] ([Id], [BaseType], [AppraisalBase], [IsDel]) VALUES (2, N'行政编制', 18000, 0)
INSERT [dbo].[AppraisalBases] ([Id], [BaseType], [AppraisalBase], [IsDel]) VALUES (3, N'事业编制', 18000, 0)
INSERT [dbo].[AppraisalBases] ([Id], [BaseType], [AppraisalBase], [IsDel]) VALUES (4, N'社会化用工', 8000, 0)
SET IDENTITY_INSERT [dbo].[AppraisalBases] OFF
GO
SET IDENTITY_INSERT [dbo].[AppraisalCoefficients] ON 

INSERT [dbo].[AppraisalCoefficients] ([Id], [AppraisalType], [AppraisalCoefficient], [CalculationMethod], [IsDel]) VALUES (1, N'请假', CAST(0.10 AS Decimal(18, 2)), N'-1', 0)
INSERT [dbo].[AppraisalCoefficients] ([Id], [AppraisalType], [AppraisalCoefficient], [CalculationMethod], [IsDel]) VALUES (2, N'迟到', CAST(0.05 AS Decimal(18, 2)), N'-1', 0)
INSERT [dbo].[AppraisalCoefficients] ([Id], [AppraisalType], [AppraisalCoefficient], [CalculationMethod], [IsDel]) VALUES (3, N'加班', CAST(0.10 AS Decimal(18, 2)), N'1', 0)
INSERT [dbo].[AppraisalCoefficients] ([Id], [AppraisalType], [AppraisalCoefficient], [CalculationMethod], [IsDel]) VALUES (4, N'项目开发', CAST(0.30 AS Decimal(18, 2)), N'1', 0)
SET IDENTITY_INSERT [dbo].[AppraisalCoefficients] OFF
GO
SET IDENTITY_INSERT [dbo].[UserAppraisals] ON 

INSERT [dbo].[UserAppraisals] ([Id], [UserId], [CoefficientId], [Count], [AssessmentYear], [IsDel]) VALUES (5, 1, 1, 5, 2018, 0)
INSERT [dbo].[UserAppraisals] ([Id], [UserId], [CoefficientId], [Count], [AssessmentYear], [IsDel]) VALUES (6, 1, 2, 1, 2018, 0)
INSERT [dbo].[UserAppraisals] ([Id], [UserId], [CoefficientId], [Count], [AssessmentYear], [IsDel]) VALUES (7, 1, 3, 20, 2018, 0)
INSERT [dbo].[UserAppraisals] ([Id], [UserId], [CoefficientId], [Count], [AssessmentYear], [IsDel]) VALUES (8, 1, 4, 8, 2018, 0)
INSERT [dbo].[UserAppraisals] ([Id], [UserId], [CoefficientId], [Count], [AssessmentYear], [IsDel]) VALUES (9, 2, 1, 7, 2018, 0)
INSERT [dbo].[UserAppraisals] ([Id], [UserId], [CoefficientId], [Count], [AssessmentYear], [IsDel]) VALUES (10, 3, 2, 3, 2018, 0)
SET IDENTITY_INSERT [dbo].[UserAppraisals] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [UserName], [Sex], [Password], [BaseTypeId], [IsDel]) VALUES (1, N'张三', N'男', N'111', 1, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Sex], [Password], [BaseTypeId], [IsDel]) VALUES (2, N'李四', N'男', N'111', 2, 0)
INSERT [dbo].[Users] ([Id], [UserName], [Sex], [Password], [BaseTypeId], [IsDel]) VALUES (3, N'王五', N'男', N'111', 3, 0)
INSERT [dbo].[Users] ([Id], [UserName], [Sex], [Password], [BaseTypeId], [IsDel]) VALUES (5, N'test', N'男', N'123456', 4, 0)
INSERT [dbo].[Users] ([Id], [UserName], [Sex], [Password], [BaseTypeId], [IsDel]) VALUES (6, N'1ost', N'男', N'123456', 1, 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[AppraisalBases] ADD  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [dbo].[AppraisalCoefficients] ADD  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [dbo].[UserAppraisals] ADD  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [dbo].[UserAppraisals]  WITH CHECK ADD  CONSTRAINT [FK_UserAppraisals_Coefficients] FOREIGN KEY([CoefficientId])
REFERENCES [dbo].[AppraisalCoefficients] ([Id])
GO
ALTER TABLE [dbo].[UserAppraisals] CHECK CONSTRAINT [FK_UserAppraisals_Coefficients]
GO
ALTER TABLE [dbo].[UserAppraisals]  WITH CHECK ADD  CONSTRAINT [FK_UserAppraisals_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserAppraisals] CHECK CONSTRAINT [FK_UserAppraisals_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_AppraisalBases] FOREIGN KEY([BaseTypeId])
REFERENCES [dbo].[AppraisalBases] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_AppraisalBases]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [CK_Sex] CHECK  (([Sex]='女' OR [Sex]='男'))
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [CK_Sex]
GO
USE [master]
GO
ALTER DATABASE [PerformanceAppraisalDb] SET  READ_WRITE 
GO
