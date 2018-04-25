namespace HomeCloud.Data.IO
{
	#region Usings

	using System;
	using System.IO;
	using System.Transactions;

	#endregion

	/// <summary>
	/// Defines methods to query data from file system.
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	public interface IFileSystemContext : IDisposable
	{
		/// <summary>
		/// Creates the file system transaction scope.
		/// </summary>
		/// <returns>The instance of <see cref="TransactionScope"/>.</returns>
		TransactionScope CreateTransaction();

		/// <summary>
		/// Copies the specified sourceFileName to destFileName.
		/// </summary>
		/// <param name="sourceFileName">The file to copy.</param>
		/// <param name="destinationFileName">The name of the destination file.</param>
		/// <param name="overwrite">true if the destination file can be overwritten, otherwise false.</param>
		void Copy(string sourceFileName, string destinationFileName, bool overwrite);

		/// <summary>
		/// Creates all directories in the specified path.
		/// </summary>
		/// <param name="path">The directory path to create.</param>
		void CreateDirectory(string path);

		/// <summary>
		/// Deletes the specified file. An exception is not thrown if the file does not exist.
		/// </summary>
		/// <param name="path">The file to be deleted.</param>
		void Delete(string path);

		/// <summary>
		/// Deletes the specified directory and all its contents. An exception is not thrown if the directory does not exist.
		/// </summary>
		/// <param name="path">The directory to be deleted.</param>
		void DeleteDirectory(string path);

		/// <summary>
		/// Moves the specified file to a new location.
		/// </summary>
		/// <param name="sourceFileName">The name of the file to move.</param>
		/// <param name="destFileName">The new path for the file.</param>
		void Move(string sourceFileName, string destinationFileName);

		/// <summary>
		/// Creates a file, write the specified contents to the file.
		/// </summary>
		/// <param name="path">The file to write to.</param>
		/// <param name="stream">The content stream.</param>
		void CreateFile(string path, Stream stream);

		/// <summary>
		/// Commits the changes to the file system.
		/// </summary>
		void Commit();

		/// <summary>
		/// Rollbacks the changes made against the file system.
		/// </summary>
		void Rollback();
	}
}
