CREATE TABLE [dbo].[Resources]
(
[ResourceId] [int] NOT NULL IDENTITY(1, 1),
[ClassName] [nvarchar] (80) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ResourceKey] [nvarchar] (80) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ResourceValue] [nvarchar] (2000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CultureInfo] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)

GO
