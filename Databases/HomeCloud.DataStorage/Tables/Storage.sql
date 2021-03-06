﻿IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Storage')
BEGIN
	CREATE TABLE [dbo].[Storage]
	(
		[ID] UNIQUEIDENTIFIER NOT NULL,
		[Name] NVARCHAR(100) NOT NULL,
		[Quota] BIGINT NULL,
		[CreationDate] DATETIME NOT NULL,
		[UpdatedDate] DATETIME NOT NULL,

		CONSTRAINT [PK_Storage] PRIMARY KEY ([ID])
	)
END
GO

