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
	/// Defines methods to handle data of <see cref="ApiResourceDocument"/> in <see cref="MongoDB"/> database.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.MongoDB.IMongoDBRepository{HomeCloud.IdentityService.DataAccess.Objects.ApiResourceDocument}" />
	public interface IApiResourceDocumentRepository : IMongoDBRepository<ApiResourceDocument>
	{
		/// <summary>
		/// Searches for the <see cref="ApiResourceDocument"/> claims by specified <paramref name="selector"/>.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <returns>The list of claim strings.</returns>
		Task<IEnumerable<string>> FindClaims(Func<ApiResourceDocument, string, bool> selector);

		/// <summary>
		/// Searches for the <see cref="ApiResourceDocument"/> secrets by specified <paramref name="selector"/>.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <returns>The list of secrets.</returns>
		Task<IEnumerable<SecretDocument>> FindSecrets(Func<ApiResourceDocument, SecretDocument, bool> selector);

		/// <summary>
		/// Searches for the <see cref="ApiResourceDocument"/> scopes by specified <paramref name="selector"/>.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <returns>The list of scope strings.</returns>
		Task<IEnumerable<string>> FindScopes(Func<ApiResourceDocument, string, bool> selector);

		/// <summary>
		/// Saves the claims of the specified <paramref name="document"/> of <see cref="ApiResourceDocument"/> type.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <returns>The updated instance of <see cref="ApiResourceDocument"/>.</returns>
		Task<ApiResourceDocument> SaveClaims(ApiResourceDocument document);

		/// <summary>
		/// Saves the secrets of the specified <paramref name="document"/> of <see cref="ApiResourceDocument"/> type.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <returns>The updated instance of <see cref="ApiResourceDocument"/>.</returns>
		Task<ApiResourceDocument> SaveSecrets(ApiResourceDocument document);

		/// <summary>
		/// Saves the scopes of the specified <paramref name="document"/> of <see cref="ApiResourceDocument"/> type.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <returns>The updated instance of <see cref="ApiResourceDocument"/>.</returns>
		Task<ApiResourceDocument> SaveScopes(ApiResourceDocument document);
	}
}
