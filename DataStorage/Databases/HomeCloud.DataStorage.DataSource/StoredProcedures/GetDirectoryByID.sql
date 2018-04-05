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
		directory.[ID],
		directory.[ParentID],
		CASE
			WHEN
				directory.[ParentID] IS NULL AND storage.[Name] IS NOT NULL
			THEN
				storage.[Name]
			ELSE
				directory.[Name]
			END AS [Name],
		directory.[CreationDate],
		directory.[UpdatedDate]
	FROM [dbo].[Directory] directory WITH(NOLOCK)
		LEFT OUTER JOIN [dbo].[Storage] storage WITH(NOLOCK) ON directory.ID = storage.ID
	WHERE
		directory.ID = @local_ID
END
GO

