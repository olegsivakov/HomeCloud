namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

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
	using System;

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

				storage = await this.mapper.MapAsync(storageContract, storage) ?? storage;

				ICatalogRepository catalogRepository = scope.GetRepository<ICatalogRepository>();

				CatalogContract catalogContract = await this.mapper.MapNewAsync<Catalog, CatalogContract>(storage.CatalogRoot);
				catalogContract = await catalogRepository.SaveAsync(catalogContract);

				storage.CatalogRoot = await this.mapper.MapAsync(catalogContract, storage.CatalogRoot) ?? storage.CatalogRoot;

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

			IEnumerable<StorageContract> data = null;
			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, false))
			{
				IStorageRepository storageRepository = scope.GetRepository<IStorageRepository>();

				data = await storageRepository.FindAsync(offset, limit);
			}

			return await this.mapper.MapNewAsync<StorageContract, Storage>(data);
		}

		/// <summary>
		/// Gets storage by specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>the instance of <see cref="Storage"/>.</returns>
		public async Task<Storage> GetStorage(Guid id)
		{
			if (id == Guid.Empty)
			{
				return null;
			}

			StorageContract data = null;
			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, false))
			{
				IStorageRepository storageRepository = scope.GetRepository<IStorageRepository>();

				data = await storageRepository.GetAsync(id);
			}

			return await this.mapper.MapNewAsync<StorageContract, Storage>(data);
		}

		/// <summary>
		/// Gets the catalog by the initial instance set.
		/// </summary>
		/// <param name="catalog">The initial catalog set.</param>
		/// <returns>The instance of <see cref="Catalog"/>.</returns>
		public async Task<Catalog> GetCatalog(Catalog catalog)
		{
			IEnumerable<CatalogContract> catalogContracts = null;

			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, false))
			{
				ICatalogRepository catalogRepository = scope.GetRepository<ICatalogRepository>();

				catalogContracts = await catalogRepository.GetByParentIDAsync(catalog.StorageID, null, 0, 1);
			}

			return await this.mapper.MapAsync(catalogContracts.FirstOrDefault(), catalog) ?? catalog;
		}

		#endregion
	}
}
