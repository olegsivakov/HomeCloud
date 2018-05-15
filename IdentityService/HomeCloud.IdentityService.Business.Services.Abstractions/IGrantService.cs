namespace HomeCloud.IdentityService.Business.Services
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.IdentityService.Business.Entities;

	#endregion

	/// <summary>
	/// Defines methods to manage client application grants.
	/// </summary>
	public interface IGrantService
	{
		/// <summary>
		/// Gets the list of available grant types.
		/// </summary>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IDictionary<int, string>>> GetGrantTypes();

		/// <summary>
		/// Gets the list of client application grants by specified search <paramref name="criteria"/>.
		/// </summary>
		/// <param name="criteria">The <see cref="Grant"/> search criteria.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IPaginable<Grant>>> FindGrants(Grant criteria);

		/// <summary>
		/// Gets the grant by specified grant identifier.
		/// </summary>
		/// <param name="grant">The grant identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<Grant>> GetGrant(string id);

		/// <summary>
		/// Saves the client application <paramref name="grant"/>.
		/// </summary>
		/// <param name="grant">The grant to save.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<Grant>> SaveGrant(Grant grant);

		/// <summary>
		/// Deletes the <paramref name="grants"/> of client application specified by client application identifier.
		/// </summary>
		/// <param name="grant">The grant collection to delete.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IPaginable<Grant>>> DeleteGrants(Guid clientID, IEnumerable<Grant> grants);
	}
}
