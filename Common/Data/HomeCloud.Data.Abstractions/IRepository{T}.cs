namespace HomeCloud.Data
{
	#region Usings

	using System;
	using System.Linq.Expressions;
	using System.Threading.Tasks;

	using HomeCloud.Core;

	#endregion

	/// <summary>
	/// Defines methods to handle data.
	/// </summary>
	/// <typeparam name="T">The type of data</typeparam>
	/// <seealso cref="HomeCloud.Data.IRepository" />
	public interface IRepository<T> : IRepository
	{
		/// <summary>
		/// Deletes the records of <see cref="T" /> type by specified expression asynchronously.
		/// </summary>
		/// <param name="selector">The data expression.</param>
		/// <returns>The asynchronous operation.</returns>
		Task DeleteAsync(Expression<Func<T, bool>> selector);

		/// <summary>
		/// Gets the records of <see cref="T" /> type by specified expression asynchronously.
		/// </summary>
		/// <param name="selector">The data expression.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The <see cref="IPaginable"/> list of instances of <see cref="T" /> type.
		/// </returns>
		Task<IPaginable<T>> FindAsync(Expression<Func<T, bool>> selector, int offset = 0, int limit = 20);

		/// <summary>
		/// Gets the entity of <see cref="T"/> by specified unique identifier asynchronously.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>The instance of <see cref="T"/> type.</returns>
		Task<T> GetAsync(Guid id);

		/// <summary>
		/// Saves the specified entity of <see cref="T"/> asynchronously.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>The instance of <see cref="T"/>.</returns>
		Task<T> SaveAsync(T entity);
	}
}
