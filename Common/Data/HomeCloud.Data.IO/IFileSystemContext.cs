namespace HomeCloud.Data.IO
{
	#region Usings

	using System;
	using System.IO;

	#endregion

	/// <summary>
	/// Represents the context to query data from the file system.
	/// </summary>
	public interface IFileSystemContext :IFileSystemOperation, IDisposable
	{
		/// <summary>
		/// Gets a value indicating whether this instance use transaction.
		/// </summary>
		/// <value>
		/// <c>True</c> if this instance use transaction; otherwise, <c>false</c>.
		/// </value>
		bool IsTransactional { get; }

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

		/// <summary>
		/// Creates the new instance of <see cref="DirectoryInfo"/> in <paramref name="parent"/>. The method doesn't create new directory in file system.
		/// </summary>
		/// <param name="name">The name of the directory.</param>
		/// <param name="parent">The instance of <see cref="DirectoryInfo"/> representing parent directory the requested by <paramref name="name"/> instance should be created in.
		/// By default the value corresponds <see cref="FileSystemOptions.Root"/>.</param>
		/// <returns>The instance of <see cref="DirectoryInfo"/></returns>
		DirectoryInfo NewDirectory(string name, DirectoryInfo parent = null);

		/// <summary>
		/// Creates the new instance of <see cref="FileInfo"/> in <paramref name="parent"/>. The method doesn't create new file in file system.
		/// </summary>
		/// <param name="name">The name of the file containing extension.</param>
		/// <param name="parent">The instance of <see cref="DirectoryInfo"/> representing parent directory the requested by <paramref name="name"/> instance should be created in.
		/// By default the value corresponds <see cref="FileSystemOptions.Root"/>.</param>
		/// <returns>The instance of <see cref="FileInfo"/></returns>
		FileInfo NewFile(string name, DirectoryInfo parent = null);
	}
}
