namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Handlers;
	using HomeCloud.DataStorage.Business.Validation;
	
	using HomeCloud.Validation;

	#endregion

	/// <summary>
	/// Provides methods to handle storages.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Services.IStorageService" />
	public class StorageService : IStorageService
	{
		#region Private Members

		/// <summary>
		/// The processor
		/// </summary>
		private readonly ICommandHandlerProcessor processor = null;

		/// <summary>
		/// The command handler factory
		/// </summary>
		private readonly IServiceFactory<IDataCommandHandler> commandHandlerFactory = null;

		/// <summary>
		/// The validation service factory.
		/// </summary>
		private readonly IValidationServiceFactory validationServiceFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StorageService" /> class.
		/// </summary>
		/// <param name="processor">The command processor.</param>
		/// <param name="commandHandlerFactory">The command handler factory.</param>
		/// <param name="validationServiceFactory">The service factory of validators.</param>
		public StorageService(
			ICommandHandlerProcessor processor,
			IServiceFactory<IDataCommandHandler> commandHandlerFactory,
			IValidationServiceFactory validationServiceFactory)
		{
			this.processor = processor;
			this.commandHandlerFactory = commandHandlerFactory;
			this.validationServiceFactory = validationServiceFactory;
		}

		#endregion

		#region IStorageService Implementations

		/// <summary>
		/// Creates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.Storage" /> type.</param>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		/// <exception cref="ValidationException">The exception thrown when the validation of the specified instance of <see cref="Storage" /> has been failed.</exception>
		public async Task<ServiceResult<Storage>> CreateStorageAsync(Storage storage)
		{
			storage.CatalogRoot.Name = Guid.NewGuid().ToString();

			IServiceFactory<IStorageValidator> storageValidator = this.validationServiceFactory.GetFactory<IStorageValidator>();
			ValidationResult result = await storageValidator.Get<IRequiredValidator>().ValidateAsync(storage);
			result += await storageValidator.Get<IUniqueValidator>().ValidateAsync(storage);

			IServiceFactory<ICatalogValidator> catalogValidator = this.validationServiceFactory.GetFactory<ICatalogValidator>();
			result += await catalogValidator.Get<IRequiredValidator>().ValidateAsync(storage.CatalogRoot);
			result += await catalogValidator.Get<IUniqueValidator>().ValidateAsync(storage.CatalogRoot);

			if (!result.IsValid)
			{
				return new ServiceResult<Storage>(storage)
				{
					Errors = result.Errors
				};
			}

			this.processor.CreateDataHandler<IDataStoreCommandHandler>().CreateAsyncCommand(async provider => storage = await provider.CreateStorage(storage), null);
			this.processor.CreateDataHandler<IFileSystemCommandHandler>().CreateAsyncCommand(async provider => storage = await provider.CreateStorage(storage), null);
			this.processor.CreateDataHandler<IAggregatedDataCommandHandler>().CreateAsyncCommand(async provider => storage = await provider.CreateStorage(storage), null);

			await this.processor.ProcessAsync();

			return new ServiceResult<Storage>(storage);
		}

		/// <summary>
		/// Updates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type.</param>
		/// <returns>
		/// The operation result containing updated instance of <see cref="Storage"/>.
		/// </returns>
		public async Task<ServiceResult<Storage>> UpdateStorageAsync(Storage storage)
		{
			storage.CatalogRoot.Name = storage.CatalogRoot.Name ?? Guid.NewGuid().ToString();

			IServiceFactory<IStorageValidator> storageValidator = this.validationServiceFactory.GetFactory<IStorageValidator>();
			ValidationResult result = await storageValidator.Get<IPresenceValidator>().ValidateAsync(storage);

			IServiceFactory<ICatalogValidator> catalogValidator = this.validationServiceFactory.GetFactory<ICatalogValidator>();
			result += await catalogValidator.Get<IRequiredValidator>().ValidateAsync(storage.CatalogRoot);

			if (!result.IsValid)
			{
				return new ServiceResult<Storage>(storage)
				{
					Errors = result.Errors
				};
			}

			this.processor.CreateDataHandler<IDataStoreCommandHandler>().CreateAsyncCommand(async provider => storage = await provider.UpdateStorage(storage), null);
			this.processor.CreateDataHandler<IFileSystemCommandHandler>().CreateAsyncCommand(async provider => storage = await provider.UpdateStorage(storage), null);
			this.processor.CreateDataHandler<IAggregatedDataCommandHandler>().CreateAsyncCommand(async provider => storage = await provider.UpdateStorage(storage), null);

			await this.processor.ProcessAsync();

			return new ServiceResult<Storage>(storage);
		}

		/// <summary>
		/// Gets the list of storages.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The list of instances of <see cref="T:HomeCloud.DataStorage.Business.Entities.Storage" /> type.
		/// </returns>
		public async Task<ServiceResult<IEnumerable<Storage>>> GetStoragesAsync(int offset = 0, int limit = 20)
		{
			IEnumerable<Storage> storages = null;

			this.processor.CreateDataHandler<IDataStoreCommandHandler>().CreateAsyncCommand(async provider => storages = await provider.GetStorages(offset, limit), null);
			this.processor.CreateDataHandler<IAggregatedDataCommandHandler>().CreateAsyncCommand(
				async provider =>
				{
					foreach (Storage storage in storages)
					{
						storage.CatalogRoot = await provider.GetCatalog(storage.CatalogRoot);
					}
				},
				null);

			await this.processor.ProcessAsync();

			return new ServiceResult<IEnumerable<Storage>>(storages);
		}

		/// <summary>
		/// Gets the storage by specified identifier.
		/// </summary>
		/// <param name="id">The storage identifier.</param>
		/// <returns>
		/// The operation result containing the list of instances of <see cref="Storage" />.
		/// </returns>
		public async Task<ServiceResult<Storage>> GetStorageAsync(Guid id)
		{
			Storage storage = new Storage() { ID = id };

			IServiceFactory<IStorageValidator> storageValidator = this.validationServiceFactory.GetFactory<IStorageValidator>();
			ValidationResult result = await storageValidator.Get<IPresenceValidator>().ValidateAsync(storage);

			if (!result.IsValid)
			{
				return new ServiceResult<Storage>(storage)
				{
					Errors = result.Errors
				};
			}

			this.processor.CreateDataHandler<IDataStoreCommandHandler>().CreateAsyncCommand(async provider => storage = await provider.GetStorage(id), null);
			this.processor.CreateDataHandler<IAggregatedDataCommandHandler>().CreateAsyncCommand(async provider => storage.CatalogRoot = await provider.GetCatalog(storage.CatalogRoot), null);

			await this.processor.ProcessAsync();

			return new ServiceResult<Storage>(storage);
		}

		/// <summary>
		/// Deletes the storage by specified identifier.
		/// </summary>
		/// <param name="id">The storage identifier.</param>
		/// <returns>The operation result.</returns>
		public async Task<ServiceResult> DeleteStorageAsync(Guid id)
		{
			Storage storage = new Storage() { ID = id };

			IServiceFactory<IStorageValidator> storageValidator = this.validationServiceFactory.GetFactory<IStorageValidator>();
			ValidationResult result = await storageValidator.Get<IPresenceValidator>().ValidateAsync(storage);

			if (!result.IsValid)
			{
				return new ServiceResult<Storage>(storage)
				{
					Errors = result.Errors
				};
			}

			this.processor.CreateDataHandler<IDataStoreCommandHandler>().CreateAsyncCommand(async provider => storage = await provider.GetStorage(id), null);
			this.processor.CreateDataHandler<IFileSystemCommandHandler>().CreateAsyncCommand(async provider => storage.CatalogRoot = await provider.GetCatalog(storage.CatalogRoot), null);
			this.processor.CreateDataHandler<IAggregatedDataCommandHandler>().CreateAsyncCommand(async provider => storage.CatalogRoot = await provider.GetCatalog(storage.CatalogRoot), null);

			await this.processor.ProcessAsync();

			return new ServiceResult<Storage>(storage);
		}

		#endregion
	}
}
