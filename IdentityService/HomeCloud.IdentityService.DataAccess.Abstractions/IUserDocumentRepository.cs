namespace HomeCloud.IdentityService.DataAccess
{
	#region Usings

	using System.Threading.Tasks;

	using HomeCloud.Data.MongoDB;
	using HomeCloud.IdentityService.DataAccess.Objects;

	#endregion

	/// <summary>
	/// Defines methods to handle data of <see cref="UserDocument"/> in <see cref="MongoDB"/> database.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.MongoDB.IMongoDBRepository{HomeCloud.IdentityService.DataAccess.Objects.UserDocument}" />
	public interface IUserDocumentRepository : IMongoDBRepository<UserDocument>
	{
		/// <summary>
		/// Gets the entity of <see cref="!:T" /> by specified username asynchronously.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <returns>
		/// The instance of <see cref="!:T" /> type.
		/// </returns>
		Task<UserDocument> GetAsync(string username);
	}
}
