namespace HomeCloud.Data.IO
{
	#region Usings

	using System;

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
	}
}
