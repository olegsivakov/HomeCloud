namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using System;
	
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.Core.Extensions;

	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataAccess.Services.Factories;

	using HomeCloud.DataStorage.Api.Configuration;
	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.DataAccess.Services.Repositories;

	using HomeCloud.Mapping;

	using Microsoft.Extensions.Options;

	using CatalogDocument = HomeCloud.DataStorage.DataAccess.Contracts.CatalogDocument;
	using FileDocument = HomeCloud.DataStorage.DataAccess.Contracts.FileDocument;

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
		private readonly IDataContextScopeFactory dataContextScopeFactory = null;

		/// <summary>
		/// The connection strings.
		/// </summary>
		private readonly ConnectionStrings connectionStrings = null;

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
			IDataContextScopeFactory dataContextScopeFactory,
			IOptionsSnapshot<ConnectionStrings> connectionStrings,
			IMapper mapper)
		{
			this.dataContextScopeFactory = dataContextScopeFactory;
			this.connectionStrings = connectionStrings?.Value;

			this.mapper = mapper;
		}

		#endregion

		#region IDataStoreProvider Implementations

		#region Storage Methods

		/// <summary>
		/// Gets a value indicating whether the specified storage already exists.
		/// </summary>
		/// <param name="storage">The storage.</param>
		/// <returns><c>true</c> if the storage exists. Otherwise <c>false.</c></returns>
		public async Task<bool> StorageExists(Storage storage)
		{
			using (IDocumentContextScope scope = this.dataContextScopeFactory.CreateDocumentContextScope(this.connectionStrings.DataAggregationDB))
			{
				ICatalogDocumentRepository repository = scope.GetRepository<ICatalogDocumentRepository>();
				CatalogDocument catalogDocument = await repository.GetAsync(storage.ID);

				return catalogDocument != null;
			}
		}

		/// <summary>
		/// Creates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to create.</param>
		/// <returns>The newly created instance of <see cref="Storage" /> type.</returns>
		public async Task<Storage> CreateStorage(Storage storage)
		{
			CatalogDocument catalogDocument = null;

			using (IDocumentContextScope scope = this.dataContextScopeFactory.CreateDocumentContextScope(this.connectionStrings.DataAggregationDB))
			{
				ICatalogDocumentRepository repository = scope.GetRepository<ICatalogDocumentRepository>();

				catalogDocument = await this.mapper.MapNewAsync<Storage, CatalogDocument>(storage);
				catalogDocument = await repository.SaveAsync(catalogDocument);
			}

			return await this.mapper.MapAsync(catalogDocument, storage);
		}

		/// <summary>
		/// Updates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to update.</param>
		/// <returns>The updated instance of <see cref="Storage"/> type.</returns>
		public async Task<Storage> UpdateStorage(Storage storage)
		{
			CatalogDocument catalogDocument = null;

			using (IDocumentContextScope scope = this.dataContextScopeFactory.CreateDocumentContextScope(this.connectionStrings.DataAggregationDB))
			{
				ICatalogDocumentRepository repository = scope.GetRepository<ICatalogDocumentRepository>();

				catalogDocument = await repository.GetAsync(storage.ID);
				catalogDocument = await this.mapper.MapAsync(storage, catalogDocument);

				if (catalogDocument.IsChanged)
				{
					catalogDocument = await repository.SaveAsync(catalogDocument);
				}
			}

			return await this.mapper.MapAsync(catalogDocument, storage);
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
			CatalogDocument catalogDocument = null;

			using (IDocumentContextScope scope = this.dataContextScopeFactory.CreateDocumentContextScope(this.connectionStrings.DataAggregationDB))
			{
				ICatalogDocumentRepository repository = scope.GetRepository<ICatalogDocumentRepository>();
				catalogDocument = await repository.GetAsync(storage.ID);
			}

			return await this.mapper.MapAsync(catalogDocument, storage);
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
			using (IDocumentContextScope scope = this.dataContextScopeFactory.CreateDocumentContextScope(this.connectionStrings.DataAggregationDB))
			{
				ICatalogDocumentRepository repository = scope.GetRepository<ICatalogDocumentRepository>();

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
			using (IDocumentContextScope scope = this.dataContextScopeFactory.CreateDocumentContextScope(this.connectionStrings.DataAggregationDB))
			{
				ICatalogDocumentRepository repository = scope.GetRepository<ICatalogDocumentRepository>();

				return (await repository.GetAsync(catalog.ID) != null);
			}
		}

		/// <summary>
		/// Creates the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="Catalog" /> type to create.</param>
		/// <returns>The newly created instance of <see cref="Catalog" /> type.</returns>
		public async Task<Catalog> CreateCatalog(Catalog catalog)
		{
			CatalogDocument catalogDocument = await this.mapper.MapNewAsync<Catalog, CatalogDocument>(catalog);
			CatalogDocument parentCatalogDocument = null;

			using (IDocumentContextScope scope = this.dataContextScopeFactory.CreateDocumentContextScope(this.connectionStrings.DataAggregationDB))
			{
				ICatalogDocumentRepository repository = scope.GetRepository<ICatalogDocumentRepository>();
				catalogDocument = await repository.SaveAsync(catalogDocument);

				if ((catalog.Parent?.ID).HasValue)
				{
					parentCatalogDocument = await repository.GetAsync(catalog.Parent.ID);
				}
			}

			catalog = await this.mapper.MapAsync(catalogDocument, catalog);
			catalog.Parent = await this.mapper.MapAsync(parentCatalogDocument, catalog.Parent as Catalog);

			return catalog;
		}

		/// <summary>
		/// Updates the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="Catalog" /> type to update.</param>
		/// <returns>The updated instance of <see cref="Catalog"/> type.</returns>
		public async Task<Catalog> UpdateCatalog(Catalog catalog)
		{
			CatalogDocument catalogDocument = null;
			CatalogDocument parentCatalogDocument = null;

			using (IDocumentContextScope scope = this.dataContextScopeFactory.CreateDocumentContextScope(this.connectionStrings.DataAggregationDB))
			{
				ICatalogDocumentRepository repository = scope.GetRepository<ICatalogDocumentRepository>();

				catalogDocument = await repository.GetAsync(catalog.ID);
				catalogDocument = await this.mapper.MapAsync(catalog, catalogDocument);

				if (catalogDocument.IsChanged)
				{
					catalogDocument = await repository.SaveAsync(catalogDocument);
				}

				if ((catalog.Parent?.ID).HasValue)
				{
					parentCatalogDocument = await repository.GetAsync(catalog.Parent.ID);
				}
			}

			catalog = await this.mapper.MapAsync(catalogDocument, catalog);
			catalog.Parent = await this.mapper.MapAsync(parentCatalogDocument, catalog.Parent as Catalog);

			return catalog;
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
			return await Task.FromException<IPaginable<Catalog>>(new NotSupportedException());
		}

		/// <summary>
		/// Gets the catalog by the initial instance set.
		/// </summary>
		/// <param name="catalog">The initial catalog set.</param>
		/// <returns>The instance of <see cref="Catalog"/> type.</returns>
		public async Task<Catalog> GetCatalog(Catalog catalog)
		{
			CatalogDocument catalogDocument = null;
			CatalogDocument parentCatalogDocument = null;

			using (IDocumentContextScope scope = this.dataContextScopeFactory.CreateDocumentContextScope(this.connectionStrings.DataAggregationDB))
			{
				ICatalogDocumentRepository repository = scope.GetRepository<ICatalogDocumentRepository>();
				catalogDocument = await repository.GetAsync(catalog.ID);

				if ((catalog.Parent?.ID).HasValue)
				{
					parentCatalogDocument = await repository.GetAsync(catalog.Parent.ID);
				}
			}

			catalog = await this.mapper.MapAsync(catalogDocument, catalog);
			catalog.Parent = await this.mapper.MapAsync(parentCatalogDocument, catalog.Parent as Catalog);

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
			using (IDocumentContextScope scope = this.dataContextScopeFactory.CreateDocumentContextScope(this.connectionStrings.DataAggregationDB))
			{
				ICatalogDocumentRepository repository = scope.GetRepository<ICatalogDocumentRepository>();

				await repository.DeleteAsync(data => data.Path != null && data.Path.StartsWith(catalog.Path));
			}

			return catalog;
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
			using (IDocumentContextScope scope = this.dataContextScopeFactory.CreateDocumentContextScope(this.connectionStrings.DataAggregationDB))
			{
				IFileDocumentRepository repository = scope.GetRepository<IFileDocumentRepository>();

				return (await repository.GetAsync(entry.ID)) != null;
			}
		}

		/// <summary>
		/// Creates the specified catalog entry.
		/// </summary>
		/// <param name="stream">The stream of <see cref="CatalogEntryStream" /> type to create the entry from.</param>
		/// <returns>The newly created instance of <see cref="CatalogEntry" /> type.</returns>
		public async Task<CatalogEntry> CreateCatalogEntry(CatalogEntryStream stream)
		{
			CatalogEntry entry = stream.Entry;

			FileDocument fileDocument = await this.mapper.MapNewAsync<CatalogEntry, FileDocument>(entry);
			CatalogDocument catalogDocument = null;

			using (IDocumentContextScope scope = this.dataContextScopeFactory.CreateDocumentContextScope(this.connectionStrings.DataAggregationDB))
			{
				IFileDocumentRepository fileRepository = scope.GetRepository<IFileDocumentRepository>();
				fileDocument = await fileRepository.SaveAsync(fileDocument);

				if ((entry.Catalog?.ID).HasValue)
				{
					ICatalogDocumentRepository catalogRepository = scope.GetRepository<ICatalogDocumentRepository>();
					catalogDocument = await catalogRepository.GetAsync(entry.Catalog.ID);
				}
			}

			entry = await this.mapper.MapAsync(fileDocument, entry);
			entry.Catalog = await this.mapper.MapAsync(catalogDocument, entry.Catalog as Catalog);

			return entry;
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
			return await Task.FromException<IPaginable<CatalogEntry>>(new NotSupportedException());
		}

		/// <summary>
		/// Gets the catalog entry by the initial instance set.
		/// </summary>
		/// <param name="entry">The initial catalog entry set.</param>
		/// <returns>The instance of <see cref="CatalogEntry"/> type.</returns>
		public async Task<CatalogEntry> GetCatalogEntry(CatalogEntry entry)
		{
			FileDocument fileDocument = null;
			CatalogDocument catalogDocument = null;

			using (IDocumentContextScope scope = this.dataContextScopeFactory.CreateDocumentContextScope(this.connectionStrings.DataAggregationDB))
			{
				IFileDocumentRepository fileRepository = scope.GetRepository<IFileDocumentRepository>();
				fileDocument = await fileRepository.GetAsync(entry.ID);

				if ((entry.Catalog?.ID).HasValue)
				{
					ICatalogDocumentRepository catalogRepository = scope.GetRepository<ICatalogDocumentRepository>();
					catalogDocument = await catalogRepository.GetAsync(entry.Catalog.ID);
				}
			}

			entry = await this.mapper.MapAsync(fileDocument, entry);
			entry.Catalog = await this.mapper.MapAsync(catalogDocument, entry.Catalog as Catalog);

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
			using (IDocumentContextScope scope = this.dataContextScopeFactory.CreateDocumentContextScope(this.connectionStrings.DataAggregationDB))
			{
				IFileDocumentRepository repository = scope.GetRepository<IFileDocumentRepository>();

				await repository.DeleteAsync(entry.ID);
			}

			return entry;
		}

		#endregion

		#endregion
	}
}
