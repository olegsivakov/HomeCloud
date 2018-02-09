namespace HomeCloud.Data.SqlServer
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using System.Data;
	using System.Data.SqlClient;

	using System.Linq.Expressions;
	using System.Threading.Tasks;

	using Dapper;

	#endregion

	/// <summary>
	/// Provides methods to query data from <see cref="SqlServer"/> database.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.SqlServer.ISqlServerDBContext" />
	public class SqlServerDBContext : ISqlServerDBContext
	{
		#region Private Members

		/// <summary>
		/// The database connection member.
		/// </summary>
		private readonly IDbConnection connection = null;

		/// <summary>
		/// The database transaction member.
		/// </summary>
		private readonly IDbTransaction transaction = null;

		/// <summary>
		/// The member indicating whether transaction commit is failed. It's considered the initial state of context as not committed and marked as failed. 
		/// </summary>
		private bool isCommitFailed = true;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DbContext" /> class.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="isTransactional">The database transaction.</param>
		public SqlServerDBContext(string connectionString, bool isTransactional = false)
		{
			this.connection = new SqlConnection(connectionString);
			this.connection.Open();

			if (isTransactional)
			{
				this.transaction = this.connection.BeginTransaction();
			}
		}

		#endregion

		#region IDataContext Implementations

		/// <summary>
		/// Queries data by the specified SQL query.
		/// </summary>
		/// <typeparam name="T">The type of data to query.</typeparam>
		/// <param name="sqlQuery">The SQL query.</param>
		/// <param name="parameter">The SQL query parameter.</param>
		/// <returns>
		/// The list of instances of <see cref="T" />.
		/// </returns>
		public async Task<IEnumerable<T>> QueryAsync<T>(string sqlQuery, object parameter = null)
		{
			return await this.connection.QueryAsync<T>(sqlQuery, parameter, this.transaction, commandType: CommandType.StoredProcedure, commandTimeout: this.connection.ConnectionTimeout);
		}

		/// <summary>
		/// Executes the specified SQL query.
		/// </summary>
		/// <param name="sqlQuery">The SQL query.</param>
		/// <param name="parameter">The SQL query parameter.</param>
		/// <returns>
		/// The number of rows affected.
		/// </returns>
		public async Task<int> ExecuteAsync(string sqlQuery, object parameter = null)
		{
			return await this.connection.ExecuteAsync(sqlQuery, parameter, this.transaction, commandType: CommandType.StoredProcedure, commandTimeout: this.connection.ConnectionTimeout);
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <typeparam name="TInput">The type of the input.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="sqlQuery">The SQL query.</param>
		/// <param name="parameters">The parameters.</param>
		/// <param name="outputParameters">The output parameters.</param>
		/// <returns>The result of SQL query.</returns>
		public async Task<TResult> ExecuteScalarAsync<TInput, TResult>(string sqlQuery, TInput parameters = default(TInput), params Expression<Func<TInput, object>>[] outputParameters)
		{
			if (parameters == null)
			{
				return await this.connection.ExecuteScalarAsync<TResult>(sqlQuery, this.transaction, commandType: CommandType.StoredProcedure, commandTimeout: this.connection.ConnectionTimeout);
			}

			DynamicParameters dynamicParams = new DynamicParameters(parameters);

			if (outputParameters != null)
			{
				foreach (var outputParameter in outputParameters)
				{
					dynamicParams.Output(parameters, outputParameter);
				}
			}

			return await this.connection.ExecuteScalarAsync<TResult>(sqlQuery, dynamicParams, this.transaction, commandType: CommandType.StoredProcedure, commandTimeout: this.connection.ConnectionTimeout);
		}

		/// <summary>
		/// Performs the specified SQL query and returns result.
		/// </summary>
		/// <typeparam name="TInput">The type of the input.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="sqlQuery">The SQL query.</param>
		/// <param name="parameters">The parameters.</param>
		/// <param name="outputParameters">The output parameters.</param>
		/// <returns>The list of instances of <see cref="TResult"/> type.</returns>
		public async Task<IEnumerable<TResult>> QueryAsync<TInput, TResult>(string sqlQuery, TInput parameters = default(TInput), params Expression<Func<TInput, object>>[] outputParameters)
		{
			if (parameters == null)
			{
				return await this.connection.QueryAsync<TResult>(sqlQuery, this.transaction, commandType: CommandType.StoredProcedure, commandTimeout: this.connection.ConnectionTimeout);
			}

			DynamicParameters dynamicParams = new DynamicParameters(parameters);

			if (outputParameters != null)
			{
				foreach (var outputParameter in outputParameters)
				{
					dynamicParams.Output(parameters, outputParameter);
				}
			}

			return await this.connection.QueryAsync<TResult>(sqlQuery, dynamicParams, this.transaction, commandType: CommandType.StoredProcedure, commandTimeout: this.connection.ConnectionTimeout);
		}

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
		public async Task<int> ExecuteAsync<TInput>(string sqlQuery, TInput parameters = default(TInput), params Expression<Func<TInput, object>>[] outputParameters)
		{
			DynamicParameters dynamicParams = new DynamicParameters(parameters);

			if (outputParameters != null)
			{
				foreach (var outputParameter in outputParameters)
				{
					dynamicParams.Output(parameters, outputParameter);
				}
			}

			return await this.connection.ExecuteAsync(sqlQuery, dynamicParams, this.transaction, commandType: CommandType.StoredProcedure, commandTimeout: this.connection.ConnectionTimeout);
		}

		#endregion

		#region ITransactionalDataContext Implementations

		/// <summary>
		/// Commits the changes to the database.
		/// </summary>
		public void Commit()
		{
			if (this.transaction != null)
			{
				this.transaction.Commit();

				this.isCommitFailed = false;
			}
		}

		/// <summary>
		/// Rollbacks the changes made against database.
		/// </summary>
		public void Rollback()
		{
			if (this.transaction != null && this.isCommitFailed)
			{
				this.transaction.Rollback();
			}
		}

		#endregion

		#region IDisposable Implementations

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			if (this.transaction != null)
			{
				try
				{
					this.Rollback();
				}
				finally
				{
					this.transaction.Dispose();
				}
			}

			if (this.connection.State != ConnectionState.Closed)
			{
				this.connection.Close();
			}
		}

		#endregion
	}
}
