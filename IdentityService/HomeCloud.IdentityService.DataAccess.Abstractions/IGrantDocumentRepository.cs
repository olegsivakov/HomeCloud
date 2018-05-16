namespace HomeCloud.IdentityService.DataAccess
{
	#region Usings

	using System.Threading.Tasks;

	using HomeCloud.Data.MongoDB;
	using HomeCloud.IdentityService.DataAccess.Objects;

	#endregion

	/// <summary>
	/// Defines methods to handle data of <see cref="GrantDocument" /> in <see cref="MongoDB" /> database.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.MongoDB.IMongoDBRepository{HomeCloud.IdentityService.DataAccess.Objects.GrantDocument}" />
	public interface IGrantDocumentRepository : IMongoDBRepository<GrantDocument>
	{
		/// <summary>
		/// Gets the entity of <see cref="GrantDocument"/> by specified unique identifier asynchronously.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>
		/// The instance of <see cref="GrantDocument" /> type.
		/// </returns>
		Task<GrantDocument> GetAsync(string id);

		/// <summary>
		/// Deletes the record by specified unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		Task DeleteAsync(string id);
	}
}
