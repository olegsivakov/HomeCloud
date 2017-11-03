namespace HomeCloud.DataStorage.DataAccess.Services.Repositories
{
	#region Usings

	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataStorage.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Defines combination of methods to handle catalog documents.
	/// </summary>
	/// <seealso cref="HomeCloud.DataAccess.Services.IDocumentRepository{HomeCloud.DataStorage.DataAccess.Contracts.CatalogDocument}" />
	public interface ICatalogDocumentRepository : IDocumentRepository<CatalogDocument>
	{
	}
}
