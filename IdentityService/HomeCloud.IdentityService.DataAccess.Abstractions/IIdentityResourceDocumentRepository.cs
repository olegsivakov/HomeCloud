namespace HomeCloud.IdentityService.DataAccess
{
	#region Usings

	using HomeCloud.Data.MongoDB;
	using HomeCloud.IdentityService.DataAccess.Objects;

	#endregion

	/// <summary>
	/// Defines methods to handle data of <see cref="IdentityResourceDocument"/> in <see cref="MongoDB"/> database.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.MongoDB.IMongoDBRepository{HomeCloud.IdentityService.DataAccess.Objects.IdentityResourceDocument}" />
	public interface IIdentityResourceDocumentRepository : IMongoDBRepository<IdentityResourceDocument>
	{
	}
}
