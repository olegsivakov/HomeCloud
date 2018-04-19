IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'GetDirectoryByParentID')
BEGIN
	DROP PROCEDURE [dbo].[GetDirectoryByParentID]
END
GO

CREATE PROCEDURE [dbo].[GetDirectoryByParentID]
	@ParentID UNIQUEIDENTIFIER = NULL,
	@Name NVARCHAR(250) = NULL,
	@StartIndex INT,
	@ChunkSize INT
AS
BEGIN
	DECLARE @local_ParentID UNIQUEIDENTIFIER = @ParentID,
			@local_Name NVARCHAR(250) = @Name,
			@local_StartIndex INT = @StartIndex,
			@local_ChunkSize INT = @ChunkSize

	SET NOCOUNT ON;

	SELECT
		directory.[ID],
		directory.[ParentID],
		CASE
			WHEN
				directory.[ParentID] IS NULL AND storage.[Name] IS NOT NULL
			THEN
				storage.[Name]
			ELSE
				directory.[Name]
			END AS [Name],
		directory.[CreationDate],
		directory.[UpdatedDate]
	FROM [dbo].[Directory] directory WITH(NOLOCK)
		LEFT OUTER JOIN [dbo].[Storage] storage WITH(NOLOCK) ON directory.ID = storage.ID
	WHERE
		(
			(
				@local_ParentID IS NULL
				AND directory.[ParentID] IS NULL
			)
			OR
			directory.[ParentID] = @local_ParentID
		)
		AND
		(
			@local_Name IS NULL
			OR (
				directory.[Name] = @local_Name
				OR
				storage.[Name] = @local_Name
			)
		)
	ORDER BY directory.[Name] ASC
	OFFSET @local_StartIndex ROWS
	FETCH NEXT @local_ChunkSize ROWS ONLY
END
GO

