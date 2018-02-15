IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'InsertStorage')
BEGIN
	DROP PROCEDURE [dbo].[InsertStorage]
END
GO

CREATE PROCEDURE [dbo].[InsertStorage]
	@ID UNIQUEIDENTIFIER,
	@Name NVARCHAR(100),
	@Quota BIGINT
AS
BEGIN
	DECLARE @local_ID UNIQUEIDENTIFIER = @ID,
			@local_Name NVARCHAR(100) = @Name,
			@local_Quota BIGINT = @Quota

	INSERT INTO [dbo].[Storage] ([ID], [Name], [Quota], [CreationDate], [UpdatedDate])
	VALUES (@local_ID, @local_Name, @local_Quota, GETDATE(), GETDATE())
END
GO

