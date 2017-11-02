namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using System.IO;

	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Provides methods to manage data from file system.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Providers.IFileSystemProvider" />
	public class FileSystemProvider : IFileSystemProvider
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemProvider" /> class.
		/// </summary>
		public FileSystemProvider()
		{
		}

		#endregion

		#region IDataStoreProvider Implementations

		/// <summary>
		/// Creates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to create.</param>
		/// <returns>The newly created instance of <see cref="Storage" /> type.</returns>
		public Storage CreateStorage(Storage storage)
		{
			storage.CatalogRoot.Path = Directory.CreateDirectory(storage.CatalogRoot.Path).FullName;

			return storage;
		}

		#endregion
	}
}
