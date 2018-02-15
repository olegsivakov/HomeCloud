namespace HomeCloud.Data.MongoDB
{
	using HomeCloud.Core;
	#region Usings

	using System;
	using System.Linq.Expressions;
	using System.Threading.Tasks;

	#endregion

	/// <summary>
	/// Defines methods to handle the data of <see cref="T" /> type stored in <see cref="MongoDB" /> database.
	/// </summary>
	/// <typeparam name="T">The type of data to handle.</typeparam>
	/// <seealso cref="HomeCloud.Data.MongoDB.IMongoDBRepository" />
	/// <seealso cref="HomeCloud.Data.IRepository{T}" />
	public interface IMongoDBRepository<T> : IMongoDBRepository, IRepository<T>
	{
		/// <summary>
		/// Deletes the records of <see cref="T" /> type by specified expression asynchronously.
		/// </summary>
		/// <param name="selector">The data expression.</param>
		/// <returns>The asynchronous operation.</returns>
		Task DeleteAsync(Expression<Func<T, bool>> selector);

		/// <summary>
		/// Gets the records of <see cref="!:T" /> type by specified expression asynchronously.
		/// </summary>
		/// <param name="selector">The data expression.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The <see cref="IPaginable"/> list of instances of <see cref="!:T" /> type.
		/// </returns>
		Task<IPaginable<T>> FindAsync(Expression<Func<T, bool>> selector, int offset = 0, int limit = 20);
	}
}
