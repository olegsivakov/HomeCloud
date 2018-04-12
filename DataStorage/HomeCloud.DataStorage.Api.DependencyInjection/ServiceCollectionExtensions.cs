namespace HomeCloud.DataStorage.Api.DependencyInjection
{
	#region Usings

	using HomeCloud.Core;

	using HomeCloud.Data.MongoDB;
	using HomeCloud.Data.SqlServer;

	using HomeCloud.DataStorage.Api.Models;
	using HomeCloud.DataStorage.Api.Models.Converters;

	using HomeCloud.DataStorage.Business.Commands;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Entities.Converters;

	using HomeCloud.DataStorage.Business.Handlers;
	using HomeCloud.DataStorage.Business.Providers;
	using HomeCloud.DataStorage.Business.Services;
	using HomeCloud.DataStorage.Business.Validation;
	using HomeCloud.DataStorage.Configuration;

	using HomeCloud.DataStorage.DataAccess;
	using HomeCloud.DataStorage.DataAccess.Aggregation;
	using HomeCloud.DataStorage.DataAccess.Aggregation.Objects;

	using HomeCloud.DependencyInjection;
	using HomeCloud.Mapping;
	using HomeCloud.Validation;

	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	using DataContracts = HomeCloud.DataStorage.DataAccess.Objects;

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
			services.AddSqlServerDB(options =>
			{
				options.ConnectionString = configuration.GetSection("ConnectionStrings").GetSection("DataStorageDB").Value;
			})
			.AddContext()
			.AddContextScope()
			.AddRepository<IStorageRepository, StorageRepository>()
			.AddRepository<ICatalogRepository, CatalogRepository>()
			.AddRepository<IFileRepository, FileRepository>();

			services.AddMongoDB(options =>
			{
				options.ConnectionString = configuration.GetSection("ConnectionStrings").GetSection("AggregationDB").Value;
			})
			.AddContext()
			.AddRepository<IFileDocumentRepository, FileDocumentRepository>()
			.AddRepository<ICatalogDocumentRepository, CatalogDocumentRepository>();

			return services;
		}

		/// <summary>
		/// Adds the file storage configuration to service collection.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="configuration">The configuration.</param>
		/// <returns>The instance of <see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddFileStorage(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<FileSystem>(configuration.GetSection(nameof(FileSystem)));

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

			services.AddTypeConverter<ITypeConverter<DataContracts.Storage, Storage>, StorageConverter>();
			services.AddTypeConverter<ITypeConverter<Storage, DataContracts.Storage>, StorageConverter>();
			services.AddTypeConverter<ITypeConverter<DataContracts.Catalog, Storage>, StorageConverter>();
			services.AddTypeConverter<ITypeConverter<Storage, DataContracts.Catalog>, StorageConverter>();
			services.AddTypeConverter<ITypeConverter<CatalogDocument, Storage>, StorageConverter>();
			services.AddTypeConverter<ITypeConverter<Storage,CatalogDocument>, StorageConverter>();

			services.AddTypeConverter<ITypeConverter<DataContracts.Catalog, Catalog>, CatalogConverter>();
			services.AddTypeConverter<ITypeConverter<Catalog, DataContracts.Catalog>, CatalogConverter>();
			services.AddTypeConverter<ITypeConverter<CatalogDocument, Catalog>, CatalogConverter>();
			services.AddTypeConverter<ITypeConverter<Catalog, CatalogDocument>, CatalogConverter>();

			services.AddTypeConverter<ITypeConverter<DataContracts.File, CatalogEntry>, CatalogEntryConverter>();
			services.AddTypeConverter<ITypeConverter<CatalogEntry, DataContracts.File>, CatalogEntryConverter>();
			services.AddTypeConverter<ITypeConverter<FileDocument, CatalogEntry>, CatalogEntryConverter>();
			services.AddTypeConverter<ITypeConverter<CatalogEntry, FileDocument>, CatalogEntryConverter>();

			services.AddTypeConverter<ITypeConverter<ValidationResult, ServiceResult>, ServiceResultConverter>();

			services.AddTypeConverter<ITypeConverter<Storage, StorageViewModel>, StorageViewModelConverter>();
			services.AddTypeConverter<ITypeConverter<StorageViewModel, Storage>, StorageViewModelConverter>();

			services.AddTypeConverter<ITypeConverter<Catalog, CatalogViewModel>, CatalogViewModelConverter>();
			services.AddTypeConverter<ITypeConverter<CatalogViewModel, Catalog>, CatalogViewModelConverter>();

			services.AddTypeConverter<ITypeConverter<Catalog, DataViewModel>, DataViewModelConverter>();
			services.AddTypeConverter<ITypeConverter<CatalogEntry, DataViewModel>, DataViewModelConverter>();

			services.AddTypeConverter<ITypeConverter<CatalogEntry, FileViewModel>, FileViewModelConverter>();
			services.AddTypeConverter<ITypeConverter<FileStreamViewModel, CatalogEntry>, FileStreamViewModelConverter>();

			return services;
		}

		/// <summary>
		/// Adds the data storage services.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <returns>The instance of <see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddDataStorageServices(this IServiceCollection services)
		{
			services.AddTransient<IPresenceValidator, PresenceValidator>();
			services.AddTransient<IUniqueValidator, UniqueValidator>();
			services.AddTransient<IRequiredValidator, RequiredValidator>();

			services.AddFactory<ICatalogEntryValidator>();
			services.AddFactory<ICatalogValidator>();
			services.AddFactory<IStorageValidator>();

			services.AddSingleton<IValidationServiceFactory, ValidationServiceFactory>();

			services.AddSingleton<IDataStoreProvider, DataStoreProvider>();
			services.AddSingleton<IFileSystemProvider, FileSystemProvider>();
			services.AddSingleton<IAggregationDataProvider, AggregationDataProvider>();
			services.AddSingleton<IDataProviderFactory, DataProviderFactory>();

			services.AddFactory<IDataProvider>();

			services.AddSingleton<IActionCommandFactory, ActionCommandFactory>();
			services.AddTransient<IDataCommandHandler, DataCommandHandler>();
			services.AddScoped<ICommandHandlerProcessor, CommandHandlerProcessor>();
			services.AddFactory<ICommandHandler>();

			services.AddScoped<IStorageService, StorageService>();
			services.AddScoped<ICatalogService, CatalogService>();
			services.AddScoped<ICatalogEntryService, CatalogEntryService>();

			return services;
		}

		#endregion
	}
}
