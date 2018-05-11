namespace HomeCloud.IdentityService.DataAccess
{
	#region Usings

	using System;

	using HomeCloud.Data.MongoDB;
	using HomeCloud.IdentityService.DataAccess.Objects;

	using MongoDB.Driver;

	#endregion

	/// <summary>
	/// Provides methods to handle data of <see cref="ApiResourceDocument" /> in <see cref="MongoDB" /> database.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.MongoDB.MongoDBRepository{HomeCloud.IdentityService.DataAccess.Objects.ApiResourceDocument}" />
	/// <seealso cref="HomeCloud.IdentityService.DataAccess.IApiResourceDocumentRepository" />
	/// <seealso cref="HomeCloud.Data.MongoDB.IMongoDBRepository{HomeCloud.IdentityService.DataAccess.Objects.ApiResourceDocument}" />
	public class ApiResourceDocumentRepository : MongoDBRepository<ApiResourceDocument>, IApiResourceDocumentRepository
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ApiResourceDocumentRepository"/> class.
		/// </summary>
		/// <param name="context">The data context.</param>
		public ApiResourceDocumentRepository(IMongoDBContext context)
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
		protected override FilterDefinition<ApiResourceDocument> GetUniqueFilterDefinition(ApiResourceDocument entity)
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
		protected override FilterDefinition<ApiResourceDocument> GetUniqueFilterDefinition(object id)
		{
			return Builders<ApiResourceDocument>.Filter.Where(entity => entity.ID == (Guid)id);
		}

		#endregion
	}
}
