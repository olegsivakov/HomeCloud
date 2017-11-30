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
	/// Provides methods to handle catalogs.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Services.ICatalogService" />
	public class CatalogService : ICatalogService
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
		public CatalogService(
			ICommandHandlerProcessor processor,
			IValidationServiceFactory validationServiceFactory)
		{
			this.processor = processor;
			this.validationServiceFactory = validationServiceFactory;
		}

		#endregion

		#region ICatalogService Implementations

		/// <summary>
		/// Creates the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.Catalog" /> type.</param>
		/// <returns>
		/// The operation result containing created instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.Catalog" />.
		/// </returns>
		public async Task<ServiceResult<Catalog>> CreateCatalogAsync(Catalog catalog)
		{
			catalog.ID = Guid.Empty;

			IServiceFactory<ICatalogValidator> validator = this.validationServiceFactory.GetFactory<ICatalogValidator>();

			ValidationResult result = await validator.Get<IRequiredValidator>().ValidateAsync(catalog);
			result += await validator.Get<IUniqueValidator>().ValidateAsync(catalog);

			if (!result.IsValid)
			{
				return new ServiceResult<Catalog>(catalog)
				{
					Errors = result.Errors
				};
			}

			Func<IDataProvider, Task> createCatalogFunction = async provider => catalog = await provider.CreateCatalog(catalog);
			Func<IDataProvider, Task> createCatalogUndoFunction = async provider => catalog = await provider.DeleteCatalog(catalog);

			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IDataStoreProvider>(createCatalogFunction, createCatalogUndoFunction);
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IAggregationDataProvider>(async provider => catalog.Parent = await provider.GetCatalog(catalog.Parent), null);
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IFileSystemProvider>(createCatalogFunction, createCatalogUndoFunction);
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IAggregationDataProvider>(createCatalogFunction, createCatalogUndoFunction);

			await this.processor.ProcessAsync();

			return new ServiceResult<Catalog>(catalog);
		}

		/// <summary>
		/// Updates the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.Catalog" /> type.</param>
		/// <returns>
		/// The operation result containing updated instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.Catalog" />.
		/// </returns>
		public async Task<ServiceResult<Catalog>> UpdateCatalogAsync(Catalog catalog)
		{
			IServiceFactory<ICatalogValidator> validator = this.validationServiceFactory.GetFactory<ICatalogValidator>();

			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(catalog);
			result += await validator.Get<IUniqueValidator>().ValidateAsync(catalog);

			if (!result.IsValid)
			{
				return new ServiceResult<Catalog>(catalog)
				{
					Errors = result.Errors
				};
			}

			Func<IDataProvider, Task> updateCatalogFunction = async provider => catalog = await provider.UpdateCatalog(catalog);

			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IDataStoreProvider>(updateCatalogFunction, null);
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IAggregationDataProvider>(async provider => catalog = await provider.GetCatalog(catalog), null);
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IFileSystemProvider>(updateCatalogFunction, null);
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IAggregationDataProvider>(updateCatalogFunction, null);

			await this.processor.ProcessAsync();

			return new ServiceResult<Catalog>(catalog);
		}

		/// <summary>
		/// Gets the catalog by specified identifier.
		/// </summary>
		/// <param name="id">The catalog identifier.</param>
		/// <returns>
		/// The operation result containing the instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.Catalog" />.
		/// </returns>
		public async Task<ServiceResult<Catalog>> GetCatalogAsync(Guid id)
		{
			Catalog catalog = new Catalog() { ID = id };

			IServiceFactory<ICatalogValidator> validator = this.validationServiceFactory.GetFactory<ICatalogValidator>();
			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(catalog);

			if (!result.IsValid)
			{
				return new ServiceResult<Catalog>(catalog)
				{
					Errors = result.Errors
				};
			}

			Func<IDataProvider, Task> getCatalogFunction = async provider => catalog = await provider.GetCatalog(catalog);

			this.processor.CreateDataHandler<IDataCommandHandler>()
				.CreateAsyncCommand<IDataStoreProvider>(getCatalogFunction, null)
				.CreateAsyncCommand<IAggregationDataProvider>(getCatalogFunction, null);

			await this.processor.ProcessAsync();

			return new ServiceResult<Catalog>(catalog);
		}

		/// <summary>
		/// Gets the list of catalogs by specified parent one.
		/// </summary>
		/// <param name="parentID">The parent catalog identifier.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The operation result containing the list of instances of <see cref="T:HomeCloud.DataStorage.Business.Entities.Catalog" />.
		/// </returns>
		public async Task<ServiceResult<IPaginable<Catalog>>> GetCatalogsAsync(Guid parentID, int offset = 0, int limit = 20)
		{
			ServiceResult<Catalog> serviceResult = await this.GetCatalogAsync(parentID);
			if (!serviceResult.IsSuccess)
			{
				return new ServiceResult<IPaginable<Catalog>>(Enumerable.Empty<Catalog>().AsPaginable())
				{
					Errors = serviceResult.Errors
				};
			}

			IPaginable<Catalog> catalogs = null;

			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommand<IDataStoreProvider>(async provider => catalogs = await provider.GetCatalogs(serviceResult.Data, offset, limit), null);
			this.processor.CreateDataHandler<IDataCommandHandler>().CreateAsyncCommandFor<Catalog, IAggregationDataProvider>(catalogs, async (provider, item) => await provider.GetCatalog(item), null);

			await this.processor.ProcessAsync();

			return new ServiceResult<IPaginable<Catalog>>(catalogs);
		}

		/// <summary>
		/// Deletes the catalog by specified identifier.
		/// </summary>
		/// <param name="id">The catalog identifier.</param>
		/// <returns>
		/// The operation result.
		/// </returns>
		public async Task<ServiceResult> DeleteCatalogAsync(Guid id)
		{
			ServiceResult<Catalog> serviceResult = await this.GetCatalogAsync(id);
			if (!serviceResult.IsSuccess)
			{
				return serviceResult;
			}

			Catalog catalog = serviceResult.Data;
			Func<IDataProvider, Task> deleteCatalogFunction = async provider => catalog = await provider.DeleteCatalog(catalog);

			this.processor.CreateDataHandler<IDataCommandHandler>()
				.CreateAsyncCommand<IDataStoreProvider>(deleteCatalogFunction, null)
				.CreateAsyncCommand<IFileSystemProvider>(deleteCatalogFunction, null)
				.CreateAsyncCommand<IAggregationDataProvider>(deleteCatalogFunction, null);

			await this.processor.ProcessAsync();

			return new ServiceResult<Catalog>(catalog);
		}

		#endregion
	}
}
