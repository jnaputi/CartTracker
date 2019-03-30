USE CartTracker

-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE "AddCategory"
	-- Add the parameters for the stored procedure here
	@CategoryName NVARCHAR(64)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @CategoryName IS NULL
		RAISERROR('A Category name must be provided', 15, 1)

	INSERT INTO dbo.Categories (
		[Name],
		DateCreated,
		LastUpdated)
	VALUES
		(@CategoryName, GETUTCDATE(), GETUTCDATE())
END
GO
