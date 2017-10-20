IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'InsertDirectory')
BEGIN
	DROP PROCEDURE [dbo].[InsertDirectory]
END
GO

CREATE PROCEDURE [dbo].[InsertDirectory]
	@ParentID UNIQUEIDENTIFIER = NULL,
	@StorageID UNIQUEIDENTIFIER,
	@Name NVARCHAR(250)
AS
BEGIN
	DECLARE @local_ParentID UNIQUEIDENTIFIER = @ParentID,
			@local_StorageID UNIQUEIDENTIFIER = @StorageID,
			@local_Name NVARCHAR(250) = @Name

	INSERT INTO [dbo].[Directory] ([ParentID], [StorageID], [Name], [CreationDate], [UpdatedDate])
	VALUES (@local_ParentID, @local_StorageID, @local_Name, GETDATE(), GETDATE())

	DECLARE @ID INT = SCOPE_IDENTITY()

	EXEC [dbo].[GetDirectoryByID] @ID
END
GO

