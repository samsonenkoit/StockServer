CREATE TABLE [dbo].[OfferItems]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[OfferId] INT NOT NULL,
	[StateId] INT NOT NULL DEFAULT 0,
	[OwnerUserId] NVARCHAR (450) NULL,

	CONSTRAINT FK_OfferItems_OfferId FOREIGN KEY ([OfferId]) REFERENCES Offer(Id),
	CONSTRAINT FK_OfferItems_OwnerUserId FOREIGN KEY ([OwnerUserId]) REFERENCES AspNetUsers(Id)
)
