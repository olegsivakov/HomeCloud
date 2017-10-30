IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'InsertFile')
BEGIN
	DROP PROCEDURE [dbo].[InsertFile]
END
GO

CREATE PROCEDURE [dbo].[InsertFile]
	@DirectoryID UNIQUEIDENTIFIER,
	@Name NVARCHAR(250),
	@Extension NVARCHAR(10)
AS
BEGIN
	DECLARE @local_DirectoryID UNIQUEIDENTIFIER = @DirectoryID,
			@local_Name NVARCHAR(250) =  @Name,
			@local_Extension NVARCHAR(10) = @Extension,
			@local_ID UNIQUEIDENTIFIER = NEWID()

	INSERT INTO [dbo].[File] ([ID], [DirectoryID], [Name], [Extension], [CreationDate], [UpdatedDate])
	VALUES (@local_ID, @local_DirectoryID, @local_Name, @local_Extension, GETDATE(), GETDATE())

	EXEC [dbo].[GetFileByID] @local_ID
END
GO

