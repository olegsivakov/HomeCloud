namespace HomeCloud.DataStorage.DataAccess.Services.Repositories
{
	#region Usings

	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataStorage.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Defines combination of methods to handle the document presenting aggregated file.
	/// </summary>
	public interface IFileAggregationRepository : IDocumentRepository<AggregatedFile>
	{
	}
}
