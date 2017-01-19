CREATE TABLE [dbo].[UserOfferDelivery]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[CreateDate] DATETIME NOT NULL,
	[CreateUserId] NVARCHAR (450) NOT NULL,
	[OfferTransactionId] INT NOT NULL,

	CONSTRAINT FK_UserOfferDelivery_CreateUserId FOREIGN KEY ([CreateUserId]) REFERENCES AspNetUsers(Id),
	CONSTRAINT FK_UserOfferDelivery_OfferTransactionId FOREIGN KEY ([OfferTransactionId]) REFERENCES OfferTransactions(Id),
	CONSTRAINT AK_OfferTransactionId UNIQUE(OfferTransactionId)

)
