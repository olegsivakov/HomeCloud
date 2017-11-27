namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using System;
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
		/// <returns>
		/// The operation result containing created instance of <see cref="Storage"/>.
		/// </returns>
		Task<ServiceResult<Storage>> CreateStorageAsync(Storage storage);

		/// <summary>
		/// Updates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type.</param>
		/// <returns>
		/// The operation result containing updated instance of <see cref="Storage"/>.
		/// </returns>
		Task<ServiceResult<Storage>> UpdateStorageAsync(Storage storage);

		/// <summary>
		/// Gets the list of storages.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>The operation result containing the list of instances of <see cref="Storage"/>.</returns>
		Task<ServicePagedResult<Storage>> GetStoragesAsync(int offset = 0, int limit = 20);

		/// <summary>
		/// Gets the storage by specified identifier.
		/// </summary>
		/// <param name="id">The storage identifier.</param>
		/// <returns>
		/// The operation result containing the instance of <see cref="Storage" />.
		/// </returns>
		Task<ServiceResult<Storage>> GetStorageAsync(Guid id);

		/// <summary>
		/// Deletes the storage by specified identifier.
		/// </summary>
		/// <param name="id">The storage identifier.</param>
		/// <returns>The operation result.</returns>
		Task<ServiceResult> DeleteStorageAsync(Guid id);
	}
}
