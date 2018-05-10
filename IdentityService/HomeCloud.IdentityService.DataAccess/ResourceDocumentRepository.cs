namespace HomeCloud.IdentityService.DataAccess
{
	#region Usings

	using System;

	using HomeCloud.Data.MongoDB;
	using HomeCloud.IdentityService.DataAccess.Objects;

	using MongoDB.Driver;

	#endregion

	/// <summary>
	/// Provides methods to handle data of <see cref="ResourceDocument"/> in <see cref="MongoDB"/> database.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.MongoDB.IMongoDBRepository{HomeCloud.IdentityService.DataAccess.Objects.ResourceDocument}" />
	public class ResourceDocumentRepository : MongoDBRepository<ResourceDocument>, IResourceDocumentRepository
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ResourceDocumentRepository"/> class.
		/// </summary>
		/// <param name="context">The data context.</param>
		public ResourceDocumentRepository(IMongoDBContext context)
			: base(context)
		{
		}

		#endregion

		#region MongoDBRepository<ResourceDocument> Implementations

		/// <summary>
		/// Gets the <see cref="T:System.Linq.Expressions.Expression" />-based <see cref="N:HomeCloud.Data.MongoDB" /> filter definition for <see cref="!:T" /> entity that have unique identifier attribute.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// The instance of <see cref="T:MongoDB.Driver.FilterDefinition`1" />.
		/// </returns>
		protected override FilterDefinition<ResourceDocument> GetUniqueFilterDefinition(ResourceDocument entity)
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
		protected override FilterDefinition<ResourceDocument> GetUniqueFilterDefinition(Guid id)
		{
			return Builders<ResourceDocument>.Filter.Where(entity => entity.ID == id);
		}

		#endregion
	}
}
