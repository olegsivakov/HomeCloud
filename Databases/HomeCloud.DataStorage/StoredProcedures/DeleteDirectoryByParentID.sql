IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'DeleteDirectoryByParentID')
BEGIN
	DROP PROCEDURE [dbo].[DeleteDirectoryByParentID]
END
GO

CREATE PROCEDURE [dbo].[DeleteDirectoryByParentID]
	@ParentID UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @local_ParentID UNIQUEIDENTIFIER = @ParentID

	;WITH RowsToDelete AS (
		SELECT
			[ID]
		FROM [dbo].[Directory]
		WHERE
			[ParentID] = @local_ParentID

		UNION ALL

		SELECT
			children.[ID]
		FROM [dbo].[Directory] AS children
		INNER JOIN RowsToDelete AS parent ON parent.[ID] = children.[ParentID]
	)

	DELETE FROM [dbo].[File]
	FROM [dbo].[File] [file]
	INNER JOIN RowsToDelete rowsToDelete ON [file].[DirectoryID] = rowsToDelete.[ID]

	DELETE FROM [dbo].[Directory]
	FROM [dbo].[Directory] directory
	INNER JOIN RowsToDelete rowsToDelete ON directory.[ID] = rowsToDelete.[ID]
END
GO

