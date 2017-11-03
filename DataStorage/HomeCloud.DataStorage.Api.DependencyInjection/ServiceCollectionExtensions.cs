namespace HomeCloud.DataStorage.Api.DependencyInjection
{
	#region Usings

	using HomeCloud.Core;

	using HomeCloud.DataAccess.Components.Factories;
	using HomeCloud.DataAccess.Services.Factories;

	using HomeCloud.DataStorage.Api.Configuration;

	using HomeCloud.DataStorage.Business.Commands;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Entities.Mapping;

	using HomeCloud.DataStorage.Business.Handlers;
	using HomeCloud.DataStorage.Business.Providers;
	using HomeCloud.DataStorage.Business.Services;
	using HomeCloud.DataStorage.Business.Validation;

	using HomeCloud.DataStorage.DataAccess.Components.Factories;

	using HomeCloud.DependencyInjection;

	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	using DataContracts = HomeCloud.DataStorage.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Configures the application dependencies.
	/// </summary>
	public static partial class ServiceCollectionExtensions
	{
		#region Public Methods

		/// <summary>
		/// Adds the dependencies to container.
		/// </summary>
		/// <param name="services">The collection of services.</param>
		public static void AddDependencies(this IServiceCollection services)
		{
			services.AddSingleton<IDbRepositoryFactory, DataStorageRepositoryFactory>();
			services.AddSingleton<IDocumentRepositoryFactory, DataAggregationRepositoryFactory>();
			services.AddSingleton<IRepositoryFactory, RepositoryFactory>();

			services.AddSingleton<IDataContextScopeFactory, DataContextScopeFactory>();

			AddConverters(services);
			AddDataProviders(services);
			AddComandHandlers(services);
			AddValidators(services);
			AddFactories(services);
			AddServices(services);

			services.AddScoped<ICommandHandlerProcessor, CommandHandlerProcessor>();
		}

		/// <summary>
		/// Adds application configuration settings to container.
		/// </summary>
		/// <param name="services">The collection of services.</param>
		/// <param name="configuration">A set of configuration properties.</param>
		public static void Configure(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<Database>(configuration.GetSection(nameof(Database)));
			services.Configure<ConnectionStrings>(configuration.GetSection(nameof(Database)).GetSection(nameof(ConnectionStrings)));

			services.Configure<FileSystem>(configuration.GetSection(nameof(FileSystem)));
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Adds the converters to container.
		/// </summary>
		/// <param name="services">The services.</param>
		private static void AddConverters(this IServiceCollection services)
		{
			StorageConverter storageConverter = new StorageConverter();

			services.AddSingleton<ITypeConverter<DataContracts.Storage, Storage>>(storageConverter);
			services.AddSingleton<ITypeConverter<Storage, DataContracts.Storage>>(storageConverter);

			CatalogConverter catalogConverter = new CatalogConverter();

			services.AddSingleton<ITypeConverter<DataContracts.Directory, Catalog>>(catalogConverter);
			services.AddSingleton<ITypeConverter<Catalog, DataContracts.Directory>>(catalogConverter);
			services.AddSingleton<ITypeConverter<DataContracts.AggregatedCatalog, Catalog>>(catalogConverter);
			services.AddSingleton<ITypeConverter<Catalog, DataContracts.AggregatedCatalog>>(catalogConverter);

			services.AddSingleton<IMapper, Mapper>();
		}

		/// <summary>
		/// Adds the comand handlers to container.
		/// </summary>
		/// <param name="services">The services.</param>
		private static void AddComandHandlers(this IServiceCollection services)
		{
			services.AddTransient<IDataStoreCommandHandler, DataStoreCommandHandler>();
			services.AddTransient<IFileSystemCommandHandler, FileSystemCommandHandler>();
			services.AddTransient<IAggregatedDataCommandHandler, AggregatedDataCommandHandler>();
		}

		/// <summary>
		/// Adds the data providers to container.
		/// </summary>
		/// <param name="services">The services.</param>
		private static void AddDataProviders(this IServiceCollection services)
		{
			services.AddSingleton<IDataStoreProvider, DataStoreProvider>();
			services.AddSingleton<IFileSystemProvider, FileSystemProvider>();
			services.AddSingleton<IAggregationDataProvider, AggregationDataProvider>();
		}

		/// <summary>
		/// Adds the data providers to container.
		/// </summary>
		/// <param name="services">The services.</param>
		private static void AddValidators(this IServiceCollection services)
		{
			services.AddTransient<IPresenceValidator, PresenceValidator>();
			services.AddTransient<IUniqueValidator, UniqueValidator>();
			services.AddTransient<IRequiredValidator, RequiredValidator>();
		}

		/// <summary>
		/// Adds the factories to the container.
		/// </summary>
		/// <param name="services">The services.</param>
		private static void AddFactories(this IServiceCollection services)
		{
			services.AddFactory<ITypeConverter>();
			services.AddFactory<ICatalogValidator>();
			services.AddFactory<IStorageValidator>();
			services.AddFactory<IDataCommandHandler>();
			services.AddFactory<IDataProvider>();

			services.AddSingleton<IActionCommandFactory, ActionCommandFactory>();
			services.AddSingleton<IValidationServiceFactory, ValidationServiceFactory>();
		}

		/// <summary>
		/// Adds the services to the container.
		/// </summary>
		/// <param name="services">The services.</param>
		private static void AddServices(this IServiceCollection services)
		{
			services.AddScoped<IStorageService, StorageService>();
		}

		#endregion
	}
}
