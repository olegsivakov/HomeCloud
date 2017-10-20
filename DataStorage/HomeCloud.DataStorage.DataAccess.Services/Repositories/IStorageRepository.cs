namespace HomeCloud.DataStorage.DataAccess.Services.Repositories
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.DataStorage.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Defines methods to handle <see cref="Storage"/> data.
	/// </summary>
	public interface IStorageRepository
	{
		/// <summary>
		/// Saves the specified entity.
		/// </summary>
		/// <param name="entity">The entity of type <see cref="Storage" />.</param>
		/// <returns>The instance of <see cref="Storage"/>.</returns>
		Storage Save(Storage entity);

		/// <summary>
		/// Deletes the entity by specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		void Delete(Guid id);

		/// <summary>
		/// Gets the entity by specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>The instance of <see cref="Storage"/>.</returns>
		Storage Get(Guid id);

		/// <summary>
		/// Gets the list of entities.
		/// </summary>
		/// <param name="startIndex">The index of the first record that should appear in the list.</param>
		/// <param name="chunkSize">The number of records to select.</param>
		/// <returns>
		/// The list of instances of <see cref="Storage" />.
		/// </returns>
		IEnumerable<Storage> Get(int startIndex, int chunkSize);
	}
}
