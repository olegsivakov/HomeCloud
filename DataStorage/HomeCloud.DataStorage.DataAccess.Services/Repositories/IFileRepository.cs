namespace HomeCloud.DataStorage.DataAccess.Services.Repositories
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

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
		/// Gets the number of entities that match the specified one.
		/// </summary>
		/// <param name="file">>The file to search by.</param>
		/// <returns>The number of entities.</returns>
		Task<int> GetCountAsync(File file);

		/// <summary>
		/// Deletes the list of entities by specified identifier of entity of <see cref="Catalog" /> type the list belongs to.
		/// </summary>
		/// <param name="id">The parent entity unique identifier.</param>
		/// <returns>The asynchronous operation.</returns>
		Task DeleteByDirectoryIDAsync(Guid id);

		/// <summary>
		/// Gets the list of entities that match the specified one.
		/// </summary>
		/// <param name="file">The file to search by.</param>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to select.</param>
		/// <returns>
		/// The list of instances of <see cref="File" />.
		/// </returns>
		Task<IEnumerable<File>> FindAsync(File file, int offset = 0, int limit = 20);
	}
}
