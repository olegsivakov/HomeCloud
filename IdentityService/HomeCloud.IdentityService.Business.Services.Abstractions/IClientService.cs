namespace HomeCloud.IdentityService.Business.Services
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.Core;

	using HomeCloud.IdentityService.Business.Entities;
	using HomeCloud.IdentityService.Business.Entities.Applications;

	#endregion

	/// <summary>
	/// Defines methods to manage client applications.
	/// </summary>
	public interface IClientService
	{
		/// <summary>
		/// Creates new client <paramref name="application"/>.
		/// </summary>
		/// <param name="application">The client application.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<Client>> CreateApplicationAsync(Client application);

		/// <summary>
		/// Updates existing client <paramref name="application"/>.
		/// </summary>
		/// <param name="application">The client application.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<Client>> UpdateApplicationAsync(Client application);

		/// <summary>
		/// Gets client application by specified client application identifier.
		/// </summary>
		/// <param name="id">The client identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<Client>> GetApplicationAsync(Guid id);

		/// <summary>
		/// Searches for the list of client applications by specified search <paramref name="criteria" />.
		/// </summary>
		/// <param name="criteria">The search criteria.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		Task<ServiceResult<IPaginable<Client>>> FindApplicationsAsync(Client criteria);

		/// <summary>
		/// Gets the list of application grants by specified client application identifier.
		/// </summary>
		/// <param name="clientID">The client application identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IEnumerable<Grant>>> GetGrantsAsync(Guid clientID);

		/// <summary>
		/// Gets the list of application origins by specified client application identifier.
		/// </summary>
		/// <param name="clientID">The client application identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IEnumerable<string>>> GetOriginsAsync(Guid clientID);

		/// <summary>
		/// Gets the list of application secrets by specified client application identifier.
		/// </summary>
		/// <param name="clientID">The client application identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IEnumerable<Secret>>> GetSecretsAsync(Guid clientID);

		/// <summary>
		/// Gets the list of application scopes by specified client application identifier.
		/// </summary>
		/// <param name="clientID">The client application identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IEnumerable<string>>> GetScopesAsync(Guid clientID);

		/// <summary>
		/// Saves the list of application origins for the client application specified by identifier.
		/// </summary>
		/// <param name="clientID">The client application identifier.</param>
		/// <param name="origins">The list of origins.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		Task<ServiceResult<IEnumerable<string>>> SaveOriginsAsync(Guid clientID, IEnumerable<string> origins);

		/// <summary>
		/// Saves the list of application grants for the client application specified by identifier.
		/// </summary>
		/// <param name="clientID">The client application identifier.</param>
		/// <param name="grants">The list of grants.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		Task<ServiceResult<IEnumerable<string>>> SaveGrantsAsync(Guid clientID, IEnumerable<Grant> grants);

		/// <summary>
		/// Saves the list of application secrets for the client application specified by identifier.
		/// </summary>
		/// <param name="clientID">The client application identifier.</param>
		/// <param name="secrets">The list of secrets.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		Task<ServiceResult<IEnumerable<string>>> SaveSecretsAsync(Guid clientID, IEnumerable<string> secrets);

		/// <summary>
		/// Saves the list of application scopes for the client application specified by identifier.
		/// </summary>
		/// <param name="clientID">The client application identifier.</param>
		/// <param name="scopes">The list of scopes.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		Task<ServiceResult<IEnumerable<string>>> SaveScopesAsync(Guid clientID, IEnumerable<string> scopes);
	}
}
