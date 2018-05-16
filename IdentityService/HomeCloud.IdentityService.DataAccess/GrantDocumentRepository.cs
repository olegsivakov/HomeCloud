namespace HomeCloud.IdentityService.DataAccess
{
	using System;
	using System.Threading.Tasks;
	#region Usings

	using HomeCloud.Data.MongoDB;
	using HomeCloud.IdentityService.DataAccess.Objects;

	using MongoDB.Driver;

	#endregion

	/// <summary>
	/// Provides methods to handle data of <see cref="GrantDocument" /> in <see cref="MongoDB" /> database.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.MongoDB.MongoDBRepository{HomeCloud.IdentityService.DataAccess.Objects.GrantDocument}" />
	/// <seealso cref="HomeCloud.IdentityService.DataAccess.IGrantDocumentRepository" />
	public class GrantDocumentRepository : MongoDBRepository<GrantDocument>, IGrantDocumentRepository
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="GrantDocumentRepository"/> class.
		/// </summary>
		/// <param name="context">The data context.</param>
		public GrantDocumentRepository(IMongoDBContext context)
			: base(context)
		{
		}

		#endregion

		#region IGrantDocumentRepository Implementations

		public async Task<GrantDocument> GetAsync(string id)
		{
			FilterDefinition<GrantDocument> filter = this.GetUniqueFilterDefinition(id);

			IAsyncCursor<GrantDocument> cursor = await this.CurrentCollection.FindAsync(
				filter,
				new FindOptions<GrantDocument>()
				{
					Skip = 0,
					Limit = 1
				});

			return await cursor.FirstOrDefaultAsync();
		}

		/// <summary>
		/// Deletes the record by specified unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public async Task DeleteAsync(string id)
		{
			FilterDefinition<GrantDocument> filter = this.GetUniqueFilterDefinition(id);

			await this.CurrentCollection.DeleteOneAsync(filter);
		}

		#endregion

		#region MongoDBRepository<GrantDocument> Implementations

		/// <summary>
		/// Gets the entity of <see cref="!:T" /> by specified unique identifier asynchronously.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>
		/// The instance of <see cref="!:T" /> type.
		/// </returns>
		/// <exception cref="System.NotSupportedException">The method is not supported due to a type of identifier.</exception>
		public override async Task<GrantDocument> GetAsync(Guid id)
		{
			await Task.FromException(new NotSupportedException("The method is not supported due to a type of identifier."));

			return null;
		}

		/// <summary>
		/// Deletes the record by specified unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		/// <exception cref="System.NotSupportedException">The method is not supported due to a type of identifier.</exception>
		public override async Task DeleteAsync(Guid id)
		{
			await Task.FromException(new NotSupportedException("The method is not supported due to a type of identifier."));
		}

		/// <summary>
		/// Gets the <see cref="T:System.Linq.Expressions.Expression" />-based <see cref="N:HomeCloud.Data.MongoDB" /> filter definition for <see cref="!:T" /> entity that have unique identifier attribute.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// The instance of <see cref="T:MongoDB.Driver.FilterDefinition`1" />.
		/// </returns>
		protected override FilterDefinition<GrantDocument> GetUniqueFilterDefinition(GrantDocument entity)
		{
			return this.GetUniqueFilterDefinition(entity.ID);
		}

		/// <summary>
		/// Gets the <see cref="T:System.Linq.Expressions.Expression" />-based <see cref="N:HomeCloud.Data.MongoDB" /> filter definition based on <see cref="T:System.Guid" /> identifier.
		/// </summary>
		/// <param name="id">The object identifier.</param>
		/// <returns>
		/// The instance of <see cref="T:MongoDB.Driver.FilterDefinition`1" />.
		/// </returns>
		protected override FilterDefinition<GrantDocument> GetUniqueFilterDefinition(object id)
		{
			return Builders<GrantDocument>.Filter.Where(entity => entity.ID == (string)id);
		}

		#endregion
	}
}
