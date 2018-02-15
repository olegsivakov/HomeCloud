IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Directory')
BEGIN
	CREATE TABLE [dbo].[Directory]
	(
		[ID] UNIQUEIDENTIFIER NOT NULL,
		[ParentID] UNIQUEIDENTIFIER NULL,
		[Name] NVARCHAR(250) NOT NULL,
		[CreationDate] DATETIME NOT NULL,
		[UpdatedDate] DATETIME NOT NULL,

		CONSTRAINT [PK_Directory] PRIMARY KEY ([ID])
	)
END
GO

IF EXISTS (SELECT name from sys.indexes WHERE name = N'UI_Directory_Name')
BEGIN
	DROP INDEX UI_Directory_Name ON [dbo].[Directory]
END
GO

CREATE UNIQUE INDEX UI_Directory_Name ON [dbo].[Directory] ([ParentID], [Name]);
GO


