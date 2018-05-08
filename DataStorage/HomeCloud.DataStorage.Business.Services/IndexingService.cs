namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using System.Linq;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.Core.Extensions;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Providers;
	using HomeCloud.DataStorage.Business.Validation;

	using HomeCloud.Validation;
	using System;

	#endregion

	/// <summary>
	/// Provides methods to index file system in data storage.
	/// </summary>
	public class IndexingService : IIndexingService
	{
		#region Private Members

		/// <summary>
		/// The data provider factory
		/// </summary>
		private readonly IDataProviderFactory providerFactory = null;

		/// <summary>
		/// The validation service factory
		/// </summary>
		private readonly IValidationServiceFactory validationServiceFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="IndexingService" /> class.
		/// </summary>
		/// <param name="providerFactory">The data provider factory.</param>
		/// <param name="validationServiceFactory">The validation service factory.</param>
		public IndexingService(
			IDataProviderFactory providerFactory,
			IValidationServiceFactory validationServiceFactory)
		{
			this.providerFactory = providerFactory;
			this.validationServiceFactory = validationServiceFactory;
		}

		#endregion

		#region IIndexingService Implementations

		/// <summary>
		/// Indexes the content located in specified storage.
		/// </summary>
		/// <param name="storage">The storage to index.</param>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.Storage" />.
		/// </returns>
		public async Task<ServiceResult<Storage>> Index(Storage storage)
		{
			IServiceFactory<IStorageValidator> validator = this.validationServiceFactory.GetFactory<IStorageValidator>();
			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(storage);

			if (!result.IsValid)
			{
				return new ServiceResult<Storage>(storage)
				{
					Errors = result.Errors
				};
			}

			storage = await this.providerFactory.GetStorage(storage);
			await this.Index((Catalog)storage);

			return new ServiceResult<Storage>(storage);
		}

		/// <summary>
		/// Indexes the specified catalog and its content.
		/// </summary>
		/// <param name="catalog">The catalog to index.</param>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.Catalog" />.
		/// </returns>
		public async Task<ServiceResult<Catalog>> Index(Catalog catalog)
		{
			IDataProvider dataStoreProvider = this.providerFactory.GetProvider<IDataStoreProvider>();

			bool exists = await dataStoreProvider.CatalogExists(new Catalog()
			{
				Name = catalog.Name,
				Parent = catalog.Parent
			});

			catalog = !exists ? await this.providerFactory.CreateCatalog(catalog) : (await this.providerFactory.GetCatalogs(catalog.Parent, 0, 1, catalog)).FirstOrDefault();

			int limit = 20;
			int offset = 0;
			int count = 0;

			IDataProvider fileSystemProvider = this.providerFactory.GetProvider<IFileSystemProvider>();

			do
			{
				IPaginable<Catalog> catalogs = await fileSystemProvider.GetCatalogs(catalog, offset, limit);
				foreach (Catalog item in catalogs)
				{
					item.Parent = catalog;

					await this.Index(item);
				}

				offset = catalogs.Offset + catalogs.Limit;
				count = catalogs.TotalCount;
			}
			while (count >= offset);

			limit = 20;
			offset = 0;
			count = 0;

			do
			{
				IPaginable<CatalogEntry> entries = await fileSystemProvider.GetCatalogEntries(catalog, offset, limit);
				foreach (CatalogEntry item in entries)
				{
					item.Catalog = catalog;

					await this.Index(item);
				}

				offset = entries.Offset + entries.Limit;
				count = entries.TotalCount;
			}
			while (count >= offset);

			return new ServiceResult<Catalog>(catalog);
		}

		/// <summary>
		/// Indexes the specified catalog entry.
		/// </summary>
		/// <param name="entry">The catalog entry to index.</param>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.CatalogEntry" />.
		/// </returns>
		public async Task<ServiceResult<CatalogEntry>> Index(CatalogEntry entry)
		{
			IDataProvider dataStoreProvider = this.providerFactory.GetProvider<IDataStoreProvider>();
			if (!await dataStoreProvider.CatalogEntryExists(entry))
			{
				using (CatalogEntryStream stream = new CatalogEntryStream(entry, 0))
				{
					entry = await dataStoreProvider.CreateCatalogEntry(stream);
					entry = await this.providerFactory.GetProvider<IAggregationDataProvider>().CreateCatalogEntry(stream);
				}
			}

			return new ServiceResult<CatalogEntry>(entry);
		}

		#endregion
	}
}
