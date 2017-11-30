namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
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

	using CatalogContract = HomeCloud.DataStorage.DataAccess.Contracts.Catalog;
	using FileContract = HomeCloud.DataStorage.DataAccess.Contracts.File;
	using StorageContract = HomeCloud.DataStorage.DataAccess.Contracts.Storage;

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
		/// Initializes a new instance of the <see cref="DataStoreProvider" /> class.
		/// </summary>
		/// <param name="dataContextScopeFactory">The data context scope factory.</param>
		/// <param name="connectionStrings">The connection strings.</param>
		/// <param name="mapper">The mapper.</param>
		public DataStoreProvider(
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
			StorageContract contract = await this.mapper.MapNewAsync<Storage, StorageContract>(storage);

			if (string.IsNullOrWhiteSpace(contract.Name) && contract.ID == Guid.Empty)
			{
				return false;
			}

			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, false))
			{
				IStorageRepository storageRepository = scope.GetRepository<IStorageRepository>();

				if (!string.IsNullOrWhiteSpace(contract.Name))
				{
					StorageContract storageResult = (await storageRepository.FindAsync(contract, 0, 1)).FirstOrDefault();

					return !(storageResult is null || (contract.ID != Guid.Empty && contract.ID == storageResult.ID));
				}

				return (await storageRepository.GetAsync(contract.ID)) != null;
			}
		}

		/// <summary>
		/// Creates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to create.</param>
		/// <returns>The newly created instance of <see cref="Storage" /> type.</returns>
		public async Task<Storage> CreateStorage(Storage storage)
		{
			StorageContract storageContract = await this.mapper.MapNewAsync<Storage, StorageContract>(storage);
			CatalogContract catalogContract = await this.mapper.MapNewAsync<Storage, CatalogContract>(storage);

			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, true))
			{
				ICatalogRepository catalogRepository = scope.GetRepository<ICatalogRepository>();
				catalogContract = await catalogRepository.SaveAsync(catalogContract);

				storageContract.ID = catalogContract.ID;

				IStorageRepository storageRepository = scope.GetRepository<IStorageRepository>();
				storageContract = await storageRepository.SaveAsync(storageContract);

				scope.Commit();
			}

			storage = await this.mapper.MapAsync(storageContract, storage);
			storage = await this.mapper.MapAsync(catalogContract, storage);

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

			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, true))
			{
				IStorageRepository storageRepository = scope.GetRepository<IStorageRepository>();

				storageContract = await storageRepository.GetAsync(storage.ID);
				storageContract = await this.mapper.MapAsync(storage, storageContract);

				if (storageContract.IsChanged)
				{
					storageContract = await storageRepository.SaveAsync(storageContract);
				}

				ICatalogRepository catalogRepository = scope.GetRepository<ICatalogRepository>();
				catalogContract = await catalogRepository.GetAsync(storage.ID);

				scope.Commit();
			}

			storage = await this.mapper.MapAsync(storageContract, storage);
			storage = await this.mapper.MapAsync(catalogContract, storage);

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
			if (limit == 0)
			{
				return await Task.FromResult(new PagedList<Storage>()
				{
					Offset = offset,
					Limit = limit
				});
			}

			IEnumerable<Storage> storages = null;
			int count = 0;

			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, false))
			{
				IStorageRepository storageRepository = scope.GetRepository<IStorageRepository>();
				ICatalogRepository catalogRepository = scope.GetRepository<ICatalogRepository>();

				count = await storageRepository.GetCountAsync();

				IEnumerable<StorageContract> data = await storageRepository.FindAsync(offset, limit);

				storages = await this.mapper.MapNewAsync<StorageContract, Storage>(data);
				storages = await Task.WhenAll(storages.Select(async storage =>
				{
					CatalogContract catalogContract = await catalogRepository.GetAsync(storage.ID);

					return await this.mapper.MapAsync(catalogContract, storage);
				}));
			}

			IPaginable<Storage> result = storages.AsPaginable();

			result.Offset = offset;
			result.Limit = limit;
			result.TotalCount = count;

			return result;
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

			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, false))
			{
				IStorageRepository storageRepository = scope.GetRepository<IStorageRepository>();
				storageContract = await storageRepository.GetAsync(storage.ID);

				ICatalogRepository catalogRepository = scope.GetRepository<ICatalogRepository>();
				catalogContract = await catalogRepository.GetAsync(storage.ID);
			}

			storage = await this.mapper.MapNewAsync<StorageContract, Storage>(storageContract);
			storage = await this.mapper.MapAsync(catalogContract, storage);

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
			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, true))
			{
				IStorageRepository storageRepository = scope.GetRepository<IStorageRepository>();
				await storageRepository.DeleteAsync(storage.ID);

				scope.Commit();
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
			CatalogContract contract = await this.mapper.MapNewAsync<Catalog, CatalogContract>(catalog);

			if (string.IsNullOrWhiteSpace(contract.Name) && contract.ID == Guid.Empty)
			{
				return false;
			}

			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, false))
			{
				ICatalogRepository catalogRepository = scope.GetRepository<ICatalogRepository>();

				if (!string.IsNullOrWhiteSpace(contract.Name))
				{
					CatalogContract catalogResult = (await catalogRepository.FindAsync(contract, 0, 1)).FirstOrDefault();

					return !(catalogResult is null || (contract.ID != Guid.Empty && contract.ID == catalogResult.ID));
				}

				return (await catalogRepository.GetAsync(contract.ID)) != null;
			}
		}

		/// <summary>
		/// Creates the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="Catalog" /> type to create.</param>
		/// <returns>The newly created instance of <see cref="Catalog" /> type.</returns>
		public async Task<Catalog> CreateCatalog(Catalog catalog)
		{
			CatalogContract catalogContract = await this.mapper.MapNewAsync<Catalog, CatalogContract>(catalog);
			CatalogContract parentCatalogContract = null;

			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, true))
			{
				ICatalogRepository catalogRepository = scope.GetRepository<ICatalogRepository>();

				catalogContract = await catalogRepository.SaveAsync(catalogContract);
				if (catalogContract.ParentID.HasValue)
				{
					parentCatalogContract = await catalogRepository.GetAsync(catalogContract.ParentID.Value);
				}

				scope.Commit();
			}

			catalog = await this.mapper.MapAsync(catalogContract, catalog);
			catalog.Parent = await this.mapper.MapAsync(parentCatalogContract, catalog.Parent);

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

			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, true))
			{
				ICatalogRepository catalogRepository = scope.GetRepository<ICatalogRepository>();

				catalogContract = await catalogRepository.GetAsync(catalog.ID);
				catalogContract = await this.mapper.MapAsync(catalog, catalogContract);

				if (catalogContract.IsChanged)
				{
					catalogContract = await catalogRepository.SaveAsync(catalogContract);
				}

				if ((catalogContract?.ParentID).HasValue)
				{
					parentCatalogContract = await catalogRepository.GetAsync(catalogContract.ParentID.Value);
				}

				scope.Commit();
			}

			catalog = await this.mapper.MapAsync(catalogContract, catalog);
			catalog.Parent = await this.mapper.MapAsync(parentCatalogContract, catalog.Parent);

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
			if (limit == 0)
			{
				return await Task.FromResult(new PagedList<Catalog>()
				{
					Offset = offset,
					Limit = limit
				});
			}

			IEnumerable<CatalogContract> data = null;
			int count = 0;

			CatalogContract contract = new CatalogContract()
			{
				ParentID = parent?.ID
			};

			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, false))
			{
				ICatalogRepository catalogRepository = scope.GetRepository<ICatalogRepository>();

				await Task.WhenAll(
					Task.Run(async () => data = await catalogRepository.FindAsync(contract, offset, limit)),
					Task.Run(async () => count = await catalogRepository.GetCountAsync(contract)));
			}

			IPaginable<Catalog> result = (await this.mapper.MapNewAsync<CatalogContract, Catalog>(data)).Select(catalog =>
			{
				catalog.Parent = (Catalog)parent;

				return catalog;
			}).AsPaginable();

			result.Offset = offset;
			result.Limit = limit;
			result.TotalCount = count;

			return result;
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

			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, false))
			{
				ICatalogRepository catalogRepository = scope.GetRepository<ICatalogRepository>();

				catalogContract = await catalogRepository.GetAsync(catalog.ID);
				if ((catalogContract?.ParentID).HasValue)
				{
					parentCatalogContract = await catalogRepository.GetAsync(catalogContract.ParentID.Value);
				}
			}

			catalog = await this.mapper.MapAsync(catalogContract, catalog);
			catalog.Parent = await this.mapper.MapAsync(parentCatalogContract, catalog.Parent);

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
			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, true))
			{
				ICatalogRepository catalogRepository = scope.GetRepository<ICatalogRepository>();
				await catalogRepository.DeleteAsync(catalog.ID);

				scope.Commit();
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
			FileContract contract = await this.mapper.MapNewAsync<CatalogEntry, FileContract>(entry);

			if (string.IsNullOrWhiteSpace(contract.Name) && contract.ID == Guid.Empty)
			{
				return false;
			}

			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, false))
			{
				IFileRepository fileRepository = scope.GetRepository<IFileRepository>();

				if (!string.IsNullOrWhiteSpace(contract.Name))
				{
					FileContract fileResult = (await fileRepository.FindAsync(contract, 0, 1)).FirstOrDefault();

					return !(fileResult is null || (contract.ID != Guid.Empty && contract.ID == fileResult.ID));
				}

				return (await fileRepository.GetAsync(contract.ID)) != null;
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

			FileContract fileContract = await this.mapper.MapNewAsync<CatalogEntry, FileContract>(entry);
			CatalogContract catalogContract = null;

			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, true))
			{
				IFileRepository fileRepository = scope.GetRepository<IFileRepository>();
				fileContract = await fileRepository.SaveAsync(fileContract);

				ICatalogRepository catalogRepository = scope.GetRepository<ICatalogRepository>();
				catalogContract = await catalogRepository.GetAsync(fileContract.DirectoryID);

				scope.Commit();
			}

			entry = await this.mapper.MapAsync(fileContract, entry);
			entry.Catalog = await this.mapper.MapAsync(catalogContract, entry.Catalog);

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
			if (limit == 0)
			{
				return await Task.FromResult(new PagedList<CatalogEntry>()
				{
					Offset = offset,
					Limit = limit
				});
			}

			IEnumerable<FileContract> data = null;
			int count = 0;

			FileContract contract = new FileContract()
			{
				DirectoryID = catalog.ID
			};

			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, false))
			{
				IFileRepository fileRepository = scope.GetRepository<IFileRepository>();

				await Task.WhenAll(
					Task.Run(async () => data = await fileRepository.FindAsync(contract, offset, limit)),
					Task.Run(async () => count = await fileRepository.GetCountAsync(contract)));
			}

			IPaginable<CatalogEntry> result = (await this.mapper.MapNewAsync<FileContract, CatalogEntry>(data)).Select(entry =>
			{
				entry.Catalog = (Catalog)catalog;

				return entry;
			}).AsPaginable();

			result.Offset = offset;
			result.Limit = limit;
			result.TotalCount = count;

			return result;
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

			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, false))
			{
				IFileRepository fileRepository = scope.GetRepository<IFileRepository>();
				fileContract = await fileRepository.GetAsync(entry.ID);

				ICatalogRepository catalogRepository = scope.GetRepository<ICatalogRepository>();
				catalogContract = await catalogRepository.GetAsync(fileContract.DirectoryID);
			}

			entry = await this.mapper.MapAsync(fileContract, entry);
			entry.Catalog = await this.mapper.MapAsync(catalogContract, entry.Catalog);

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
			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, true))
			{
				IFileRepository fileRepository = scope.GetRepository<IFileRepository>();
				await fileRepository.DeleteAsync(entry.ID);

				scope.Commit();
			}

			return entry;
		}

		#endregion

		#endregion
	}
}
