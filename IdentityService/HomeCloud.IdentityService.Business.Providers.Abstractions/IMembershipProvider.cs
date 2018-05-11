namespace HomeCloud.IdentityService.Business.Providers
{
	#region Usings

	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.IdentityService.Business.Entities;

	#endregion

	/// <summary>
	/// Defines methods to handle membership.
	/// </summary>
	public interface IMembershipProvider
	{
		/// <summary>
		/// Gets a value indicating whether the specified user already exists.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns><c>true</c> if the user exists. Otherwise <c>false.</c></returns>
		Task<bool> UserExists(User user);

		/// <summary>
		/// Gets the list of all users.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>The list of instances of <see cref="User"/>.</returns>
		Task<IPaginable<User>> GetAllUsers(int offset = 0, int limit = 20);

		/// <summary>
		/// Gets the list of users by specified user <paramref name="role"/>.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>The list of instances of <see cref="User"/>.</returns>
		Task<IPaginable<User>> GetUsersInRole(Role role, int offset = 0, int limit = 20);

		/// <summary>
		/// Gets the list of roles.
		/// </summary>
		/// <returns>The list of <see cref="string"/>.</returns>
		Task<IEnumerable<string>> GetRoles();

		/// <summary>
		/// Validates the user by specified <paramref name="username"/> and <paramref name="password"/>.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns><c>True</c> if user valid. Otherwise <c>false</c></returns>
		Task<bool> ValidateUser(string username, string password);

		/// <summary>
		/// Creates the <paramref name="user"/>.
		/// </summary>
		/// <param name="user">The user to create.</param>
		/// <returns>The instance of newly created user of <see cref="User"/>.</returns>
		Task<User> CreateUser(User user);

		/// <summary>
		/// Updates the specified <paramref name="user"/>.
		/// </summary>
		/// <param name="user">The user to update.</param>
		/// <returns>The instance of updated user of <see cref="User"/>.</returns>
		Task<User> UpdateUser(User user);

		/// <summary>
		/// Deletes the specified <paramref name="user"/>.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns>The instace of <see cref="User"/> corresponded to deleted user.</returns>
		Task<User> DeleteUser(User user);
	}
}
