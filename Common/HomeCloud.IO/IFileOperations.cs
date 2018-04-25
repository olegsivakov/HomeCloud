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
		/// Appends the specified string the file, creating the file if it doesn't already exist.
		/// </summary>
		/// <param name="path">The file to append the string to.</param>
		/// <param name="contents">The string to append to the file.</param>
		void AppendAllText(string path, string content);

		/// <summary>
		/// Copies the specified <paramref name="sourceFileName"/> to <paramref name="destFileName"/>.
		/// </summary>
		/// <param name="sourceFileName">The file to copy.</param>
		/// <param name="destFileName">The name of the destination file.</param>
		/// <param name="overwrite">true if the destination file can be overwritten, otherwise false.</param>
		void Copy(string sourceFileName, string destFileName, bool overwrite);

		/// <summary>
		/// Creates directory in the specified path.
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
		/// Moves the specified file or directory to the new location.
		/// </summary>
		/// <param name="sourcePath">The source path.</param>
		/// <param name="destinationPath">The destination path.</param>
		void Move(string sourcePath, string destinationPath);

		/// <summary>
		/// Takes a snapshot of the file or directory specified by path. Used to rollback the file or directory later.
		/// </summary>
		/// <param name="fileName">The file or directory path to take a snapshot for.</param>
		void Snapshot(string fileName);

		/// <summary>
		/// Creates a file, writes the specified <paramref name="content"/> to the file.
		/// </summary>
		/// <param name="path">The file to write <paramref name="content"/> to.</param>
		/// <param name="content">The array of bytes to write to the file.</param>
		void WriteAllBytes(string path, byte[] content);
	}
}
