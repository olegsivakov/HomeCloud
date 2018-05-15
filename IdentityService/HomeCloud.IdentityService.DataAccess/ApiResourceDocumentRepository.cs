namespace HomeCloud.IdentityService.DataAccess
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using HomeCloud.Core;
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

		#region IApiResourceDocumentRepository Implementations

		/// <summary>
		/// Searches for the <see cref="ApiResourceDocument"/> claims by specified <paramref name="selector"/>.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <param name="offset">The start index to search from.</param>
		/// <param name="limit">The number of records tor eturn.</param>
		/// <returns>The list of claim strings.</returns>
		public async Task<IPaginable<string>> FindClaims(Func<ApiResourceDocument, string, bool> selector, int offset, int limit = 20)
		{
			ProjectionDefinition<ApiResourceDocument> projection = Builders<ApiResourceDocument>.Projection.ElemMatch(contract => contract.Claims, projectionDocument => projectionDocument != null ? selector(null, projectionDocument) : false);
			FilterDefinition<ApiResourceDocument> filter = Builders<ApiResourceDocument>.Filter.Where(document => selector(document, null));

			IAsyncCursor<string> cursor = await this.CurrentCollection.FindAsync(filter, new FindOptions<ApiResourceDocument, string>()
			{
				Projection = projection
			});

			IEnumerable<string> result = await cursor.ToListAsync();

			return new PagedList<string>(result.Skip(offset).Take(limit))
			{
				Offset = offset,
				Limit = limit,
				TotalCount = result.Count()
			};
		}

		/// <summary>
		/// Searches for the <see cref="ApiResourceDocument"/> secrets by specified <paramref name="selector"/>.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <param name="offset">The start index to search from.</param>
		/// <param name="limit">The number of records tor eturn.</param>
		/// <returns>The list of secret strings.</returns>
		public async Task<IPaginable<string>> FindSecrets(Func<ApiResourceDocument, string, bool> selector, int offset, int limit = 20)
		{
			ProjectionDefinition<ApiResourceDocument> projection = Builders<ApiResourceDocument>.Projection.ElemMatch(contract => contract.Secrets, projectionDocument => projectionDocument != null ? selector(null, projectionDocument) : false);
			FilterDefinition<ApiResourceDocument> filter = Builders<ApiResourceDocument>.Filter.Where(document => selector(document, null));

			IAsyncCursor<string> cursor = await this.CurrentCollection.FindAsync(filter, new FindOptions<ApiResourceDocument, string>()
			{
				Projection = projection
			});

			IEnumerable<string> result = await cursor.ToListAsync();

			return new PagedList<string>(result.Skip(offset).Take(limit))
			{
				Offset = offset,
				Limit = limit,
				TotalCount = result.Count()
			};
		}

		/// <summary>
		/// Searches for the <see cref="ApiResourceDocument"/> scopes by specified <paramref name="selector"/>.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <param name="offset">The start index to search from.</param>
		/// <param name="limit">The number of records tor eturn.</param>
		/// <returns>The list of scope strings.</returns>
		public async Task<IPaginable<string>> FindScopes(Func<ApiResourceDocument, string, bool> selector, int offset, int limit = 20)
		{
			ProjectionDefinition<ApiResourceDocument> projection = Builders<ApiResourceDocument>.Projection.ElemMatch(contract => contract.Scopes, projectionDocument => projectionDocument != null ? selector(null, projectionDocument) : false);
			FilterDefinition<ApiResourceDocument> filter = Builders<ApiResourceDocument>.Filter.Where(document => selector(document, null));

			IAsyncCursor<string> cursor = await this.CurrentCollection.FindAsync(filter, new FindOptions<ApiResourceDocument, string>()
			{
				Projection = projection
			});

			IEnumerable<string> result = await cursor.ToListAsync();

			return new PagedList<string>(result.Skip(offset).Take(limit))
			{
				Offset = offset,
				Limit = limit,
				TotalCount = result.Count()
			};
		}

		/// <summary>
		/// Saves the claims of the specified <paramref name="document"/> of <see cref="ApiResourceDocument"/> type.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <returns>The updated instance of <see cref="ApiResourceDocument"/>.</returns>
		public async Task<ApiResourceDocument> SaveClaims(ApiResourceDocument document)
		{
			if (document.ID == Guid.Empty)
			{
				document = await this.SaveAsync(document);
			}

			return await this.CurrentCollection.FindOneAndUpdateAsync(
				this.GetUniqueFilterDefinition(document),
				Builders<ApiResourceDocument>.Update.Set(contract => contract.Claims, document.Claims),
				new FindOneAndUpdateOptions<ApiResourceDocument>()
				{
					IsUpsert = true,
					ReturnDocument = ReturnDocument.After
				});
		}

		/// <summary>
		/// Saves the secrets of the specified <paramref name="document"/> of <see cref="ApiResourceDocument"/> type.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <returns>The updated instance of <see cref="ApiResourceDocument"/>.</returns>
		public async Task<ApiResourceDocument> SaveSecrets(ApiResourceDocument document)
		{
			if (document.ID == Guid.Empty)
			{
				document = await this.SaveAsync(document);
			}

			return await this.CurrentCollection.FindOneAndUpdateAsync(
				this.GetUniqueFilterDefinition(document),
				Builders<ApiResourceDocument>.Update.Set(contract => contract.Secrets, document.Secrets),
				new FindOneAndUpdateOptions<ApiResourceDocument>()
				{
					IsUpsert = true,
					ReturnDocument = ReturnDocument.After
				});
		}

		/// <summary>
		/// Saves the scopes of the specified <paramref name="document"/> of <see cref="ApiResourceDocument"/> type.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <returns>The updated instance of <see cref="ApiResourceDocument"/>.</returns>
		public async Task<ApiResourceDocument> SaveScopes(ApiResourceDocument document)
		{
			if (document.ID == Guid.Empty)
			{
				document = await this.SaveAsync(document);
			}

			return await this.CurrentCollection.FindOneAndUpdateAsync(
				this.GetUniqueFilterDefinition(document),
				Builders<ApiResourceDocument>.Update.Set(contract => contract.Scopes, document.Scopes),
				new FindOneAndUpdateOptions<ApiResourceDocument>()
				{
					IsUpsert = true,
					ReturnDocument = ReturnDocument.After
				});
		}

		#endregion

		#region MongoDBRepository<ResourceDocument> Implementations

		/// <summary>
		/// Saves the specified entity of <see cref="!:T" /> asynchronously.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// The instance of <see cref="!:T" />.
		/// </returns>
		public override async Task<ApiResourceDocument> SaveAsync(ApiResourceDocument entity)
		{
			entity.ID = entity.ID == Guid.Empty ? Guid.NewGuid() : entity.ID;

			return await this.CurrentCollection.FindOneAndUpdateAsync(
				this.GetUniqueFilterDefinition(entity),
				Builders<ApiResourceDocument>.Update
											.Set(contract => contract.Name, entity.Name),
				new FindOneAndUpdateOptions<ApiResourceDocument>()
				{
					IsUpsert = true,
					ReturnDocument = ReturnDocument.After
				});
		}

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
