namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using System.Threading.Tasks;

	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Marks the data method definitions to provide data from file system.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Providers.IDataProvider" />
	public interface IFileSystemProvider : IDataProvider
	{
		/// <summary>
		/// Generates the absolute path to the catalog of the specified storage.
		/// </summary>
		/// <param name="storage">The storage.</param>
		/// <returns>The absolute path to the storage catalog.</returns>
		Task<string> GeneratePath(Storage storage);

		/// <summary>
		/// Generates the absolute path to the specified catalog.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		/// <returns>The absolute path to the storage catalog.</returns>
		Task<string> GeneratePath(Catalog catalog);
	}
}
