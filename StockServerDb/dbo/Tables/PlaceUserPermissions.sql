CREATE TABLE [dbo].[PlaceUserPermissions]
(
	[UserId] NVARCHAR (450)     NOT NULL,
	[PlaceId] INT NOT NULL,

	CONSTRAINT PK_PlaceUserPermissions PRIMARY KEY (UserId, PlaceId),
	CONSTRAINT FK_PlaceUserPermissions_UserId FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id),
	CONSTRAINT FK_PlaceUserPermissions_PlaceId FOREIGN KEY (PlaceId) REFERENCES Place(Id) 
)
