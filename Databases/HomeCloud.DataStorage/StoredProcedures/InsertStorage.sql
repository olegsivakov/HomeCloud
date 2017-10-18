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
			@local_Quota INT = @Quota

	INSERT INTO [dbo].[Storage] ([Name], [Quota], [CreationDate], [UpdatedDate])
	VALUES (@local_Name, @local_Quota, GETDATE(), GETDATE())
END
GO

