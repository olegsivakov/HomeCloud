﻿namespace HomeCloud.Data.Http
{
	/// <summary>
	/// Defines methods to add <see cref="Http"/> resource access services to service collection.
	/// </summary>
	public interface IHttpBuilder
	{
		/// <summary>
		/// Adds default <see cref="IHttpContext" /> data context to the service collection.
		/// </summary>
		/// <returns>
		/// The instance of <see cref="IHttpBuilder" />.
		/// </returns>
		IHttpBuilder AddContext();

		/// <summary>
		/// Adds specified <see cref="IHttpContext" /> data context to the service collection.
		/// </summary>
		/// <typeparam name="TContext">The type of the data context derived from <see cref="IHttpContext" />.</typeparam>
		/// <typeparam name="TImplementation">The type of <see cref="IHttpContext" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="IHttpBuilder" />.
		/// </returns>
		IHttpBuilder AddContext<TContext, TImplementation>()
			where TContext : class, IHttpContext
			where TImplementation : HttpContext, TContext;

		/// <summary>
		/// Adds the specified <see cref="IHttpRepository" /> repository to the service collection.
		/// </summary>
		/// <typeparam name="TRepository">The type of the repository derived from <see cref="IHttpRepository" />.</typeparam>
		/// <typeparam name="TImplementation">The type of the <see cref="IHttpRepository" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="IHttpBuilder" />.
		/// </returns>
		IHttpBuilder AddRepository<TRepository, TImplementation>()
			where TRepository : class, IHttpRepository
			where TImplementation : class, TRepository;
	}
}