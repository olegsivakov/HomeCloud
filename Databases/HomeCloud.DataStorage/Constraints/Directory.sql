IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE parent_object_id = OBJECT_ID('Directory') AND referenced_object_id = OBJECT_ID('Directory'))
BEGIN
	ALTER TABLE [dbo].[Directory]
	ADD CONSTRAINT FK_Directory_Directory
	FOREIGN KEY (ParentID) REFERENCES Directory(ID);
END
GO

