USE [SQL2012_935745_wfc]
GO

/****** Object:  Table [dbo].[Members]    Script Date: 10/4/2014 2:21:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Members] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (100) NOT NULL,
    [ContactFirstName] NVARCHAR (50)  NULL,
    [ContactLastName]  NVARCHAR (50)  NULL,
    [Email]            NVARCHAR (250) NULL,
    [Address]          NVARCHAR (500) NOT NULL,
    [City]             NVARCHAR (50)  NOT NULL,
    [Zip]              NCHAR (5)      NOT NULL,
    [RangeInMeters]    INT            NOT NULL,
    [Phone]            NCHAR (10)     NOT NULL,
    [Latitude]         FLOAT (53)     NULL,
    [Longitude]        FLOAT (53)     NULL,
    [EIN]              NCHAR (10)     NULL,
    [UserId] NVARCHAR(128) NULL, 
    CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Members_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers]([Id])
);
GO

