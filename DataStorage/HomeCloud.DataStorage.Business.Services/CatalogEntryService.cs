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
	/// Provides methods to handle catalog entries.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Services.ICatalogEntryService" />
	public class CatalogEntryService : ICatalogEntryService
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
		/// Initializes a new instance of the <see cref="CatalogEntryService"/> class.
		/// </summary>
		/// <param name="dataFactory">The data factory.</param>
		/// <param name="validationServiceFactory">The service factory of validators.</param>
		public CatalogEntryService(
			IDataProviderFactory dataFactory,
			IValidationServiceFactory validationServiceFactory)
		{
			this.dataFactory = dataFactory;
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
			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
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

				stream.Entry = await this.dataFactory.CreateCatalogEntry(stream);

				scope.Complete();
			}

			this.dataFactory.RecalculateSize(stream.Entry.Catalog);

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
			ServiceResult<CatalogEntry> serviceResult = null;

			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				serviceResult = await this.GetEntryAsync(id);
				if (!serviceResult.IsSuccess)
				{
					return serviceResult;
				}

				await this.dataFactory.DeleteCatalogEntry(serviceResult.Data);

				scope.Complete();
			}

			this.dataFactory.RecalculateSize(serviceResult.Data.Catalog);

			return serviceResult;
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

			entry = await this.dataFactory.GetCatalogEntry(entry);

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

			CatalogEntryStream stream = await this.dataFactory.GetCatalogEntryStream(entry, offset, length);

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
			Catalog catalog = new Catalog() { ID = catalogID };

			IServiceFactory<ICatalogValidator> validator = this.validationServiceFactory.GetFactory<ICatalogValidator>();
			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(catalog);
			if (!result.IsValid)
			{
				return new ServiceResult<IPaginable<CatalogEntry>>(null)
				{
					Errors = result.Errors
				};
			}

			IPaginable<CatalogEntry> entries = await this.dataFactory.GetCatalogEntries(catalog, offset, limit);

			return new ServiceResult<IPaginable<CatalogEntry>>(entries);
		}

		#endregion
	}
}
