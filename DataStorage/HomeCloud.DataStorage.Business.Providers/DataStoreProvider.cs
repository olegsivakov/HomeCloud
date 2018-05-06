﻿namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Transactions;

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
		/// The repository factory
		/// </summary>
		private readonly IServiceFactory<ISqlServerDBRepository> repositoryFactory = null;

		/// <summary>
		/// The object mapper.
		/// </summary>
		private readonly IMapper mapper = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DataStoreProvider" /> class.
		/// </summary>
		/// <param name="repositoryFactory">The repository factory.</param>
		/// <param name="mapper">The mapper.</param>
		public DataStoreProvider(
			IServiceFactory<ISqlServerDBRepository> repositoryFactory,
			IMapper mapper)
		{
			this.repositoryFactory = repositoryFactory;
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

			IStorageRepository storageRepository = this.repositoryFactory.GetService<IStorageRepository>();

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
			StorageContract storageContract = null;
			await this.ExecuteTransactionAsync(async () =>
			{
				ICatalogRepository catalogRepository = this.repositoryFactory.GetService<ICatalogRepository>();
				catalogContract = await catalogRepository.SaveAsync(catalogContract);

				storageContract = this.mapper.MapNew<Storage, StorageContract>(storage);
				storageContract.ID = catalogContract.ID;

				IStorageRepository storageRepository = this.repositoryFactory.GetService<IStorageRepository>();
				storageContract = await storageRepository.SaveAsync(storageContract);
			});

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

			await this.ExecuteTransactionAsync(async () =>
			{
				IStorageRepository storageRepository = this.repositoryFactory.GetService<IStorageRepository>();

				storageContract = await storageRepository.GetAsync(storage.ID);
				storageContract = this.mapper.Map(storage, storageContract);

				if (storageContract.IsChanged)
				{
					storageContract = await storageRepository.SaveAsync(storageContract);
				}

				ICatalogRepository catalogRepository = this.repositoryFactory.GetService<ICatalogRepository>();
				catalogContract = await catalogRepository.GetAsync(storage.ID);
			});

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
			IStorageRepository storageRepository = this.repositoryFactory.GetService<IStorageRepository>();

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

			ICatalogRepository catalogRepository = this.repositoryFactory.GetService<ICatalogRepository>();

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

			IStorageRepository storageRepository = this.repositoryFactory.GetService<IStorageRepository>();
			storageContract = await storageRepository.GetAsync(storage.ID);

			ICatalogRepository catalogRepository = this.repositoryFactory.GetService<ICatalogRepository>();
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
			await this.ExecuteTransactionAsync(async () =>
			{
				IStorageRepository storageRepository = this.repositoryFactory.GetService<IStorageRepository>();
				await storageRepository.DeleteAsync(storage.ID);
			});

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

			ICatalogRepository catalogRepository = this.repositoryFactory.GetService<ICatalogRepository>();

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

			await this.ExecuteTransactionAsync(async () =>
			{
				ICatalogRepository catalogRepository = this.repositoryFactory.GetService<ICatalogRepository>();

				catalogContract = await catalogRepository.SaveAsync(catalogContract);
				if (catalogContract.ParentID.HasValue)
				{
					parentCatalogContract = await catalogRepository.GetAsync(catalogContract.ParentID.Value);
				}
			});

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

			await this.ExecuteTransactionAsync(async () =>
			{
				ICatalogRepository catalogRepository = this.repositoryFactory.GetService<ICatalogRepository>();

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
			});

			catalog = this.mapper.Map(catalogContract, catalog);
			catalog.Parent = this.mapper.Map(parentCatalogContract, catalog.Parent);

			return catalog;
		}

		/// <summary>
		/// Gets the list of catalogs located in specified parent catalog and met specified catalog criteria.
		/// </summary>
		/// <param name="parent">The parent catalog of <see cref="CatalogRoot" /> type.</param>
		/// <param name="criteria">The catalog criteria.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return. If set to 0 the empty collection with total count set up is returned.</param>
		/// <returns>
		/// The list of instances of <see cref="Catalog" /> type.
		/// </returns>
		public async Task<IPaginable<Catalog>> GetCatalogs(CatalogRoot parent, Catalog criteria = null, int offset = 0, int limit = 20)
		{
			CatalogContract contract = this.mapper.MapNew<Catalog, CatalogContract>(criteria);
			contract.ParentID = parent?.ID;

			ICatalogRepository catalogRepository = this.repositoryFactory.GetService<ICatalogRepository>();

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

			ICatalogRepository catalogRepository = this.repositoryFactory.GetService<ICatalogRepository>();

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
			await this.ExecuteTransactionAsync(async () =>
			{
				ICatalogRepository catalogRepository = this.repositoryFactory.GetService<ICatalogRepository>();
				await catalogRepository.DeleteAsync(catalog.ID);
			});

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

			IFileRepository fileRepository = this.repositoryFactory.GetService<IFileRepository>();

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

			await this.ExecuteTransactionAsync(async () =>
			{
				IFileRepository fileRepository = this.repositoryFactory.GetService<IFileRepository>();
				fileContract = await fileRepository.SaveAsync(fileContract);

				ICatalogRepository catalogRepository = this.repositoryFactory.GetService<ICatalogRepository>();
				catalogContract = await catalogRepository.GetAsync(fileContract.DirectoryID);
			});

			entry = this.mapper.Map(fileContract, entry);
			entry.Catalog = this.mapper.Map(catalogContract, entry.Catalog);

			return entry;
		}

		/// <summary>
		/// Gets the list of catalog entries located in specified catalog and met specified catalog entry criteria.
		/// </summary>
		/// <param name="catalog">The catalog of <see cref="CatalogRoot" /> type.</param>
		/// <param name="criteria">The catalog entry criteria.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return. If set to 0 the empty collection with total count set up is returned.</param>
		/// <returns>
		/// The list of instances of <see cref="CatalogEntry" /> type.
		/// </returns>
		public async Task<IPaginable<CatalogEntry>> GetCatalogEntries(CatalogRoot catalog, CatalogEntry criteria = null, int offset = 0, int limit = 20)
		{
			FileContract contract = this.mapper.MapNew<CatalogEntry, FileContract>(criteria);
			contract.DirectoryID = catalog.ID;

			IFileRepository fileRepository = this.repositoryFactory.GetService<IFileRepository>();

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

			IFileRepository fileRepository = this.repositoryFactory.GetService<IFileRepository>();
			fileContract = await fileRepository.GetAsync(entry.ID);

			ICatalogRepository catalogRepository = this.repositoryFactory.GetService<ICatalogRepository>();
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
			await this.ExecuteTransactionAsync(async () =>
			{
				IFileRepository fileRepository = this.repositoryFactory.GetService<IFileRepository>();
				await fileRepository.DeleteAsync(entry.ID);
			});

			return entry;
		}

		#endregion

		#endregion

		#region Private Methods

		/// <summary>
		/// Executes the specified action as a single transaction asynchronously.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns>The asynchronous operation.</returns>
		private async Task ExecuteTransactionAsync(Func<Task> action)
		{
			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
			{
				await action();

				scope.Complete();
			}
		}

		#endregion
	}
}
