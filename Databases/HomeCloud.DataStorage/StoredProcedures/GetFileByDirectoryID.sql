IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'GetFileByDirectoryID')
BEGIN
	DROP PROCEDURE [dbo].[GetFileByDirectoryID]
END
GO

CREATE PROCEDURE [dbo].[GetFileByDirectoryID]
	@DirectoryID UNIQUEIDENTIFIER,
	@StartIndex INT,
	@ChunkSize INT
AS
BEGIN
	DECLARE @local_DirectoryID UNIQUEIDENTIFIER = @DirectoryID,
			@local_StartIndex INT = @StartIndex,
			@local_ChunkSize INT = @ChunkSize

	SET NOCOUNT ON;

	SELECT
		[ID],
		[DirectoryID],
		[Name],
		[Extension],
		[CreationDate],
		[UpdatedDate]
	FROM [dbo].[File]
	WHERE
		[DirectoryID] = @local_DirectoryID
	ORDER BY [Name] ASC
	OFFSET (@local_StartIndex * @local_ChunkSize) ROWS
	FETCH NEXT @local_ChunkSize ROWS ONLY
END
GO

