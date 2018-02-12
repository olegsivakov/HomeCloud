namespace HomeCloud.Data.SqlServer
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Linq.Expressions;
	using System.Threading.Tasks;

	#endregion

	/// <summary>
	/// Defines methods to query data from <see cref="SqlServer"/> database.
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	public interface ISqlServerDBContext : IDisposable
	{
		/// <summary>
		/// Gets or sets the database connection.
		/// </summary>
		/// <value>
		/// The database connection.
		/// </value>
		IDbConnection Connection { get; }

		/// <summary>
		/// Creates the database transaction for <see cref="ISqlServerDBContext.Connection"/>.
		/// </summary>
		/// <returns>The instance of <see cref="IDbTransaction"/>.</returns>
		IDbTransaction CreateTransaction();

		/// <summary>
		/// Queries data by the specified SQL query.
		/// </summary>
		/// <typeparam name="T">The type of data to query.</typeparam>
		/// <param name="sqlQuery">The SQL query.</param>
		/// <param name="parameter">The SQL query parameter.</param>
		/// <returns>The list of instances of <see cref="T"/>.</returns>
		Task<IEnumerable<T>> QueryAsync<T>(string sqlQuery, object parameter = null);

		/// <summary>
		/// Executes the specified SQL query.
		/// </summary>
		/// <param name="sqlQuery">The SQL query.</param>
		/// <param name="parameter">The SQL query parameter.</param>
		/// <returns>The number of rows affected.</returns>
		Task<int> ExecuteAsync(string sqlQuery, object parameter = null);

		/// <summary>
		/// Executes the specified SQL query.
		/// </summary>
		/// <typeparam name="TInput">The type of the input.</typeparam>
		/// <param name="sqlQuery">The SQL query.</param>
		/// <param name="parameters">The parameters.</param>
		/// <param name="outputParameters">The output parameters.</param>
		/// <returns>
		/// The number of rows affected.
		/// </returns>
		Task<int> ExecuteAsync<TInput>(string sqlQuery, TInput parameters = default(TInput), params Expression<Func<TInput, object>>[] outputParameters);

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <typeparam name="TInput">The type of the input.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="sqlQuery">The SQL query.</param>
		/// <param name="parameters">The parameters.</param>
		/// <param name="outputParameters">The output parameters.</param>
		/// <returns>The result of SQL query.</returns>
		Task<TResult> ExecuteScalarAsync<TInput, TResult>(string sqlQuery, TInput parameters = default(TInput), params Expression<Func<TInput, object>>[] outputParameters);

		/// <summary>
		/// Performs the specified SQL query and returns result.
		/// </summary>
		/// <typeparam name="TInput">The type of the parameters.</typeparam>
		/// <typeparam name="TResult">The type of the result returned.</typeparam>
		/// <param name="sqlQuery">The SQL query.</param>
		/// <param name="parameters">The input parameters.</param>
		/// <param name="outputParameters">The definition of input parameters marked as output.</param>
		/// <returns>The list of instances of <see cref="TResult"/>.</returns>
		Task<IEnumerable<TResult>> QueryAsync<TInput, TResult>(string sqlQuery, TInput parameters = default(TInput), params Expression<Func<TInput, object>>[] outputParameters);

		/// <summary>
		/// Commits the changes to the database.
		/// </summary>
		void Commit();

		/// <summary>
		/// Rollbacks the changes made against database.
		/// </summary>
		void Rollback();
	}
}
