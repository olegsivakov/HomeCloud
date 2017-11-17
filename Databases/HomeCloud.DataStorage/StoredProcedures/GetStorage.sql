IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'GetStorage')
BEGIN
	DROP PROCEDURE [dbo].[GetStorage]
END
GO

CREATE PROCEDURE [dbo].[GetStorage]
	@Name NVARCHAR(100) = NULL,
	@StartIndex INT,
	@ChunkSize INT
AS
BEGIN
	DECLARE @local_Name NVARCHAR(100) = @Name,
			@local_StartIndex INT = @StartIndex,
			@local_ChunkSize INT = @ChunkSize

	SET NOCOUNT ON;

	SELECT
		[ID],
		[Name],
		[Quota],
		[CreationDate],
		[UpdatedDate]
	FROM [dbo].[Storage] WITH(NOLOCK)
	WHERE
		@local_Name IS NULL
		OR
		[Name] = @local_Name
	ORDER BY [Name] ASC
	OFFSET (@local_StartIndex * @local_ChunkSize) ROWS
	FETCH NEXT @local_ChunkSize ROWS ONLY
END
GO

