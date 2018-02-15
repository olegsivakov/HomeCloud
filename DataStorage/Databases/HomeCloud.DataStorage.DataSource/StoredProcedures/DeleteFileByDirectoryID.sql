IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'DeleteFileByDirectoryID')
BEGIN
	DROP PROCEDURE [dbo].[DeleteFileByDirectoryID]
END
GO

CREATE PROCEDURE [dbo].[DeleteFileByDirectoryID]
	@DirectoryID UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @local_DirectoryID UNIQUEIDENTIFIER = @DirectoryID

	DELETE FROM [dbo].[File]
	WHERE
		[DirectoryID] = @local_DirectoryID
END
GO

