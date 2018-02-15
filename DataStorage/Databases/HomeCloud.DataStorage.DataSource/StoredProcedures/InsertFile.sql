IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'InsertFile')
BEGIN
	DROP PROCEDURE [dbo].[InsertFile]
END
GO

CREATE PROCEDURE [dbo].[InsertFile]
	@ID UNIQUEIDENTIFIER,
	@DirectoryID UNIQUEIDENTIFIER,
	@Name NVARCHAR(250)
AS
BEGIN
	DECLARE @local_ID UNIQUEIDENTIFIER = @ID,
			@local_DirectoryID UNIQUEIDENTIFIER = @DirectoryID,
			@local_Name NVARCHAR(250) =  @Name

	INSERT INTO [dbo].[File] ([ID], [DirectoryID], [Name], [CreationDate], [UpdatedDate])
	VALUES (@local_ID, @local_DirectoryID, @local_Name, GETDATE(), GETDATE())
END
GO

