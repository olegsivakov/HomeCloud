IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE parent_object_id = OBJECT_ID('Storage') AND referenced_object_id = OBJECT_ID('Directory'))
BEGIN
	ALTER TABLE [dbo].[Storage]
	ADD CONSTRAINT FK_Storage_Directory
	FOREIGN KEY (ID) REFERENCES Directory(ID);
END
GO

