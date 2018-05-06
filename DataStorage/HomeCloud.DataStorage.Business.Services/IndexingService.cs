namespace HomeCloud.DataStorage.Business.Services
{
	using System.Linq;
	#region Usings

	using System.Threading.Tasks;
	using HomeCloud.Core;
	using HomeCloud.Core.Extensions;
	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Providers;

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

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="IndexingService" /> class.
		/// </summary>
		/// <param name="providerFactory">The data provider factory.</param>
		public IndexingService(IDataProviderFactory providerFactory)
		{
			this.providerFactory = providerFactory;
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
		public async Task<Storage> Index(Storage storage)
		{
			await this.Index((Catalog)storage);

			return storage;
		}

		/// <summary>
		/// Indexes the specified catalog and its content.
		/// </summary>
		/// <param name="catalog">The catalog to index.</param>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.Catalog" />.
		/// </returns>
		public async Task<Catalog> Index(Catalog catalog)
		{
			IDataProvider dataStoreProvider = this.providerFactory.GetProvider<IDataStoreProvider>();
			if (!await dataStoreProvider.CatalogExists(catalog))
			{
				catalog = await this.providerFactory.CreateCatalog(catalog);
			}

			int limit = 20;
			int offset = 0;
			int count = 0;

			do
			{
				IPaginable<Catalog> catalogs = await this.providerFactory.GetProvider<IFileSystemProvider>().GetCatalogs(catalog, null, offset, limit);
				catalogs.ForEachAsync(async item =>
				{
					item.Parent = catalog;

					await this.Index(item);
				}, catalogs.Count());

				offset = catalogs.Offset + catalogs.Limit;
				count = catalogs.TotalCount;
			}
			while (count >= offset);

			limit = 20;
			offset = 0;
			count = 0;

			do
			{
				IPaginable<CatalogEntry> entries = await this.providerFactory.GetProvider<IFileSystemProvider>().GetCatalogEntries(catalog, null, offset, limit);
				entries.ForEachAsync(async item =>
				{
					item.Catalog = catalog;

					await this.Index(item);
				}, entries.Count());

				offset = entries.Offset + entries.Limit;
				count = entries.TotalCount;
			}
			while (count >= offset);

			return catalog;
		}

		/// <summary>
		/// Indexes the specified catalog entry.
		/// </summary>
		/// <param name="entry">The catalog entry to index.</param>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.CatalogEntry" />.
		/// </returns>
		public async Task<CatalogEntry> Index(CatalogEntry entry)
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

			return entry;
		}

		#endregion
	}
}
