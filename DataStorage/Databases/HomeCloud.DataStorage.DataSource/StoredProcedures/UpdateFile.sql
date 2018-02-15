IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'UpdateFile')
BEGIN
	DROP PROCEDURE [dbo].[UpdateFile]
END
GO

CREATE PROCEDURE [dbo].[UpdateFile]
	@ID UNIQUEIDENTIFIER,
	@DirectoryID UNIQUEIDENTIFIER,
	@Name NVARCHAR(250)
AS
BEGIN
	DECLARE @local_ID UNIQUEIDENTIFIER = @ID,
			@local_DirectoryID UNIQUEIDENTIFIER = @DirectoryID,
			@local_Name NVARCHAR(250) = @Name

	UPDATE [dbo].[File]
	SET
		[DirectoryID] = @local_DirectoryID,
		[Name] = @local_Name,
		[UpdatedDate] = GETDATE()
	WHERE
		[ID] = @local_ID
END
GO

