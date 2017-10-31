namespace HomeCloud.DataStorage.Business.Components.Providers
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataAccess.Services.Factories;

	using HomeCloud.DataStorage.Api.Configuration;

	using HomeCloud.DataStorage.DataAccess.Services.Repositories;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Entities.Mapping.Extensions;

	using HomeCloud.DataStorage.Business.Services.Providers;

	using Microsoft.Extensions.Options;

	using StorageContract = HomeCloud.DataStorage.DataAccess.Contracts.Storage;
	using DirectoryContract = HomeCloud.DataStorage.DataAccess.Contracts.Directory;
	using HomeCloud.DataStorage.Business.Validation.Abstractions;
	using HomeCloud.Validation;
	using HomeCloud.Exceptions;

	#endregion

	/// <summary>
	///  Provides methods to manage data from data store database.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Services.Providers.IDataStoreProvider" />
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

		/// <summary>
		/// The validator factory.
		/// </summary>
		private readonly IServiceFactory<IStorageValidator> validatorFactory = null;

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
			IMapper mapper,
			IServiceFactory<IStorageValidator> validatorFactory)
		{
			this.dataContextScopeFactory = dataContextScopeFactory;
			this.connectionStrings = connectionStrings?.Value;

			this.mapper = mapper;
			this.validatorFactory = validatorFactory;
		}

		#endregion

		#region IDataStoreProvider Implementations

		/// <summary>
		/// Creates the storage.
		/// </summary>
		/// <param name="storage">The storage.</param>
		public void CreateStorage(Storage storage)
		{
			using (IDbContextScope scope = this.dataContextScopeFactory.CreateDbContextScope(this.connectionStrings.DataStorageDB, true))
			{
				IStorageRepository storageRepository = scope.GetRepository<IStorageRepository>();

				StorageContract storageContract = storageRepository.Save(this.mapper.MapNew<Storage, StorageContract>(storage));

				storage = this.mapper.Map(storageContract, storage);

				IDirectoryRepository directoryRepository = scope.GetRepository<IDirectoryRepository>();

				directoryRepository.Save(this.mapper.MapNew<Catalog, DirectoryContract>(storage.CatalogRoot));

				scope.Commit();
			}
		}

		public void 

		#endregion
	}
}
