namespace HomeCloud.Data.IO
{
	#region Usings

	using System.Transactions;

	#endregion

	/// <summary>
	/// Provides execution of operations against the file system in a single scope.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.IO.IFileSystemContextScope" />
	public class FileSystemContextScope : IFileSystemContextScope
	{
		#region Private Members

		/// <summary>
		/// The synchronization object
		/// </summary>
		private readonly object synchronizationObject = new object();

		/// <summary>
		/// The context
		/// </summary>
		private readonly IFileSystemContext context = null;

		/// <summary>
		/// The transaction scope.
		/// </summary>
		private TransactionScope scope = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemContextScope" /> class.
		/// </summary>
		/// <param name="repositoryFactory">The <see cref="IFileSystemRepository" /> factory.</param>
		public FileSystemContextScope(IFileSystemContext context)
		{
			this.context = context;
		}

		#endregion

		#region IFileSystemContextScope Implementations

		/// <summary>
		/// Begins the current scope.
		/// </summary>
		public void Begin()
		{
			if (this.scope is null)
			{
				lock (this.synchronizationObject)
				{
					if (this.scope is null)
					{
						this.scope = new TransactionScope(TransactionScopeOption.Required);
					}
				}
			}
		}

		/// <summary>
		/// Gets the interface that provides the file system management methods.
		/// </summary>
		/// <returns>The instance of <see cref="IFileSystemOperation" />.</returns>
		public IFileSystemOperation GetOperationCollection()
		{
			return this.context;
		}

		/// <summary>
		/// Commits the changes to the database.
		/// </summary>
		public void Commit()
		{
			if (this.scope != null)
			{
				lock (this.synchronizationObject)
				{
					if (this.scope != null)
					{
						this.scope.Complete();
						this.scope.Dispose();
						this.scope = null;
					}
				}
			}
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
				lock (this.synchronizationObject)
				{
					if (this.scope != null)
					{
						this.scope.Dispose();
						this.scope = null;
					}
				}
			}
		}

		#endregion
	}
}
