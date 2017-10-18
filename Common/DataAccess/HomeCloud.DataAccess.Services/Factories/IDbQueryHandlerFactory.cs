namespace HomeCloud.DataAccess.Services.Factories
{
	#region Usings

	using HomeCloud.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Defines methods to create handlers that performs database queries.
	/// </summary>
	public interface IDbQueryHandlerFactory : IQueryHandlerFactory
	{
		/// <summary>
		/// Gets the database-specific handler for the specified data context.
		/// </summary>
		/// <typeparam name="T">The type of database handler derived from <see cref="IDbQueryHandler"/>.</typeparam>
		/// <param name="context">The database context.</param>
		/// <returns>The instance of <see cref="IDbQueryHandler"/> type.</returns>
		T GetHandler<T>(IDbContext context) where T : IDbQueryHandler;
	}
}
