namespace HomeCloud.DataAccess.Services
{
	#region Usings

	using HomeCloud.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Marks the handler to query data from database.
	/// </summary>
	public interface IDbQueryHandler
	{
	}

	/// <summary>
	/// Defines combination of methods to query data of <see cref="TResult" /> type.
	/// </summary>
	/// <typeparam name="TQuery">The type of the database query.</typeparam>
	/// <typeparam name="TResult">The type of the data to query.</typeparam>
	public interface IDbQueryHandler<TQuery, out TResult> : IDbQueryHandler
		where TQuery : IDbQuery<TResult>
	{
		/// <summary>
		/// Executes the specified database query and returns the result of <see cref="TResult"/> type..
		/// </summary>
		/// <param name="query">The database query.</param>
		/// <returns>The instance of <see cref="TResult"/> type.</returns>
		TResult Execute(TQuery query);
	}
}
