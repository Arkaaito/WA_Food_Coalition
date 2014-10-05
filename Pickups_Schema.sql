USE [SQL2012_935745_wfc]
GO

/****** Object:  Table [dbo].[Pickups]    Script Date: 10/4/2014 2:21:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pickups](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DonorId] [int] NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[City] [nvarchar](max) NOT NULL,
	[Zip] [nvarchar](max) NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[Items] [nvarchar](max) NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[Schedule] [datetime] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Pickups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Pickups]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Pickups_dbo.Donors_DonorId] FOREIGN KEY([DonorId])
REFERENCES [dbo].[Donors] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Pickups] CHECK CONSTRAINT [FK_dbo.Pickups_dbo.Donors_DonorId]
GO

