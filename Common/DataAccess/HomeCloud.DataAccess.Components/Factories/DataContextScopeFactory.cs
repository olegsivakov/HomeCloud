namespace HomeCloud.DataAccess.Components.Factories
{
	#region Usings

	using HomeCloud.DataAccess.Contracts;
	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataAccess.Services.Factories;

	#endregion

	/// <summary>
	///  Provides methods for creation of context scopes.
	/// </summary>
	/// <seealso cref="IDataContextScopeFactory" />
	public sealed class DataContextScopeFactory : IDataContextScopeFactory
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IRepositoryFactory"/> member.
		/// </summary>
		private readonly IRepositoryFactory repositoryFactory = null;

		/// <summary>
		/// The <see cref="IQueryHandlerFactory"/> member.
		/// </summary>
		private readonly IQueryHandlerFactory queryHandlerFactory = null;

		/// <summary>
		/// The <see cref="ICommandHandlerFactory"/> member.
		/// </summary>
		private readonly ICommandHandlerFactory commandHandlerFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DataContextScopeFactory" /> class.
		/// </summary>
		/// <param name="repositoryFactory">The <see cref="IRepositoryFactory" /> factory.</param>
		/// <param name="queryHandlerFactory">The <see cref="IQueryHandlerFactory" /> factory.</param>
		/// <param name="commandHandlerFactory">The <see cref="ICommandHandlerFactory" /> factory.</param>
		public DataContextScopeFactory(IRepositoryFactory repositoryFactory = null, IQueryHandlerFactory queryHandlerFactory = null, ICommandHandlerFactory commandHandlerFactory = null)
		{
			if (repositoryFactory != null)
			{
				this.repositoryFactory = repositoryFactory;
			}

			if (queryHandlerFactory != null)
			{
				this.queryHandlerFactory = queryHandlerFactory;
			}

			if (commandHandlerFactory != null)
			{
				this.commandHandlerFactory = commandHandlerFactory;
			}
		}

		#endregion

		#region IDataContextScopeFactory Implementations

		/// <summary>
		/// Creates database context scope.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="isTransactional">Indicates whether the scope supports transactions.</param>
		/// <returns>
		/// The instance of <see cref="T:IDataContextScope" />.
		/// </returns>
		public IDbContextScope CreateDbContextScope(string connectionString, bool isTransactional = false)
		{
			IDbRepositoryFactory repositoryFactory = this.repositoryFactory?.GetFactory<IDbRepositoryFactory>();
			IDbQueryHandlerFactory queryHandlerFactory = this.queryHandlerFactory?.GetFactory<IDbQueryHandlerFactory>();
			IDbCommandHandlerFactory commandHandlerFactory = this.commandHandlerFactory?.GetFactory<IDbCommandHandlerFactory>();

			return new DbContextScope(connectionString, isTransactional, repositoryFactory, queryHandlerFactory, commandHandlerFactory);
		}

		/// <summary>
		/// Creates the database context scope.
		/// </summary>
		/// <param name="context">The database context.</param>
		/// <returns>
		/// The instance of <see cref="IDbContextScope" />.
		/// </returns>
		public IDbContextScope CreateDbContextScope(ITransactionalDbContext context)
		{
			IDbRepositoryFactory repositoryFactory = this.repositoryFactory?.GetFactory<IDbRepositoryFactory>();
			IDbQueryHandlerFactory queryHandlerFactory = this.queryHandlerFactory?.GetFactory<IDbQueryHandlerFactory>();
			IDbCommandHandlerFactory commandHandlerFactory = this.commandHandlerFactory?.GetFactory<IDbCommandHandlerFactory>();

			return new DbContextScope(context, repositoryFactory, queryHandlerFactory, commandHandlerFactory);
		}

		#endregion
	}
}
