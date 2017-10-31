using HomeCloud.Core;
using HomeCloud.Core.Extensions;
using HomeCloud.DataAccess.Services;
using HomeCloud.DataAccess.Services.Factories;
using HomeCloud.DataStorage.Api.Configuration;
using HomeCloud.DataStorage.Business.Entities;
using AggregatedCatalogContract = HomeCloud.DataStorage.DataAccess.Contracts.AggregatedCatalog;
using HomeCloud.DataStorage.DataAccess.Services.Repositories;
using Microsoft.Extensions.Options;
using HomeCloud.DataStorage.Business.Validation.Abstractions;
using HomeCloud.Validation;
using HomeCloud.Exceptions;

namespace HomeCloud.DataStorage.Business.Providers
{
	public class AggregationDataProvider : IAggregationDataProvider
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
		/// The factory of catalog validators.
		/// </summary>
		private readonly IServiceFactory<ICatalogValidator> catalogValidatorFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AggregationDataProvider" /> class.
		/// </summary>
		/// <param name="dataContextScopeFactory">The data context scope factory.</param>
		/// <param name="connectionStrings">The connection strings.</param>
		/// <param name="mapper">The mapper.</param>
		public AggregationDataProvider(
			IDataContextScopeFactory dataContextScopeFactory,
			IOptionsSnapshot<ConnectionStrings> connectionStrings,
			IMapper mapper,
			IServiceFactory<ICatalogValidator> catalogValidatorFactory)
		{
			this.dataContextScopeFactory = dataContextScopeFactory;
			this.connectionStrings = connectionStrings?.Value;

			this.mapper = mapper;
			this.catalogValidatorFactory = catalogValidatorFactory;
		}

		#endregion

		#region IDataStoreProvider Implementations

		/// <summary>
		/// Creates the storage.
		/// </summary>
		/// <param name="storage">The storage.</param>
		public Storage CreateStorage(Storage storage)
		{
			ValidationResult validationResult = this.catalogValidatorFactory.Get<IIdentifierRequiredValidator>().Validate(storage.CatalogRoot);
			validationResult += this.catalogValidatorFactory.Get<ICatalogRequiredValidator>().Validate(storage.CatalogRoot);

			if (!validationResult.IsValid)
			{
				throw new ValidationException(validationResult.Errors);
			}

			using (IDocumentContextScope scope = this.dataContextScopeFactory.CreateDocumentContextScope(this.connectionStrings.DataAggregationDB))
			{
				ICatalogAggregationRepository repository = scope.GetRepository<ICatalogAggregationRepository>();

				repository.Save(this.mapper.MapNew<Catalog, AggregatedCatalogContract>(storage.CatalogRoot));
			}

			return storage;
		}

		public void DeleteStorage(Storage storage)
		{
		}

		public void SetStorageQuota(Storage storage, long quota)
		{
		}

		#endregion
	}
}
