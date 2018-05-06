namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using System;
	
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.Core.Extensions;
	using HomeCloud.Data.MongoDB;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.DataAccess.Aggregation;
	using HomeCloud.DataStorage.DataAccess.Aggregation.Objects;

	using HomeCloud.Mapping;
	using HomeCloud.Mapping.Extensions;

	#endregion

	/// <summary>
	/// Provides methods to provide data from aggregated data source.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Providers.IAggregationDataProvider" />
	public class AggregationDataProvider : IAggregationDataProvider
	{
		#region Private Members

		/// <summary>
		/// The data context scope factory.
		/// </summary>
		private readonly IServiceFactory<IMongoDBRepository> repositoryFactory = null;

		/// <summary>
		/// The object mapper.
		/// </summary>
		private readonly IMapper mapper = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AggregationDataProvider" /> class.
		/// </summary>
		/// <param name="dataContextScopeFactory">The data context scope factory.</param>
		/// <param name="connectionStrings">The connection strings.</param>
		/// <param name="mapper">The mapper.</param>
		public AggregationDataProvider(
			IServiceFactory<IMongoDBRepository> repositoryFactory,
			IMapper mapper)
		{
			this.repositoryFactory = repositoryFactory;
			this.mapper = mapper;
		}

		#endregion

		#region IAggregationDataProvider Implementations

		#region Storage Methods

		/// <summary>
		/// Gets a value indicating whether the specified storage already exists.
		/// </summary>
		/// <param name="storage">The storage.</param>
		/// <returns><c>true</c> if the storage exists. Otherwise <c>false.</c></returns>
		public async Task<bool> StorageExists(Storage storage)
		{
			ICatalogDocumentRepository repository = this.repositoryFactory.GetService<ICatalogDocumentRepository>();
			CatalogDocument catalogDocument = await repository.GetAsync(storage.ID);

			return catalogDocument != null;
		}

		/// <summary>
		/// Creates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to create.</param>
		/// <returns>The newly created instance of <see cref="Storage" /> type.</returns>
		public async Task<Storage> CreateStorage(Storage storage)
		{
			CatalogDocument catalogDocument = this.mapper.MapNew<Storage, CatalogDocument>(storage);

			ICatalogDocumentRepository repository = this.repositoryFactory.GetService<ICatalogDocumentRepository>();
			catalogDocument = await repository.SaveAsync(catalogDocument);

			return this.mapper.Map(catalogDocument, storage);
		}

		/// <summary>
		/// Updates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to update.</param>
		/// <returns>The updated instance of <see cref="Storage"/> type.</returns>
		public async Task<Storage> UpdateStorage(Storage storage)
		{
			ICatalogDocumentRepository repository = this.repositoryFactory.GetService<ICatalogDocumentRepository>();

			CatalogDocument catalogDocument = await repository.GetAsync(storage.ID);
			catalogDocument = this.mapper.Map(storage, catalogDocument);

			catalogDocument = await repository.SaveAsync(catalogDocument);

			return this.mapper.Map(catalogDocument, storage);
		}

		/// <summary>
		/// Gets the list of storages.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>The list of instances of <see cref="Storage"/> type.</returns>
		public async Task<IPaginable<Storage>> GetStorages(int offset = 0, int limit = 20)
		{
			return await Task.FromException<IPaginable<Storage>>(new NotSupportedException());
		}

		/// <summary>
		/// Gets storage by initial instance set.
		/// </summary>
		/// <param name="storage">The initial storage set.</param>
		/// <returns>The instance of <see cref="Storage"/> type.</returns>
		public async Task<Storage> GetStorage(Storage storage)
		{
			ICatalogDocumentRepository repository = this.repositoryFactory.GetService<ICatalogDocumentRepository>();
			CatalogDocument catalogDocument = await repository.GetAsync(storage.ID);

			return this.mapper.Map(catalogDocument, storage);
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
			if (!string.IsNullOrWhiteSpace(storage.Path))
			{
				ICatalogDocumentRepository repository = this.repositoryFactory.GetService<ICatalogDocumentRepository>();
				await repository.DeleteAsync(data => data.Path != null && data.Path.StartsWith(storage.Path));
			}

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
			ICatalogDocumentRepository repository = this.repositoryFactory.GetService<ICatalogDocumentRepository>();
			CatalogDocument catalogDocument = await repository.GetAsync(catalog.ID);

			return catalogDocument != null;
		}

		/// <summary>
		/// Creates the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="Catalog" /> type to create.</param>
		/// <returns>The newly created instance of <see cref="Catalog" /> type.</returns>
		public async Task<Catalog> CreateCatalog(Catalog catalog)
		{
			CatalogDocument catalogDocument = this.mapper.MapNew<Catalog, CatalogDocument>(catalog);

			ICatalogDocumentRepository repository = this.repositoryFactory.GetService<ICatalogDocumentRepository>();
			catalogDocument = await repository.SaveAsync(catalogDocument);

			CatalogDocument parentCatalogDocument = null;
			if ((catalog.Parent?.ID).HasValue)
			{
				parentCatalogDocument = await repository.GetAsync(catalog.Parent.ID);
			}

			catalog = this.mapper.Map(catalogDocument, catalog);
			catalog.Parent = this.mapper.Map(parentCatalogDocument, catalog.Parent);

			return catalog;
		}

		/// <summary>
		/// Updates the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="Catalog" /> type to update.</param>
		/// <returns>The updated instance of <see cref="Catalog"/> type.</returns>
		public async Task<Catalog> UpdateCatalog(Catalog catalog)
		{
			ICatalogDocumentRepository repository = this.repositoryFactory.GetService<ICatalogDocumentRepository>();

			CatalogDocument catalogDocument = await repository.GetAsync(catalog.ID);
			catalogDocument = this.mapper.Map(catalog, catalogDocument);

			catalogDocument = await repository.SaveAsync(catalogDocument);

			CatalogDocument parentCatalogDocument = null;
			if ((catalog.Parent?.ID).HasValue)
			{
				parentCatalogDocument = await repository.GetAsync(catalog.Parent.ID);
			}

			catalog = this.mapper.Map(catalogDocument, catalog);
			catalog.Parent = this.mapper.Map(parentCatalogDocument, catalog.Parent);

			return catalog;
		}

		/// <summary>
		/// Gets the list of catalogs located in specified parent catalog and met specified catalog criteria.
		/// </summary>
		/// <param name="parent">The parent catalog of <see cref="CatalogRoot" /> type.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return. If set to 0 the empty collection with total count set up is returned.</param>
		/// <param name="criteria">The catalog criteria.</param>
		/// <returns>
		/// The list of instances of <see cref="Catalog" /> type.
		/// </returns>
		public async Task<IPaginable<Catalog>> GetCatalogs(CatalogRoot parent, int offset = 0, int limit = 20, Catalog criteria = null)
		{
			return await Task.FromException<IPaginable<Catalog>>(new NotSupportedException());
		}

		/// <summary>
		/// Gets the catalog by the initial instance set.
		/// </summary>
		/// <param name="catalog">The initial catalog set.</param>
		/// <returns>The instance of <see cref="Catalog"/> type.</returns>
		public async Task<Catalog> GetCatalog(Catalog catalog)
		{
			ICatalogDocumentRepository repository = this.repositoryFactory.GetService<ICatalogDocumentRepository>();
			CatalogDocument catalogDocument = await repository.GetAsync(catalog.ID);

			CatalogDocument parentCatalogDocument = null;
			if ((catalog.Parent?.ID).HasValue)
			{
				parentCatalogDocument = await repository.GetAsync(catalog.Parent.ID);
			}

			catalog = this.mapper.Map(catalogDocument, catalog);
			catalog.Parent = this.mapper.Map(parentCatalogDocument, catalog.Parent);

			return catalog;
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
			if (!string.IsNullOrWhiteSpace(catalog.Path))
			{
				ParallelExtensions.InvokeAsync(
				async () =>
				{
					ICatalogDocumentRepository catalogRepository = this.repositoryFactory.GetService<ICatalogDocumentRepository>();
					await catalogRepository.DeleteAsync(data => data.Path != null && data.Path.StartsWith(catalog.Path));
				},
				async () =>
				{
					IFileDocumentRepository fileRepository = this.repositoryFactory.GetService<IFileDocumentRepository>();
					await fileRepository.DeleteAsync(data => data.Path != null && data.Path.StartsWith(catalog.Path));
				});
			}

			return await Task.FromResult(catalog);
		}

		/// <summary>
		/// Recalculates the size of the specified catalog.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		/// <returns>The updated instance of <see cref="Catalog"/>.</returns>
		public async Task<Catalog> RecalculateSize(Catalog catalog)
		{
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
			IFileDocumentRepository repository = this.repositoryFactory.GetService<IFileDocumentRepository>();

			FileDocument file = await repository.GetAsync(entry.ID);

			return file != null;
		}

		/// <summary>
		/// Creates the specified catalog entry.
		/// </summary>
		/// <param name="stream">The stream of <see cref="CatalogEntryStream" /> type to create the entry from.</param>
		/// <returns>The newly created instance of <see cref="CatalogEntry" /> type.</returns>
		public async Task<CatalogEntry> CreateCatalogEntry(CatalogEntryStream stream)
		{
			CatalogEntry entry = stream.Entry;

			FileDocument fileDocument = this.mapper.MapNew<CatalogEntry, FileDocument>(entry);
			CatalogDocument catalogDocument = null;

			IFileDocumentRepository fileRepository = this.repositoryFactory.GetService<IFileDocumentRepository>();
			fileDocument = await fileRepository.SaveAsync(fileDocument);

			if ((entry.Catalog?.ID).HasValue)
			{
				ICatalogDocumentRepository catalogRepository = this.repositoryFactory.GetService<ICatalogDocumentRepository>();
				catalogDocument = await catalogRepository.GetAsync(entry.Catalog.ID);
			}

			entry = this.mapper.Map(fileDocument, entry);
			entry.Catalog = this.mapper.Map(catalogDocument, entry.Catalog);

			return entry;
		}

		/// <summary>
		/// Gets the list of catalog entries located in specified catalog and met specified catalog entry criteria.
		/// </summary>
		/// <param name="catalog">The catalog of <see cref="CatalogRoot" /> type.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return. If set to 0 the empty collection with total count set up is returned.</param>
		/// <param name="criteria">The catalog entry criteria.</param>
		/// <returns>
		/// The list of instances of <see cref="CatalogEntry" /> type.
		/// </returns>
		public async Task<IPaginable<CatalogEntry>> GetCatalogEntries(CatalogRoot catalog, int offset = 0, int limit = 20, CatalogEntry criteria = null)
		{
			return await Task.FromException<IPaginable<CatalogEntry>>(new NotSupportedException());
		}

		/// <summary>
		/// Gets the catalog entry by the initial instance set.
		/// </summary>
		/// <param name="entry">The initial catalog entry set.</param>
		/// <returns>The instance of <see cref="CatalogEntry"/> type.</returns>
		public async Task<CatalogEntry> GetCatalogEntry(CatalogEntry entry)
		{
			IFileDocumentRepository fileRepository = this.repositoryFactory.GetService<IFileDocumentRepository>();
			FileDocument fileDocument = await fileRepository.GetAsync(entry.ID);

			CatalogDocument catalogDocument = null;
			if ((entry.Catalog?.ID).HasValue)
			{
				ICatalogDocumentRepository catalogRepository = this.repositoryFactory.GetService<ICatalogDocumentRepository>();
				catalogDocument = await catalogRepository.GetAsync(entry.Catalog.ID);
			}

			entry = this.mapper.Map(fileDocument, entry);
			entry.Catalog = this.mapper.Map(catalogDocument, entry.Catalog);

			return entry;
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
			return await Task.FromException<CatalogEntryStream>(new NotSupportedException());
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
			IFileDocumentRepository repository = this.repositoryFactory.GetService<IFileDocumentRepository>();

			await repository.DeleteAsync(entry.ID);

			return entry;
		}

		#endregion

		#endregion
	}
}
