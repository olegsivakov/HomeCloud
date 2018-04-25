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
		/// The context synchronization object.
		/// </summary>
		private readonly object contextSynchronizer = new object();

		/// <summary>
		/// The file system context.
		/// </summary>
		private readonly IFileSystemContext context = null;

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
		/// <param name="context">The file system context.</param>
		/// <param name="repositoryFactory">The <see cref="IFileSystemRepository" /> factory.</param>
		public FileSystemContextScope(IFileSystemContext context, IServiceFactory<IFileSystemRepository> repositoryFactory)
		{
			this.context = context;
			this.repositoryFactory = repositoryFactory;
		}

		#endregion

		#region IFileSystemContextScope Implementations

		/// <summary>
		/// Gets the <see cref="IFileSystemRepository" /> repository.
		/// </summary>
		/// <typeparam name="T">The type of the repository derived from <see cref="IFileSystemRepository" />.</typeparam>
		/// <returns>The instance of <see cref="!:T" />.</returns>
		public T GetRepository<T>() where T : IFileSystemRepository
		{
			if (this.scope is null)
			{
				lock (this.contextSynchronizer)
				{
					if (this.scope is null)
					{
						this.scope = this.context.CreateTransaction();
					}
				}
			}

			return this.repositoryFactory.GetService<T>();
		}

		/// <summary>
		/// Commits the changes to the database.
		/// </summary>
		public void Commit()
		{
			if (this.scope != null)
			{
				lock (this.contextSynchronizer)
				{
					if (this.scope != null)
					{
						this.context.Commit();

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
			lock (this.contextSynchronizer)
			{
				this.context.Dispose();
			}
		}

		#endregion
	}
}
