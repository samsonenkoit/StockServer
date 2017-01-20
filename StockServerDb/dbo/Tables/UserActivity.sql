CREATE TABLE [dbo].[UserActivity]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UserId] NVARCHAR (450) NOT NULL,
	[ActivityTypeId] INT NOT NULL,
	[DateTime] DATETIME NOT NULL,

	CONSTRAINT FK_UserActivity_UserId FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id),
	CONSTRAINT FK_ActivityTypeId FOREIGN KEY (ActivityTypeId) REFERENCES UserActivityType(Id)
)
