namespace IdentityService.Api.DependencyInjection
{
	#region Usings

	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// Configures the application dependencies.
	/// </summary>
	public static partial class ServiceCollectionExtensions
	{
		#region Public Methods

		/// <summary>
		/// Adds the database services to the service collection.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="configuration">The configuration.</param>
		/// <returns>The instance of <see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddDatabases(this IServiceCollection services, IConfiguration configuration)
		{
			return services;
		}

		/// <summary>
		/// Adds the type mappings to the service collection.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <returns>The instance of <see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddMappings(this IServiceCollection services)
		{
			return services;
		}

		/// <summary>
		/// Adds the identity services.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <returns>The instance of <see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddIdentityServices(this IServiceCollection services)
		{
			return services;
		}

		#endregion
	}
}
