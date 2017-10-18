﻿IF EXISTS (SELECT 1 FROM sys.procedures WHERE [name] = 'GetFileByID')
BEGIN
	DROP PROCEDURE [dbo].[GetFileByID]
END
GO

CREATE PROCEDURE [dbo].[GetFileByID]
	@ID UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @local_ID UNIQUEIDENTIFIER = @ID

	SET NOCOUNT ON;

	SELECT
		[ID],
		[DirectoryID],
		[Name],
		[Extension],
		[CreationDate],
		[UpdatedDate]
	FROM [dbo].[File]
	WHERE
		ID = @local_ID
END
GO

