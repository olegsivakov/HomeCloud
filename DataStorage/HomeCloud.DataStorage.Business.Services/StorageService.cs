namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using System;
	using System.IO;
	using System.Threading.Tasks;

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Api.Configuration;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Handlers;
	using HomeCloud.DataStorage.Business.Validation;

	using HomeCloud.Exceptions;
	using HomeCloud.Validation;

	using Microsoft.Extensions.Options;

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
		/// The file system settings
		/// </summary>
		private readonly FileSystem fileSystemSettings = null;

		#region Private Members

		/// <summary>
		/// The validation service factory.
		/// </summary>
		private readonly IValidationServiceFactory validationServiceFactory = null;

		#endregion

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StorageService" /> class.
		/// </summary>
		/// <param name="processor">The command processor.</param>
		/// <param name="commandHandlerFactory">The command handler factory.</param>
		/// <param name="fileSystemSettings">The file system settings.</param>
		/// <param name="validationServiceFactory">The service factory of validators.</param>
		public StorageService(
			ICommandHandlerProcessor processor,
			IServiceFactory<IDataCommandHandler> commandHandlerFactory,
			IOptionsSnapshot<FileSystem> fileSystemSettings,
			IValidationServiceFactory validationServiceFactory)
		{
			this.processor = processor;
			this.commandHandlerFactory = commandHandlerFactory;

			this.fileSystemSettings = fileSystemSettings?.Value;
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
		public async Task CreateStorageAsync(Storage storage)
		{
			storage.CatalogRoot.Name = Guid.NewGuid().ToString();
			storage.CatalogRoot.Path = Path.Combine(this.fileSystemSettings.StorageRootPath, storage.CatalogRoot.Name);

			IServiceFactory<IStorageValidator> storageValidator = this.validationServiceFactory.GetFactory<IStorageValidator>();
			ValidationResult result = await storageValidator.Get<IRequiredValidator>().ValidateAsync(storage);
			result += await storageValidator.Get<IUniqueValidator>().ValidateAsync(storage);

			IServiceFactory<ICatalogValidator> catalogValidator = this.validationServiceFactory.GetFactory<ICatalogValidator>();
			result += await catalogValidator.Get<IRequiredValidator>().ValidateAsync(storage.CatalogRoot);
			result += await catalogValidator.Get<IUniqueValidator>().ValidateAsync(storage.CatalogRoot);

			if (!result.IsValid)
			{
				throw new ValidationException(result.Errors);
			}

			this.processor.CreateDataHandler<IDataStoreCommandHandler>().CreateAsyncCommand(async provider => storage = await provider.CreateStorage(storage), null);
			this.processor.CreateDataHandler<IFileSystemCommandHandler>().CreateAsyncCommand(async provider => storage = await provider.CreateStorage(storage), null);
			this.processor.CreateDataHandler<IAggregatedDataCommandHandler>().CreateAsyncCommand(async provider => storage = await provider.CreateStorage(storage), null);

			await this.processor.ProcessAsync();
		}

		#endregion
	}
}
