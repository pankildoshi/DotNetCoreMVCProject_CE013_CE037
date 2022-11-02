USE [stationary]
GO

/****** Object: Table [dbo].[Products] Script Date: 27-10-2022 01:00:28 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Products] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (MAX) NOT NULL,
    [Price] INT            NOT NULL,
	[Quantity] INt NOT NULL
);


