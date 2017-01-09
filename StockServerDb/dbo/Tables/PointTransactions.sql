CREATE TABLE [dbo].[PointTransactions]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[CreateDate] DATETIME NOT NULL,
	[CreateUserId] NVARCHAR (450) NOT NULL,
	[UserId] NVARCHAR (450) NOT NULL,
	[Amount] INT NOT NULL,
	[TypeId] INT NOT NULL,

	CONSTRAINT FK_PointTransactions_CreateUserId FOREIGN KEY ([CreateUserId]) REFERENCES AspNetUsers(Id),
	CONSTRAINT FK_PointTransactions_UserId FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id),
	CONSTRAINT FK_PointTransactions_TypeId FOREIGN KEY (TypeId) REFERENCES PointTransactionType(Id)
)
