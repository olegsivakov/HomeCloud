namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using System;
	using System.Linq;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.Core.Extensions;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Extensions;
	using HomeCloud.DataStorage.Business.Handlers;
	using HomeCloud.DataStorage.Business.Providers;
	using HomeCloud.DataStorage.Business.Validation;

	using HomeCloud.Validation;

	#endregion

	/// <summary>
	/// Provides methods to handle catalog entries.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Services.ICatalogEntryService" />
	public class CatalogEntryService : ICatalogEntryService
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

		/// <summary>
		/// The catalog service
		/// </summary>
		private readonly ICatalogService catalogService = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CatalogEntryService"/> class.
		/// </summary>
		/// <param name="processor">The command processor.</param>
		/// <param name="validationServiceFactory">The service factory of validators.</param>
		/// <param name="catalogService">The catalog service.</param>
		public CatalogEntryService(
			ICommandHandlerProcessor processor,
			IValidationServiceFactory validationServiceFactory,
			ICatalogService catalogService)
		{
			this.processor = processor;
			this.validationServiceFactory = validationServiceFactory;

			this.catalogService = catalogService;
		}

		#endregion

		#region ICatalogEntryService Implementations

		/// <summary>
		/// Creates the specified catalog entry.
		/// </summary>
		/// <param name="stream">The <see cref="T:HomeCloud.DataStorage.Business.Entities.CatalogEntryStream" /> content stream of catalog entry.</param>
		/// <returns>
		/// The operation result containing created instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.CatalogEntry" />.
		/// </returns>
		public async Task<ServiceResult<CatalogEntry>> CreateEntryAsync(CatalogEntryStream stream)
		{
			stream.Entry.ID = Guid.Empty;

			IServiceFactory<ICatalogEntryValidator> validator = this.validationServiceFactory.GetFactory<ICatalogEntryValidator>();

			ValidationResult result = await validator.Get<IRequiredValidator>().ValidateAsync(stream.Entry);
			result += await validator.Get<IUniqueValidator>().ValidateAsync(stream.Entry);

			if (!result.IsValid)
			{
				return new ServiceResult<CatalogEntry>(stream.Entry)
				{
					Errors = result.Errors
				};
			}

			Func<IDataProvider, Task> createCatalogEntryFunction = async provider => stream.Entry = await provider.CreateCatalogEntry(stream);
			Func<IDataProvider, Task> createCatalogEntryUndoFunction = async provider => stream.Entry = await provider.DeleteCatalogEntry(stream.Entry);

			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IDataStoreProvider>(createCatalogEntryFunction, createCatalogEntryUndoFunction);
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IAggregationDataProvider>(async provider => stream.Entry.Catalog = await provider.GetCatalog(stream.Entry.Catalog), null);
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IFileSystemProvider>(createCatalogEntryFunction, createCatalogEntryUndoFunction);
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IAggregationDataProvider>(createCatalogEntryFunction, createCatalogEntryUndoFunction);

			await this.processor.ProcessAsync();

			return new ServiceResult<CatalogEntry>(stream.Entry);
		}

		/// <summary>
		/// Deletes the catalog entry by specified identifier.
		/// </summary>
		/// <param name="id">The catalog entry identifier.</param>
		/// <returns>
		/// The operation result.
		/// </returns>
		public async Task<ServiceResult> DeleteEntryAsync(Guid id)
		{
			ServiceResult<CatalogEntry> serviceResult = await this.GetEntryAsync(id);
			if (!serviceResult.IsSuccess)
			{
				return serviceResult;
			}

			CatalogEntry entry = serviceResult.Data;
			Func<IDataProvider, Task> deleteCatalogEntryFunction = async provider => entry = await provider.DeleteCatalogEntry(entry);

			this.processor.CreateDataHandler<IDataCommandHandler>()
				.CreateAsyncCommand<IDataStoreProvider>(deleteCatalogEntryFunction, null)
				.CreateAsyncCommand<IFileSystemProvider>(deleteCatalogEntryFunction, null)
				.CreateAsyncCommand<IAggregationDataProvider>(deleteCatalogEntryFunction, null);

			await this.processor.ProcessAsync();

			return new ServiceResult<CatalogEntry>(entry);
		}

		/// <summary>
		/// Gets the catalog entry by specified identifier.
		/// </summary>
		/// <param name="id">The catalog entry identifier.</param>
		/// <returns>
		/// The operation result containing the instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.CatalogEntry" />.
		/// </returns>
		public async Task<ServiceResult<CatalogEntry>> GetEntryAsync(Guid id)
		{
			CatalogEntry entry = new CatalogEntry() { ID = id };

			IServiceFactory<ICatalogEntryValidator> validator = this.validationServiceFactory.GetFactory<ICatalogEntryValidator>();
			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(entry);

			if (!result.IsValid)
			{
				return new ServiceResult<CatalogEntry>(entry)
				{
					Errors = result.Errors
				};
			}

			Func<IDataProvider, Task> getCatalogEntryFunction = async provider => entry = await provider.GetCatalogEntry(entry);

			this.processor.CreateDataHandler<IDataCommandHandler>()
				.CreateAsyncCommand<IDataStoreProvider>(getCatalogEntryFunction, null)
				.CreateAsyncCommand<IAggregationDataProvider>(getCatalogEntryFunction, null);

			await this.processor.ProcessAsync();

			return new ServiceResult<CatalogEntry>(entry);
		}

		/// <summary>
		/// Gets the content stream of the catalog entry by specified entry identifier.
		/// </summary>
		/// <param name="id">The catalog entry identifier.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="length">The number of bytes from byte array to return.</param>
		/// <returns>
		/// The operation result containing the instance of <see cref="CatalogEntryStream" />.
		/// </returns>
		public async Task<ServiceResult<CatalogEntryStream>> GetEntryStreamAsync(Guid id, int offset, int length = 1024)
		{
			CatalogEntry entry = new CatalogEntry() { ID = id };

			IServiceFactory<ICatalogEntryValidator> validator = this.validationServiceFactory.GetFactory<ICatalogEntryValidator>();
			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(entry);

			if (!result.IsValid)
			{
				return new ServiceResult<CatalogEntryStream>(new CatalogEntryStream(entry, 0))
				{
					Errors = result.Errors
				};
			}

			CatalogEntryStream stream = null;

			Func<IDataProvider, Task> getCatalogEntryStreamFunction = async provider => entry = await provider.GetCatalogEntry(entry);

			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IAggregationDataProvider>(async provider => entry = await provider.GetCatalogEntry(entry), null);
			this.processor.CreateDataHandler<IDataCommandHandler>()
				.CreateAsyncCommand<IDataStoreProvider>(async provider => entry = await provider.GetCatalogEntry(entry), null)
				.CreateAsyncCommand<IFileSystemProvider>(async provider => stream = await provider.GetCatalogEntryStream(entry, offset, length), null);

			await this.processor.ProcessAsync();

			return new ServiceResult<CatalogEntryStream>(stream);
		}

		/// <summary>
		/// Gets the list of catalog entries that belong to the catalog which identifier is specified.
		/// </summary>
		/// <param name="catalogID">The catalog identifier.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The operation result containing the list of instances of <see cref="T:HomeCloud.DataStorage.Business.Entities.CatalogEntry" />.
		/// </returns>
		public async Task<ServiceResult<IPaginable<CatalogEntry>>> GetEntriesAsync(Guid catalogID, int offset = 0, int limit = 20)
		{
			ServiceResult<Catalog> serviceResult = await this.catalogService.GetCatalogAsync(catalogID);
			if (!serviceResult.IsSuccess)
			{
				return new ServiceResult<IPaginable<CatalogEntry>>(Enumerable.Empty<CatalogEntry>().AsPaginable())
				{
					Errors = serviceResult.Errors
				};
			}

			IPaginable<CatalogEntry> entries = null;

			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IDataStoreProvider>(async provider => entries = await provider.GetCatalogEntries(serviceResult.Data, offset, limit), null);
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommandFor<CatalogEntry, IAggregationDataProvider>(entries, async (provider, item) => await provider.GetCatalogEntry(item), null);

			await this.processor.ProcessAsync();

			return new ServiceResult<IPaginable<CatalogEntry>>(entries);
		}

		#endregion
	}
}
