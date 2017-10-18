namespace HomeCloud.DataAccess.Contracts
{
	/// <summary>
	/// Represents database query.
	/// </summary>
	public interface IDbQuery
	{
		/// <summary>
		/// Provides SQL query string.
		/// </summary>
		/// <returns>The SQL query.</returns>
		string ToSQL();
	}

	/// <summary>
	/// Represents database query.
	/// </summary>
	/// <typeparam name="T">The type of data to be returned by query.</typeparam>
	public interface IDbQuery<T> : IDbQuery
	{
	}
}
