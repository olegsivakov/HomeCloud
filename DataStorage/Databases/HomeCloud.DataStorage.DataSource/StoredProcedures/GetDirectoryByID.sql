IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'GetDirectoryByID')
BEGIN
	DROP PROCEDURE [dbo].[GetDirectoryByID]
END
GO

CREATE PROCEDURE [dbo].[GetDirectoryByID]
	@ID UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @local_ID UNIQUEIDENTIFIER = @ID

	SET NOCOUNT ON;

	SELECT
		[ID],
		[ParentID],
		[Name],
		[CreationDate],
		[UpdatedDate]
	FROM [dbo].[Directory] WITH(NOLOCK)
	WHERE
		ID = @local_ID
END
GO

