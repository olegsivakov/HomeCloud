IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE parent_object_id = OBJECT_ID('Directory') AND referenced_object_id = OBJECT_ID('File'))
BEGIN
	ALTER TABLE [dbo].[File]
	ADD CONSTRAINT FK_File_Directory
	FOREIGN KEY (DirectoryID) REFERENCES [File](ID);
END
GO

