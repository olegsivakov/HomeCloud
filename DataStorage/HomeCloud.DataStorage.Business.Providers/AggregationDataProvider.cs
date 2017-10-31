using HomeCloud.Core;
using HomeCloud.Core.Extensions;
using HomeCloud.DataAccess.Services;
using HomeCloud.DataAccess.Services.Factories;
using HomeCloud.DataStorage.Api.Configuration;
using HomeCloud.DataStorage.Business.Entities;
using AggregatedCatalogContract = HomeCloud.DataStorage.DataAccess.Contracts.AggregatedCatalog;
using HomeCloud.DataStorage.DataAccess.Services.Repositories;
using Microsoft.Extensions.Options;

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
			IMapper mapper)
		{
			this.dataContextScopeFactory = dataContextScopeFactory;
			this.connectionStrings = connectionStrings?.Value;

			this.mapper = mapper;
		}

		#endregion

		#region IDataStoreProvider Implementations

		/// <summary>
		/// Creates the storage.
		/// </summary>
		/// <param name="storage">The storage.</param>
		public void CreateStorage(Storage storage)
		{
			using (IDocumentContextScope scope = this.dataContextScopeFactory.CreateDocumentContextScope(this.connectionStrings.DataAggregationDB))
			{
				ICatalogAggregationRepository repository = scope.GetRepository<ICatalogAggregationRepository>();

				repository.Save(this.mapper.MapNew<Catalog, AggregatedCatalogContract>(storage.CatalogRoot));
			}
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
