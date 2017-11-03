namespace HomeCloud.DataStorage.DataAccess.Services.Repositories
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataStorage.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Defines combination of methods against <see cref="File"/> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.DataAccess.Services.IDbRepository{HomeCloud.DataStorage.DataAccess.Contracts.File}" />
	public interface IFileRepository : IDbRepository<File>
	{
		/// <summary>
		/// Deletes the list of entities by specified identifier of entity of <see cref="Catalog"/> type the list belongs to.
		/// </summary>
		/// <param name="id">The parent entity unique identifier.</param>
		void DeleteByDirectoryID(Guid id);

		/// <summary>
		/// Gets the list of entities by specified identifier of entity of <see cref="Catalog"/> type the list belongs to.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to select.</param>
		/// <returns>The list of instances of <see cref="File"/>.</returns>
		IEnumerable<File> GetByDirectoryID(Guid id, int offset = 0, int limit = 20);
	}
}
