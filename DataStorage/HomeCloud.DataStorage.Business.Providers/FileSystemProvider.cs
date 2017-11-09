namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Threading.Tasks;

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
		public async Task<Storage> CreateStorage(Storage storage)
		{
			return await Task.Run(() =>
			{
				storage.CatalogRoot.Path = Directory.CreateDirectory(storage.CatalogRoot.Path).FullName;

				return storage;
			});
		}

		/// <summary>
		/// Gets the list of storages.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>The list of instances of <see cref="Storage"/> type.</returns>
		public async Task<IEnumerable<Storage>> GetStorages(int offset = 0, int limit = 20)
		{
			return await Task.FromException<IEnumerable<Storage>>(new NotSupportedException());
		}

		/// <summary>
		/// Gets storage by specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>the instance of <see cref="Storage"/>.</returns>
		public async Task<Storage> GetStorage(Guid id)
		{
			return await Task.FromException<Storage>(new NotSupportedException());
		}

		/// <summary>
		/// Gets the catalog by the initial instance set.
		/// </summary>
		/// <param name="catalog">The initial catalog set.</param>
		/// <returns>The instance of <see cref="Catalog"/>.</returns>
		public async Task<Catalog> GetCatalog(Catalog catalog)
		{
			return await Task.Run(() =>
			{
				if (Directory.Exists(catalog.Path))
				{
					catalog.Size = Directory.GetFiles(catalog.Path, "*", SearchOption.AllDirectories).Select(filePath => new FileInfo(filePath)).Sum(file => file.Length);
				}

				return catalog;
			});
		}

		#endregion
	}
}
