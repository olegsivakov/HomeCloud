namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using System.Threading.Tasks;

	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Defines methods to index file system in data storage.
	/// </summary>
	public interface IIndexingService
	{
		/// <summary>
		/// Indexes the content located in specified storage.
		/// </summary>
		/// <param name="storage">The storage to index.</param>
		/// <returns>The instance of <see cref="Storage"/>.</returns>
		Task<Storage> Index(Storage storage);

		/// <summary>
		/// Indexes the specified catalog and its content.
		/// </summary>
		/// <param name="catalog">The catalog to index.</param>
		/// <returns>The instance of <see cref="Catalog"/>.</returns>
		Task<Catalog> Index(Catalog catalog);

		/// <summary>
		/// Indexes the specified catalog entry.
		/// </summary>
		/// <param name="entry">The catalog entry to index.</param>
		/// <returns>The instance of <see cref="CatalogEntry"/>.</returns>
		Task<CatalogEntry> Index(CatalogEntry entry);
	}
}
