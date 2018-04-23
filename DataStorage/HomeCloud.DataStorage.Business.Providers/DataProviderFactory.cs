namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using System;
	using System.Linq;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.Core.Extensions;

	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Provides factory methods to provide data.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Providers.IDataProviderFactory" />
	public class DataProviderFactory : IDataProviderFactory
	{
		#region Private Methods

		/// <summary>
		/// The data provider factory
		/// </summary>
		private readonly IServiceFactory<IDataProvider> providerFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DataProviderFactory" /> class.
		/// </summary>
		/// <param name="providerFactory">The provider factory.</param>
		/// <param name="mapper">The mapper.</param>
		public DataProviderFactory(
			IServiceFactory<IDataProvider> providerFactory)
		{
			this.providerFactory = providerFactory;
		}

		#endregion

		#region IDataProviderFactory Implementations

		#region Storage Methods

		/// <summary>
		/// Gets a value indicating whether the specified storage already exists.
		/// </summary>
		/// <param name="storage">The storage.</param>
		/// <returns><c>true</c> if the storage exists. Otherwise <c>false.</c></returns>
		public async Task<bool> StorageExists(Storage storage)
		{
			return await this.providerFactory.Get<IDataStoreProvider>().StorageExists(storage);
		}

		/// <summary>
		/// Creates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to create.</param>
		/// <returns>The newly created instance of <see cref="Storage" /> type.</returns>
		public async Task<Storage> CreateStorage(Storage storage)
		{
			ParallelExtensions.InvokeAsync(
					async () => storage = await this.providerFactory.Get<IFileSystemProvider>().CreateStorage(storage),
					async () => storage = await this.providerFactory.Get<IDataStoreProvider>().CreateStorage(storage));

			return await this.providerFactory.Get<IAggregationDataProvider>().CreateStorage(storage);
		}

		/// <summary>
		/// Deletes the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to delete.</param>
		/// <returns>
		/// The deleted instance of <see cref="Storage"/> type.
		/// </returns>
		public async Task<Storage> DeleteStorage(Storage storage)
		{
			ParallelExtensions.InvokeAsync(
					async () => storage = await this.providerFactory.Get<IAggregationDataProvider>().DeleteStorage(storage),
					async () => storage = await this.providerFactory.Get<IDataStoreProvider>().DeleteStorage(storage),
					async () => storage = await this.providerFactory.Get<IFileSystemProvider>().DeleteStorage(storage));

			return await Task.FromResult(storage);
		}

		/// <summary>
		/// Gets storage by initial instance set.
		/// </summary>
		/// <param name="storage">The initial storage set.</param>
		/// <returns>The instance of <see cref="Storage"/> type.</returns>
		public async Task<Storage> GetStorage(Storage storage)
		{
			if (storage.ID == Guid.Empty)
			{
				return null;
			}

			ParallelExtensions.InvokeAsync(
					async () => storage = await this.providerFactory.Get<IAggregationDataProvider>().GetStorage(storage),
					async () => storage = await this.providerFactory.Get<IDataStoreProvider>().GetStorage(storage));

			return await Task.FromResult(storage);
		}

		/// <summary>
		/// Gets the list of storages.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>The list of instances of <see cref="Storage"/> type.</returns>
		public async Task<IPaginable<Storage>> GetStorages(int offset = 0, int limit = 20)
		{
			IPaginable<Storage> result = await this.providerFactory.Get<IDataStoreProvider>().GetStorages(offset, limit);
			result.AsParallel().ForAll(async item => item = await this.providerFactory.Get<IAggregationDataProvider>().GetStorage(item));

			return result;
		}

		/// <summary>
		/// Updates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to update.</param>
		/// <returns>The updated instance of <see cref="Storage"/> type.</returns>
		public async Task<Storage> UpdateStorage(Storage storage)
		{
			ParallelExtensions.InvokeAsync(
					async () => storage = await this.providerFactory.Get<IDataStoreProvider>().UpdateStorage(storage),
					async () => storage = await this.providerFactory.Get<IFileSystemProvider>().UpdateStorage(storage));

			storage = await this.providerFactory.Get<IAggregationDataProvider>().UpdateStorage(storage);

			return storage;
		}

		#endregion

		#region Catalog Methods

		/// <summary>
		/// Gets a value indicating whether the specified catalog already exists.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		/// <returns><c>true</c> if the catalog exists. Otherwise <c>false.</c></returns>
		public async Task<bool> CatalogExists(Catalog catalog)
		{
			return await this.providerFactory.Get<IDataStoreProvider>().CatalogExists(catalog);
		}

		/// <summary>
		/// Creates the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="Catalog" /> type to create.</param>
		/// <returns>The newly created instance of <see cref="Catalog" /> type.</returns>
		public async Task<Catalog> CreateCatalog(Catalog catalog)
		{
			catalog.Parent = await this.providerFactory.Get<IAggregationDataProvider>().GetCatalog(catalog.Parent);

			ParallelExtensions.InvokeAsync(
					async () => catalog = await this.providerFactory.Get<IFileSystemProvider>().CreateCatalog(catalog),
					async () => catalog = await this.providerFactory.Get<IDataStoreProvider>().CreateCatalog(catalog));

			return await this.providerFactory.Get<IAggregationDataProvider>().CreateCatalog(catalog);
		}

		/// <summary>
		/// Deletes the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="Catalog" /> type to delete.</param>
		/// <returns>
		/// The deleted instance of <see cref="Catalog"/> type.
		/// </returns>
		public async Task<Catalog> DeleteCatalog(Catalog catalog)
		{
			ParallelExtensions.InvokeAsync(
					async () => catalog = await this.providerFactory.Get<IAggregationDataProvider>().DeleteCatalog(catalog),
					async () => catalog = await this.providerFactory.Get<IDataStoreProvider>().DeleteCatalog(catalog),
					async () => catalog = await this.providerFactory.Get<IFileSystemProvider>().DeleteCatalog(catalog));

			return await Task.FromResult(catalog);
		}

		/// <summary>
		/// Gets the list of catalogs located in specified parent catalog.
		/// </summary>
		/// <param name="parent">The parent catalog of <see cref="CatalogRoot"/> type.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The list of instances of <see cref="Catalog" /> type.
		/// </returns>
		public async Task<IPaginable<Catalog>> GetCatalogs(CatalogRoot parent, int offset = 0, int limit = 20)
		{
			IPaginable<Catalog> result = await this.providerFactory.Get<IDataStoreProvider>().GetCatalogs(parent, offset, limit);
			result.AsParallel().ForAll(async item => item = await this.providerFactory.Get<IAggregationDataProvider>().GetCatalog(item));

			return result;
		}

		/// <summary>
		/// Gets the catalog by the initial instance set.
		/// </summary>
		/// <param name="catalog">The initial catalog set.</param>
		/// <returns>The instance of <see cref="Catalog"/> type.</returns>
		public async Task<Catalog> GetCatalog(Catalog catalog)
		{
			if (catalog.ID == Guid.Empty)
			{
				return null;
			}

			ParallelExtensions.InvokeAsync(
					async () => catalog = await this.providerFactory.Get<IAggregationDataProvider>().GetCatalog(catalog),
					async () => catalog = await this.providerFactory.Get<IDataStoreProvider>().GetCatalog(catalog));

			return await Task.FromResult(catalog);
		}

		/// <summary>
		/// Updates the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="Catalog" /> type to update.</param>
		/// <returns>The updated instance of <see cref="Catalog"/> type.</returns>
		public async Task<Catalog> UpdateCatalog(Catalog catalog)
		{
			catalog.Parent = await this.providerFactory.Get<IAggregationDataProvider>().GetCatalog(catalog.Parent);

			ParallelExtensions.InvokeAsync(
					async () => catalog = await this.providerFactory.Get<IDataStoreProvider>().UpdateCatalog(catalog),
					async () => catalog = await this.providerFactory.Get<IFileSystemProvider>().UpdateCatalog(catalog));

			catalog = await this.providerFactory.Get<IAggregationDataProvider>().UpdateCatalog(catalog);

			return catalog;
		}

		/// <summary>
		/// Recalculates the size of the specified catalog.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		/// <returns>The updated instance of <see cref="Catalog"/>.</returns>
		public async Task<Catalog> RecalculateSize(Catalog catalog)
		{
			if (catalog.ID == Guid.Empty)
			{
				return catalog;
			}

			IAggregationDataProvider aggregationSourceProvider = this.providerFactory.GetService<IAggregationDataProvider>();

			catalog = await aggregationSourceProvider.RecalculateSize(catalog);
			catalog = await this.providerFactory.GetService<IFileSystemProvider>().RecalculateSize(catalog);
			catalog = await aggregationSourceProvider.UpdateCatalog(catalog);

			if (catalog.Parent != null && catalog.Parent.ID != Guid.Empty)
			{
				await this.RecalculateSize(catalog.Parent);
			}

			return await this.GetCatalog(catalog);
		}

		#endregion

		#region CatalogEntry Methods

		/// <summary>
		/// Gets a value indicating whether the specified catalog entry already exists.
		/// </summary>
		/// <param name="entry">The catalog entry.</param>
		/// <returns><c>true</c> if the catalog entry exists. Otherwise <c>false.</c></returns>
		public async Task<bool> CatalogEntryExists(CatalogEntry entry)
		{
			return await this.providerFactory.Get<IDataStoreProvider>().CatalogEntryExists(entry);
		}

		/// <summary>
		/// Creates the specified catalog entry.
		/// </summary>
		/// <param name="stream">The stream of <see cref="CatalogEntryStream" /> type to create the entry from.</param>
		/// <returns>The newly created instance of <see cref="CatalogEntry" /> type.</returns>
		public async Task<CatalogEntry> CreateCatalogEntry(CatalogEntryStream stream)
		{
			stream.Entry.Catalog = await this.providerFactory.Get<IAggregationDataProvider>().GetCatalog(stream.Entry.Catalog);

			CatalogEntry result = null;

			ParallelExtensions.InvokeAsync(
					async () => result = await this.providerFactory.Get<IFileSystemProvider>().CreateCatalogEntry(stream),
					async () => result = await this.providerFactory.Get<IDataStoreProvider>().CreateCatalogEntry(stream));

			return await this.providerFactory.Get<IAggregationDataProvider>().CreateCatalogEntry(stream);
		}

		/// <summary>
		/// Deletes the specified catalog entry.
		/// </summary>
		/// <param name="entry">The instance of <see cref="CatalogEntry" /> type to delete.</param>
		/// <returns>
		/// The deleted instance of <see cref="CatalogEntry"/> type.
		/// </returns>
		public async Task<CatalogEntry> DeleteCatalogEntry(CatalogEntry entry)
		{
			ParallelExtensions.InvokeAsync(
					async () => entry = await this.providerFactory.Get<IAggregationDataProvider>().DeleteCatalogEntry(entry),
					async () => entry = await this.providerFactory.Get<IDataStoreProvider>().DeleteCatalogEntry(entry),
					async () => entry = await this.providerFactory.Get<IFileSystemProvider>().DeleteCatalogEntry(entry));

			return await Task.FromResult(entry);
		}

		/// <summary>
		/// Gets the list of catalog entries located in specified catalog.
		/// </summary>
		/// <param name="catalog">The catalog of <see cref="CatalogRoot"/> type.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The list of instances of <see cref="CatalogEntry" /> type.
		/// </returns>
		public async Task<IPaginable<CatalogEntry>> GetCatalogEntries(CatalogRoot catalog, int offset = 0, int limit = 20)
		{
			IPaginable<CatalogEntry> result = await this.providerFactory.Get<IDataStoreProvider>().GetCatalogEntries(catalog, offset, limit);
			result.AsParallel().ForAll(async item => item = await this.providerFactory.Get<IAggregationDataProvider>().GetCatalogEntry(item));

			return result;
		}

		/// <summary>
		/// Gets the catalog entry by the initial instance set.
		/// </summary>
		/// <param name="entry">The initial catalog entry set.</param>
		/// <returns>The instance of <see cref="CatalogEntry"/> type.</returns>
		public async Task<CatalogEntry> GetCatalogEntry(CatalogEntry entry)
		{
			if (entry.ID == Guid.Empty)
			{
				return null;
			}

			ParallelExtensions.InvokeAsync(
					async () => entry = await this.providerFactory.Get<IAggregationDataProvider>().GetCatalogEntry(entry),
					async () => entry = await this.providerFactory.Get<IDataStoreProvider>().GetCatalogEntry(entry));

			return await Task.FromResult(entry);
		}

		/// <summary>
		/// Gets the catalog entry by the initial instance set.
		/// </summary>
		/// <param name="entry">The initial catalog entry set.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="length">The number of bytes from byte array to return.</param>
		/// <returns>
		/// The instance of <see cref="CatalogEntry" /> type.
		/// </returns>
		public async Task<CatalogEntryStream> GetCatalogEntryStream(CatalogEntry entry, int offset = 0, int length = 0)
		{
			CatalogEntryStream result = null;

			ParallelExtensions.InvokeAsync(
					async () => entry = await this.providerFactory.Get<IAggregationDataProvider>().GetCatalogEntry(entry),
					async () => entry = await this.providerFactory.Get<IDataStoreProvider>().GetCatalogEntry(entry),
					async () => result = await this.providerFactory.Get<IDataStoreProvider>().GetCatalogEntryStream(entry));

			return await Task.FromResult(result);
		}

		#endregion

		#endregion
	}
}
