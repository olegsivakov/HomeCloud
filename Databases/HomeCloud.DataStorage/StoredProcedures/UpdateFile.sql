IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'UpdateFile')
BEGIN
	DROP PROCEDURE [dbo].[UpdateFile]
END
GO

CREATE PROCEDURE [dbo].[UpdateFile]
	@ID UNIQUEIDENTIFIER,
	@DirectoryID UNIQUEIDENTIFIER,
	@Name NVARCHAR(250),
	@Extension NVARCHAR(10)
AS
BEGIN
	DECLARE @local_ID UNIQUEIDENTIFIER = @ID,
			@local_DirectoryID UNIQUEIDENTIFIER = @DirectoryID,
			@local_Name NVARCHAR(250) = @Name,
			@local_Extension NVARCHAR(10) = @Extension

	UPDATE [dbo].[File]
	SET
		[DirectoryID] = @local_DirectoryID,
		[Name] = @local_Name,
		[Extension] = @local_Extension,
		[UpdatedDate] = GETDATE()
	WHERE
		[ID] = @local_ID
END
GO

