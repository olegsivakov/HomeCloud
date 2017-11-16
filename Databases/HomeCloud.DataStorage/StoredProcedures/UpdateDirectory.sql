IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'UpdateDirectory')
BEGIN
	DROP PROCEDURE [dbo].[UpdateDirectory]
END
GO

CREATE PROCEDURE [dbo].[UpdateDirectory]
	@ID UNIQUEIDENTIFIER,
	@ParentID UNIQUEIDENTIFIER = NULL,
	@Name NVARCHAR(250)
AS
BEGIN
	DECLARE @local_ID UNIQUEIDENTIFIER = @ID,
			@local_ParentID UNIQUEIDENTIFIER = @ParentID,
			@local_Name NVARCHAR(250) = @Name

	UPDATE [dbo].[Directory]
	SET
		[ParentID] = @local_ParentID,
		[Name] = @local_Name,
		[UpdatedDate] = GETDATE()
	WHERE
		[ID] = @local_ID
END
GO

