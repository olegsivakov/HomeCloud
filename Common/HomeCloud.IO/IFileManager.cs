namespace HomeCloud.IO
{
	/// <summary>
	/// Defines methods to manage files or directories.
	/// </summary>
	public interface IFileManager : IFileOperations
	{
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
		/// Creates a temporary file. The file is not automatically created.
		/// </summary>
		/// <param name="extension">File extension.</param>
		/// <returns>The path to the temporary file.</returns>
		string GetTemporaryFileName(string extension);

		/// <summary>
		/// Creates the temporary directory. The file is not automatically created.
		/// </summary>
		/// <returns>The path to the temporary directory.</returns>
		string GetTemporaryDirectory();
	}
}
