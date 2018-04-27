namespace HomeCloud.Data.SqlServer
{
	#region Usings

	using System.Data;

	using HomeCloud.Core;

	#endregion

	/// <summary>
	/// Provides execution of operations against the <see cref="SqlServer"/> database in a single scope.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.SqlServer.ISqlServerDBContextScope" />
	public class SqlServerDBContextScope : ISqlServerDBContextScope
	{
		#region Private Members

		/// <summary>
		/// The context synchronization object.
		/// </summary>
		private readonly object contextSynchronizer = new object();

		/// <summary>
		/// The database context.
		/// </summary>
		private readonly ISqlServerDBContext context = null;

		/// <summary>
		/// The <see cref="ISqlServerDBRepository"/> factory.
		/// </summary>
		private readonly IServiceFactory<ISqlServerDBRepository> repositoryFactory = null;

		/// <summary>
		/// The database transaction
		/// </summary>
		private IDbTransaction transaction = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlServerDBContextScope" /> class.
		/// </summary>
		/// <param name="context">The database context.</param>
		/// <param name="repositoryFactory">The <see cref="ISqlServerDBRepository" /> factory.</param>
		public SqlServerDBContextScope(ISqlServerDBContext context, IServiceFactory<ISqlServerDBRepository> repositoryFactory)
		{
			this.context = context;
			this.repositoryFactory = repositoryFactory;
		}

		#endregion

		#region ISqlServerDBContextScope Implementations

		/// <summary>
		/// Begins the current scope.
		/// </summary>
		public void Begin()
		{
			if (this.transaction is null)
			{
				lock (this.contextSynchronizer)
				{
					if (this.transaction is null)
					{
						this.transaction = this.context.CreateTransaction();
					}
				}
			}
		}

		/// <summary>
		/// Gets the <see cref="ISqlServerDBRepository" /> repository.
		/// </summary>
		/// <typeparam name="T">The type of the repository derived from <see cref="ISqlServerDBRepository" />.</typeparam>
		/// <returns>The instance of <see cref="!:T" />.</returns>
		public T GetRepository<T>() where T : ISqlServerDBRepository
		{
			return this.repositoryFactory.GetService<T>();
		}

		/// <summary>
		/// Commits the changes to the database.
		/// </summary>
		public void Commit()
		{
			if (this.transaction != null)
			{
				lock (this.contextSynchronizer)
				{
					if (this.transaction != null)
					{
						this.transaction.Commit();
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
				if (this.transaction != null)
				{
					this.transaction.Dispose();
					this.transaction = null;
				}

				this.context.Dispose();
			}
		}

		#endregion
	}
}
