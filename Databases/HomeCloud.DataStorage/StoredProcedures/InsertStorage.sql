IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'InsertStorage')
BEGIN
	DROP PROCEDURE [dbo].[InsertStorage]
END
GO

CREATE PROCEDURE [dbo].[InsertStorage]
	@Name NVARCHAR(100),
	@Quota BIGINT
AS
BEGIN
	DECLARE @local_Name INT = @Name,
			@local_Quota INT = @Quota,
			@local_ID UNIQUEIDENTIFIER = NEWID()

	INSERT INTO [dbo].[Storage] ([ID], [Name], [Quota], [CreationDate], [UpdatedDate])
	VALUES (@local_ID, @local_Name, @local_Quota, GETDATE(), GETDATE())

	EXEC [dbo].[GetStorageByID] @local_ID
END
GO

