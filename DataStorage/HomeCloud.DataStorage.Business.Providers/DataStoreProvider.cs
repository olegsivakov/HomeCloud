namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using HomeCloud.Core.Extensions;

	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataAccess.Services.Factories;

	using HomeCloud.DataStorage.Api.Configuration;
	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.DataAccess.Services.Repositories;

	using HomeCloud.Mapping;

	using Microsoft.Extensions.Options;

	using CatalogContract = HomeCloud.DataStorage.DataAccess.Contracts.Catalog;
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

		/// <summary>
		/// Gets a value indicating whether the specified storage already exists.
		/// </summary>
		/// <param name="storage">The storage.</param>
		/// <returns><c>true</c> if the storage exists. Otherwise <c>false.</c></returns>
		public async Task<bool> StorageExists(Storage storage)
		{
			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, false))
			{
				IStorageRepository storageRepository = scope.GetRepository<IStorageRepository>();
				StorageContract data = await storageRepository.GetAsync(storage.ID);

				return data != null;
			}
		}

		/// <summary>
		/// Creates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to create.</param>
		/// <returns>The newly created instance of <see cref="Storage" /> type.</returns>
		public async Task<Storage> CreateStorage(Storage storage)
		{
			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, true))
			{
				IStorageRepository storageRepository = scope.GetRepository<IStorageRepository>();

				StorageContract storageContract = await this.mapper.MapNewAsync<Storage, StorageContract>(storage);
				storageContract = await storageRepository.SaveAsync(storageContract);

				storage = await this.mapper.MapAsync(storageContract, storage);

				ICatalogRepository catalogRepository = scope.GetRepository<ICatalogRepository>();

				CatalogContract catalogContract = await this.mapper.MapNewAsync<Catalog, CatalogContract>(storage.CatalogRoot);
				catalogContract = await catalogRepository.SaveAsync(catalogContract);

				storage.CatalogRoot = await this.mapper.MapAsync(catalogContract, storage.CatalogRoot);

				scope.Commit();
			}

			return storage;
		}

		/// <summary>
		/// Updates the specified storage.
		/// </summary>
		/// <param name="storage">The storage.</param>
		/// <returns>The updated instance of <see cref="Storage"/> type.</returns>
		public async Task<Storage> UpdateStorage(Storage storage)
		{
			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, true))
			{
				IStorageRepository storageRepository = scope.GetRepository<IStorageRepository>();

				StorageContract storageContract = await storageRepository.GetAsync(storage.ID);
				storageContract = await this.mapper.MapAsync(storage, storageContract);

				if (storageContract.IsChanged)
				{
					storageContract = await storageRepository.SaveAsync(storageContract);
					storageContract.AcceptChanges();

					storage = await this.mapper.MapAsync(storageContract, storage);
				}

				ICatalogRepository catalogRepository = scope.GetRepository<ICatalogRepository>();

				CatalogContract catalogContract = (await catalogRepository.GetByParentIDAsync(storage.ID, null, 0, 1)).FirstOrDefault();
				if (catalogContract == null)
				{
					catalogContract = await this.mapper.MapNewAsync<Catalog, CatalogContract>(storage.CatalogRoot);
					catalogContract = await catalogRepository.SaveAsync(catalogContract);
				}

				storage.CatalogRoot = await this.mapper.MapAsync(catalogContract, storage.CatalogRoot);

				scope.Commit();
			}

			return storage;
		}

		/// <summary>
		/// Gets the list of storages.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>The list of instances of <see cref="Storage"/> type.</returns>
		public async Task<IEnumerable<Storage>> GetStorages(int offset = 0, int limit = 20)
		{
			if (limit == 0)
			{
				return await Task.FromResult(Enumerable.Empty<Storage>());
			}

			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, false))
			{
				IStorageRepository storageRepository = scope.GetRepository<IStorageRepository>();

				IEnumerable<StorageContract> data = await storageRepository.FindAsync(offset, limit);

				IEnumerable<Storage> storages = await this.mapper.MapNewAsync<StorageContract, Storage>(data);
				foreach (Storage storage in storages)
				{
					storage.CatalogRoot = await this.ExecuteGetCatalog(storage.CatalogRoot, scope);
				}

				return storages;
			}
		}

		/// <summary>
		/// Gets storage by the initial instance set.
		/// </summary>
		/// <param name="storage">The initial storage stet.</param>
		/// <returns>the instance of <see cref="Storage"/>.</returns>
		public async Task<Storage> GetStorage(Storage storage)
		{
			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, false))
			{
				return await this.ExecuteGetStorage(storage, scope);
			}
		}

		/// <summary>
		/// Gets a value indicating whether the specified catalog already exists.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		/// <returns><c>true</c> if the catalog exists. Otherwise <c>false.</c></returns>
		public async Task<bool> CatalogExists(Catalog catalog)
		{
			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, false))
			{
				ICatalogRepository catalogRepository = scope.GetRepository<ICatalogRepository>();
				CatalogContract data = await catalogRepository.GetAsync(catalog.ID);

				return data != null;
			}
		}

		/// <summary>
		/// Gets the catalog by the initial instance set.
		/// </summary>
		/// <param name="catalog">The initial catalog set.</param>
		/// <returns>The instance of <see cref="Catalog"/>.</returns>
		public async Task<Catalog> GetCatalog(Catalog catalog)
		{
			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, false))
			{
				return await this.ExecuteGetCatalog(catalog, scope);
			}
		}

		/// <summary>
		/// Deletes the specified storage.
		/// </summary>
		/// <param name="storage">The storage.</param>
		/// <returns>
		/// The deleted instance of <see cref="Storage"/>.
		/// </returns>
		public async Task<Storage> DeleteStorage(Storage storage)
		{
			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, true))
			{
				storage = await this.ExecuteGetStorage(storage, scope);

				IStorageRepository storageRepository = scope.GetRepository<IStorageRepository>();
				await storageRepository.DeleteAsync(storage.ID);

				scope.Commit();
			}

			return storage;
		}

		/// <summary>
		/// Deletes the specified catalog.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		/// <returns>
		/// The deleted instance of <see cref="Catalog"/>.
		/// </returns>
		public async Task<Catalog> DeleteCatalog(Catalog catalog)
		{
			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, true))
			{
				catalog = await this.ExecuteGetCatalog(catalog, scope);

				ICatalogRepository catalogRepository = scope.GetRepository<ICatalogRepository>();
				await catalogRepository.DeleteAsync(catalog.ID);

				scope.Commit();
			}

			return catalog;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Executes the operation to get the catalog data by the initial instance set.
		/// </summary>
		/// <param name="catalog">The initial catalog.</param>
		/// <param name="scope">The database scope used to get the catalog.</param>
		/// <returns>The instance of <see cref="Catalog"/>.</returns>
		private async Task<Catalog> ExecuteGetCatalog(Catalog catalog, IDbContextScope scope)
		{
			ICatalogRepository catalogRepository = scope.GetRepository<ICatalogRepository>();
			IEnumerable<CatalogContract> catalogContracts = await catalogRepository.GetByParentIDAsync(catalog.StorageID, null, 0, 1);

			return await this.mapper.MapAsync(catalogContracts.FirstOrDefault(), catalog);
		}

		/// <summary>
		/// Executes the operation to get the storage data by the initial instance set.
		/// </summary>
		/// <param name="storage">The initial storage.</param>
		/// <param name="scope">The database scope used to get the storage.</param>
		/// <returns>The instance of <see cref="Storage"/>.</returns>
		private async Task<Storage> ExecuteGetStorage(Storage storage, IDbContextScope scope)
		{
			IStorageRepository storageRepository = scope.GetRepository<IStorageRepository>();
			StorageContract data = await storageRepository.GetAsync(storage.ID);

			storage = await this.mapper.MapNewAsync<StorageContract, Storage>(data);
			storage.CatalogRoot = await this.ExecuteGetCatalog(storage.CatalogRoot, scope);

			return storage;
		}

		#endregion
	}
}
