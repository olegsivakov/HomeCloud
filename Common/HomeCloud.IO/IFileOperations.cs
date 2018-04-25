namespace HomeCloud.IO
{
	#region Usings

	using System.IO;

	#endregion

	/// <summary>
	/// Defines methods to handle files and directories.
	/// </summary>
	public interface IFileOperations
	{
		/// <summary>
		/// Appends the specified string to the file, creating the file if it doesn't already exist.
		/// </summary>
		/// <param name="path">The file to append the string to.</param>
		/// <param name="contents">The string to append to the file.</param>
		void AppendAllText(string path, string content);

		/// <summary>
		/// Copies the specified <paramref name="sourceFileName"/> to <paramref name="destFileName"/>.
		/// </summary>
		/// <param name="sourcePath">The file or directory path to copy from.</param>
		/// <param name="destFileName">The destination file or directory path to copy to.</param>
		/// <param name="overwrite">Indicates whether the destination file should be overwritten, otherwise false.</param>
		void Copy(string sourcePath, string destinationPath, bool overwrite);

		/// <summary>
		/// Creates directory by the specified path.
		/// </summary>
		/// <param name="path">The directory path to create.</param>
		void CreateDirectory(string path);

		/// <summary>
		/// Creates the file by specified path and from the specified <see cref="Stream" />.
		/// </summary>
		/// <param name="path">The file path to create.</param>
		/// <param name="stream">The stream containing file data.</param>
		void CreateFile(string path, Stream stream);

		/// <summary>
		/// Deletes the specified file or directory. An exception is not thrown if the file does not exist.
		/// </summary>
		/// <param name="path">The path to the file or directory to delete.</param>
		void Delete(string path);

		/// <summary>
		/// Moves the specified file or directory to the new location specified by <paramref name="destinationPath"/>.
		/// </summary>
		/// <param name="sourcePath">The source path to the file or directory to move.</param>
		/// <param name="destinationPath">The destination path to move to.</param>
		void Move(string sourcePath, string destinationPath);

		/// <summary>
		/// Takes a snapshot of the file or directory specified by path. Used to rollback the file or directory later.
		/// </summary>
		/// <param name="path">The file or directory path to take a snapshot for.</param>
		void Snapshot(string path);

		/// <summary>
		/// Creates a file, writes the specified <paramref name="content"/> to the file.
		/// </summary>
		/// <param name="path">The file to write <paramref name="content"/> to.</param>
		/// <param name="content">The array of bytes to write to the file.</param>
		void WriteAllBytes(string path, byte[] content);

		/// <summary>
		/// Determines whether the specified path refers to a directory that exists on disk.
		/// </summary>
		/// <param name="path">The directory to determine.</param>
		/// <returns>True if the directory exists. Otherwise it returns false.</returns>
		bool DirectoryExists(string path);

		/// <summary>
		/// Determines whether the specified fpath refers to a file that exists on disk.
		/// </summary>
		/// <param name="path">The file to determine.</param>
		/// <returns>True if the file exists. Otherwise it returns false.</returns>
		bool FileExists(string path);

		/// <summary>
		/// Gets the files in the specified directory.
		/// </summary>
		/// <param name="path">The directory to get files.</param>
		/// <param name="handler">The <see cref="FileEventHandler" /> object to call on each file found.</param>
		/// <param name="recursive">if set to <c>true</c>, the method searches for files sub-directories recursively.</param>
		void GetFiles(string path, FileEventHandler handler, bool recursive);
	}
}
