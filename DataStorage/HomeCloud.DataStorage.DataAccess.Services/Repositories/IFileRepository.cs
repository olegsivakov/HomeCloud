namespace HomeCloud.DataStorage.DataAccess.Services.Repositories
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.DataStorage.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Defines methods to handle <see cref="File"/> data.
	/// </summary>
	public interface IFileRepository
	{
		/// <summary>
		/// Saves the specified entity.
		/// </summary>
		/// <param name="entity">The entity of type <see cref="File" />.</param>
		/// <returns>The instance of <see cref="File"/>.</returns>
		File Save(File entity);

		/// <summary>
		/// Deletes the entity by specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		void Delete(Guid id);

		/// <summary>
		/// Deletes the list of entities by specified identifier of entity of <see cref="Directory"/> type the list belongs to.
		/// </summary>
		/// <param name="id">The parent entity unique identifier.</param>
		void DeleteByDirectoryID(Guid id);

		/// <summary>
		/// Gets the entity by specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>The instance of <see cref="File"/>.</returns>
		File Get(Guid id);

		/// <summary>
		/// Gets the list of entities by specified identifier of entity of <see cref="Directory"/> type the list belongs to.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <param name="startIndex">The index of the first record that should appear in the list.</param>
		/// <param name="chunkSize">The number of records to select.</param>
		/// <returns>The list of instances of <see cref="File"/>.</returns>
		IEnumerable<File> GetByDirectoryID(Guid id, int startIndex, int chunkSize);
	}
}
