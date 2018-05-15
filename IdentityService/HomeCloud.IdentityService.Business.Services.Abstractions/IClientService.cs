namespace HomeCloud.IdentityService.Business.Services
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.IdentityService.Business.Entities.Applications;
	using HomeCloud.IdentityService.Business.Entities;

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
		Task<ServiceResult<Client>> CreateApplication(Client application);

		/// <summary>
		/// Updates existing client <paramref name="application"/>.
		/// </summary>
		/// <param name="application">The client application.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<Client>> UpdateApplication(Client application);

		/// <summary>
		/// Gets client application by specified client application identifier.
		/// </summary>
		/// <param name="id">The client identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<ApiResource>> GetApplication(Guid id);

		/// <summary>
		/// Searches for the list of client applications by specified search <paramref name="criteria" />.
		/// </summary>
		/// <param name="criteria">The search criteria.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		Task<ServiceResult<IPaginable<ApiResource>>> FindClients(Client criteria);

		/// <summary>
		/// Gets the list of application origins by specified client application identifier.
		/// </summary>
		/// <param name="id">The client application identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IPaginable<string>>> GetOrigins(Guid id);

		/// <summary>
		/// Gets the list of application secrets by specified client application identifier.
		/// </summary>
		/// <param name="id">The client application identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IPaginable<string>>> GetSecrets(Guid id);

		/// <summary>
		/// Gets the list of application scopes by specified client application identifier.
		/// </summary>
		/// <param name="id">The client application identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IPaginable<string>>> GetScopes(Guid id);

		/// <summary>
		/// Saves the list of application origins for the client application specified by identifier.
		/// </summary>
		/// <param name="id">The client application identifier.</param>
		/// <param name="claims">The list of origins.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		Task<ServiceResult<IPaginable<string>>> SaveOrigins(Guid id, IEnumerable<string> origins);

		/// <summary>
		/// Saves the list of application secrets for the client application specified by identifier.
		/// </summary>
		/// <param name="id">The client application identifier.</param>
		/// <param name="secrets">The list of secrets.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		Task<ServiceResult<IPaginable<string>>> SaveSecrets(Guid id, IEnumerable<string> secrets);

		/// <summary>
		/// Saves the list of application scopes for the client application specified by identifier.
		/// </summary>
		/// <param name="id">The client application identifier.</param>
		/// <param name="scopes">The list of scopes.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		Task<ServiceResult<IPaginable<string>>> SaveScopes(Guid id, IEnumerable<string> scopes);
	}
}
