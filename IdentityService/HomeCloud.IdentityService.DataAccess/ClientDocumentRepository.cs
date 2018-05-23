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

		#region IClientDocumentRepository Implementations

		/// <summary>
		/// Searches for the origins by specified <paramref name="projectionSelector" /> that belong to the clients specified by <paramref name="clientSelector"/>.
		/// </summary>
		/// <param name="clientSelector">The client selector.</param>
		/// <param name="projectionSelector">The origins selector.</param>
		/// <returns> The list of of instances of <see cref="string"/>.</returns>
		public async Task<IEnumerable<string>> FindOrigins(Expression<Func<ClientDocument, bool>> clientSelector, Expression<Func<string, bool>> projectionSelector)
		{
			ProjectionDefinition<ClientDocument, IEnumerable<string>> projection =
				projectionSelector is null ?
				Builders<ClientDocument>.Projection.Expression(client => client.Origins ?? Enumerable.Empty<string>()) :
				Builders<ClientDocument>.Projection.Expression(client => client.Origins == null ? Enumerable.Empty<string>() : client.Origins.Where(projectionSelector.Compile()));

			IAsyncCursor<IEnumerable<string>> cursor = await this.CurrentCollection.FindAsync(clientSelector ?? (_ => true), new FindOptions<ClientDocument, IEnumerable<string>>()
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
		/// Searches for the secrets by specified <paramref name="projectionSelector" /> that belong to the clients specified by <paramref name="clientSelector"/>.
		/// </summary>
		/// <param name="clientSelector">The client selector.</param>
		/// <param name="projectionSelector">The secret selector.</param>
		/// <returns>
		/// The list of of instances of <see cref="SecretDocument"/>.
		/// </returns>
		public async Task<IEnumerable<SecretDocument>> FindSecrets(Expression<Func<ClientDocument, bool>> clientSelector, Expression<Func<SecretDocument, bool>> projectionSelector)
		{
			ProjectionDefinition<ClientDocument, IEnumerable<SecretDocument>> projection = 
				projectionSelector is null ?
				Builders<ClientDocument>.Projection.Expression(client => client.Secrets ?? Enumerable.Empty<SecretDocument>()) :
				Builders<ClientDocument>.Projection.Expression(client => client.Secrets == null ? Enumerable.Empty<SecretDocument>() : client.Secrets.Where(projectionSelector.Compile()));

			IAsyncCursor<IEnumerable<SecretDocument>> cursor = await this.CurrentCollection.FindAsync(clientSelector ?? (_ => true), new FindOptions<ClientDocument, IEnumerable<SecretDocument>>()
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
		/// Searches for the scopes by specified <paramref name="projectionSelector" /> that belong to the clients specified by <paramref name="clientSelector"/>.
		/// </summary>
		/// <param name="clientSelector">The client selector.</param>
		/// <param name="projectionSelector">The scope selector.</param>
		/// <returns> The list of of instances of <see cref="string"/>.</returns>
		public async Task<IEnumerable<string>> FindScopes(Expression<Func<ClientDocument, bool>> clientSelector, Expression<Func<string, bool>> projectionSelector)
		{
			ProjectionDefinition<ClientDocument, IEnumerable<string>> projection =
				projectionSelector is null ?
				Builders<ClientDocument>.Projection.Expression(client => client.Scopes ?? Enumerable.Empty<string>()) :
				Builders<ClientDocument>.Projection.Expression(client => client.Scopes == null ? Enumerable.Empty<string>() : client.Scopes.Where(projectionSelector.Compile()));

			IAsyncCursor<IEnumerable<string>> cursor = await this.CurrentCollection.FindAsync(clientSelector ?? (_ => true), new FindOptions<ClientDocument, IEnumerable<string>>()
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
		/// Searches for the grants by specified <paramref name="projectionSelector" /> that belong to the clients specified by <paramref name="clientSelector"/>.
		/// </summary>
		/// <param name="clientSelector">The client selector.</param>
		/// <param name="projectionSelector">The grant selector.</param>
		/// <returns> The list of of instances of <see cref="GrantDocument"/>.</returns>
		public async Task<IEnumerable<GrantDocument>> FindGrants(Expression<Func<ClientDocument, bool>> clientSelector, Expression<Func<GrantDocument, bool>> projectionSelector)
		{
			ProjectionDefinition<ClientDocument, IEnumerable<GrantDocument>> projection =
				projectionSelector is null ?
				Builders<ClientDocument>.Projection.Expression(client => client.Grants == null ? Enumerable.Empty<GrantDocument>() : this.SetGrantClient(client, client.Grants)) :
				Builders<ClientDocument>.Projection.Expression(client => client.Grants == null ? Enumerable.Empty<GrantDocument>() : this.SetGrantClient(client, client.Grants).Where(projectionSelector.Compile()));

			IAsyncCursor<IEnumerable<GrantDocument>> cursor = await this.CurrentCollection.FindAsync(clientSelector ?? (_ => true), new FindOptions<ClientDocument, IEnumerable<GrantDocument>>()
			{
				Projection = projection
			});

			IEnumerable<GrantDocument> result = (await cursor.ToListAsync()).SelectMany(item => item);

			return new PagedList<GrantDocument>(result)
			{
				Offset = 0,
				Limit = result.Count(),
				TotalCount = result.Count()
			};
		}

		/// <summary>
		/// Saves the origins of the specified <paramref name="document"/> of <see cref="ClientDocument"/> type.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <returns>The updated instance of <see cref="ClientDocument"/>.</returns>
		public async Task<ClientDocument> SaveOrigins(ClientDocument document)
		{
			if (document.ID == Guid.Empty)
			{
				document = await this.SaveAsync(document);
			}

			return await this.CurrentCollection.FindOneAndUpdateAsync(
				this.GetUniqueFilterDefinition(document),
				Builders<ClientDocument>.Update.Set(contract => contract.Origins, document.Origins),
				new FindOneAndUpdateOptions<ClientDocument>()
				{
					IsUpsert = true,
					ReturnDocument = ReturnDocument.After
				});
		}

		/// <summary>
		/// Saves the secrets of the specified <paramref name="document"/> of <see cref="ClientDocument"/> type.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <returns>The updated instance of <see cref="ClientDocument"/>.</returns>
		public async Task<ClientDocument> SaveSecrets(ClientDocument document)
		{
			if (document.ID == Guid.Empty)
			{
				document = await this.SaveAsync(document);
			}

			return await this.CurrentCollection.FindOneAndUpdateAsync(
				this.GetUniqueFilterDefinition(document),
				Builders<ClientDocument>.Update.Set(contract => contract.Secrets, document.Secrets),
				new FindOneAndUpdateOptions<ClientDocument>()
				{
					IsUpsert = true,
					ReturnDocument = ReturnDocument.After
				});
		}

		/// <summary>
		/// Saves the scopes of the specified <paramref name="document"/> of <see cref="ClientDocument"/> type.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <returns>The updated instance of <see cref="ClientDocument"/>.</returns>
		public async Task<ClientDocument> SaveScopes(ClientDocument document)
		{
			if (document.ID == Guid.Empty)
			{
				document = await this.SaveAsync(document);
			}

			return await this.CurrentCollection.FindOneAndUpdateAsync(
				this.GetUniqueFilterDefinition(document),
				Builders<ClientDocument>.Update.Set(contract => contract.Scopes, document.Scopes),
				new FindOneAndUpdateOptions<ClientDocument>()
				{
					IsUpsert = true,
					ReturnDocument = ReturnDocument.After
				});
		}

		/// <summary>
		/// Saves the grants of the specified <paramref name="document"/> of <see cref="ClientDocument"/> type.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <returns>The updated instance of <see cref="ClientDocument"/>.</returns>
		public async Task<ClientDocument> SaveGrants(ClientDocument document)
		{
			if (document.ID == Guid.Empty)
			{
				document = await this.SaveAsync(document);
			}

			document = await this.CurrentCollection.FindOneAndUpdateAsync(
				this.GetUniqueFilterDefinition(document),
				Builders<ClientDocument>.Update.Set(contract => contract.Grants, document.Grants),
				new FindOneAndUpdateOptions<ClientDocument>()
				{
					IsUpsert = true,
					ReturnDocument = ReturnDocument.After
				});

			if (document.Grants != null)
			{
				this.SetGrantClient(document, document.Grants);
			}

			return document;
		}

		/// <summary>
		/// Deletes the <paramref name="client" /> grant by specified grant identifier.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="grantID">The grant identifier.</param>
		/// <returns>
		/// The deleted grant.
		/// </returns>
		public async Task<GrantDocument> DeleteGrant(ClientDocument client, string grantID)
		{
			client.Grants = await this.FindGrants(document => document.ID == client.ID, item => item.ID != grantID);
			client = await this.SaveGrants(client);

			return new GrantDocument()
			{
				ID = grantID
			};
		}

		#endregion

		#region MongoDBRepository<ClientDocument> Implementations

		/// <summary>
		/// Saves the specified entity of <see cref="!:T" /> asynchronously.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// The instance of <see cref="!:T" />.
		/// </returns>
		public override async Task<ClientDocument> SaveAsync(ClientDocument entity)
		{
			entity.ID = entity.ID == Guid.Empty ? Guid.NewGuid() : entity.ID;

			return await this.CurrentCollection.FindOneAndUpdateAsync(
				this.GetUniqueFilterDefinition(entity),
				Builders<ClientDocument>.Update
											.Set(contract => contract.Name, entity.Name)
											.Set(contract => contract.GrantType, entity.GrantType)
											.Set(contract => contract.RedirectUrl, entity.RedirectUrl)
											.Set(contract => contract.PostLogoutRedirectUrl, entity.PostLogoutRedirectUrl)
											.Set(contract => contract.IdentityTokenLifetime, entity.IdentityTokenLifetime)
											.Set(contract => contract.AccessTokenLifetime, entity.AccessTokenLifetime)
											.Set(contract => contract.AbsoluteRefreshTokenLifetime, entity.AbsoluteRefreshTokenLifetime)
											.Set(contract => contract.SlidingRefreshTokenLifetime, entity.SlidingRefreshTokenLifetime),
				new FindOneAndUpdateOptions<ClientDocument>()
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

		#region Private Methods

		/// <summary>
		/// Sets the client to the related list of grants.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="grants">The grants.</param>
		/// <returns>The list of instances of <see cref="GrantDocument"/>.</returns>
		private IEnumerable<GrantDocument> SetGrantClient(ClientDocument client, IEnumerable<GrantDocument> grants)
		{
			return grants.Select(grant =>
			{
				grant.ClientID = client.ID;

				return grant;
			}).ToList();
		}

		#endregion
	}
}
