namespace HomeCloud.DependencyInjection
{
	#region Usings

	using HomeCloud.Core;

	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// Provides extension methods to register services in dependency injection container.
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds factory of the specified <see cref="TFactoryService"/> service to the container.
		/// </summary>
		/// <typeparam name="TFactoryService">The type of the factory service.</typeparam>
		/// <param name="services">The collection of services.</param>
		/// <returns>The instance of <see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddFactory<TFactoryService>(this IServiceCollection services)
		{
			services.AddSingleton<IServiceFactory<TFactoryService>>(provider =>
			{
				return new ServiceFactory<TFactoryService>(provider);
			});

			return services;
		}
	}
}
