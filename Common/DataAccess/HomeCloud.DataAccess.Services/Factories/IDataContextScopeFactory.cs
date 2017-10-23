namespace HomeCloud.DataAccess.Services.Factories
{
	#region Usings

	using HomeCloud.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Defines methods for creation of context scopes.
	/// </summary>
	public interface IDataContextScopeFactory
	{
		/// <summary>
		/// Creates database context scope.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="isTransactional">Indicates whether the scope supports transactions.</param>
		/// <returns>
		/// The instance of <see cref="T:IDataContextScope" />.
		/// </returns>
		IDbContextScope CreateDbContextScope(string connectionString, bool isTransactional = false);

		/// <summary>
		/// Creates document context scope.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		/// <returns>
		/// The instance of <see cref="T:IDataContextScope" />.
		/// </returns>
		IDocumentContextScope CreateDocumentContextScope(string connectionString);
	}
}
