namespace HomeCloud.IdentityService.DataAccess
{
	#region Usings

	using System;

	using HomeCloud.Data.MongoDB;
	using HomeCloud.IdentityService.DataAccess.Objects;

	using MongoDB.Driver;

	#endregion

	/// <summary>
	/// Provides methods to handle data of <see cref="IdentityResourceDocument" /> in <see cref="MongoDB" /> database.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.MongoDB.MongoDBRepository{HomeCloud.IdentityService.DataAccess.Objects.IdentityResourceDocument}" />
	/// <seealso cref="HomeCloud.IdentityService.DataAccess.IIdentityResourceDocumentRepository" />
	/// <seealso cref="HomeCloud.Data.MongoDB.IMongoDBRepository{HomeCloud.IdentityService.DataAccess.Objects.IdentityResourceDocument}" />
	public class IdentityResourceDocumentRepository : MongoDBRepository<IdentityResourceDocument>, IIdentityResourceDocumentRepository
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="IdentityResourceDocumentRepository"/> class.
		/// </summary>
		/// <param name="context">The data context.</param>
		public IdentityResourceDocumentRepository(IMongoDBContext context)
			: base(context)
		{
		}

		#endregion

		#region MongoDBRepository<IdentityResourceDocument> Implementations

		/// <summary>
		/// Gets the <see cref="T:System.Linq.Expressions.Expression" />-based <see cref="N:HomeCloud.Data.MongoDB" /> filter definition for <see cref="!:T" /> entity that have unique identifier attribute.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// The instance of <see cref="T:MongoDB.Driver.FilterDefinition`1" />.
		/// </returns>
		protected override FilterDefinition<IdentityResourceDocument> GetUniqueFilterDefinition(IdentityResourceDocument entity)
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
		protected override FilterDefinition<IdentityResourceDocument> GetUniqueFilterDefinition(object id)
		{
			return Builders<IdentityResourceDocument>.Filter.Where(entity => entity.ID == (string)id);
		}

		#endregion
	}
}
