IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'InsertDirectory')
BEGIN
	DROP PROCEDURE [dbo].[InsertDirectory]
END
GO

CREATE PROCEDURE [dbo].[InsertDirectory]
	@ID UNIQUEIDENTIFIER,
	@ParentID UNIQUEIDENTIFIER = NULL,
	@Name NVARCHAR(250)
AS
BEGIN
	DECLARE @local_ID UNIQUEIDENTIFIER = @ID,
			@local_ParentID UNIQUEIDENTIFIER = @ParentID,
			@local_Name NVARCHAR(250) = @Name

	INSERT INTO [dbo].[Directory] ([ID], [ParentID], [Name], [CreationDate], [UpdatedDate])
	VALUES (@local_ID, @local_ParentID, @local_Name, GETDATE(), GETDATE())
END
GO

