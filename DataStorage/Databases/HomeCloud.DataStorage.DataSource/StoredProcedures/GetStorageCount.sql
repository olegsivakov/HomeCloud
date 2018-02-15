IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'GetStorageCount')
BEGIN
	DROP PROCEDURE [dbo].[GetStorageCount]
END
GO

CREATE PROCEDURE [dbo].[GetStorageCount]
	@Name NVARCHAR(100) = NULL
AS
BEGIN
	DECLARE @local_Name NVARCHAR(100) = @Name

	SET NOCOUNT ON;

	SELECT
		COUNT(1)
	FROM [dbo].[Storage] WITH(NOLOCK)
	WHERE
		@local_Name IS NULL
		OR
		[Name] = @local_Name
END
GO

