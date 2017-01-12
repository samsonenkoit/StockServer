CREATE TABLE [dbo].[UserOfferDelivery]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[CreateDate] DATETIME NOT NULL,
	[CreateUserId] NVARCHAR (450) NOT NULL,
	[OfferTransactionId] INT NOT NULL,

	CONSTRAINT FK_UserOfferDelivery_CreateUserId FOREIGN KEY ([CreateUserId]) REFERENCES AspNetUsers(Id),
	CONSTRAINT FK_UserOfferDelivery_OfferTransactionId FOREIGN KEY ([OfferTransactionId]) REFERENCES OfferTransactions(Id)

)
