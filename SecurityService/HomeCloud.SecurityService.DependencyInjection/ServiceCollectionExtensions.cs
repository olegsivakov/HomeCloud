namespace HomeCloud.SecurityService.DependencyInjection
{
	#region Usings

	using HomeCloud.Core;

	using HomeCloud.Data.Http;

	using HomeCloud.SecurityService.DataAccess;
	using HomeCloud.SecurityService.DataAccess.Objects;
	using HomeCloud.SecurityService.Stores;
	using HomeCloud.SecurityService.Stores.Converters;

	using HomeCloud.Mapping;

	using IdentityServer4.Models;
	using IdentityServer4.Stores;

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
		/// Adds the thrid-party services to the service collection.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="configuration">The configuration.</param>
		/// <returns>The instance of <see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddHttp(options =>
			{
				options.BaseAddress = configuration.GetSection("ConnectionStrings").GetSection("IdentityServiceUrl").Value;
			})
			.AddContext()
			.AddRepository<IUserDocumentRepository, UserDocumentRepository>()
			.AddRepository<IClientDocumentRepository, ClientDocumentRepository>()
			.AddRepository<IApiResourceDocumentRepository, ApiResourceDocumentRepository>()
			.AddRepository<IIdentityResourceDocumentRepository, IdentityResourceDocumentRepository>()
			.AddRepository<IGrantDocumentRepository, GrantDocumentRepository>();

			return services;
		}

		/// <summary>
		/// Adds the identity configuration storage to the service collection.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <returns></returns>
		public static IServiceCollection AddResourceStore(this IServiceCollection services)
		{
			services.AddScoped<IResourceStore, ResourceStore>();
			services.AddScoped<IClientStore, ClientStore>();

			return services;
		}

		/// <summary>
		/// Adds the identity grant storage to the service collection.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <returns></returns>
		public static IServiceCollection AddGrantStore(this IServiceCollection services)
		{
			services.AddScoped<IPersistedGrantStore, PersistedGrantStore>();

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
			services.AddTypeConverter<ITypeConverter<ApiResourceDocument, ApiResource>, ApiResourceConverter>();
			services.AddTypeConverter<ITypeConverter<IdentityResourceDocument, IdentityResource>, IdentityResourceConverter>();
			services.AddTypeConverter<ITypeConverter<GrantDocument, PersistedGrant>, PersistedGrantConverter>();
			services.AddTypeConverter<ITypeConverter<PersistedGrant, GrantDocument>, PersistedGrantConverter>();

			return services;
		}

		/// <summary>
		/// Adds the security services to the service collection.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <returns>The instance of <see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddSecurityServices(this IServiceCollection services)
		{
			return services;
		}

		#endregion
	}
}
