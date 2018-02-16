namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Business.Entities;

	using HomeCloud.DataStorage.Business.Handlers;
	using HomeCloud.DataStorage.Business.Handlers.Extensions;

	using HomeCloud.DataStorage.Business.Providers;
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
		/// The validation service factory.
		/// </summary>
		private readonly IValidationServiceFactory validationServiceFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StorageService" /> class.
		/// </summary>
		/// <param name="processor">The command processor.</param>
		/// <param name="validationServiceFactory">The service factory of validators.</param>
		public StorageService(
			ICommandHandlerProcessor processor,
			IValidationServiceFactory validationServiceFactory)
		{
			this.processor = processor;
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
			storage.ID = Guid.Empty;
			storage.Name = Guid.NewGuid().ToString();

			IServiceFactory<IStorageValidator> validator = this.validationServiceFactory.GetFactory<IStorageValidator>();

			ValidationResult result = await validator.Get<IRequiredValidator>().ValidateAsync(storage);
			result += await validator.Get<IUniqueValidator>().ValidateAsync(storage);

			if (!result.IsValid)
			{
				return new ServiceResult<Storage>(storage)
				{
					Errors = result.Errors
				};
			}

			Func<IDataProvider, Task> createStorageFunction = async provider => storage = await provider.CreateStorage(storage);
			Func<IDataProvider, Task> createStorageUndoFunction = async provider => storage = await provider.DeleteStorage(storage);

			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IDataStoreProvider>(createStorageFunction, createStorageUndoFunction);
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IFileSystemProvider>(createStorageFunction, createStorageUndoFunction);
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IAggregationDataProvider>(createStorageFunction, createStorageUndoFunction);

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
			IServiceFactory<IStorageValidator> validator = this.validationServiceFactory.GetFactory<IStorageValidator>();

			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(storage);
			result += await validator.Get<IUniqueValidator>().ValidateAsync(storage);

			if (!result.IsValid)
			{
				return new ServiceResult<Storage>(storage)
				{
					Errors = result.Errors
				};
			}

			Func<IDataProvider, Task> updateStorageFunction = async provider => storage = await provider.UpdateStorage(storage);

			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IDataStoreProvider>(updateStorageFunction, null);
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IFileSystemProvider>(updateStorageFunction, null);
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IAggregationDataProvider>(updateStorageFunction, null);

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
		public async Task<ServiceResult<IPaginable<Storage>>> GetStoragesAsync(int offset = 0, int limit = 20)
		{
			IPaginable<Storage> storages = null;

			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IDataStoreProvider>(async provider => storages = await provider.GetStorages(offset, limit), null);
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommandFor<Storage, IAggregationDataProvider>(storages, async (provider, item) => await provider.GetStorage(item), null);

			await this.processor.ProcessAsync();

			return new ServiceResult<IPaginable<Storage>>(storages);
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

			IServiceFactory<IStorageValidator> validator = this.validationServiceFactory.GetFactory<IStorageValidator>();
			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(storage);

			if (!result.IsValid)
			{
				return new ServiceResult<Storage>(storage)
				{
					Errors = result.Errors
				};
			}

			Func<IDataProvider, Task> getStorageFunction = async provider => storage = await provider.GetStorage(storage);

			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IDataStoreProvider>(getStorageFunction, null);
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IAggregationDataProvider>(getStorageFunction, null);

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
			ServiceResult<Storage> serviceResult = await this.GetStorageAsync(id);
			if (!serviceResult.IsSuccess)
			{
				return serviceResult;
			}

			Storage storage = serviceResult.Data;
			Func<IDataProvider, Task> deleteStorageFunction = async provider => storage = await provider.DeleteStorage(storage);

			this.processor.CreateDataHandler<IDataCommandHandler>()
				.CreateAsyncCommand<IDataStoreProvider>(deleteStorageFunction, null)
				.CreateAsyncCommand<IFileSystemProvider>(deleteStorageFunction, null)
				.CreateAsyncCommand<IAggregationDataProvider>(deleteStorageFunction, null);

			await this.processor.ProcessAsync();

			return new ServiceResult<Storage>(storage);
		}

		#endregion
	}
}
