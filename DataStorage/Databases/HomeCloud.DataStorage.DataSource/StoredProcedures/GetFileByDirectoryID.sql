IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'GetFileByDirectoryID')
BEGIN
	DROP PROCEDURE [dbo].[GetFileByDirectoryID]
END
GO

CREATE PROCEDURE [dbo].[GetFileByDirectoryID]
	@DirectoryID UNIQUEIDENTIFIER,
	@Name NVARCHAR(250) = NULL,
	@StartIndex INT,
	@ChunkSize INT
AS
BEGIN
	DECLARE @local_DirectoryID UNIQUEIDENTIFIER = @DirectoryID,
			@local_Name NVARCHAR(250) = @Name,
			@local_StartIndex INT = @StartIndex,
			@local_ChunkSize INT = @ChunkSize

	SET NOCOUNT ON;

	SELECT
		[ID],
		[DirectoryID],
		[Name],
		[CreationDate],
		[UpdatedDate]
	FROM [dbo].[File] WITH(NOLOCK)
	WHERE
		[DirectoryID] = @local_DirectoryID
		AND
		(
			@local_Name IS NULL
			OR
			[Name] = @local_Name
		)
	ORDER BY [Name] ASC
	OFFSET @local_StartIndex ROWS
	FETCH NEXT @local_ChunkSize ROWS ONLY
END
GO

