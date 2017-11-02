IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE parent_object_id = OBJECT_ID('File') AND referenced_object_id = OBJECT_ID('Directory'))
BEGIN
	ALTER TABLE [dbo].[File]
	ADD CONSTRAINT FK_File_Directory
	FOREIGN KEY (DirectoryID) REFERENCES [Directory](ID);
END
GO

