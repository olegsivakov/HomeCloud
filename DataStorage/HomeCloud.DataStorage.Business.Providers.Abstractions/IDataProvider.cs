namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Defines methods to provide data.
	/// </summary>
	public interface IDataProvider
	{
		/// <summary>
		/// Creates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to create.</param>
		/// <returns>The newly created instance of <see cref="Storage" /> type.</returns>
		Task<Storage> CreateStorage(Storage storage);

		/// <summary>
		/// Updates the specified storage.
		/// </summary>
		/// <param name="storage">The storage.</param>
		/// <returns>The updated instance of <see cref="Storage"/> type.</returns>
		Task<Storage> UpdateStorage(Storage storage);

		/// <summary>
		/// Gets the list of storages.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>The list of instances of <see cref="Storage"/> type.</returns>
		Task<IEnumerable<Storage>> GetStorages(int offset = 0, int limit = 20);

		/// <summary>
		/// Gets storage by specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>the instance of <see cref="Storage"/>.</returns>
		Task<Storage> GetStorage(Guid id);

		/// <summary>
		/// Gets the catalog by the initial instance set.
		/// </summary>
		/// <param name="catalog">The initial catalog set.</param>
		/// <returns>The instance of <see cref="Catalog"/>.</returns>
		Task<Catalog> GetCatalog(Catalog catalog);
	}
}
