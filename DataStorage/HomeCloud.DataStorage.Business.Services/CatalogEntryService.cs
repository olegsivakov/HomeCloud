namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.DataStorage.Business.Entities;
	using System.Collections.Generic;
	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Validation;
	using HomeCloud.DataStorage.Business.Handlers;
	using HomeCloud.Validation;
	using HomeCloud.DataStorage.Business.Providers;

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

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CatalogService" /> class.
		/// </summary>
		/// <param name="processor">The command processor.</param>
		/// <param name="validationServiceFactory">The service factory of validators.</param>
		public CatalogEntryService(
			ICommandHandlerProcessor processor,
			IValidationServiceFactory validationServiceFactory)
		{
			this.processor = processor;
			this.validationServiceFactory = validationServiceFactory;
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
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IAggregationDataProvider>(async provider => stream.Entry.Catalog = await provider.GetCatalog(stream.Entry.Catalog as Catalog), null);
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IFileSystemProvider>(createCatalogEntryFunction, createCatalogEntryUndoFunction);
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IAggregationDataProvider>(createCatalogEntryFunction, createCatalogEntryUndoFunction);

			await this.processor.ProcessAsync();

			return new ServiceResult<Catalog>(catalog);
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
		}

		/// <summary>
		/// Gets the entry stream asynchronous.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="size">The size.</param>
		/// <returns></returns>
		public async Task<ServiceResult<CatalogEntryStream>> GetEntryStreamAsync(Guid id, int offset, int size = 1024)
		{
		}

		/// <summary>
		/// Gets the entries asynchronous.
		/// </summary>
		/// <param name="catalogID">The catalog identifier.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="limit">The limit.</param>
		/// <returns></returns>
		public async Task<ServiceResult<IEnumerable<CatalogEntry>>> GetEntriesAsync(Guid catalogID, int offset = 0, int limit = 20)
		{
		}
	}
}
