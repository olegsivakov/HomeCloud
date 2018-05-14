namespace HomeCloud.IdentityService.DataAccess
{
	#region Usings

	using System;

	using HomeCloud.Data.MongoDB;
	using HomeCloud.IdentityService.DataAccess.Objects;

	using MongoDB.Driver;

	#endregion

	/// <summary>
	/// Provides methods to handle data of <see cref="ClientDocument" /> in <see cref="MongoDB" /> database.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.MongoDB.MongoDBRepository{HomeCloud.IdentityService.DataAccess.Objects.ClientDocument}" />
	/// <seealso cref="HomeCloud.IdentityService.DataAccess.IClientDocumentRepository" />
	/// <seealso cref="HomeCloud.Data.MongoDB.IMongoDBRepository{HomeCloud.IdentityService.DataAccess.Objects.ClientDocument}" />
	public class ClientDocumentRepository : MongoDBRepository<ClientDocument>, IClientDocumentRepository
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ClientDocumentRepository"/> class.
		/// </summary>
		/// <param name="context">The data context.</param>
		public ClientDocumentRepository(IMongoDBContext context)
			: base(context)
		{
		}

		#endregion

		#region MongoDBRepository<ClientDocument> Implementations

		/// <summary>
		/// Gets the <see cref="T:System.Linq.Expressions.Expression" />-based <see cref="N:HomeCloud.Data.MongoDB" /> filter definition for <see cref="!:T" /> entity that have unique identifier attribute.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// The instance of <see cref="T:MongoDB.Driver.FilterDefinition`1" />.
		/// </returns>
		protected override FilterDefinition<ClientDocument> GetUniqueFilterDefinition(ClientDocument entity)
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
		protected override FilterDefinition<ClientDocument> GetUniqueFilterDefinition(object id)
		{
			return Builders<ClientDocument>.Filter.Where(entity => entity.ID == (Guid)id);
		}

		#endregion
	}
}
