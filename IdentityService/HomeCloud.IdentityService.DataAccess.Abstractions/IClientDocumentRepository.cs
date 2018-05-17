namespace HomeCloud.IdentityService.DataAccess
{
	#region Usings

	using System;
	using System.Collections.Generic;
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
		/// Searches for the <see cref="ClientDocument"/> origins by specified <paramref name="selector"/>.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <returns>The list of origin strings.</returns>
		Task<IEnumerable<string>> FindOrigins(Func<ClientDocument, string, bool> selector);

		/// <summary>
		/// Searches for the <see cref="ClientDocument"/> secrets by specified <paramref name="selector"/>.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <returns>The list of secrets.</returns>
		Task<IEnumerable<SecretDocument>> FindSecrets(Func<ClientDocument, SecretDocument, bool> selector);

		/// <summary>
		/// Searches for the <see cref="ClientDocument"/> scopes by specified <paramref name="selector"/>.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <returns>The list of scope strings.</returns>
		Task<IEnumerable<string>> FindScopes(Func<ClientDocument, string, bool> selector);

		/// <summary>
		/// Searches for the <see cref="ClientDocument"/> grants by specified <paramref name="selector"/>.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <returns>The list of instances of <see cref="GrantDocument"/>.</returns>
		Task<IEnumerable<GrantDocument>> FindGrants(Func<ClientDocument, GrantDocument, bool> selector);

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
	}
}
