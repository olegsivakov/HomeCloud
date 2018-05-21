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
		/// Searches for the <see cref="ClientDocument"/> origins by specified <paramref name="selector"/>.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <returns>The list of origin strings.</returns>
		public async Task<IEnumerable<string>> FindOrigins(Func<ClientDocument, string, bool> selector)
		{
			ProjectionDefinition<ClientDocument> projection = Builders<ClientDocument>.Projection.ElemMatch(contract => contract.Origins, projectionDocument => projectionDocument != null && selector(null, projectionDocument));
			FilterDefinition<ClientDocument> filter = Builders<ClientDocument>.Filter.Where(document => selector(document, null));

			IAsyncCursor<string> cursor = await this.CurrentCollection.FindAsync(filter, new FindOptions<ClientDocument, string>()
			{
				Projection = projection
			});

			IList<string> result = await cursor.ToListAsync();

			return new PagedList<string>(result)
			{
				Offset = 0,
				Limit = result.Count,
				TotalCount = result.Count
			};
		}

		/// <summary>
		/// Searches for the <see cref="ClientDocument"/> secrets by specified <paramref name="selector"/>.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <returns>The list of secrets.</returns>
		public async Task<IEnumerable<SecretDocument>> FindSecrets(Func<ClientDocument, SecretDocument, bool> selector)
		{
			ProjectionDefinition<ClientDocument> projection = Builders<ClientDocument>.Projection.ElemMatch(contract => contract.Secrets, projectionDocument => projectionDocument != null && selector(null, projectionDocument));
			FilterDefinition<ClientDocument> filter = Builders<ClientDocument>.Filter.Where(document => selector(document, null));

			IAsyncCursor<SecretDocument> cursor = await this.CurrentCollection.FindAsync(filter, new FindOptions<ClientDocument, SecretDocument>()
			{
				Projection = projection
			});

			IList<SecretDocument> result = await cursor.ToListAsync();

			return new PagedList<SecretDocument>(result)
			{
				Offset = 0,
				Limit = result.Count,
				TotalCount = result.Count
			};
		}

		/// <summary>
		/// Searches for the <see cref="ClientDocument"/> scopes by specified <paramref name="selector"/>.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <returns>The list of scope strings.</returns>
		public async Task<IEnumerable<string>> FindScopes(Func<ClientDocument, string, bool> selector)
		{
			ProjectionDefinition<ClientDocument> projection = Builders<ClientDocument>.Projection.ElemMatch(contract => contract.Scopes, projectionDocument => projectionDocument != null && selector(null, projectionDocument));
			FilterDefinition<ClientDocument> filter = Builders<ClientDocument>.Filter.Where(document => selector(document, null));

			IAsyncCursor<string> cursor = await this.CurrentCollection.FindAsync(filter, new FindOptions<ClientDocument, string>()
			{
				Projection = projection
			});

			IList<string> result = await cursor.ToListAsync();

			return new PagedList<string>(result)
			{
				Offset = 0,
				Limit = result.Count,
				TotalCount = result.Count
			};
		}

		/// <summary>
		/// Searches for the <see cref="ClientDocument"/> grants by specified <paramref name="selector"/>.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <returns>The list of instances of <see cref="GrantDocument"/>.</returns>
		public async Task<IEnumerable<GrantDocument>> FindGrants(Func<ClientDocument, GrantDocument, bool> selector)
		{
			ProjectionDefinition<ClientDocument> projection = Builders<ClientDocument>.Projection.ElemMatch(contract => this.SetGrantClient(contract, contract.Grants), projectionDocument => projectionDocument != null && selector(null, projectionDocument));
			FilterDefinition<ClientDocument> filter = Builders<ClientDocument>.Filter.Where(document => selector(document, null));

			IAsyncCursor<GrantDocument> cursor = await this.CurrentCollection.FindAsync(filter, new FindOptions<ClientDocument, GrantDocument>()
			{
				Projection = projection
			});

			IList<GrantDocument> result = await cursor.ToListAsync();

			return new PagedList<GrantDocument>(result)
			{
				Offset = 0,
				Limit = result.Count,
				TotalCount = result.Count
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

			return await this.CurrentCollection.FindOneAndUpdateAsync(
				this.GetUniqueFilterDefinition(document),
				Builders<ClientDocument>.Update.Set(contract => contract.Grants, document.Grants),
				new FindOneAndUpdateOptions<ClientDocument>()
				{
					IsUpsert = true,
					ReturnDocument = ReturnDocument.After
				});
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
			ProjectionDefinition<ClientDocument> projection = Builders<ClientDocument>.Projection.ElemMatch(contract => this.SetGrantClient(contract, contract.Grants), projectionDocument => projectionDocument != null && projectionDocument.ID == grantID);
			FilterDefinition<ClientDocument> filter = Builders<ClientDocument>.Filter.Where(document => document.ID == client.ID);

			return await this.CurrentCollection.FindOneAndDeleteAsync(filter, new FindOneAndDeleteOptions<ClientDocument, GrantDocument>());
		}

		/// <summary>
		/// Deletes the client application grants specified by <paramref name="selector"/>.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <returns>The list of grants left undeleted after operation execution.</returns>
		public async Task<IEnumerable<GrantDocument>> DeleteGrants(Func<ClientDocument, GrantDocument, bool> selector)
		{
			ProjectionDefinition<ClientDocument> projection = Builders<ClientDocument>.Projection.ElemMatch(contract => this.SetGrantClient(contract, contract.Grants), projectionDocument => projectionDocument != null && !selector(null, projectionDocument));
			FilterDefinition<ClientDocument> filter = Builders<ClientDocument>.Filter.Where(document => selector(document, null));

			IAsyncCursor<GrantDocument> cursor = await this.CurrentCollection.FindAsync(filter, new FindOptions<ClientDocument, GrantDocument>()
			{
				Projection = projection
			});

			IEnumerable<ClientDocument> clients = cursor.ToEnumerable().GroupBy(document => document.ClientID).Select(group => new ClientDocument()
			{
				ID = group.Key,
				Grants = group.AsEnumerable()
			});

			foreach (ClientDocument client in clients)
			{
				await this.SaveGrants(client);
			}

			return clients.SelectMany(client => client.Grants);
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
