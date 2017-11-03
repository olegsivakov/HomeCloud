namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.Core.Extensions;

	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataAccess.Services.Factories;

	using HomeCloud.DataStorage.Api.Configuration;

	using HomeCloud.DataStorage.Business.Entities;

	using HomeCloud.DataStorage.DataAccess.Services.Repositories;

	using Microsoft.Extensions.Options;

	using DirectoryContract = HomeCloud.DataStorage.DataAccess.Contracts.Catalog;
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

				ICatalogRepository directoryRepository = scope.GetRepository<ICatalogRepository>();

				DirectoryContract directoryContract = await this.mapper.MapNewAsync<Catalog, DirectoryContract>(storage.CatalogRoot);
				directoryContract = await directoryRepository.SaveAsync(directoryContract);

				storage.CatalogRoot = await this.mapper.MapAsync(directoryContract, storage.CatalogRoot);

				scope.Commit();
			}

			return storage;
		}

		#endregion
	}
}
