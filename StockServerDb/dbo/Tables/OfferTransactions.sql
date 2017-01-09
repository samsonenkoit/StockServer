CREATE TABLE [dbo].[OfferTransactions]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[CreateDate] DATETIME NOT NULL,
	[CreateUserId] NVARCHAR (450) NOT NULL,
	[OfferId] INT NOT NULL,
	[BuyUserId] NVARCHAR (450) NULL,
	[Amount] INT NOT NULL,
	[PointTransactionId] INT NULL,
	[TypeId] INT NOT NULL,

	CONSTRAINT FK_OfferTransactions_CreateUserId FOREIGN KEY ([CreateUserId]) REFERENCES AspNetUsers(Id),
	CONSTRAINT FK_OfferTransactions_BuyUserId FOREIGN KEY ([BuyUserId]) REFERENCES AspNetUsers(Id),
	CONSTRAINT FK_OfferTransactions_PointTransactionId FOREIGN KEY (PointTransactionId) REFERENCES PointTransactions(Id),
	CONSTRAINT FK_OfferTransactions_TypeId FOREIGN KEY (TypeId) REFERENCES OfferTransactionType(Id),
	CONSTRAINT FK_OfferTransactions_OfferId FOREIGN KEY (OfferId) REFERENCES Offer(Id)
)
