namespace HomeCloud.DataStorage.DataAccess.Aggregation
{
	#region Usings

	using HomeCloud.Data.MongoDB;
	using HomeCloud.DataStorage.DataAccess.Aggregation.Objects;

	#endregion

	/// <summary>
	/// Defines combination of methods to handle <see cref="CatalogDocument"/>.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.MongoDB.IMongoDBRepository{HomeCloud.DataStorage.DataAccess.Aggregation.Objects.CatalogDocument}" />
	public interface ICatalogDocumentRepository : IMongoDBRepository<CatalogDocument>
	{
	}
}
