namespace HomeCloud.Data.MongoDB
{
	#region Usings

	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// Implements methods to add <see cref="MongoDB"/> database services to service collection.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.DependencyInjection.Builders.IMongoDBBuilder" />
	internal class MongoDBBuilder : IMongoDBBuilder
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IServiceCollection"/>.
		/// </summary>
		private readonly IServiceCollection services = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MongoDBBuilder"/> class.
		/// </summary>
		/// <param name="services">The services.</param>
		public MongoDBBuilder(IServiceCollection services)
		{
			this.services = services;
		}

		#endregion

		#region IMongoDBBuilder Implementations

		/// <summary>
		/// Adds default <see cref="T:HomeCloud.Data.MongoDB.IMongoDBContext" /> data context to the service collection.
		/// </summary>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.Data.DependencyInjection.Builders.IMongoDBBuilder" />.
		/// </returns>
		public IMongoDBBuilder AddContext()
		{
			this.services.AddSingleton<IMongoDBContext, MongoDBContext>();

			return this;
		}

		/// <summary>
		/// Adds specified <see cref="T:HomeCloud.Data.MongoDB.IMongoDBContext" /> data context to the service collection.
		/// </summary>
		/// <typeparam name="TContext">The type of the data context derived from <see cref="T:HomeCloud.Data.MongoDB.IMongoDBContext" />.</typeparam>
		/// <typeparam name="TImplementation">The type of <see cref="T:HomeCloud.Data.MongoDB.IMongoDBContext" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.Data.DependencyInjection.Builders.IMongoDBBuilder" />.
		/// </returns>
		public IMongoDBBuilder AddContext<TContext, TImplementation>()
			where TContext : class, IMongoDBContext
			where TImplementation : MongoDBContext, TContext
		{
			this.services.AddSingleton<TContext, TImplementation>();
			return this;
		}

		/// <summary>
		/// Adds the specified <see cref="T:HomeCloud.Data.MongoDB.IMongoDBRepository`1" /> repository to the service collection.
		/// </summary>
		/// <typeparam name="TRepository">The type of the repository derived from <see cref="T:HomeCloud.Data.MongoDB.IMongoDBRepository`1" />.</typeparam>
		/// <typeparam name="TContract">The type of the contract handled by the repository.</typeparam>
		/// <typeparam name="TImplementation">The type of the <see cref="T:HomeCloud.Data.MongoDB.IMongoDBRepository`1" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.Data.DependencyInjection.Builders.IMongoDBBuilder" />.
		/// </returns>
		public IMongoDBBuilder AddRepository<TRepository, TContract, TImplementation>()
			where TRepository : class, IMongoDBRepository<TContract>
			where TImplementation : class, TRepository
		{
			services.AddSingleton<TRepository, TImplementation>();

			return this;
		}

		#endregion
	}
}
