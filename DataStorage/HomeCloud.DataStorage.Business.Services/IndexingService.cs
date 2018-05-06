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
			int limit = 20;
			int offset = 0;
			int count = 0;

			CatalogRoot root = await this.providerFactory.GetStorage(storage);

			do
			{
				IPaginable<Catalog> catalogs = await this.providerFactory.GetProvider<IFileSystemProvider>().GetCatalogs(root, offset, limit);
				catalogs.ForEachAsync(async item =>
				{
					await this.Index(item);
				},catalogs.Count());

				offset = catalogs.Offset + catalogs.Limit;
				count = catalogs.TotalCount;
			}
			while (count >= offset);
			
			throw new System.NotImplementedException();
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
			throw new System.NotImplementedException();
		}

		#endregion
	}
}
