namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Business.Entities;

	using HomeCloud.DataStorage.Business.Providers;
	using HomeCloud.DataStorage.Business.Validation;

	using HomeCloud.Mapping;
	using HomeCloud.Mapping.Extensions;

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
		/// The mapper
		/// </summary>
		private readonly IMapper mapper = null;

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
		/// Initializes a new instance of the <see cref="CatalogService" /> class.
		/// </summary>
		/// <param name="dataFactory">The data factory.</param>
		/// <param name="validationServiceFactory">The service factory of validators.</param>
		public CatalogService(
			IMapper mapper,
			IDataProviderFactory dataFactory,
			IValidationServiceFactory validationServiceFactory)
		{
			this.mapper = mapper;
			this.dataFactory = dataFactory;
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

			catalog = await this.dataFactory.CreateCatalog(catalog);

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
			ServiceResult<Catalog> serviceResult = await this.GetCatalogAsync(catalog.ID);
			if (!serviceResult.IsSuccess)
			{
				return serviceResult;
			}

			this.mapper.Merge(serviceResult.Data, catalog);

			IServiceFactory<ICatalogValidator> validator = this.validationServiceFactory.GetFactory<ICatalogValidator>();
			ValidationResult result = await validator.Get<IUniqueValidator>().ValidateAsync(catalog);
			if (!result.IsValid)
			{
				serviceResult.Errors = result.Errors;

				return serviceResult;
			}

			catalog = await this.dataFactory.UpdateCatalog(catalog);

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

			catalog = await this.dataFactory.GetCatalog(catalog);

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
			Catalog parentCatalog = new Catalog() { ID = parentID };

			IServiceFactory<ICatalogValidator> validator = this.validationServiceFactory.GetFactory<ICatalogValidator>();
			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(parentCatalog);
			if (!result.IsValid)
			{
				return new ServiceResult<IPaginable<Catalog>>(null)
				{
					Errors = result.Errors
				};
			}

			IPaginable<Catalog> catalogs = await this.dataFactory.GetCatalogs(parentCatalog, offset, limit);

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

			await this.dataFactory.DeleteCatalog(serviceResult.Data);

			return serviceResult;
		}

		#endregion
	}
}
