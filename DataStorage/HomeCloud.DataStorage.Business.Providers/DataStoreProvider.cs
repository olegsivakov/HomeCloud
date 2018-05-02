namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.Core.Extensions;

	using HomeCloud.Data.SqlServer;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.DataAccess;

	using HomeCloud.Mapping;
	using HomeCloud.Mapping.Extensions;

	using CatalogContract = HomeCloud.DataStorage.DataAccess.Objects.Catalog;
	using FileContract = HomeCloud.DataStorage.DataAccess.Objects.File;
	using StorageContract = HomeCloud.DataStorage.DataAccess.Objects.Storage;

	#endregion

	/// <summary>
	///  Provides methods to provide data from data store database.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Providers.IDataStoreProvider" />
	public class DataStoreProvider : IDataStoreProvider
	{
		#region Private Members

		/// <summary>
		/// The data context scope factory.
		/// </summary>
		private readonly ISqlServerDBContextScope dataContextScope = null;

		/// <summary>
		/// The object mapper.
		/// </summary>
		private readonly IMapper mapper = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DataStoreProvider" /> class.
		/// </summary>
		/// <param name="dataContextScope">The data context scope.</param>
		/// <param name="mapper">The mapper.</param>
		public DataStoreProvider(
			ISqlServerDBContextScope dataContextScope,
			IMapper mapper)
		{
			this.dataContextScope = dataContextScope;
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
			StorageContract contract = this.mapper.MapNew<Storage, StorageContract>(storage);

			if (string.IsNullOrWhiteSpace(contract.Name) && contract.ID == Guid.Empty)
			{
				return false;
			}

			IStorageRepository storageRepository = this.dataContextScope.GetRepository<IStorageRepository>();

			if (!string.IsNullOrWhiteSpace(contract.Name))
			{
				StorageContract storageResult = (await storageRepository.FindAsync(contract, 0, 1)).FirstOrDefault();

				return !(storageResult is null || (contract.ID != Guid.Empty && contract.ID == storageResult.ID));
			}

			return (await storageRepository.GetAsync(contract.ID)) != null;
		}

		/// <summary>
		/// Creates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to create.</param>
		/// <returns>The newly created instance of <see cref="Storage" /> type.</returns>
		public async Task<Storage> CreateStorage(Storage storage)
		{
			CatalogContract catalogContract = this.mapper.MapNew<Storage, CatalogContract>(storage);

			this.dataContextScope.Begin();

			ICatalogRepository catalogRepository = this.dataContextScope.GetRepository<ICatalogRepository>();
			catalogContract = await catalogRepository.SaveAsync(catalogContract);

			StorageContract storageContract = this.mapper.MapNew<Storage, StorageContract>(storage);
			storageContract.ID = catalogContract.ID;

			IStorageRepository storageRepository = this.dataContextScope.GetRepository<IStorageRepository>();
			storageContract = await storageRepository.SaveAsync(storageContract);

			this.dataContextScope.Commit();

			storage = this.mapper.Map(storageContract, storage);
			storage = this.mapper.Map(catalogContract, storage);

			return storage;
		}

		/// <summary>
		/// Updates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to update.</param>
		/// <returns>The updated instance of <see cref="Storage"/> type.</returns>
		public async Task<Storage> UpdateStorage(Storage storage)
		{
			StorageContract storageContract = null;
			CatalogContract catalogContract = null;

			this.dataContextScope.Begin();

			IStorageRepository storageRepository = this.dataContextScope.GetRepository<IStorageRepository>();

			storageContract = await storageRepository.GetAsync(storage.ID);
			storageContract = this.mapper.Map(storage, storageContract);

			if (storageContract.IsChanged)
			{
				storageContract = await storageRepository.SaveAsync(storageContract);
			}

			ICatalogRepository catalogRepository = this.dataContextScope.GetRepository<ICatalogRepository>();
			catalogContract = await catalogRepository.GetAsync(storage.ID);

			this.dataContextScope.Commit();

			storage = this.mapper.Map(storageContract, storage);
			storage = this.mapper.Map(catalogContract, storage);

			return storage;
		}

		/// <summary>
		/// Gets the list of storages.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>The list of instances of <see cref="Storage"/> type.</returns>
		public async Task<IPaginable<Storage>> GetStorages(int offset = 0, int limit = 20)
		{
			IStorageRepository storageRepository = this.dataContextScope.GetRepository<IStorageRepository>();

			if (limit == 0)
			{
				int count = await storageRepository.GetCountAsync();

				return await Task.FromResult(new PagedList<Storage>()
				{
					Offset = offset,
					Limit = limit,
					TotalCount = count
				});
			}

			ICatalogRepository catalogRepository = this.dataContextScope.GetRepository<ICatalogRepository>();

			IPaginable<StorageContract> data = await storageRepository.FindAsync(offset, limit);
			IEnumerable<Storage> storages = this.mapper.MapNew<StorageContract, Storage>(data);
			storages = storages.SelectAsync(async storage =>
			{
				CatalogContract catalogContract = await catalogRepository.GetAsync(storage.ID);

				return this.mapper.Map(catalogContract, storage);
			});

			return new PagedList<Storage>(storages)
			{
				Offset = data.Offset,
				Limit = data.Limit,
				TotalCount = data.TotalCount
			};
		}

		/// <summary>
		/// Gets storage by initial instance set.
		/// </summary>
		/// <param name="storage">The initial storage set.</param>
		/// <returns>The instance of <see cref="Storage"/> type.</returns>
		public async Task<Storage> GetStorage(Storage storage)
		{
			StorageContract storageContract = null;
			CatalogContract catalogContract = null;

			IStorageRepository storageRepository = this.dataContextScope.GetRepository<IStorageRepository>();
			storageContract = await storageRepository.GetAsync(storage.ID);

			ICatalogRepository catalogRepository = this.dataContextScope.GetRepository<ICatalogRepository>();
			catalogContract = await catalogRepository.GetAsync(storage.ID);

			storage = this.mapper.Map(storageContract, storage);
			storage = this.mapper.Map(catalogContract, storage);

			return storage;
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
			this.dataContextScope.Begin();

			IStorageRepository storageRepository = this.dataContextScope.GetRepository<IStorageRepository>();
			await storageRepository.DeleteAsync(storage.ID);

			this.dataContextScope.Commit();

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
			CatalogContract contract = this.mapper.MapNew<Catalog, CatalogContract>(catalog);

			if (string.IsNullOrWhiteSpace(contract.Name) && contract.ID == Guid.Empty)
			{
				return false;
			}

			ICatalogRepository catalogRepository = this.dataContextScope.GetRepository<ICatalogRepository>();

			if (!string.IsNullOrWhiteSpace(contract.Name))
			{
				CatalogContract catalogResult = (await catalogRepository.FindAsync(contract, 0, 1)).FirstOrDefault();

				return !(catalogResult is null || (contract.ID != Guid.Empty && contract.ID == catalogResult.ID));
			}

			return (await catalogRepository.GetAsync(contract.ID)) != null;
		}

		/// <summary>
		/// Creates the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="Catalog" /> type to create.</param>
		/// <returns>The newly created instance of <see cref="Catalog" /> type.</returns>
		public async Task<Catalog> CreateCatalog(Catalog catalog)
		{
			CatalogContract catalogContract = this.mapper.MapNew<Catalog, CatalogContract>(catalog);
			CatalogContract parentCatalogContract = null;

			this.dataContextScope.Begin();

			ICatalogRepository catalogRepository = this.dataContextScope.GetRepository<ICatalogRepository>();

			catalogContract = await catalogRepository.SaveAsync(catalogContract);
			if (catalogContract.ParentID.HasValue)
			{
				parentCatalogContract = await catalogRepository.GetAsync(catalogContract.ParentID.Value);
			}

			this.dataContextScope.Commit();

			catalog = this.mapper.Map(catalogContract, catalog);
			catalog.Parent = this.mapper.Map(parentCatalogContract, catalog.Parent);

			return catalog;
		}

		/// <summary>
		/// Updates the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="Catalog" /> type to update.</param>
		/// <returns>The updated instance of <see cref="Catalog"/> type.</returns>
		public async Task<Catalog> UpdateCatalog(Catalog catalog)
		{
			CatalogContract catalogContract = null;
			CatalogContract parentCatalogContract = null;

			this.dataContextScope.Begin();

			ICatalogRepository catalogRepository = this.dataContextScope.GetRepository<ICatalogRepository>();

			catalogContract = await catalogRepository.GetAsync(catalog.ID);
			catalogContract = this.mapper.Map(catalog, catalogContract);

			if (catalogContract.IsChanged)
			{
				catalogContract = await catalogRepository.SaveAsync(catalogContract);
			}

			if ((catalogContract?.ParentID).HasValue)
			{
				parentCatalogContract = await catalogRepository.GetAsync(catalogContract.ParentID.Value);
			}

			this.dataContextScope.Commit();

			catalog = this.mapper.Map(catalogContract, catalog);
			catalog.Parent = this.mapper.Map(parentCatalogContract, catalog.Parent);

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
			ICatalogRepository catalogRepository = this.dataContextScope.GetRepository<ICatalogRepository>();
			CatalogContract contract = new CatalogContract()
			{
				ParentID = parent?.ID
			};

			if (limit == 0)
			{
				int count = await catalogRepository.GetCountAsync(contract);

				return await Task.FromResult(new PagedList<Catalog>()
				{
					Offset = offset,
					Limit = limit,
					TotalCount = count
				});
			}

			IPaginable<CatalogContract> data = await catalogRepository.FindAsync(contract, offset, limit);
			IEnumerable<Catalog> catalogs = this.mapper.MapNew<CatalogContract, Catalog>(data);
			catalogs = catalogs.AsParallel().Select(catalog =>
			{
				catalog.Parent = (Catalog)parent;

				return catalog;
			});

			return new PagedList<Catalog>(catalogs)
			{
				Offset = data.Offset,
				Limit = data.Limit,
				TotalCount = data.TotalCount
			};
		}

		/// <summary>
		/// Gets the catalog by the initial instance set.
		/// </summary>
		/// <param name="catalog">The initial catalog set.</param>
		/// <returns>The instance of <see cref="Catalog"/> type.</returns>
		public async Task<Catalog> GetCatalog(Catalog catalog)
		{
			CatalogContract catalogContract = null;
			CatalogContract parentCatalogContract = null;

			ICatalogRepository catalogRepository = this.dataContextScope.GetRepository<ICatalogRepository>();

			catalogContract = await catalogRepository.GetAsync(catalog.ID);
			if ((catalogContract?.ParentID).HasValue)
			{
				parentCatalogContract = await catalogRepository.GetAsync(catalogContract.ParentID.Value);
			}

			catalog = this.mapper.Map(catalogContract, catalog);
			catalog.Parent = this.mapper.Map(parentCatalogContract, catalog.Parent);

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
			this.dataContextScope.Begin();

			ICatalogRepository catalogRepository = this.dataContextScope.GetRepository<ICatalogRepository>();
			await catalogRepository.DeleteAsync(catalog.ID);

			this.dataContextScope.Commit();

			return catalog;
		}

		/// <summary>
		/// Recalculates the size of the specified catalog.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		/// <returns>The updated instance of <see cref="Catalog"/>.</returns>
		public async Task<Catalog> RecalculateSize(Catalog catalog)
		{
			return await Task.FromResult(catalog);
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
			FileContract contract = this.mapper.MapNew<CatalogEntry, FileContract>(entry);

			if (string.IsNullOrWhiteSpace(contract.Name) && contract.ID == Guid.Empty)
			{
				return false;
			}

			IFileRepository fileRepository = this.dataContextScope.GetRepository<IFileRepository>();

			if (!string.IsNullOrWhiteSpace(contract.Name))
			{
				FileContract fileResult = (await fileRepository.FindAsync(contract, 0, 1)).FirstOrDefault();

				return !(fileResult is null || (contract.ID != Guid.Empty && contract.ID == fileResult.ID));
			}

			return (await fileRepository.GetAsync(contract.ID)) != null;
		}

		/// <summary>
		/// Creates the specified catalog entry.
		/// </summary>
		/// <param name="stream">The stream of <see cref="CatalogEntryStream" /> type to create the entry from.</param>
		/// <returns>The newly created instance of <see cref="CatalogEntry" /> type.</returns>
		public async Task<CatalogEntry> CreateCatalogEntry(CatalogEntryStream stream)
		{
			CatalogEntry entry = stream.Entry;

			FileContract fileContract = this.mapper.MapNew<CatalogEntry, FileContract>(entry);
			CatalogContract catalogContract = null;

			this.dataContextScope.Begin();

			IFileRepository fileRepository = this.dataContextScope.GetRepository<IFileRepository>();
			fileContract = await fileRepository.SaveAsync(fileContract);

			ICatalogRepository catalogRepository = this.dataContextScope.GetRepository<ICatalogRepository>();
			catalogContract = await catalogRepository.GetAsync(fileContract.DirectoryID);

			this.dataContextScope.Commit();

			entry = this.mapper.Map(fileContract, entry);
			entry.Catalog = this.mapper.Map(catalogContract, entry.Catalog);

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
			IFileRepository fileRepository = this.dataContextScope.GetRepository<IFileRepository>();
			FileContract contract = new FileContract()
			{
				DirectoryID = catalog.ID
			};

			if (limit == 0)
			{
				int count = await fileRepository.GetCountAsync(contract);

				return await Task.FromResult(new PagedList<CatalogEntry>()
				{
					Offset = offset,
					Limit = limit,
					TotalCount = count
				});
			}

			IPaginable<FileContract> data = await fileRepository.FindAsync(contract, offset, limit);
			IEnumerable<CatalogEntry> entries = this.mapper.MapNew<FileContract, CatalogEntry>(data);
			entries = entries.AsParallel().Select(entry =>
			{
				entry.Catalog = (Catalog)catalog;

				return entry;
			});

			return new PagedList<CatalogEntry>(entries)
			{
				Offset = data.Offset,
				Limit = data.Limit,
				TotalCount = data.TotalCount
			};
		}

		/// <summary>
		/// Gets the catalog entry by the initial instance set.
		/// </summary>
		/// <param name="entry">The initial catalog entry set.</param>
		/// <returns>The instance of <see cref="CatalogEntry"/> type.</returns>
		public async Task<CatalogEntry> GetCatalogEntry(CatalogEntry entry)
		{
			FileContract fileContract = null;
			CatalogContract catalogContract = null;

			IFileRepository fileRepository = this.dataContextScope.GetRepository<IFileRepository>();
			fileContract = await fileRepository.GetAsync(entry.ID);

			ICatalogRepository catalogRepository = this.dataContextScope.GetRepository<ICatalogRepository>();
			catalogContract = await catalogRepository.GetAsync(fileContract.DirectoryID);

			entry = this.mapper.Map(fileContract, entry);
			entry.Catalog = this.mapper.Map(catalogContract, entry.Catalog);

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
			this.dataContextScope.Begin();

			IFileRepository fileRepository = this.dataContextScope.GetRepository<IFileRepository>();
			await fileRepository.DeleteAsync(entry.ID);

			this.dataContextScope.Commit();

			return entry;
		}

		#endregion

		#endregion
	}
}
