namespace HomeCloud.IdentityService.DataAccess
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
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
		/// Searches for the claims by specified <paramref name="projectionSelector" /> that belong to the api resource specified by <paramref name="resourceSelector"/>.
		/// </summary>
		/// <param name="resourceSelector">The api resource selector.</param>
		/// <param name="projectionSelector">The claim selector.</param>
		/// <returns> The list of of instances of <see cref="string"/>.</returns>
		public async Task<IEnumerable<string>> FindClaims(Expression<Func<ApiResourceDocument, bool>> resourceSelector, Expression<Func<string, bool>> projectionSelector)
		{
			ProjectionDefinition<ApiResourceDocument, IEnumerable<string>> projection =
				projectionSelector is null ?
				Builders<ApiResourceDocument>.Projection.Expression(resource => resource.Claims ?? Enumerable.Empty<string>()) :
				Builders<ApiResourceDocument>.Projection.Expression(resource => resource.Claims == null ? Enumerable.Empty<string>() : resource.Claims.Where(projectionSelector.Compile()));

			IAsyncCursor<IEnumerable<string>> cursor = await this.CurrentCollection.FindAsync(resourceSelector ?? (_ => true), new FindOptions<ApiResourceDocument, IEnumerable<string>>()
			{
				Projection = projection
			});

			IEnumerable<string> result = (await cursor.ToListAsync()).SelectMany(item => item);

			return new PagedList<string>(result)
			{
				Offset = 0,
				Limit = result.Count(),
				TotalCount = result.Count()
			};
		}

		/// <summary>
		/// Searches for the secrets by specified <paramref name="projectionSelector" /> that belong to the api resource specified by <paramref name="clientSelector"/>.
		/// </summary>
		/// <param name="resourceSelector">The api resource selector.</param>
		/// <param name="projectionSelector">The secret selector.</param>
		/// <returns>
		/// The list of of instances of <see cref="SecretDocument"/>.
		/// </returns>
		public async Task<IEnumerable<SecretDocument>> FindSecrets(Expression<Func<ApiResourceDocument, bool>> resourceSelector, Expression<Func<SecretDocument, bool>> projectionSelector)
		{
			ProjectionDefinition<ApiResourceDocument, IEnumerable<SecretDocument>> projection =
				projectionSelector is null ?
				Builders<ApiResourceDocument>.Projection.Expression(resource => resource.Secrets ?? Enumerable.Empty<SecretDocument>()) :
				Builders<ApiResourceDocument>.Projection.Expression(resource => resource.Secrets == null ? Enumerable.Empty<SecretDocument>() : resource.Secrets.Where(projectionSelector.Compile()));

			IAsyncCursor<IEnumerable<SecretDocument>> cursor = await this.CurrentCollection.FindAsync(resourceSelector ?? (_ => true), new FindOptions<ApiResourceDocument, IEnumerable<SecretDocument>>()
			{
				Projection = projection
			});

			IEnumerable<SecretDocument> result = (await cursor.ToListAsync()).SelectMany(item => item);

			return new PagedList<SecretDocument>(result)
			{
				Offset = 0,
				Limit = result.Count(),
				TotalCount = result.Count()
			};
		}

		/// <summary>
		/// Searches for the scopes by specified <paramref name="projectionSelector" /> that belong to the api resource specified by <paramref name="resourceSelector"/>.
		/// </summary>
		/// <param name="resourceSelector">The api resource selector.</param>
		/// <param name="projectionSelector">The scope selector.</param>
		/// <returns> The list of of instances of <see cref="string"/>.</returns>
		public async Task<IEnumerable<string>> FindScopes(Expression<Func<ApiResourceDocument, bool>> resourceSelector, Expression<Func<string, bool>> projectionSelector)
		{
			ProjectionDefinition<ApiResourceDocument, IEnumerable<string>> projection =
				projectionSelector is null ?
				Builders<ApiResourceDocument>.Projection.Expression(resource => resource.Scopes ?? Enumerable.Empty<string>()) :
				Builders<ApiResourceDocument>.Projection.Expression(resource => resource.Scopes == null ? Enumerable.Empty<string>() : resource.Scopes.Where(projectionSelector.Compile()));

			IAsyncCursor<IEnumerable<string>> cursor = await this.CurrentCollection.FindAsync(resourceSelector ?? (_ => true), new FindOptions<ApiResourceDocument, IEnumerable<string>>()
			{
				Projection = projection
			});

			IEnumerable<string> result = (await cursor.ToListAsync()).SelectMany(item => item);

			return new PagedList<string>(result)
			{
				Offset = 0,
				Limit = result.Count(),
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
