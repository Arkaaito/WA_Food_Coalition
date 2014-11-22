USE [SQL2012_935745_wfc]
GO

/****** Object:  Table [dbo].[Donors]    Script Date: 10/4/2014 2:21:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Donors] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [DeviceId]  NVARCHAR (40)  NOT NULL,
    [FirstName] NVARCHAR (50)  NULL,
    [LastName]  NVARCHAR (50)  NULL,
    [Phone]     NCHAR (10)     NULL,
    [Email]     NVARCHAR (250) NULL,
    [OptIn]     BIT            NULL,
    [UserId] NVARCHAR(128) NOT NULL, 
    CONSTRAINT [PK_Donors] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Donors_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers]([Id])
);

GO

