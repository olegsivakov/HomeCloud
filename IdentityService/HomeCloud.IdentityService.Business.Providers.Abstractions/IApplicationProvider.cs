namespace HomeCloud.IdentityService.Business.Providers
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.IdentityService.Business.Entities;

	#endregion

	/// <summary>
	/// Defines methods to handle applications.
	/// </summary>
	public interface IApplicationProvider
	{
		/// <summary>
		/// Indicates whether the specified application already exists.
		/// </summary>
		/// <param name="application">The application.</param>
		/// <returns><c>True</c> if application exists. Otherwise it returns <c>false</c>.</returns>
		Task<bool> ApplicationExists(Application application);

		/// <summary>
		/// Gets the list of applications by specified <paramref name="application"/> criteria.
		/// </summary>
		/// <param name="application">The application to search by.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="limit">The limit.</param>
		/// <returns>
		/// The list of instances of <see cref="Application" />.
		/// </returns>
		Task<IPaginable<Application>> FindApplications(Application application = null, int offset = 0, int limit = 20);

		/// <summary>
		/// Gets the application by specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>the instance of <see cref="Application"/></returns>
		Task<Application> GetApplication(Guid id);

		/// <summary>
		/// Creates the specified application.
		/// </summary>
		/// <param name="application">The application.</param>
		/// <returns>The newly created instance of <see cref="Application"/>.</returns>
		Task<Application> CreateApplication(Application application);

		/// <summary>
		/// Updates the specified application.
		/// </summary>
		/// <param name="application">The application.</param>
		/// <returns>The updated instanc eof <see cref="Application"/>.</returns>
		Task<Application> UpdateApplication(Application application);

		/// <summary>
		/// Deletes the specified application.
		/// </summary>
		/// <param name="application">The application.</param>
		/// <returns></returns>
		Task<Application> DeleteApplication(Application application);

		/// <summary>
		/// Indicates whether the specified grant already exists.
		/// </summary>
		/// <param name="grant">The grant.</param>
		/// <returns><c>True</c> if grant exists. Otherwise it returns <c>false</c>.</returns>
		Task<bool> GrantExists(Grant grant);

		/// <summary>
		/// Gets the list of grants by specified <paramref name="grant"/> criteria.
		/// </summary>
		/// <param name="grant">The grant to search for.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="limit">The limit.</param>
		/// <returns>The list of instances of <see cref="Grant"/>.</returns>
		Task<IPaginable<Grant>> FindGrants(Grant grant, int offset = 0, int limit = 20);

		/// <summary>
		/// Gets the grant by specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>The instance of <see cref="Grant"/>.</returns>
		Task<Grant> GetGrant(string id);

		/// <summary>
		/// Creates the specified grant.
		/// </summary>
		/// <param name="grant">The grant.</param>
		/// <returns>The newly created instance of <see cref="Grant"/>.</returns>
		Task<Grant> CreateGrant(Grant grant);

		/// <summary>
		/// Deletes the specified grant.
		/// </summary>
		/// <param name="grant">The grant.</param>
		/// <returns>The deleted instance of <see cref="Grant"/>.</returns>
		Task<Grant> DeleteGrant(Grant grant);
	}
}
