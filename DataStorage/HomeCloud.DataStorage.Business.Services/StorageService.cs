namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using System;
	using System.Threading.Tasks;
	using System.Transactions;

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Business.Entities;
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
		/// The data factory
		/// </summary>
		private readonly IDataProviderFactory dataFactory = null;

		/// <summary>
		/// The validation service factory.
		/// </summary>
		private readonly IValidationServiceFactory validationServiceFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StorageService" /> class.
		/// </summary>
		/// <param name="dataFactory">The data factory.</param>
		/// <param name="validationServiceFactory">The service factory of validators.</param>
		public StorageService(
			IDataProviderFactory dataFactory,
			IValidationServiceFactory validationServiceFactory)
		{
			this.dataFactory = dataFactory;
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
			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				storage.ID = Guid.Empty;
				storage.Name = storage.DisplayName;

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

				storage = await this.dataFactory.CreateStorage(storage);

				scope.Complete();
			}

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
			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
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

				storage = await this.dataFactory.UpdateStorage(storage);

				scope.Complete();
			}

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
			IPaginable<Storage> storages = await this.dataFactory.GetStorages(offset, limit);

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

			storage = await this.dataFactory.GetStorage(storage);

			return new ServiceResult<Storage>(storage);
		}

		/// <summary>
		/// Deletes the storage by specified identifier.
		/// </summary>
		/// <param name="id">The storage identifier.</param>
		/// <returns>The operation result.</returns>
		public async Task<ServiceResult> DeleteStorageAsync(Guid id)
		{
			Storage storage = null;

			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				ServiceResult<Storage> serviceResult = await this.GetStorageAsync(id);
				if (!serviceResult.IsSuccess)
				{
					return serviceResult;
				}

				storage = serviceResult.Data;
				Func<IDataProvider, Task> deleteStorageFunction = async provider => storage = await provider.DeleteStorage(storage);

				storage = await this.dataFactory.DeleteStorage(storage);

				scope.Complete();
			}

			return new ServiceResult<Storage>(storage);
		}

		#endregion
	}
}
