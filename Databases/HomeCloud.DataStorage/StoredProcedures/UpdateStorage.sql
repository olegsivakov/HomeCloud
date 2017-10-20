IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'UpdateStorage')
BEGIN
	DROP PROCEDURE [dbo].[UpdateStorage]
END
GO

CREATE PROCEDURE [dbo].[UpdateStorage]
	@ID UNIQUEIDENTIFIER,
	@Name NVARCHAR(100),
	@Quota BIGINT
AS
BEGIN
	DECLARE @local_ID UNIQUEIDENTIFIER = @ID,
			@local_Name INT = @Name,
			@local_Quota INT = @Quota

	UPDATE [dbo].[Storage]
	SET
		[Name] = @local_Name,
		[Quota] = @local_Quota,
		[UpdatedDate] = GETDATE()
	WHERE
		[ID] = @local_ID

	EXEC [dbo].[GetStorageByID] @local_ID
END
GO

