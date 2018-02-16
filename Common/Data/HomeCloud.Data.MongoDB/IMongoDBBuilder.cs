namespace HomeCloud.Data.MongoDB
{
	/// <summary>
	/// Defines methods to add <see cref="MongoDB"/> database services to service collection.
	/// </summary>
	public interface IMongoDBBuilder
	{
		/// <summary>
		/// Adds default <see cref="IMongoDBContext" /> data context to the service collection.
		/// </summary>
		/// <returns>
		/// The instance of <see cref="IMongoDBBuilder" />.
		/// </returns>
		IMongoDBBuilder AddContext();

		/// <summary>
		/// Adds specified <see cref="IMongoDBContext" /> data context to the service collection.
		/// </summary>
		/// <typeparam name="TContext">The type of the data context derived from <see cref="IMongoDBContext" />.</typeparam>
		/// <typeparam name="TImplementation">The type of <see cref="IMongoDBContext" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="IMongoDBBuilder" />.
		/// </returns>
		IMongoDBBuilder AddContext<TContext, TImplementation>()
			where TContext : class, IMongoDBContext
			where TImplementation : MongoDBContext, TContext;

		/// <summary>
		/// Adds the specified <see cref="IMongoDBRepository" /> repository to the service collection.
		/// </summary>
		/// <typeparam name="TRepository">The type of the repository derived from <see cref="IMongoDBRepository" />.</typeparam>
		/// <typeparam name="TImplementation">The type of the <see cref="IMongoDBRepository" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="IMongoDBBuilder" />.
		/// </returns>
		IMongoDBBuilder AddRepository<TRepository, TImplementation>()
			where TRepository : class, IMongoDBRepository
			where TImplementation : class, TRepository;
	}
}
