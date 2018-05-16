namespace HomeCloud.IdentityService.Business.Services
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.Core;

	using HomeCloud.IdentityService.Business.Entities.Membership;
	using HomeCloud.IdentityService.Business.Entities;

	#endregion

	/// <summary>
	/// Defines methods to manage users, roles and grants.
	/// </summary>
	public interface IMembershipService
	{
		/// <summary>
		/// Validates the user specified by <paramref name="username"/> and <paramref name="password"/>.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult> ValidateUserAsync(string username, string password);

		/// <summary>
		/// Creates the <paramref name="user"/>.
		/// </summary>
		/// <param name="user">The user to create.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<User>> CreateUserAsync(User user);

		/// <summary>
		/// Updates the <paramref name="user"/>.
		/// </summary>
		/// <param name="user">The user to update.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<User>> UpdateUserAsync(User user);

		/// <summary>
		/// Deletes the <paramref name="user"/>.
		/// </summary>
		/// <param name="user">The user to delete.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<User>> DeleteUserAsync(User user);

		/// <summary>
		/// Gets the user by specified user identifier.
		/// </summary>
		/// <param name="id">The user identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<User>> GetUserAsync(Guid id);

		/// <summary>
		/// Gets the user by specified <paramref name="username"/>.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<User>> GetUserAsync(string username);

		/// <summary>
		/// Searches for the list of users by specified search <paramref name="criteria"/>.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IPaginable<User>>> FindUsersAsync(User criteria, int offset = 0, int limit = 20);

		/// <summary>
		/// Gets the list of available user roles.
		/// </summary>
		/// <returns>The result of execution of service operation.</returns>
		Task<ServiceResult<IDictionary<int, string>>> GetRolesAsync();
	}
}
