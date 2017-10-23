namespace HomeCloud.DataStorage.DataAccess.Services.Repositories
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataStorage.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Defines methods to handle <see cref="Directory" /> data.
	/// </summary>
	/// <seealso cref="HomeCloud.DataAccess.Services.IDbRepository{HomeCloud.DataStorage.DataAccess.Contracts.Directory}" />
	public interface IDirectoryRepository : IDbRepository<Directory>
	{
		/// <summary>
		/// Deletes the list of entities by specified identifier of parent entity the list belongs to.
		/// </summary>
		/// <param name="id">The parent entity unique identifier.</param>
		void DeleteByParentID(Guid? id);

		/// <summary>
		/// Gets the list of entities by specified identifier of parent entity the list belongs to.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to select.</param>
		/// <returns>The list of instances of <see cref="Directory"/>.</returns>
		IEnumerable<Directory> GetByParentID(Guid? id, int offset = 0, int limit = 20);
	}
}
