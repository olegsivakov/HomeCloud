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

	DELETE FROM [dbo].[File]
	FROM [dbo].[File] [file]
	INNER JOIN [dbo].[Directory] directory ON directory.[ID] = [file].[DirectoryID]
	WHERE
		directory.[StorageID] = @local_ID

	DELETE FROM [dbo].[Directory]
	WHERE
		[StorageID] = @local_ID
END
GO

