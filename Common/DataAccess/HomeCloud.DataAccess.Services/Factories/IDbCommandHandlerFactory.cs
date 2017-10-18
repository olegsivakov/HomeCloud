namespace HomeCloud.DataAccess.Services.Factories
{
	#region Usings

	using HomeCloud.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Defines methods to create handlers that executes commands against database.
	/// </summary>
	public interface IDbCommandHandlerFactory : ICommandHandlerFactory
	{
		/// <summary>
		/// Gets the database-specific handler for the specified data context.
		/// </summary>
		/// <typeparam name="T">The type of database handler derived from <see cref="IDbCommandHandler"/>.</typeparam>
		/// <param name="context">The database context.</param>
		/// <returns>The instance of <see cref="IDbCommandHandler"/> type.</returns>
		T GetHandler<T>(IDbContext context) where T : IDbCommandHandler;
	}
}
