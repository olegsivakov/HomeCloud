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
	/// Defines methods to handle data of <see cref="ApiResourceDocument"/> in <see cref="MongoDB"/> database.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.MongoDB.IMongoDBRepository{HomeCloud.IdentityService.DataAccess.Objects.ApiResourceDocument}" />
	public interface IApiResourceDocumentRepository : IMongoDBRepository<ApiResourceDocument>
	{
		/// <summary>
		/// Searches for the claims by specified <paramref name="projectionSelector" /> that belong to the api resource specified by <paramref name="resourceSelector"/>.
		/// </summary>
		/// <param name="resourceSelector">The api resource selector.</param>
		/// <param name="projectionSelector">The claim selector.</param>
		/// <returns> The list of of instances of <see cref="string"/>.</returns>
		Task<IEnumerable<string>> FindClaims(Expression<Func<ApiResourceDocument, bool>> resourceSelector, Expression<Func<string, bool>> projectionSelector);

		/// <summary>
		/// Searches for the secrets by specified <paramref name="projectionSelector" /> that belong to the api resource specified by <paramref name="clientSelector"/>.
		/// </summary>
		/// <param name="resourceSelector">The api resource selector.</param>
		/// <param name="projectionSelector">The secret selector.</param>
		/// <returns>
		/// The list of of instances of <see cref="SecretDocument"/>.
		/// </returns>
		Task<IEnumerable<SecretDocument>> FindSecrets(Expression<Func<ApiResourceDocument, bool>> resourceSelector, Expression<Func<SecretDocument, bool>> projectionSelector);

		/// <summary>
		/// Searches for the scopes by specified <paramref name="projectionSelector" /> that belong to the api resource specified by <paramref name="resourceSelector"/>.
		/// </summary>
		/// <param name="resourceSelector">The api resource selector.</param>
		/// <param name="projectionSelector">The scope selector.</param>
		/// <returns> The list of of instances of <see cref="string"/>.</returns>
		Task<IEnumerable<string>> FindScopes(Expression<Func<ApiResourceDocument, bool>> resourceSelector, Expression<Func<string, bool>> projectionSelector);

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
