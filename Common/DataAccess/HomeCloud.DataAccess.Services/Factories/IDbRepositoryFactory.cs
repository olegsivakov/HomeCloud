namespace HomeCloud.DataAccess.Services.Factories
{
	#region Usings

	using HomeCloud.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Defines methods to create database repositories.
	/// </summary>
	public interface IDbRepositoryFactory : IRepositoryFactory
	{
		/// <summary>
		/// Gets the database-specific repository for specified data context.
		/// </summary>
		/// <typeparam name="T">The type of the repository derived from <see cref="IDbRepository"/>.</typeparam>
		/// <param name="context">The database context.</param>
		/// <returns> The instance of <see cref="IDbRepository" /> type.</returns>
		T GetRepository<T>(IDbContext context) where T : IDbRepository;
	}
}
