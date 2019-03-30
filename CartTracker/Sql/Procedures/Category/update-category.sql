USE CartTracker

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE "UpdateCategory"
	@CategoryId INT,
	@CategoryName NVARCHAR(64) 
AS
BEGIN
	SET NOCOUNT ON;

	IF @CategoryId IS NULL
		RAISERROR('The category is invalid', 15, 1)

	IF @CategoryName IS NULL
		RAISERROR('A Category Name must be provided', 15, 1)

	UPDATE Categories
	SET [Name] = @CategoryName,
		LastUpdated = GETUTCDATE()
	WHERE CategoryId = @CategoryId

END
GO
