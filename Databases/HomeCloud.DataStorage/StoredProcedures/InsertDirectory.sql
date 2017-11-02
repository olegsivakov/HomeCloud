IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'InsertDirectory')
BEGIN
	DROP PROCEDURE [dbo].[InsertDirectory]
END
GO

CREATE PROCEDURE [dbo].[InsertDirectory]
	@ID UNIQUEIDENTIFIER,
	@ParentID UNIQUEIDENTIFIER = NULL,
	@StorageID UNIQUEIDENTIFIER,
	@Name NVARCHAR(250)
AS
BEGIN
	DECLARE @local_ID UNIQUEIDENTIFIER = @ID,
			@local_ParentID UNIQUEIDENTIFIER = @ParentID,
			@local_StorageID UNIQUEIDENTIFIER = @StorageID,
			@local_Name NVARCHAR(250) = @Name

	INSERT INTO [dbo].[Directory] ([ID], [ParentID], [StorageID], [Name], [CreationDate], [UpdatedDate])
	VALUES (@local_ID, @local_ParentID, @local_StorageID, @local_Name, GETDATE(), GETDATE())
END
GO

