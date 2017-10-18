IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'GetStorage')
BEGIN
	DROP PROCEDURE [dbo].[GetStorage]
END
GO

CREATE PROCEDURE [dbo].[GetStorage]
	@StartIndex INT,
	@ChunkSize INT
AS
BEGIN
	DECLARE @local_StartIndex INT = @StartIndex,
			@local_ChunkSize INT = @ChunkSize

	SET NOCOUNT ON;

	SELECT
		[ID],
		[Name],
		[Quota],
		[CreationDate],
		[UpdatedDate]
	FROM [dbo].[Storage]
	ORDER BY [Name] ASC
	OFFSET (@local_StartIndex * @local_ChunkSize) ROWS
	FETCH NEXT @local_ChunkSize ROWS ONLY
END
GO

