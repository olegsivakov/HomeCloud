namespace HomeCloud.DataAccess.Services
{
	#region Usings

	using System.Collections.Generic;

	using HomeCloud.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Marks the repository to query data from database.
	/// </summary>
	public interface IDbRepository
	{
	}

	/// <summary>
	/// Defines combination of methods against data entity of <see cref="T"/> type.
	/// </summary>
	/// <typeparam name="T">The type of entity.</typeparam>
	public interface IDbRepository<T> : IDbRepository
	{
		/// <summary>
		/// Gets the entity by specified unique identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>The instance of <see cref="T"/> type.</returns>
		T Get(int id);

		/// <summary>
		/// Looks for all records of <see cref="T" /> type.
		/// </summary>
		/// <returns>
		/// The list of instances of <see cref="T" /> type.
		/// </returns>
		IEnumerable<T> FindAll();

		/// <summary>
		/// Looks for all records of <see cref="T" /> type.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <returns>
		/// The list of instances of <see cref="T" /> type.
		/// </returns>
		IEnumerable<T> FindAll(string userName);

		/// <summary>
		/// Saves the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		void Save(T entity);

		/// <summary>
		/// Deletes the entity by specified unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier..</param>
		void Delete(int id);
	}
}
