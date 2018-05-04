namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using System.Threading.Tasks;

	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Provides methods to index file system in data storage.
	/// </summary>
	public class IndexingService : IIndexingService
	{
		#region Private Members

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="IndexingService"/> class.
		/// </summary>
		public IndexingService()
		{
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
		public Task<Storage> Index(Storage storage)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Indexes the specified catalog and its content.
		/// </summary>
		/// <param name="catalog">The catalog to index.</param>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.Catalog" />.
		/// </returns>
		public Task<Catalog> Index(Catalog catalog)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Indexes the specified catalog entry.
		/// </summary>
		/// <param name="entry">The catalog entry to index.</param>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.CatalogEntry" />.
		/// </returns>
		public Task<CatalogEntry> Index(CatalogEntry entry)
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}
