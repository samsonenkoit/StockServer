CREATE TABLE [dbo].[Offer](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Title] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](19, 2) NOT NULL,
	[IsActive] [bit] NOT NULL DEFAULT ((1)),
	[PlaceId] INT NOT NULL,
	[LogoUrl] [nvarchar](max) NOT NULL,

	CONSTRAINT FK_Offer_PlaceId FOREIGN KEY ([PlaceId]) REFERENCES Place(Id)
	)