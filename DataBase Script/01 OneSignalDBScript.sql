USE [master]
GO
/****** Object:  Database [OneSignal]    Script Date: 10/19/2020 1:48:54 AM ******/
CREATE DATABASE [OneSignal]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OneSignal', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\OneSignal.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OneSignal_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\OneSignal_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [OneSignal] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OneSignal].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OneSignal] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OneSignal] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OneSignal] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OneSignal] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OneSignal] SET ARITHABORT OFF 
GO
ALTER DATABASE [OneSignal] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OneSignal] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OneSignal] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OneSignal] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OneSignal] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OneSignal] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OneSignal] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OneSignal] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OneSignal] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OneSignal] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OneSignal] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OneSignal] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OneSignal] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OneSignal] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OneSignal] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OneSignal] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OneSignal] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OneSignal] SET RECOVERY FULL 
GO
ALTER DATABASE [OneSignal] SET  MULTI_USER 
GO
ALTER DATABASE [OneSignal] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OneSignal] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OneSignal] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OneSignal] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OneSignal] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'OneSignal', N'ON'
GO
ALTER DATABASE [OneSignal] SET QUERY_STORE = OFF
GO
USE [OneSignal]
GO
/****** Object:  Table [dbo].[App]    Script Date: 10/19/2020 1:48:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[App](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Players] [int] NULL,
	[Messageable_players] [int] NULL,
	[Updated_at] [datetime] NULL,
	[Created_at] [datetime] NULL,
	[Gcm_key] [nvarchar](255) NULL,
	[Chrome_key] [nvarchar](255) NULL,
	[Chrome_web_origin] [nvarchar](255) NULL,
	[Chrome_web_gcm_sender_id] [nvarchar](255) NULL,
	[Chrome_web_default_notification_icon] [nvarchar](255) NULL,
	[Chrome_web_sub_domain] [nvarchar](255) NULL,
	[Apns_env] [nvarchar](255) NULL,
	[Apns_certificates] [nvarchar](255) NULL,
	[Safari_apns_certificate] [nvarchar](255) NULL,
	[Safari_site_origin] [nvarchar](255) NULL,
	[Safari_push_id] [nvarchar](255) NULL,
	[Safari_icon_16_16] [nvarchar](255) NULL,
	[Safari_icon_32_32] [nvarchar](255) NULL,
	[Safari_icon_64_64] [nvarchar](255) NULL,
	[Safari_icon_128_128] [nvarchar](255) NULL,
	[Safari_icon_256_256] [nvarchar](255) NULL,
	[Site_name] [nvarchar](255) NULL,
	[Basic_auth_key] [text] NULL,
	[IsActive] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[App] ([Id], [Name], [Players], [Messageable_players], [Updated_at], [Created_at], [Gcm_key], [Chrome_key], [Chrome_web_origin], [Chrome_web_gcm_sender_id], [Chrome_web_default_notification_icon], [Chrome_web_sub_domain], [Apns_env], [Apns_certificates], [Safari_apns_certificate], [Safari_site_origin], [Safari_push_id], [Safari_icon_16_16], [Safari_icon_32_32], [Safari_icon_64_64], [Safari_icon_128_128], [Safari_icon_256_256], [Site_name], [Basic_auth_key], [IsActive]) VALUES (N'92911750-242d-4260-9e00-9d9034f139ce', N'Your app 1', 150, 143, CAST(N'2014-04-01T00:00:00.000' AS DateTime), CAST(N'2014-04-01T08:20:02.003' AS DateTime), N'a gcm push key', N'A Chrome Web Push GCM key', N'Chrome Web Push Site URL', N'Chrome Web Push GCM Sender ID', N'http://yoursite.com/chrome_notification_icon', N'your_site_name', N'sandbox', N'Your apns certificate', N'Your Safari APNS certificate', N'The homename for your website for Safari Push, including http or https', N'The certificate bundle ID for Safari Web Push', N'http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/16x16.png', N'http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/16x16@2.png', N'http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/32x32@2x.png', N'http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/128x128.png', N'http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/128x128@2x.png', N'The URL to your website for Web Push', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJPbmVTaWduYWxBcGktQWNjZXNzVG9rZW4iLCJqdGkiOiJmOTAyZTdkYi1iMGU4LTRmYTItOGYwNC1jYWM2OTFmOTVmYmYiLCJpYXQiOiIxMC8xOC8yMDIwIDg6MTc6MjUgUE0iLCJJZCI6IjkyOTExNzUwLTI0MmQtNDI2MC05ZTAwLTlkOTAzNGYxMzljZSIsIkFwcE5hbWUiOiJZb3VyIGFwcCAxIiwiZXhwIjoxNzYwODE4NjQ1LCJpc3MiOiJPbmVTaWduYWxBcGkiLCJhdWQiOiJPbmVTaWduYWxBcGkifQ.v0_GfBIJx71im64PLx56yIjtNcbxEyCA4a2_Ns9eyJg', 1)
INSERT [dbo].[App] ([Id], [Name], [Players], [Messageable_players], [Updated_at], [Created_at], [Gcm_key], [Chrome_key], [Chrome_web_origin], [Chrome_web_gcm_sender_id], [Chrome_web_default_notification_icon], [Chrome_web_sub_domain], [Apns_env], [Apns_certificates], [Safari_apns_certificate], [Safari_site_origin], [Safari_push_id], [Safari_icon_16_16], [Safari_icon_32_32], [Safari_icon_64_64], [Safari_icon_128_128], [Safari_icon_256_256], [Site_name], [Basic_auth_key], [IsActive]) VALUES (N'e4e87830-b954-11e3-811d-f3b376925f15', N'Your app 2', 100, 80, CAST(N'2014-04-01T08:20:02.003' AS DateTime), CAST(N'2014-04-01T08:20:02.003' AS DateTime), N'a gcm push key', N'A Chrome Web Push GCM key', N'Chrome Web Push Site URL', N'Chrome Web Push GCM Sender ID', N'http://yoursite.com/chrome_notification_icon', N'your_site_name', N'production', N'Your apns certificate', N'Your Safari APNS certificate', N'The homename for your website for Safari Push, including http or https', N'The certificate bundle ID for Safari Web Push', N'http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/16x16.png', N'http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/16x16@2.png', N'http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/32x32@2x.png', N'http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/128x128.png', N'http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/128x128@2x.png', N'The URL to your website for Web Push', NULL, 1)
GO
ALTER TABLE [dbo].[App] ADD  CONSTRAINT [DF_App_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
USE [master]
GO
ALTER DATABASE [OneSignal] SET  READ_WRITE 
GO
