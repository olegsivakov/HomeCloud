namespace HomeCloud.IdentityService.DataAccess
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Data.MongoDB;
	using HomeCloud.IdentityService.DataAccess.Objects;

	using MongoDB.Driver;

	#endregion

	/// <summary>
	/// Provides methods to handle data of <see cref="UserDocument" /> in <see cref="MongoDB" /> database.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.MongoDB.MongoDBRepository{HomeCloud.IdentityService.DataAccess.Objects.UserDocument}" />
	/// <seealso cref="HomeCloud.IdentityService.DataAccess.IUserDocumentRepository" />
	/// <seealso cref="HomeCloud.Data.MongoDB.IMongoDBRepository{HomeCloud.IdentityService.DataAccess.Objects.UserDocument}" />
	public class UserDocumentRepository : MongoDBRepository<UserDocument>, IUserDocumentRepository
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="UserDocumentRepository"/> class.
		/// </summary>
		/// <param name="context">The data context.</param>
		public UserDocumentRepository(IMongoDBContext context)
			: base(context)
		{
		}

		#endregion

		#region IUserDocumentRepositoryImplementations

		/// <summary>
		/// Gets the entity of <see cref="!:T" /> by specified username asynchronously.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <returns>
		/// The instance of <see cref="!:T" /> type.
		/// </returns>
		public async Task<UserDocument> GetAsync(string username)
		{
			IAsyncCursor<UserDocument> cursor = await this.CurrentCollection.FindAsync(
				entity => entity.Username.ToLower() == username.ToLower(),
				new FindOptions<UserDocument>()
				{
					Skip = 0,
					Limit = 1
				});

			return await cursor.FirstOrDefaultAsync();
		}

		#endregion

		#region MongoDBRepository<UserDocument> Implementations

		/// <summary>
		/// Gets the <see cref="T:System.Linq.Expressions.Expression" />-based <see cref="N:HomeCloud.Data.MongoDB" /> filter definition for <see cref="!:T" /> entity that have unique identifier attribute.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// The instance of <see cref="T:MongoDB.Driver.FilterDefinition`1" />.
		/// </returns>
		protected override FilterDefinition<UserDocument> GetUniqueFilterDefinition(UserDocument entity)
		{
			return this.GetUniqueFilterDefinition(entity.ID);
		}

		/// <summary>
		/// Gets the <see cref="T:System.Linq.Expressions.Expression" />-based <see cref="N:HomeCloud.Data.MongoDB" /> filter definition based on <see cref="T:System.Guid" /> identifier.
		/// </summary>
		/// <param name="id">The <see cref="T:System.Guid" /> identifier.</param>
		/// <returns>
		/// The instance of <see cref="T:MongoDB.Driver.FilterDefinition`1" />.
		/// </returns>
		protected override FilterDefinition<UserDocument> GetUniqueFilterDefinition(object id)
		{
			return Builders<UserDocument>.Filter.Where(entity => entity.ID == (Guid)id);
		}

		#endregion
	}
}
