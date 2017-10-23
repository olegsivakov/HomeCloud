namespace HomeCloud.DataStorage.DataAccess.Services.Repositories
{
	#region Usings

	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataStorage.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Defines combination of methods to handle the document presenting aggregated catalog.
	/// </summary>
	/// <seealso cref="HomeCloud.DataAccess.Services.IDocumentRepository{HomeCloud.DataStorage.DataAccess.Contracts.AggregatedCatalog}" />
	public interface ICatalogAggregationRepository : IDocumentRepository<AggregatedCatalog>
	{
	}
}
