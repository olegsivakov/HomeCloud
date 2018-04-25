namespace HomeCloud.Data.IO
{
	#region Usings

	using System;
	using System.IO;
	using System.Transactions;

	using HomeCloud.IO;

	using Microsoft.Extensions.Options;

	#endregion

	/// <summary>
	/// Provides methods to query data from file system.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.IO.IFileSystemContext" />
	public class FileSystemContext : FileSystemClient, IFileSystemContext
	{
		#region Private Members

		/// <summary>
		/// The transaction scope.
		/// </summary>
		private TransactionScope scope = null;

		/// <summary>
		/// The configuration options.
		/// </summary>
		private readonly FileSystemOptions options = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemContext" /> class.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="accessor">The configuration options accessor.</param>
		/// <exception cref="System.ArgumentNullException">accessor or <see cref="IOptionsSnapshot{FileSystemOptions}.Value"/> or <see cref="FileSystemOptions.Root"/>.</exception>
		public FileSystemContext(IFileSystemClient client, IOptionsSnapshot<FileSystemOptions> accessor)
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
		/// Commits the changes to the file system.
		/// </summary>
		public void Commit()
		{
			if (this.scope != null)
			{
				this.scope.Complete();
			}
		}

		#endregion

		#region IDisposable Implementations

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public override void Dispose()
		{
			if (this.scope != null)
			{
				this.scope.Dispose();
			}

			this.Initialize();
			base.Dispose();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Initializes current instance.
		/// </summary>
		private void Initialize()
		{
			this.scope = null;
		}

		#endregion
	}
}
