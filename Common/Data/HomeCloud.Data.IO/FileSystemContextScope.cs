namespace HomeCloud.Data.IO
{
	#region Usings

	using System.Transactions;

	using HomeCloud.Core;

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
		/// The <see cref="IFileSystemRepository"/> factory.
		/// </summary>
		private readonly IServiceFactory<IFileSystemRepository> repositoryFactory = null;

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
		public FileSystemContextScope(IServiceFactory<IFileSystemRepository> repositoryFactory)
		{
			this.repositoryFactory = repositoryFactory;
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
		/// Gets the <see cref="IFileSystemRepository" /> repository.
		/// </summary>
		/// <typeparam name="T">The type of the repository derived from <see cref="IFileSystemRepository" />.</typeparam>
		/// <returns>The instance of <see cref="!:T" />.</returns>
		public T GetRepository<T>() where T : IFileSystemRepository
		{
			return this.repositoryFactory.GetService<T>();
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
					}
				}
			}
		}

		#endregion
	}
}
