IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'GetFileCountByDirectoryID')
BEGIN
	DROP PROCEDURE [dbo].[GetFileCountByDirectoryID]
END
GO

CREATE PROCEDURE [dbo].[GetFileCountByDirectoryID]
	@DirectoryID UNIQUEIDENTIFIER,
	@Name NVARCHAR(250) = NULL
AS
BEGIN
	DECLARE @local_DirectoryID UNIQUEIDENTIFIER = @DirectoryID,
			@local_Name NVARCHAR(250) = @Name

	SET NOCOUNT ON;

	SELECT
		COUNT(1)
	FROM [dbo].[File] WITH(NOLOCK)
	WHERE
		[DirectoryID] = @local_DirectoryID
		AND
		(
			@local_Name IS NULL
			OR
			[Name] = @local_Name
		)
END
GO

