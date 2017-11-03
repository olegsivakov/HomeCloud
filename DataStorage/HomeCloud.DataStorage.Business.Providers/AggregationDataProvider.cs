namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.Core.Extensions;

	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataAccess.Services.Factories;

	using HomeCloud.DataStorage.Api.Configuration;
	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.DataAccess.Services.Repositories;

	using Microsoft.Extensions.Options;

	using CatalogDocument = HomeCloud.DataStorage.DataAccess.Contracts.CatalogDocument;
	using System;

	#endregion

	/// <summary>
	/// Provides methods to provide data from aggregated data source.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Providers.IAggregationDataProvider" />
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
		/// Creates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to create.</param>
		/// <returns>The newly created instance of <see cref="Storage" /> type.</returns>
		public async Task<Storage> CreateStorage(Storage storage)
		{
			using (IDocumentContextScope scope = this.dataContextScopeFactory.CreateDocumentContextScope(this.connectionStrings.DataAggregationDB))
			{
				ICatalogDocumentRepository repository = scope.GetRepository<ICatalogDocumentRepository>();

				CatalogDocument catalogDocument = await this.mapper.MapNewAsync<Catalog, CatalogDocument>(storage.CatalogRoot);
				catalogDocument = await repository.SaveAsync(catalogDocument);

				storage.CatalogRoot = await this.mapper.MapAsync(catalogDocument, storage.CatalogRoot) ?? storage.CatalogRoot;
			}

			return storage;
		}

		/// <summary>
		/// Gets the list of storages.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>The list of instances of <see cref="Storage"/> type.</returns>
		public async Task<IEnumerable<Storage>> GetStorages(int offset = 0, int limit = 20)
		{
			return await Task.FromException<IEnumerable<Storage>>(new NotSupportedException());
		}

		/// <summary>
		/// Gets the catalog by the initial instance set.
		/// </summary>
		/// <param name="catalog">The initial catalog set.</param>
		/// <returns>The instance of <see cref="Catalog"/>.</returns>
		public async Task<Catalog> GetCatalog(Catalog catalog)
		{
			CatalogDocument catalogDocument = null;

			using (IDocumentContextScope scope = this.dataContextScopeFactory.CreateDocumentContextScope(this.connectionStrings.DataAggregationDB))
			{
				ICatalogDocumentRepository repository = scope.GetRepository<ICatalogDocumentRepository>();

				catalogDocument = await repository.GetAsync(catalog.ID);
			}

			return await this.mapper.MapAsync(catalogDocument, catalog) ?? catalog;
		}

		#endregion
	}
}
