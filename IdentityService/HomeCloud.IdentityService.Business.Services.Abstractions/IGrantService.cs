namespace HomeCloud.IdentityService.Business.Services
{
	#region Usings

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
		Task<ServiceResult<IDictionary<int, string>>> GetGrantTypesAsync();

		/// <summary>
		/// Gets the list of client application grants by specified search <paramref name="criteria" />.
		/// </summary>
		/// <param name="criteria">The <see cref="GrantSearchCriteria" /> search criteria.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		Task<ServiceResult<IEnumerable<Grant>>> FindGrantsAsync(GrantSearchCriteria criteria);

		/// <summary>
		/// Gets the grant by specified grant identifier.
		/// </summary>
		/// <param name="grant">The grant identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<Grant>> GetGrantAsync(string id);

		/// <summary>
		/// Saves the client application <paramref name="grant"/>.
		/// </summary>
		/// <param name="grant">The grant to save.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<Grant>> SaveGrantAsync(Grant grant);

		/// <summary>
		/// Searches for the list of <paramref name="grants"/> and deletes them by specified <paramref name="criteria"/>. If <paramref name="criteria"/> is not set or empty no grants will be deleted.
		/// </summary>
		/// <param name="criteria">The <see cref="GrantSearchCriteria" /> search criteria.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IEnumerable<Grant>>> DeleteGrantsAsync(GrantSearchCriteria criteria);

		/// <summary>
		/// Deletes the grant by specified grant identifier.
		/// </summary>
		/// <param name="id">The grant identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<Grant>> DeleteGrantAsync(string id);
	}
}
