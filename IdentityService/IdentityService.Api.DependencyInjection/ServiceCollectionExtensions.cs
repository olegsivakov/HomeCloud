namespace IdentityService.Api.DependencyInjection
{
	#region Usings

	using System.Linq;

	using HomeCloud.Core;

	using HomeCloud.Data.MongoDB;

	using HomeCloud.IdentityService.DataAccess;
	using HomeCloud.IdentityService.DataAccess.Objects;
	using HomeCloud.IdentityService.Stores.Converters;

	using HomeCloud.Mapping;

	using IdentityServer4.Models;

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
			services.AddMongoDB(options =>
			{
				options.ConnectionString = configuration.GetSection("ConnectionStrings").GetSection("IdentityDB").Value;
			})
			.AddContext()
			.AddRepository<IUserDocumentRepository, UserDocumentRepository>()
			.AddRepository<IClientDocumentRepository, ClientDocumentRepository>()
			.AddRepository<IResourceDocumentRepository, ResourceDocumentRepository>();

			return services;
		}

		public static IServiceCollection AddIdentityServerFramework(this IServiceCollection services)
		{
			var descriptor = services.First(item => item.ServiceType == typeof(IResourceDocumentRepository));
			services
				.AddIdentityServer()
				.AddDeveloperSigningCredential();

			return services;
		}

		/// <summary>
		/// Adds the type mappings to the service collection.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <returns>The instance of <see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddMappings(this IServiceCollection services)
		{
			services.AddMapper();

			services.AddTypeConverter<ITypeConverter<ClientDocument, Client>, ClientConverter>();

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
