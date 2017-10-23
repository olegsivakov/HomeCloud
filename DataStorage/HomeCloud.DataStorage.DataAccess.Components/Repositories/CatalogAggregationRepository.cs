namespace HomeCloud.DataStorage.DataAccess.Components.Repositories
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.DataAccess.Contracts;

	using HomeCloud.DataStorage.DataAccess.Contracts;
	using HomeCloud.DataStorage.DataAccess.Services.Repositories;

	#endregion

	/// <summary>
	/// Provides combination of methods to handle the document presenting aggregated catalog.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.DataAccess.Services.Repositories.ICatalogAggregationRepository" />
	public class CatalogAggregationRepository : ICatalogAggregationRepository
	{
		#region Private Members

		/// <summary>
		/// The document context
		/// </summary>
		private readonly IDocumentContext context = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CatalogAggregationRepository" /> class.
		/// </summary>
		/// <param name="context">The document context.</param>
		public CatalogAggregationRepository(IDocumentContext context)
		{
			this.context = context;
		}

		#endregion

		#region IAggregatedCatalogRepository Implementations

		public void Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<AggregatedCatalog> Find(int offset = 0, int limit = 20)
		{
			throw new NotImplementedException();
		}

		public AggregatedCatalog Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Save(AggregatedCatalog entity)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
