IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'GetStorageByID')
BEGIN
	DROP PROCEDURE [dbo].[GetStorageByID]
END
GO

CREATE PROCEDURE [dbo].[GetStorageByID]
	@ID UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @local_ID UNIQUEIDENTIFIER = @ID

	SET NOCOUNT ON;

	SELECT
		[ID],
		[Name],
		[Quota],
		[CreationDate],
		[UpdatedDate]
	FROM [dbo].[Storage] WITH(NOLOCK)
	WHERE
		ID = @local_ID
END
GO

