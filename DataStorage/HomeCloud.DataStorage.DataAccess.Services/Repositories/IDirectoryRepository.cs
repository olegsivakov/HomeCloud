namespace HomeCloud.DataStorage.DataAccess.Services.Repositories
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.DataStorage.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Defines methods to handle <see cref="Directory"/> data.
	/// </summary>
	public interface IDirectoryRepository
	{
		/// <summary>
		/// Saves the specified entity.
		/// </summary>
		/// <param name="entity">The entity of type <see cref="Directory" />.</param>
		/// <returns>The instance of <see cref="Directory" />.</returns>
		Directory Save(Directory entity);

		/// <summary>
		/// Deletes the entity by specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		void Delete(Guid id);

		/// <summary>
		/// Deletes the list of entities by specified identifier of parent entity the list belongs to.
		/// </summary>
		/// <param name="id">The parent entity unique identifier.</param>
		void DeleteByParentID(Guid? id);

		/// <summary>
		/// Gets the entity by specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>The instance of <see cref="Directory"/>.</returns>
		Directory Get(Guid id);

		/// <summary>
		/// Gets the list of entities by specified identifier of parent entity the list belongs to.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <param name="startIndex">The index of the first record that should appear in the list.</param>
		/// <param name="chunkSize">The number of records to select.</param>
		/// <returns>The list of instances of <see cref="Directory"/>.</returns>
		IEnumerable<Directory> GetByParentID(Guid? id, int startIndex, int chunkSize);
	}
}
