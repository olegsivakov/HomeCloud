namespace HomeCloud.DataAccess.Contracts
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;

	#endregion

	/// <summary>
	/// Defines methods to query data from database sources.
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	/// <seealso cref="HomeCloud.DataAccess.Contracts.IContext" />
	public interface IDbContext : IDisposable
	{
		/// <summary>
		/// Queries data by the specified SQL query.
		/// </summary>
		/// <typeparam name="T">The type of data to query.</typeparam>
		/// <param name="sqlQuery">The SQL query.</param>
		/// <param name="parameter">The SQL query parameter.</param>
		/// <returns>The list of instances of <see cref="T"/>.</returns>
		IEnumerable<T> Query<T>(string sqlQuery, object parameter = null);

		/// <summary>
		/// Executes the specified SQL query.
		/// </summary>
		/// <param name="sqlQuery">The SQL query.</param>
		/// <param name="parameter">The SQL query parameter.</param>
		/// <returns>The number of rows affected.</returns>
		int Execute(string sqlQuery, object parameter = null);

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
		int Execute<TInput>(string sqlQuery, TInput parameters = default(TInput), params Expression<Func<TInput, object>>[] outputParameters);

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <typeparam name="TInput">The type of the input.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="sqlQuery">The SQL query.</param>
		/// <param name="parameters">The parameters.</param>
		/// <param name="outputParameters">The output parameters.</param>
		/// <returns>The result of SQL query.</returns>
		TResult ExecuteScalar<TInput, TResult>(string sqlQuery, TInput parameters = default(TInput), params Expression<Func<TInput, object>>[] outputParameters);

		/// <summary>
		/// Performs the specified SQL query and returns result.
		/// </summary>
		/// <typeparam name="TInput">The type of the parameters.</typeparam>
		/// <typeparam name="TResult">The type of the result returned.</typeparam>
		/// <param name="sqlQuery">The SQL query.</param>
		/// <param name="parameters">The input parameters.</param>
		/// <param name="outputParameters">The definition of input parameters marked as output.</param>
		/// <returns>The list of instances of <see cref="TResult"/>.</returns>
		IEnumerable<TResult> Query<TInput, TResult>(string sqlQuery, TInput parameters = default(TInput), params Expression<Func<TInput, object>>[] outputParameters);
	}
}
