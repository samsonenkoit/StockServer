
/*
	Совершает покупку
	Возвращаемые значения:
	1 - Успех
	101 - Не достаточно денег
	102 - не достаточное кол-во продукции
*/
CREATE PROCEDURE [dbo].[BuyOfferProcedure]

	--пользователь вызывающий процедуру
	@createUserId NVARCHAR (450),
	--совершающий покупку пользователь
	@buyUserId NVARCHAR (450),
	--id предложения
	@offerId INT,
	--дата покупки
	@createDate DATETIME,
	--кол-во покупаемого продукта
	@amount INT
AS

    SET NOCOUNT ON;

	DECLARE @offersPointsCost INT
	--вычисляем баллы необходимые для покупки
	SET @offersPointsCost = (SELECT Price * @amount FROM Offer WHERE Id = @offerId)

	--проверяем что у пользователя достаточно баллов
	DECLARE @buyUserIdWithMoney NVARCHAR (450)
	SET @buyUserIdWithMoney =	(select uInfo.Id from 
									( select u.Id, SUM(pt.Amount) as Points from AspNetUsers u
									join PointTransactions pt on u.Id = pt.UserId
									group by u.Id
									) as uInfo
								where uInfo.Id = @buyUserId and uInfo.Points >= @offersPointsCost)

	--балллов не достаточно, возвращает код ошибки
	if(@buyUserIdWithMoney is null)
	begin
		return 101
	end

	--проверяем что есть необходимое кол-во товара
	DECLARE @availableOfferId INT
	set @availableOfferId = (select offerInf.OfferId from 
								(
								select ot.OfferId, SUM(ot.Amount) as Amount from OfferTransactions ot
								group by ot.OfferId
								) as offerInf
							join dbo.Offer as offer on offerInf.OfferId = offer.Id
							where offerInf.OfferId = @offerId and offerInf.Amount >= @amount and offer.IsActive = 1)
	
	--нет необходимого кол-ва товара
	if(@availableOfferId is null)
	begin
		return 102
	end

	--снимаем баллы
	INSERT INTO dbo.PointTransactions (CreateDate, CreateUserId, UserId, Amount, TypeId)
	VALUES
		(@createDate,@createUserId, @buyUserIdWithMoney, -@offersPointsCost, 1)

	DECLARE @buyPointTrId INT
	SET @buyPointTrId = (SELECT SCOPE_IDENTITY())

	--покупаем продукию
	INSERT INTO dbo.OfferTransactions (CreateDate, CreateUserId, OfferId, BuyUserId, Amount, PointTransactionId, TypeId)
	VALUES (@createDate, @createUserId, @offerId, @buyUserId, -@amount, @buyPointTrId, 1)

	return 1

