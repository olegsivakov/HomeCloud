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

	using Microsoft.Extensions.Options;

	#endregion

	/// <summary>
	/// Provides methods to query data from <see cref="SqlServer" /> database.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.SqlServer.ISqlServerDBContext" />
	public class SqlServerDBContext : ISqlServerDBContext
	{
		#region Private Members

		/// <summary>
		/// The database connection member.
		/// </summary>
		private IDbConnection connection = null;

		/// <summary>
		/// The database transaction.
		/// </summary>
		private IDbTransaction transaction = null;

		/// <summary>
		/// The member indicating whether transaction commit is failed. It's considered the initial state of context as not committed and marked as failed.
		/// </summary>
		private bool isCommitFailed = true;

		/// <summary>
		/// The configuration optionsю
		/// </summary>
		private readonly SqlServerDBOptions options = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DbContext" /> class.
		/// </summary>
		/// <param name="accessor">The configuration options accessor.</param>
		/// <exception cref="System.ArgumentNullException">accessor or <see cref="IOptionsSnapshot{SqlServerDBOptions}.Value"/> or <see cref="SqlServerDBOptions.ConnectionString"/>.</exception>
		public SqlServerDBContext(IOptionsSnapshot<SqlServerDBOptions> accessor)
		{
			if (accessor is null)
			{
				throw new ArgumentNullException(nameof(accessor));
			}

			if (accessor.Value is null)
			{
				throw new ArgumentNullException(nameof(accessor.Value));
			}

			if (string.IsNullOrWhiteSpace(accessor.Value.ConnectionString))
			{
				throw new ArgumentNullException(nameof(accessor.Value.ConnectionString));
			}

			this.options = accessor.Value;

			this.Initialize();
		}

		#endregion

		#region IDataContext Implementations

		/// <summary>
		/// Gets or sets the database connection.
		/// </summary>
		/// <value>
		/// The database connection.
		/// </value>
		public IDbConnection Connection => this.connection ?? (this.connection = this.ConfigureConnection());

		/// <summary>
		/// Creates the database transaction for <see cref="ISqlServerDBContext.Connection"/>.
		/// </summary>
		/// <returns>The instance of <see cref="IDbTransaction"/>.</returns>
		public IDbTransaction CreateTransaction()
		{
			return this.transaction = this.Connection.BeginTransaction();
		}

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
			return await this.Connection.QueryAsync<T>(sqlQuery, parameter, this.transaction, commandType: CommandType.StoredProcedure, commandTimeout: this.Connection.ConnectionTimeout);
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
			return await this.Connection.ExecuteAsync(sqlQuery, parameter, this.transaction, commandType: CommandType.StoredProcedure, commandTimeout: this.Connection.ConnectionTimeout);
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
				return await this.Connection.ExecuteScalarAsync<TResult>(sqlQuery, this.transaction, commandType: CommandType.StoredProcedure, commandTimeout: this.Connection.ConnectionTimeout);
			}

			DynamicParameters dynamicParams = new DynamicParameters(parameters);

			if (outputParameters != null)
			{
				foreach (var outputParameter in outputParameters)
				{
					dynamicParams.Output(parameters, outputParameter);
				}
			}

			return await this.Connection.ExecuteScalarAsync<TResult>(sqlQuery, dynamicParams, this.transaction, commandType: CommandType.StoredProcedure, commandTimeout: this.Connection.ConnectionTimeout);
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
				return await this.Connection.QueryAsync<TResult>(sqlQuery, this.transaction, commandType: CommandType.StoredProcedure, commandTimeout: this.Connection.ConnectionTimeout);
			}

			DynamicParameters dynamicParams = new DynamicParameters(parameters);

			if (outputParameters != null)
			{
				foreach (var outputParameter in outputParameters)
				{
					dynamicParams.Output(parameters, outputParameter);
				}
			}

			return await this.Connection.QueryAsync<TResult>(sqlQuery, dynamicParams, this.transaction, commandType: CommandType.StoredProcedure, commandTimeout: this.Connection.ConnectionTimeout);
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

			return await this.Connection.ExecuteAsync(sqlQuery, dynamicParams, this.transaction, commandType: CommandType.StoredProcedure, commandTimeout: this.Connection.ConnectionTimeout);
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

			if (this.connection != null && this.Connection.State != ConnectionState.Closed)
			{
				this.Connection.Close();
			}

			this.Initialize();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Configures the connection to database.
		/// </summary>
		/// <param name="connection">The database connection.</param>
		/// <returns>The instance of <see cref="IDbConnection"/>.</returns>
		private IDbConnection ConfigureConnection()
		{
			if (this.connection is null)
			{
				this.connection = new SqlConnection(this.options.ConnectionString);
				this.connection.Open();
			}

			return this.connection;
		}

		/// <summary>
		/// Initializes current instance.
		/// </summary>
		private void Initialize()
		{
			this.isCommitFailed = true;
			this.connection = null;
			this.transaction = null;
		}

		#endregion
	}
}
