namespace HomeCloud.DataAccess.Components
{
	#region Usings

	using HomeCloud.DataAccess.Contracts;

	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataAccess.Services.Factories;

	#endregion

	/// <summary>
	/// Provides methods for operations within data context executed as single scope.
	/// </summary>
	/// <seealso cref="IDbContextScope" />
	public class DbContextScope : IDbContextScope
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IDbRepositoryFactory"/> member.
		/// </summary>
		private static IDbRepositoryFactory repositoryFactory = null;

		/// <summary>
		/// The <see cref="IDbQueryHandlerFactory"/> member.
		/// </summary>
		private static IDbQueryHandlerFactory queryHandlerFactory = null;

		/// <summary>
		/// The <see cref="IDbCommandHandlerFactory"/> member.
		/// </summary>
		private static IDbCommandHandlerFactory commandHandlerFactory = null;

		/// <summary>
		/// The <see cref="ITransactionalDbContext"/> member.
		/// </summary>
		private readonly ITransactionalDbContext context = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DbContextScope" /> class.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="isTransactional">Indicates whether the scope should support database transactions.</param>
		/// <param name="repositoryFactory">The <see cref="IDbRepositoryFactory" /> factory.</param>
		/// <param name="queryHandlerFactory">The <see cref="IDbQueryHandlerFactory" /> factory.</param>
		/// <param name="commandHandlerFactory">The <see cref="IDbCommandHandlerFactory" /> factory.</param>
		public DbContextScope(string connectionString, bool isTransactional, IDbRepositoryFactory repositoryFactory = null, IDbQueryHandlerFactory queryHandlerFactory = null, IDbCommandHandlerFactory commandHandlerFactory = null)
		{
			DbContextScope.repositoryFactory = repositoryFactory;
			DbContextScope.queryHandlerFactory = queryHandlerFactory;
			DbContextScope.commandHandlerFactory = commandHandlerFactory;
			
			this.context = new DbContext(connectionString, isTransactional);
		}

		#endregion

		#region IDataContextScope Implementations

		/// <summary>
		/// Gets the <see cref="T:HomeCloud.DataAccess.Services.IDbRepository" /> repository.
		/// </summary>
		/// <typeparam name="T">The type of the repository derived from <see cref="T:HomeCloud.DataAccess.Services.IDbRepository" />.</typeparam>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataAccess.Services.IDbRepository" />.
		/// </returns>
		public T GetRepository<T>() where T : IDbRepository
		{
			return repositoryFactory.GetRepository<T>(this.context);
		}

		/// <summary>
		/// Gets the <see cref="T:HomeCloud.DataAccess.Services.IDbQueryHandler" /> handler.
		/// </summary>
		/// <typeparam name="T">The type of the query handler derived from <see cref="T:HomeCloud.DataAccess.Services.IDbQueryHandler" />.</typeparam>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataAccess.Services.IDbQueryHandler" />.
		/// </returns>
		public T GetQueryHandler<T>() where T : IDbQueryHandler
		{
			return queryHandlerFactory.GetHandler<T>(this.context);
		}

		/// <summary>
		/// Gets the <see cref="T:HomeCloud.DataAccess.Services.IDbCommandHandler" /> handler.
		/// </summary>
		/// <typeparam name="T">The type of the command handler derived from <see cref="T:HomeCloud.DataAccess.Services.IDbQueryHandler" />.</typeparam>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataAccess.Services.IDbCommandHandler" />.
		/// </returns>
		public T GetCommandHandler<T>() where T : IDbCommandHandler
		{
			return commandHandlerFactory.GetHandler<T>(this.context);
		}

		/// <summary>
		/// Commits the existing changes in database.
		/// </summary>
		public void Commit()
		{
			this.context.Commit();
		}

		#endregion

		#region IDisposable Implementations

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			this.context.Dispose();
		}

		#endregion
	}
}
