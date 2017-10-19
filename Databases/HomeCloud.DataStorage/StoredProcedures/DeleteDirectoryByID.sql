IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'DeleteDirectoryByID')
BEGIN
	DROP PROCEDURE [dbo].[DeleteDirectoryByID]
END
GO

CREATE PROCEDURE [dbo].[DeleteDirectoryByID]
	@ID UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @local_ID UNIQUEIDENTIFIER = @ID

	EXEC [dbo].[DeleteDirectoryByParentID] @local_ID

	DELETE FROM [dbo].[File]
	WHERE
		[DirectoryID] = @local_ID

	DELETE FROM [dbo].[Directory]
	WHERE
		[ID] = @local_ID
END
GO

