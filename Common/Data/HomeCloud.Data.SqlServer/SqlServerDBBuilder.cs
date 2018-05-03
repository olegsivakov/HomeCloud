namespace HomeCloud.Data.SqlServer
{
	#region Usings

	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// Implements methods to add <see cref="SqlServer" /> database services to service collection.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.SqlServer.ISqlServerDBBuilder" />
	internal class SqlServerDBBuilder : ISqlServerDBBuilder
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IServiceCollection"/>.
		/// </summary>
		private readonly IServiceCollection services = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlServerDBBuilder"/> class.
		/// </summary>
		/// <param name="services">The services.</param>
		public SqlServerDBBuilder(IServiceCollection services)
		{
			this.services = services;
		}

		#endregion

		#region ISqlServerDBBuilder Implementations

		/// <summary>
		/// Adds default <see cref="T:HomeCloud.Data.SqlServer.ISqlServerDBContext" /> data context to the service collection.
		/// </summary>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.Data.DependencyInjection.Builders.ISqlServerDBBuilder" />.
		/// </returns>
		public ISqlServerDBBuilder AddContext()
		{
			this.services.AddScoped<ISqlServerDBContext, SqlServerDBContext>();

			return this;
		}

		/// <summary>
		/// Adds specified <see cref="T:HomeCloud.Data.SqlServer.ISqlServerDBContext" /> data context to the service collection.
		/// </summary>
		/// <typeparam name="TContext">The type of the data context derived from <see cref="T:HomeCloud.Data.SqlServer.ISqlServerDBContext" />.</typeparam>
		/// <typeparam name="TImplementation">The type of <see cref="T:HomeCloud.Data.SqlServer.ISqlServerDBContext" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.Data.DependencyInjection.Builders.ISqlServerDBBuilder" />.
		/// </returns>
		public ISqlServerDBBuilder AddContext<TContext, TImplementation>()
			where TContext : class, ISqlServerDBContext
			where TImplementation : SqlServerDBContext, TContext
		{
			this.services.AddScoped<TContext, TImplementation>();

			return this;
		}

		/// <summary>
		/// Adds the specified <see cref="T:HomeCloud.Data.SqlServer.ISqlServerDBRepository" /> repository to the service collection.
		/// </summary>
		/// <typeparam name="TRepository">The type of the repository derived from <see cref="T:HomeCloud.Data.SqlServer.ISqlServerDBRepository" />.</typeparam>
		/// <typeparam name="TImplementation">The type of the <see cref="T:HomeCloud.Data.SqlServer.ISqlServerDBRepository" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.Data.DependencyInjection.Builders.ISqlServerDBBuilder" />.
		/// </returns>
		public ISqlServerDBBuilder AddRepository<TRepository, TImplementation>()
			where TRepository : class, ISqlServerDBRepository
			where TImplementation : class, TRepository
		{
			this.services.AddScoped<TRepository, TImplementation>();

			return this;
		}

		#endregion
	}
}
