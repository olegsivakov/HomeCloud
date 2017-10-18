namespace HomeCloud.DataAccess.Contracts
{
	/// <summary>
	/// Represents database command.
	/// </summary>
	public interface IDbCommand
	{
		/// <summary>
		/// Provides SQL query string.
		/// </summary>
		/// <returns>The SQL query.</returns>
		string ToSQL();
	}
}
