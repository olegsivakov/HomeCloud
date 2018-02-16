namespace HomeCloud.Data.Http
{
	#region Usings

	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// Implements methods to add <see cref="MongoDB"/> database services to service collection.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.DependencyInjection.Builders.IHttpBuilder" />
	internal class HttpBuilder : IHttpBuilder
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IServiceCollection"/>.
		/// </summary>
		private readonly IServiceCollection services = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpBuilder" /> class.
		/// </summary>
		/// <param name="services">The services.</param>
		public HttpBuilder(IServiceCollection services)
		{
			this.services = services;
		}

		#endregion

		#region IHttpBuilder Implementations

		/// <summary>
		/// Adds default <see cref="T:HomeCloud.Data.Http.IHttpContext" /> data context to the service collection.
		/// </summary>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.Data.DependencyInjection.Builders.IHttpBuilder" />.
		/// </returns>
		public IHttpBuilder AddContext()
		{
			this.services.AddSingleton<IHttpContext, HttpContext>();

			return this;
		}

		/// <summary>
		/// Adds specified <see cref="T:HomeCloud.Data.Http.IHttpContext" /> data context to the service collection.
		/// </summary>
		/// <typeparam name="TContext">The type of the data context derived from <see cref="T:HomeCloud.Data.Http.IHttpContext" />.</typeparam>
		/// <typeparam name="TImplementation">The type of <see cref="T:HomeCloud.Data.Http.IHttpContext" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.Data.DependencyInjection.Builders.IHttpBuilder" />.
		/// </returns>
		public IHttpBuilder AddContext<TContext, TImplementation>()
			where TContext : class, IHttpContext
			where TImplementation : HttpContext, TContext
		{
			this.services.AddSingleton<TContext, TImplementation>();

			return this;
		}

		/// <summary>
		/// Adds the specified <see cref="T:HomeCloud.Data.Http.IHttpRepository`1" /> repository to the service collection.
		/// </summary>
		/// <typeparam name="TRepository">The type of the repository derived from <see cref="T:HomeCloud.Data.Http.IHttpRepository" />.</typeparam>
		/// <typeparam name="TImplementation">The type of the <see cref="T:HomeCloud.Data.Http.IHttpRepository" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.Data.DependencyInjection.Builders.IHttpBuilder" />.
		/// </returns>
		public IHttpBuilder AddRepository<TRepository, TImplementation>()
			where TRepository : class, IHttpRepository
			where TImplementation : class, TRepository
		{
			services.AddSingleton<TRepository, TImplementation>();

			return this;
		}

		#endregion
	}
}
