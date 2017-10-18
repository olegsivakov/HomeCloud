namespace HomeCloud.DataAccess.Services
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Defines methods for operation within data context executed as a single scope.
	/// </summary>
	public interface IDbContextScope : IDisposable
	{
		/// <summary>
		/// Gets the <see cref="IDbRepository"/> repository.
		/// </summary>
		/// <typeparam name="T">The type of the repository derived from <see cref="IDbRepository"/>.</typeparam>
		/// <returns>The instance of <see cref="IDbRepository"/>.</returns>
		T GetRepository<T>() where T : IDbRepository;

		/// <summary>
		/// Gets the <see cref="IDbQueryHandler"/> handler.
		/// </summary>
		/// <typeparam name="T">The type of the query handler derived from <see cref="IDbQueryHandler"/>.</typeparam>
		/// <returns>The instance of <see cref="IDbQueryHandler"/>.</returns>
		T GetQueryHandler<T>() where T : IDbQueryHandler;

		/// <summary>
		/// Gets the <see cref="IDbCommandHandler"/> handler.
		/// </summary>
		/// <typeparam name="T">The type of the command handler derived from <see cref="IDbQueryHandler"/>.</typeparam>
		/// <returns>The instance of <see cref="IDbCommandHandler"/>.</returns>
		T GetCommandHandler<T>() where T : IDbCommandHandler;

		/// <summary>
		/// Commits existing changes to database.
		/// </summary>
		void Commit();
	}
}
