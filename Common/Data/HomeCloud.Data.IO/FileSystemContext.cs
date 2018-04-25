namespace HomeCloud.Data.IO
{
	#region Usings

	using System;
	using System.IO;
	using System.Transactions;

	using ChinhDo.Transactions;

	using Microsoft.Extensions.Options;

	#endregion

	/// <summary>
	/// Provides methods to query data from file system.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.IO.IFileSystemContext" />
	public class FileSystemContext : IFileSystemContext
	{
		#region Private Members

		/// <summary>
		/// The transactional file manager.
		/// </summary>
		private TxFileManager manager = null;

		/// <summary>
		/// The transaction scope.
		/// </summary>
		private TransactionScope scope = null;

		/// <summary>
		/// The member indicating whether transaction commit is failed. It's considered the initial state of context as not committed and marked as failed.
		/// </summary>
		private bool isCommitFailed = true;

		/// <summary>
		/// The configuration options.
		/// </summary>
		private readonly FileSystemOptions options = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemContext" /> class.
		/// </summary>
		/// <param name="accessor">The configuration options accessor.</param>
		/// <exception cref="System.ArgumentNullException">accessor or <see cref="IOptionsSnapshot{FileSystemOptions}.Value"/> or <see cref="FileSystemOptions.Root"/>.</exception>
		public FileSystemContext(IOptionsSnapshot<FileSystemOptions> accessor)
		{
			if (accessor is null)
			{
				throw new ArgumentNullException(nameof(accessor));
			}

			if (accessor.Value is null)
			{
				throw new ArgumentNullException(nameof(accessor.Value));
			}

			if (string.IsNullOrWhiteSpace(accessor.Value.Root))
			{
				throw new ArgumentNullException(nameof(accessor.Value.Root));
			}

			this.options = accessor.Value;

			this.Initialize();
		}

		#endregion

		#region IFileSystemContext Implementations

		/// <summary>
		/// Creates the file system transaction scope.
		/// </summary>
		/// <returns>
		/// The instance of <see cref="TransactionScope" />.
		/// </returns>
		public TransactionScope CreateTransaction()
		{
			return this.scope = new TransactionScope(TransactionScopeOption.Required);
		}

		/// <summary>
		/// Copies the specified sourceFileName to destFileName.
		/// </summary>
		/// <param name="sourceFileName">The file to copy.</param>
		/// <param name="destinationFileName">The name of the destination file.</param>
		/// <param name="overwrite">true if the destination file can be overwritten, otherwise false.</param>
		public void Copy(string sourceFileName, string destinationFileName, bool overwrite)
		{
		}

		/// <summary>
		/// Creates all directories in the specified path.
		/// </summary>
		/// <param name="path">The directory path to create.</param>
		public void CreateDirectory(string path)
		{
		}

		/// <summary>
		/// Deletes the specified file. An exception is not thrown if the file does not exist.
		/// </summary>
		/// <param name="path">The file to be deleted.</param>
		public void Delete(string path)
		{
		}

		/// <summary>
		/// Deletes the specified directory and all its contents. An exception is not thrown if the directory does not exist.
		/// </summary>
		/// <param name="path">The directory to be deleted.</param>
		public void DeleteDirectory(string path)
		{
		}

		/// <summary>
		/// Moves the specified file to a new location.
		/// </summary>
		/// <param name="sourceFileName">The name of the file to move.</param>
		/// <param name="destFileName">The new path for the file.</param>
		public void Move(string sourceFileName, string destinationFileName)
		{
		}

		/// <summary>
		/// Creates a file, write the specified contents to the file.
		/// </summary>
		/// <param name="path">The file to write to.</param>
		/// <param name="stream">The content stream.</param>
		public void CreateFile(string path, Stream stream)
		{
		}

		/// <summary>
		/// Commits the changes to the file system.
		/// </summary>
		public void Commit()
		{
			if (this.scope != null)
			{
				this.scope.Complete();
				this.isCommitFailed = false;
			}
		}

		/// <summary>
		/// Rollbacks the changes made against the file system.
		/// </summary>
		public void Rollback()
		{
		}

		#endregion

		#region IDisposable Implementations

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			if (this.scope != null)
			{
				try
				{
					this.Rollback();
				}
				finally
				{
					this.scope.Dispose();
				}
			}

			this.Initialize();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Configures the transactional file manager.
		/// </summary>
		/// <returns>The instance of <see cref="TxFileManager"/>.</returns>
		private TxFileManager ConfigureManager()
		{
			if (this.manager is null)
			{
				this.manager = new TxFileManager();
			}

			return this.manager;
		}

		/// <summary>
		/// Initializes current instance.
		/// </summary>
		private void Initialize()
		{
			this.isCommitFailed = true;
			this.manager = null;

			this.scope.Dispose();
			this.scope = null;
		}

		#endregion
	}
}
