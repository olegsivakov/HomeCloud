IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'GetDirectoryCountByParentID')
BEGIN
	DROP PROCEDURE [dbo].[GetDirectoryCountByParentID]
END
GO

CREATE PROCEDURE [dbo].[GetDirectoryCountByParentID]
	@ParentID UNIQUEIDENTIFIER = NULL,
	@Name NVARCHAR(250) = NULL
AS
BEGIN
	DECLARE @local_ParentID UNIQUEIDENTIFIER = @ParentID,
			@local_Name NVARCHAR(250) = @Name

	SET NOCOUNT ON;

	SELECT
		COUNT(1)
	FROM [dbo].[Directory] directory WITH(NOLOCK)
		LEFT OUTER JOIN [dbo].[Storage] storage WITH(NOLOCK) ON directory.ID = storage.ID
	WHERE
		(
			(
				@local_ParentID IS NULL
				AND directory.[ParentID] IS NULL
			)
			OR
			directory.[ParentID] = @local_ParentID
		)
		AND
		(
			@local_Name IS NULL
			OR (
				directory.[Name] = @local_Name
				OR
				storage.[Name] = @local_Name
			)
		)
END
GO

