﻿IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'GetDirectoryByParentID')
BEGIN
	DROP PROCEDURE [dbo].[GetDirectoryByParentID]
END
GO

CREATE PROCEDURE [dbo].[GetDirectoryByParentID]
	@ParentID UNIQUEIDENTIFIER = NULL,
	@Name NVARCHAR(250) = NULL,
	@StartIndex INT,
	@ChunkSize INT
AS
BEGIN
	DECLARE @local_ParentID UNIQUEIDENTIFIER = @ParentID,
			@local_Name NVARCHAR(250) = @Name,
			@local_StartIndex INT = @StartIndex,
			@local_ChunkSize INT = @ChunkSize

	SET NOCOUNT ON;

	SELECT
		[ID],
		[ParentID],
		[Name],
		[CreationDate],
		[UpdatedDate]
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
	ORDER BY [Name] ASC
	OFFSET (@local_StartIndex * @local_ChunkSize) ROWS
	FETCH NEXT @local_ChunkSize ROWS ONLY
END
GO

