

CREATE TABLE [dbo].[Place](
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](512) NOT NULL,
	[Address] [nvarchar](512) NULL,
	[Contact] [nvarchar](max) NULL,
	[LogoUrl] [nvarchar](max) NULL,
	[ViewUrl] [nvarchar](max) NULL,
	[GeoPoint] [geography] NOT NULL
)

GO



