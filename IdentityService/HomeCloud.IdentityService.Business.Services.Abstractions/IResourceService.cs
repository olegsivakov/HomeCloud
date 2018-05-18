namespace HomeCloud.IdentityService.Business.Services
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.IdentityService.Business.Entities.Applications;

	#endregion

	/// <summary>
	/// Defines methods to manage <see cref="Api"/> resource applications.
	/// </summary>
	public interface IResourceService
	{
		/// <summary>
		/// Creates new api resource <paramref name="application"/>.
		/// </summary>
		/// <param name="application">The api resource application.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<ApiResource>> CreateApplicationAsync(ApiResource application);

		/// <summary>
		/// Updates existing api resource <paramref name="application"/>.
		/// </summary>
		/// <param name="application">The api resource application.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<ApiResource>> UpdateApplicationAsync(ApiResource application);

		/// <summary>
		/// Gets api resource application by specified api resource application identifier.
		/// </summary>
		/// <param name="id">The api resource identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<ApiResource>> GetApplicationAsync(Guid id);

		/// <summary>
		/// Searches for the list of api resource applications by specified search <paramref name="criteria" />.
		/// </summary>
		/// <param name="criteria">The search criteria.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		Task<ServiceResult<IPaginable<ApiResource>>> FindApplicationsAsync(ApiResource criteria, int offset = 0, int limit = 20);

		/// <summary>
		/// Gets the list of application claims by specified api resource application identifier.
		/// </summary>
		/// <param name="applicationID">The api resource application identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IEnumerable<string>>> GetClaimsAsync(Guid applicationID);

		/// <summary>
		/// Gets the list of application secrets by specified api resource application identifier.
		/// </summary>
		/// <param name="applicationID">The api resource application identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IEnumerable<Secret>>> GetSecretsAsync(Guid applicationID);

		/// <summary>
		/// Gets the list of application scopes by specified api resource application identifier.
		/// </summary>
		/// <param name="applicationID">The api resource application identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IEnumerable<string>>> GetScopesAsync(Guid applicationID);

		/// <summary>
		/// Saves the list of application clames for the api resource application specified by identifier.
		/// </summary>
		/// <param name="applicationID">The api resource application identifier.</param>
		/// <param name="claims">The list of claims.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		Task<ServiceResult<IEnumerable<string>>> SaveClaimsAsync(Guid applicationID, IEnumerable<string> claims);

		/// <summary>
		/// Saves the list of application secrets for the api resource application specified by identifier.
		/// </summary>
		/// <param name="applicationID">The api resource application identifier.</param>
		/// <param name="secrets">The list of secrets.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		Task<ServiceResult<IEnumerable<Secret>>> SaveSecretsAsync(Guid applicationID, IEnumerable<Secret> secrets);

		/// <summary>
		/// Saves the list of application scopes for the api resource application specified by identifier.
		/// </summary>
		/// <param name="applicationID">The api resource application identifier.</param>
		/// <param name="scopes">The list of scopes.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		Task<ServiceResult<IEnumerable<string>>> SaveScopesAsync(Guid applicationID, IEnumerable<string> scopes);
	}
}
