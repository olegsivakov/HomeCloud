namespace HomeCloud.DataStorage.DataAccess.Aggregation
{
	#region Usings

	using System;

	using HomeCloud.Data.MongoDB;
	using HomeCloud.DataStorage.DataAccess.Aggregation.Objects;

	using MongoDB.Driver;

	#endregion

	/// <summary>
	/// Provides methods to handle <see cref="CatalogDocument" />.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.MongoDB.MongoDBRepository{HomeCloud.DataStorage.DataAccess.Aggregation.Objects.CatalogDocument}" />
	/// <seealso cref="HomeCloud.DataStorage.DataAccess.Aggregation.ICatalogDocumentRepository" />
	public sealed class CatalogDocumentRepository : MongoDBRepository<CatalogDocument>, ICatalogDocumentRepository
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CatalogDocumentRepository" /> class.
		/// </summary>
		/// <param name="context">The data context.</param>
		public CatalogDocumentRepository(IMongoDBContext context)
			: base(context)
		{
		}

		#endregion

		#region MongoDBRepository<CatalogDocument> Implementations

		/// <summary>
		/// Gets the <see cref="T:System.Linq.Expressions.Expression" />-based <see cref="N:HomeCloud.Data.MongoDB" /> filter definition for <see cref="!:T" /> entity that have unique identifier attribute.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// The instance of <see cref="T:MongoDB.Driver.FilterDefinition`1" />.
		/// </returns>
		protected override FilterDefinition<CatalogDocument> GetUniqueFilterDefinition(CatalogDocument entity)
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
		protected override FilterDefinition<CatalogDocument> GetUniqueFilterDefinition(Guid id)
		{
			return Builders<CatalogDocument>.Filter.Where(entity => entity.ID == id);
		}

		#endregion
	}
}
