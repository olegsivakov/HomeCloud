namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Defines methods to handle storages.
	/// </summary>
	public interface IStorageService
	{
		/// <summary>
		/// Creates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type.</param>
		/// <returns>The asynchronous operation.</returns>
		Task CreateStorageAsync(Storage storage);

		/// <summary>
		/// Gets the list of storages.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>The list of instances of <see cref="Storage"/> type.</returns>
		Task<IEnumerable<Storage>> GetStorages(int offset = 0, int limit = 20);
	}
}
