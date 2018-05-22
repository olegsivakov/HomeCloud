namespace HomeCloud.IdentityService.DataAccess
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Threading.Tasks;

	using HomeCloud.Data.MongoDB;
	using HomeCloud.IdentityService.DataAccess.Objects;

	#endregion

	/// <summary>
	/// Defines methods to handle data of <see cref="ClientDocument"/> in <see cref="MongoDB"/> database.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.MongoDB.IMongoDBRepository{HomeCloud.IdentityService.DataAccess.Objects.ClientDocument}" />
	public interface IClientDocumentRepository : IMongoDBRepository<ClientDocument>
	{
		/// <summary>
		/// Searches for the origins by specified <paramref name="projectionSelector" /> that belong to the clients specified by <paramref name="clientSelector"/>.
		/// </summary>
		/// <param name="clientSelector">The client selector.</param>
		/// <param name="projectionSelector">The origins selector.</param>
		/// <returns> The list of of instances of <see cref="string"/>.</returns>
		Task<IEnumerable<string>> FindOrigins(Expression<Func<ClientDocument, bool>> clientSelector, Expression<Func<string, bool>> projectionSelector);

		/// <summary>
		/// Searches for the secrets by specified <paramref name="projectionSelector" /> that belong to the clients specified by <paramref name="clientSelector"/>.
		/// </summary>
		/// <param name="clientSelector">The client selector.</param>
		/// <param name="projectionSelector">The secret selector.</param>
		/// <returns> The list of of instances of <see cref="SecretDocument"/>.</returns>
		Task<IEnumerable<SecretDocument>> FindSecrets(Expression<Func<ClientDocument, bool>> clientSelector, Expression<Func<SecretDocument, bool>> projectionSelector);

		/// <summary>
		/// Searches for the scopes by specified <paramref name="projectionSelector" /> that belong to the clients specified by <paramref name="clientSelector"/>.
		/// </summary>
		/// <param name="clientSelector">The client selector.</param>
		/// <param name="projectionSelector">The scope selector.</param>
		/// <returns> The list of of instances of <see cref="string"/>.</returns>
		Task<IEnumerable<string>> FindScopes(Expression<Func<ClientDocument, bool>> clientSelector, Expression<Func<string, bool>> projectionSelector);

		/// <summary>
		/// Searches for the grants by specified <paramref name="projectionSelector" /> that belong to the clients specified by <paramref name="clientSelector"/>.
		/// </summary>
		/// <param name="clientSelector">The client selector.</param>
		/// <param name="projectionSelector">The grant selector.</param>
		/// <returns> The list of of instances of <see cref="GrantDocument"/>.</returns>
		Task<IEnumerable<GrantDocument>> FindGrants(Expression<Func<ClientDocument, bool>> clientSelector, Expression<Func<GrantDocument, bool>> projectionSelector);

		/// <summary>
		/// Saves the origins of the specified <paramref name="document"/> of <see cref="ClientDocument"/> type.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <returns>The updated instance of <see cref="ClientDocument"/>.</returns>
		Task<ClientDocument> SaveOrigins(ClientDocument document);

		/// <summary>
		/// Saves the secrets of the specified <paramref name="document"/> of <see cref="ClientDocument"/> type.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <returns>The updated instance of <see cref="ClientDocument"/>.</returns>
		Task<ClientDocument> SaveSecrets(ClientDocument document);

		/// <summary>
		/// Saves the scopes of the specified <paramref name="document"/> of <see cref="ClientDocument"/> type.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <returns>The updated instance of <see cref="ClientDocument"/>.</returns>
		Task<ClientDocument> SaveScopes(ClientDocument document);

		/// <summary>
		/// Saves the grants of the specified <paramref name="document"/> of <see cref="ClientDocument"/> type.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <returns>The updated instance of <see cref="ClientDocument"/>.</returns>
		Task<ClientDocument> SaveGrants(ClientDocument document);

		/// <summary>
		/// Deletes the <paramref name="client" /> grant by specified grant identifier.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="grantID">The grant identifier.</param>
		/// <returns>
		/// The deleted grant.
		/// </returns>
		Task<GrantDocument> DeleteGrant(ClientDocument client, string grantID);
	}
}
