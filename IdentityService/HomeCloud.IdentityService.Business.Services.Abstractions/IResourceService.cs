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
		/// Creates new <see cref="Api"/> resource <paramref name="application"/>.
		/// </summary>
		/// <param name="application">The resource application.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<ApiResource>> CreateApplication(ApiResource application);

		/// <summary>
		/// Updates existing <see cref="Api"/> resource <paramref name="application"/>.
		/// </summary>
		/// <param name="application">The resource application.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<ApiResource>> UpdateApplication(ApiResource application);

		/// <summary>
		/// Gets the <see cref="Api"/> resource application by specified resource identifier.
		/// </summary>
		/// <param name="id">The resource application identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<ApiResource>> GetApplication(Guid id);

		/// <summary>
		/// Searches for the list of <see cref="Api"/> resource applications by specified search <paramref name="criteria"/>.
		/// </summary>
		/// <param name="criteria">The <see cref="ApiResource"/> search criteria.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IPaginable<ApiResource>>> FindApplications(ApiResource criteria);

		/// <summary>
		/// Deletes existing <see cref="Api"/> resource application by specified <paramref name="resource"/> identifier.
		/// </summary>
		/// <param name="id">The resource application identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<ApiResource>> DeleteApplication(Guid id);

		/// <summary>
		/// Gets the list of application claims by specified resource application identifier.
		/// </summary>
		/// <param name="id">The resource application identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IPaginable<string>>> GetClaims(Guid id);

		/// <summary>
		/// Gets the list of application secrets by specified resource application identifier.
		/// </summary>
		/// <param name="id">The resource application identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IPaginable<string>>> GetSecrets(Guid id);

		/// <summary>
		/// Gets the list of application scopes by specified resource application identifier.
		/// </summary>
		/// <param name="id">The resource application identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IPaginable<string>>> GetScopes(Guid id);

		/// <summary>
		/// Saves the list of application claims for resource application specified by identifier.
		/// </summary>
		/// <param name="id">The resource application identifier.</param>
		/// <param name="claims">The list of claims.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		Task<ServiceResult<IPaginable<string>>> SaveClaims(Guid id, IEnumerable<string> claims);

		/// <summary>
		/// Saves the list of application secrets for resource application specified by identifier.
		/// </summary>
		/// <param name="id">The resource application identifier.</param>
		/// <param name="secrets">The list of secrets.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		Task<ServiceResult<IPaginable<string>>> SaveSecrets(Guid id, IEnumerable<string> secrets);

		/// <summary>
		/// Saves the list of application scopes for resource application specified by identifier.
		/// </summary>
		/// <param name="id">The resource application identifier.</param>
		/// <param name="scopes">The list of scopes.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		Task<ServiceResult<IPaginable<string>>> SaveScopes(Guid id, IEnumerable<string> scopes);
	}
}
