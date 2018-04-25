namespace HomeCloud.IO
{
	/// <summary>
	/// Represents the client that performs the actions against file system.
	/// </summary>
	public interface IFileSystemClient : IFileOperations
	{
		/// <summary>
		/// Gets a value indicating whether this instance use transaction.
		/// </summary>
		/// <value>
		/// <c>True</c> if this instance use transaction; otherwise, <c>false</c>.
		/// </value>
		bool IsTransactional { get; }

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

		/// <summary>
		/// Creates a temporary file with the given extension. The file is automatically created.
		/// </summary>
		/// <param name="extension">File extension. Default value is '<see cref=".tmp"/>'.</param>
		/// <returns>The path to the temporary file.</returns>
		string CreateTemporaryFile(string extension = ".tmp");

		/// <summary>
		/// Creates the temporary directory with the given directory prefix.
		/// </summary>
		/// <param name="prefix">The prefix of the directory name. Default value is <see cref="Nullable"/></param>
		/// <returns>
		/// The path to the newly created temporary directory.
		/// </returns>
		string CreateTemporaryDirectory(string prefix = null);
	}
}
