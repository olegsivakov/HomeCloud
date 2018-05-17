namespace HomeCloud.IdentityService.Api.DependencyInjection
{
	#region Usings

	using HomeCloud.Core;

	using HomeCloud.Data.MongoDB;
	using HomeCloud.DependencyInjection;
	using HomeCloud.IdentityService.Business.Entities;
	using HomeCloud.IdentityService.Business.Entities.Applications;
	using HomeCloud.IdentityService.Business.Entities.Converters;
	using HomeCloud.IdentityService.Business.Entities.Membership;
	using HomeCloud.IdentityService.Business.Validation;
	using HomeCloud.IdentityService.DataAccess;
	using HomeCloud.IdentityService.DataAccess.Objects;

	using HomeCloud.Mapping;

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
			.AddRepository<IApiResourceDocumentRepository, ApiResourceDocumentRepository>()
			.AddRepository<IIdentityResourceDocumentRepository, IdentityResourceDocumentRepository>();

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
			services.AddTypeConverter<ITypeConverter<Client, ClientDocument>, ClientConverter>();
			services.AddTypeConverter<ITypeConverter<Client, Client>, ClientConverter>();
			services.AddTypeConverter<ITypeConverter<ApiResourceDocument, ApiResource>, ApiResourceConverter>();
			services.AddTypeConverter<ITypeConverter<ApiResource, ApiResourceDocument>, ApiResourceConverter>();
			services.AddTypeConverter<ITypeConverter<ApiResource, ApiResource>, ApiResourceConverter>();
			services.AddTypeConverter<ITypeConverter<GrantDocument, Grant>, GrantConverter>();
			services.AddTypeConverter<ITypeConverter<Grant, GrantDocument>, GrantConverter>();
			services.AddTypeConverter<ITypeConverter<Grant, Grant>, GrantConverter>();
			services.AddTypeConverter<ITypeConverter<UserDocument, User>, UserConverter>();
			services.AddTypeConverter<ITypeConverter<User, UserDocument>, UserConverter>();
			services.AddTypeConverter<ITypeConverter<User, User>, UserConverter>();

			return services;
		}

		/// <summary>
		/// Adds the identity services.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <returns>The instance of <see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddIdentityServices(this IServiceCollection services)
		{
			services.AddTransient<IPresenceValidator, PresenceValidator>();
			services.AddTransient<IUniqueValidator, UniqueValidator>();
			services.AddTransient<IRequiredValidator, RequiredValidator>();

			services.AddFactory<IClientValidator>();
			services.AddFactory<IApiResourceValidator>();
			services.AddFactory<IGrantValidator>();
			services.AddFactory<IUserValidator>();

			services.AddScoped<IValidationServiceFactory, ValidationServiceFactory>();

			return services;
		}

		#endregion
	}
}
