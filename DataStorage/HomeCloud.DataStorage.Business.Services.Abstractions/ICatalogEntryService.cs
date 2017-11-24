namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Defines methods to handle catalog entries.
	/// </summary>
	public interface ICatalogEntryService
	{
		/// <summary>
		/// Creates the specified catalog entry.
		/// </summary>
		/// <param name="entry">The <see cref="CatalogEntryStream" /> content stream of catalog entry.</param>
		/// <returns>
		/// The operation result containing created instance of <see cref="CatalogEntry"/>.
		/// </returns>
		Task<ServiceResult<CatalogEntry>> CreateEntryAsync(CatalogEntryStream stream);

		/// <summary>
		/// Deletes the catalog entry by specified identifier.
		/// </summary>
		/// <param name="id">The catalog entry identifier.</param>
		/// <returns>The operation result.</returns>
		Task<ServiceResult> DeleteEntryAsync(Guid id);

		/// <summary>
		/// Gets the catalog entry by specified identifier.
		/// </summary>
		/// <param name="id">The catalog entry identifier.</param>
		/// <returns>
		/// The operation result containing the instance of <see cref="CatalogEntry" />.
		/// </returns>
		Task<ServiceResult<CatalogEntry>> GetEntryAsync(Guid id);

		/// <summary>
		/// Gets the content stream of the catalog entry by specified entry identifier.
		/// </summary>
		/// <param name="id">The catalog entry identifier.</param>
		/// <returns>
		/// The operation result containing the instance of <see cref="CatalogEntryStream" />.
		/// </returns>
		Task<CatalogEntryStream> GetEntryStreamAsync(Guid id, int offset = 0, int size = 1024);

		/// <summary>
		/// Gets the list of catalog entries that belong to the catalog which identifier is specified.
		/// </summary>
		/// <param name="catalogID">The catalog identifier.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The operation result containing the list of instances of <see cref="CatalogEntry" />.
		/// </returns>
		Task<IEnumerable<CatalogEntry>> GetEntriesAsync(Guid catalogID, int offset = 0, int limit = 20);
	}
}
