IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'GetDirectoryByParentID')
BEGIN
	DROP PROCEDURE [dbo].[GetDirectoryByParentID]
END
GO

CREATE PROCEDURE [dbo].[GetDirectoryByParentID]
	@StorageID UNIQUEIDENTIFIER,
	@ParentID UNIQUEIDENTIFIER = NULL,
	@StartIndex INT,
	@ChunkSize INT
AS
BEGIN
	DECLARE @local_StorageID UNIQUEIDENTIFIER = @StorageID,
			@local_ParentID UNIQUEIDENTIFIER = @ParentID,
			@local_StartIndex INT = @StartIndex,
			@local_ChunkSize INT = @ChunkSize

	SET NOCOUNT ON;

	SELECT
		[ID],
		[ParentID],
		[StorageID],
		[Name],
		[CreationDate],
		[UpdatedDate]
	FROM [dbo].[Directory]
	WHERE
		[StorageID] = @local_StorageID
		AND
		((@local_ParentID IS NULL AND [ParentID] IS NULL)
		OR
		([ParentID] = @local_ParentID))
	ORDER BY [Name] ASC
	OFFSET (@local_StartIndex * @local_ChunkSize) ROWS
	FETCH NEXT @local_ChunkSize ROWS ONLY
END
GO

