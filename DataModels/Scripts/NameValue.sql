CREATE TABLE [dbo].[EntityValue] (
	[EntityId] [int] IDENTITY (1, 1) NOT NULL ,
	[EntityName] [nvarchar] (2000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[EntityValue] [nvarchar] (2000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

