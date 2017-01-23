/*
	Процедура начисляет пользователю баллы, если он еще не авторизовывался в этот день.
	Так же обновляет UserActivity таблицу
*/
CREATE PROCEDURE [dbo].[EnrollmentPointsForActivityIfNeed]
	-- авторищующийся пользователь
	@targetUserId NVARCHAR (450)
AS

	SET NOCOUNT ON;

	DECLARE @adminUserId NVARCHAR (450)
	-- Id админа сервера, от его имени начисляются баллы
	SET @adminUserId = '355fbe33-1bf2-4865-a5da-1c075b45f944'

	DECLARE @currentDateTime DATETIME
	SET @currentDateTime = GETUTCDATE()

	INSERT INTO dbo.PointTransactions (CreateUserId, CreateDate, UserId, Amount, TypeId)
	SELECT @adminUserId, @currentDateTime, users.Id, 10, 2
	FROM dbo.AspNetUsers as users
	LEFT JOIN 
		(SELECT UserId, ActivityTypeId, MAX(DateTime) as Dt  FROM dbo.UserActivity 
		WHERE ActivityTypeId = 1 AND UserId = @targetUserId
		GROUP BY UserId, ActivityTypeId) as usAct
	ON users.Id = usAct.UserId
	WHERE users.Id = @targetUserId AND ( ( CAST(@currentDateTime AS DATE) > CAST(usAct.Dt AS DATE) ) OR (usAct.UserId IS NULL) )

	 INSERT INTO dbo.UserActivity (UserId, ActivityTypeId, DateTime)
	 VALUES (@targetUserId, 1, @currentDateTime)



RETURN 0
