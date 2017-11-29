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
	FROM [dbo].[Directory] WITH(NOLOCK)
	WHERE
		(
			(
				@local_ParentID IS NULL
				AND [ParentID] IS NULL
			)
			OR
			[ParentID] = @local_ParentID
		)
		AND
		(
			@local_Name IS NULL
			OR
			[Name] = @local_Name
		)
END
GO

