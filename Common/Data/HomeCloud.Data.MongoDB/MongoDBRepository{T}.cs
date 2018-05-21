namespace HomeCloud.Data.MongoDB
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using System.Linq;
	using System.Linq.Expressions;

	using System.Threading.Tasks;

	using HomeCloud.Core;

	using global::MongoDB.Driver;
	using global::MongoDB.Driver.Linq;

	#endregion

	/// <summary>
	/// Implements the common repository to handle data objects through <see cref="IMongoDBContext"/> context.
	/// </summary>
	/// <typeparam name="T">The document type.</typeparam>
	public abstract class MongoDBRepository<T> : IMongoDBRepository<T>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MongoDBRepository{T}"/> class.
		/// </summary>
		/// <param name="context">The data context.</param>
		/// <exception cref="System.ArgumentNullException">context</exception>
		protected MongoDBRepository(IMongoDBContext context)
		{
			this.Context = context ?? throw new ArgumentNullException(nameof(context));

			this.CurrentCollection = this.GetCollection();
		}

		#endregion

		#region Protected Properties

		/// <summary>
		/// Gets the data context.
		/// </summary>
		/// <value>
		/// The data context.
		/// </value>
		protected IMongoDBContext Context { get; private set; }

		/// <summary>
		/// Gets the <see cref="MongoDB"/> collection within the current repository instance.
		/// </summary>
		/// <value>
		/// The collection.
		/// </value>
		protected IMongoCollection<T> CurrentCollection { get; private set; }

		#endregion

		#region IRepository<T> Implementations

		/// <summary>
		/// Deletes the records of <see cref="T" /> type by specified expression asynchronously.
		/// </summary>
		/// <param name="selector">The data expression.</param>
		/// <returns>The asynchronous operation.</returns>
		public virtual async Task DeleteAsync(Expression<Func<T, bool>> selector)
		{
			await this.CurrentCollection.DeleteManyAsync<T>(selector);
		}

		/// <summary>
		/// Gets the records of <see cref="!:T" /> type by specified expression asynchronously.
		/// </summary>
		/// <param name="selector">The data expression.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The <see cref="IPaginable"/> list of instances of <see cref="!:T" /> type.
		/// </returns>
		public virtual async Task<IPaginable<T>> FindAsync(Expression<Func<T, bool>> selector, int offset = 0, int limit = 20)
		{
			IEnumerable<T> data = await this.FindAllAsync(selector);

			return new PagedList<T>(data.Skip(offset).Take(limit))
			{
				Offset = offset,
				Limit = limit,
				TotalCount = data.Count()
			};
		}

		/// <summary>
		/// Gets all records of <see cref="!:T" /> type by specified expression asynchronously.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <returns>
		/// The <see cref="IEnumerable{T}"/> list of instances of <see cref="!:T" /> type.
		/// </returns>
		public virtual async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> selector)
		{
			IMongoQueryable<T> query = this.CurrentCollection.AsQueryable();

			IEnumerable<T> result = selector is null ? query : query.Where(selector.Compile());

			return await Task.FromResult(result);
		}

		/// <summary>
		/// Gets the records of <see cref="!:T" /> type by specified expression asynchronously.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The <see cref="T:HomeCloud.Core.IPaginable" /> list of instances of <see cref="!:T" /> type.
		/// </returns>
		public virtual async Task<IPaginable<T>> FindAsync(int offset = 0, int limit = 20)
		{
			return await this.FindAsync(null, offset, limit);
		}

		/// <summary>
		/// Deletes the record by specified unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public virtual async Task DeleteAsync(Guid id)
		{
			FilterDefinition<T> filter = this.GetUniqueFilterDefinition(id);

			await this.CurrentCollection.DeleteOneAsync(filter);
		}

		/// <summary>
		/// Gets the entity of <see cref="!:T" /> by specified unique identifier asynchronously.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>
		/// The instance of <see cref="!:T" /> type.
		/// </returns>
		public virtual async Task<T> GetAsync(Guid id)
		{
			FilterDefinition<T> filter = this.GetUniqueFilterDefinition(id);

			IAsyncCursor<T> cursor = await this.CurrentCollection.FindAsync(
				filter,
				new FindOptions<T>()
				{
					Skip = 0,
					Limit = 1
				});

			return await cursor.FirstOrDefaultAsync();
		}

		/// <summary>
		/// Saves the specified entity of <see cref="!:T" /> asynchronously.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// The instance of <see cref="!:T" />.
		/// </returns>
		public async virtual Task<T> SaveAsync(T entity)
		{
			FilterDefinition<T> filter = this.GetUniqueFilterDefinition(entity);
			FindOneAndReplaceOptions<T> options = new FindOneAndReplaceOptions<T>()
			{
				IsUpsert = true,
				ReturnDocument = ReturnDocument.After
			};

			return await this.CurrentCollection.FindOneAndReplaceAsync(filter, entity, options);
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Gets the <see cref="System.Linq.Expressions.Expression"/>-based <see cref="MongoDB"/> filter definition for <see cref="T"/> entity that have unique identifier attribute.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>The instance of <see cref="FilterDefinition{T}"/>.</returns>
		protected abstract FilterDefinition<T> GetUniqueFilterDefinition(T entity);

		/// <summary>
		/// Gets the <see cref="System.Linq.Expressions.Expression"/>-based <see cref="MongoDB"/> filter definition based on <see cref="Guid"/> identifier.
		/// </summary>
		/// <param name="id">The object identifier.</param>
		/// <returns>The instance of <see cref="FilterDefinition{T}"/>.</returns>
		protected abstract FilterDefinition<T> GetUniqueFilterDefinition(object id);

		#endregion

		#region Private Methods

		/// <summary>
		/// Gets the <see cref="MongoDB"/> collection.
		/// </summary>
		/// <returns>The instance of <see cref="IMongoCollection{T}"/></returns>
		private IMongoCollection<T> GetCollection()
		{
			Task<IMongoCollection<T>> task = this.Context.GetCollectionAsync<T>();
			Task.WhenAll(task);

			return task.Result;
		}

		#endregion
	}
}
