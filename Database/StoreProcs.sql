CREATE PROCEDURE dbo.spr_UpsertRestaurant
	@id BIGINT,
	@name NVARCHAR(300)
AS
DECLARE @RestaurantId BIGINT;
SET NOCOUNT ON
IF EXISTS(SELECT TOP 1 Id FROM dbo.Restaurant WHERE Id = @id)
	BEGIN
		UPDATE dbo.Restaurant
		SET [Name] = @name
		WHERE Id = @id

		SET @RestaurantId = (SELECT TOP 1 Id FROM dbo.Restaurant WHERE Id = @id);
	END
ELSE
	BEGIN
		INSERT INTO dbo.Restaurant([Name])
		VALUES(@name)    

		SET @RestaurantId = (SELECT TOP 1 Id FROM dbo.Restaurant WHERE [Name] = @name);
	END
SELECT @RestaurantId AS RestaurantId;
GO

CREATE PROCEDURE dbo.spr_GetRestaurantByName
	@name NVARCHAR(300)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 Id
	,[Name]
	FROM dbo.Restaurant
	WHERE [Name] = @name
END
GO

CREATE PROCEDURE dbo.spr_GetAllRestaurants
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	SET NOCOUNT ON;
	SELECT Id
	,[Name]
	FROM dbo.Restaurant
END
GO

CREATE PROCEDURE dbo.spr_DeleteRestaurant
	@restaurantId BIGINT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE 
	FROM Restaurant 
	WHERE Id = @restaurantId;

END
GO

CREATE PROCEDURE dbo.spr_GetRestaurantById
	@restaurantId BIGINT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 Id
	,[Name]
	FROM Restaurant 
	WHERE Id = @restaurantId;
END
GO

CREATE PROCEDURE dbo.spr_UpsertReservation
	@id BIGINT,
	@restaurantId BIGINT,
	@reservationStatusId BIGINT,
	@startDateTime DATETIME,
	@description NVARCHAR(300)
AS
DECLARE @ReservationId BIGINT;
SET NOCOUNT ON
IF EXISTS(SELECT TOP 1 Id FROM dbo.Reservation WHERE Id = @id)
	BEGIN
		UPDATE dbo.Reservation
		SET ReservationStatusId = @reservationStatusId, StartDateTime = @startDateTime, [Description] = @description
		WHERE Id = @id

		SET @ReservationId = (SELECT TOP 1 Id FROM dbo.Reservation WHERE StartDateTime = @startDateTime AND [Description] = @description);
	END
ELSE
	BEGIN
		INSERT INTO dbo.Reservation(RestaurantId, ReservationStatusId, StartDateTime, [Description])
		VALUES(@restaurantId, @reservationStatusId, @startDateTime,  @description)    

		SET @ReservationId = (SELECT TOP 1 Id FROM dbo.Reservation WHERE StartDateTime = @startDateTime AND [Description] = @description);
	END
SELECT @ReservationId AS ReservationId;
GO


CREATE PROCEDURE dbo.spr_GetReservationByDescription
	@description NVARCHAR(300)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 Id
	,RestaurantId
	,ReservationStatusId
	,StartDateTime
	,[Description]
	,DateCreated
	FROM dbo.Reservation
	WHERE [Description] = @description
END
GO

ALTER PROCEDURE dbo.spr_GetAllReservations
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	SET NOCOUNT ON;
	SELECT rv.Id
	,rs.[Name] AS RestaurantName
	,rvs.[Name] AS ReservationStatus
	,rv.StartDateTime
	,rv.[Description]
	,rv.DateCreated
	FROM dbo.Reservation rv INNER JOIN dbo.Restaurant rs
	ON rv.RestaurantId = rs.Id INNER JOIN dbo.ReservationStatus rvs
	ON rv.ReservationStatusId = rvs.Id
END
GO

CREATE PROCEDURE dbo.spr_DeleteReservation
	@reservationId BIGINT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE 
	FROM Reservation 
	WHERE Id = @reservationId;

END
GO

CREATE PROCEDURE dbo.spr_GetReservationById
	@reservationId BIGINT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 rv.Id
	,rs.[Name] AS RestaurantName
	,rvs.[Name] AS ReservationStatus
	,rv.StartDateTime
	,rv.[Description]
	,rv.DateCreated
	FROM dbo.Reservation rv INNER JOIN dbo.Restaurant rs
	ON rv.RestaurantId = rs.Id INNER JOIN dbo.ReservationStatus rvs
	ON rv.ReservationStatusId = rvs.Id
	WHERE rv.Id = @reservationId;
END
GO

CREATE PROCEDURE dbo.spr_GetReservationByIdAndForeignKeysId
	@reservationId BIGINT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 Id
	,RestaurantId
	,ReservationStatusId
	,StartDateTime
	,[Description]
	,DateCreated
	FROM dbo.Reservation
	WHERE Id = @reservationId;
END
GO


CREATE PROCEDURE dbo.spr_GetReservationStatus
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Id
	,[Name]
	FROM dbo.ReservationStatus
END
GO

CREATE PROCEDURE dbo.spr_AddOrder
	@reservationId BIGINT,
	@description NVARCHAR(300)
AS
DECLARE @OrderId BIGINT;
BEGIN
	INSERT INTO dbo.Orders(ReservationId, [Description])
	VALUES(@ReservationId, @Description) 
	SET @OrderId = (SELECT TOP 1 Id FROM dbo.Orders WHERE ReservationId = @ReservationId AND [Description] = @Description);
END
SELECT @OrderId AS OrderId;
GO

CREATE PROCEDURE dbo.spr_GetOrderByReservationId
	@reservationId BIGINT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id]
		,[ReservationId]
		,[Description]
		,[DateCreated]
	FROM [Newtryx].[dbo].[Orders]
	WHERE ReservationId = @reservationId
END
GO

ALTER PROCEDURE dbo.spr_GetAllOrders
AS
BEGIN
	SET NOCOUNT ON;
	SELECT o.[Id]
		,r.[Description] AS Reservation
		,o.[Description]
		,o.[DateCreated]
	FROM [Newtryx].[dbo].[Orders] o INNER JOIN dbo.Reservation  r
	ON o.[ReservationId] = r.Id
END
GO

CREATE PROCEDURE dbo.spr_DeleteOrder
	@orderId BIGINT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE 
	FROM Orders 
	WHERE Id = @orderId;
END
GO

CREATE PROCEDURE dbo.spr_GetOrderById
	@orderId BIGINT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 Id
	,ReservationId
	,[Description]
	,DateCreated
	FROM dbo.Orders
	WHERE Id = @orderId
END
GO
CREATE PROCEDURE dbo.spr_UpdateOrderDescriptionById
	@orderId BIGINT,
	@description NVARCHAR(300)
AS
BEGIN
	UPDATE dbo.Orders
	SET [Description] = @description
	WHERE Id = @OrderId
END
GO