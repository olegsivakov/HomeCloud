namespace HomeCloud.Data.SqlServer
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Threading.Tasks;

	using HomeCloud.Core;

	#endregion

	/// <summary>
	/// Implements the common repository to handle data objects through <see cref="ISqlServerDBContext" /> context.
	/// </summary>
	/// <typeparam name="T">The type of data.</typeparam>
	/// <seealso cref="HomeCloud.Data.SqlServer.ISqlServerDBRepository{T}" />
	public abstract class SqlServerDBRepository<T> : ISqlServerDBRepository<T>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MongoDBRepository{T}"/> class.
		/// </summary>
		/// <param name="context">The data context.</param>
		/// <exception cref="System.ArgumentNullException">context</exception>
		protected SqlServerDBRepository(ISqlServerDBContext context)
		{
			this.Context = context ?? throw new ArgumentNullException(nameof(context));
		}

		#endregion

		#region Protected Properties

		/// <summary>
		/// Gets the data context.
		/// </summary>
		/// <value>
		/// The data context.
		/// </value>
		protected ISqlServerDBContext Context { get; private set; }

		#endregion

		#region ISqlServerDBRepository<T> Implementations

		/// <summary>
		/// Deletes the record by specified unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public abstract Task DeleteAsync(Guid id);

		/// <summary>
		/// Searches for all records of <see cref="!:T" /> type.
		/// </summary>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The list of instances of <see cref="!:T" /> type.
		/// </returns>
		public abstract Task<IPaginable<T>> FindAsync(int offset = 0, int limit = 20);

		/// <summary>
		/// Gets the entity of <see cref="!:T" /> by specified unique identifier asynchronously.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>
		/// The instance of <see cref="!:T" /> type.
		/// </returns>
		public abstract Task<T> GetAsync(Guid id);

		/// <summary>
		/// Saves the specified entity of <see cref="!:T" /> asynchronously.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// The instance of <see cref="!:T" />.
		/// </returns>
		public abstract Task<T> SaveAsync(T entity);

		#endregion
	}
}
