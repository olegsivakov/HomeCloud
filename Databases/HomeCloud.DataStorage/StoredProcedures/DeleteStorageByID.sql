IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'DeleteStorageByID')
BEGIN
	DROP PROCEDURE [dbo].[DeleteStorageByID]
END
GO

CREATE PROCEDURE [dbo].[DeleteStorageByID]
	@ID UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @local_ID UNIQUEIDENTIFIER = @ID

	EXEC [dbo].[DeleteDirectoryByParentID] @local_ID
	EXEC [dbo].[DeleteDirectoryByID] @local_ID
END
GO

