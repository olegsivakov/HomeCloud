namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Threading.Tasks;

	using HomeCloud.DataStorage.Api.Configuration;
	using HomeCloud.DataStorage.Business.Entities;

	using Microsoft.Extensions.Options;

	#endregion

	/// <summary>
	/// Provides methods to manage data from file system.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Providers.IFileSystemProvider" />
	public class FileSystemProvider : IFileSystemProvider
	{
		#region Private Members

		/// <summary>
		/// The file system settings
		/// </summary>
		private readonly FileSystem fileSystemSettings = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemProvider" /> class.
		/// </summary>
		/// <param name="fileSystemSettings">The file system settings.</param>
		public FileSystemProvider(IOptionsSnapshot<FileSystem> fileSystemSettings)
		{
			this.fileSystemSettings = fileSystemSettings?.Value;
		}

		#endregion

		#region IFileSystemProvider Implementations

		/// <summary>
		/// Generates the absolute path to the catalog of the specified storage.
		/// </summary>
		/// <param name="storage">The storage.</param>
		/// <returns>The absolute path to the storage catalog.</returns>
		public async Task<string> GeneratePath(Storage storage)
		{
			return await Task.Run(() =>
			{
				if (string.IsNullOrWhiteSpace(this.fileSystemSettings.StorageRootPath))
				{
					throw new ArgumentException("The root path to the storages is not configured.");
				}

				if (string.IsNullOrWhiteSpace(storage.Name))
				{
					throw new ArgumentException("The storage catalog name is empty");
				}

				return Path.Combine(this.fileSystemSettings.StorageRootPath, storage.Name);
			});
		}

		/// <summary>
		/// Generates the absolute path to the specified catalog.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		/// <returns>The absolute path to the storage catalog.</returns>
		public async Task<string> GeneratePath(Catalog catalog)
		{
			return await Task.Run(() =>
			{
				if (string.IsNullOrWhiteSpace(catalog.Parent?.Path))
				{
					throw new ArgumentException("The parent catalog path is empty");
				}

				if (!Directory.Exists(catalog.Parent.Path))
				{
					throw new DirectoryNotFoundException("The catalog doesn't exist by specified path.");
				}

				if (string.IsNullOrWhiteSpace(catalog.Name))
				{
					throw new ArgumentException("The catalog name is empty");
				}

				return Path.Combine(catalog.Parent.Path, catalog.Name);
			});
		}

		#endregion

		#region IDataStoreProvider Implementations

		/// <summary>
		/// Gets a value indicating whether the specified storage already exists.
		/// </summary>
		/// <param name="storage">The storage.</param>
		/// <returns><c>true</c> if the storage exists. Otherwise <c>false.</c></returns>
		public async Task<bool> StorageExists(Storage storage)
		{
			this.CheckStorageRoot();

			string path = !string.IsNullOrWhiteSpace(storage.Path) ? storage.Path : await this.GeneratePath(storage);

			return Directory.Exists(path);
		}

		/// <summary>
		/// Creates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to create.</param>
		/// <returns>The newly created instance of <see cref="Storage" /> type.</returns>
		public async Task<Storage> CreateStorage(Storage storage)
		{
			return await Task.Run(() =>
			{
				if (string.IsNullOrWhiteSpace(storage.CatalogRoot.Path) && string.IsNullOrWhiteSpace(storage.CatalogRoot.Name))
				{
					return storage;
				}

				storage.CatalogRoot.Path = storage.CatalogRoot.Path ?? Path.Combine(this.fileSystemSettings.StorageRootPath, storage.CatalogRoot.Name);
				storage.CatalogRoot.Path = Directory.CreateDirectory(storage.CatalogRoot.Path).FullName;

				return storage;
			});
		}

		/// <summary>
		/// Updates the specified storage.
		/// </summary>
		/// <param name="storage">The storage.</param>
		/// <returns>The updated instance of <see cref="Storage"/> type.</returns>
		public async Task<Storage> UpdateStorage(Storage storage)
		{
			return await Task.Run(() =>
			{
				if (string.IsNullOrWhiteSpace(storage.CatalogRoot.Path) && string.IsNullOrWhiteSpace(storage.CatalogRoot.Name))
				{
					return storage;
				}

				storage.CatalogRoot.Path = storage.CatalogRoot.Path ?? Path.Combine(this.fileSystemSettings.StorageRootPath, storage.CatalogRoot.Name);

				if (!Directory.Exists(storage.CatalogRoot.Path))
				{
					storage.CatalogRoot.Path = Directory.CreateDirectory(storage.CatalogRoot.Path).FullName;
				}

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
		/// Gets storage by the initial instance set.
		/// </summary>
		/// <param name="storage">The initial storage stet.</param>
		/// <returns>the instance of <see cref="Storage"/>.</returns>
		public async Task<Storage> GetStorage(Storage storage)
		{
			return await Task.FromException<Storage>(new NotSupportedException());
		}

		/// <summary>
		/// Gets a value indicating whether the specified catalog already exists.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		/// <returns><c>true</c> if the catalog exists. Otherwise <c>false.</c></returns>
		public async Task<bool> CatalogExists(Catalog catalog)
		{
			string path = !string.IsNullOrWhiteSpace(catalog.Path) ? catalog.Path : await this.GeneratePath(catalog);

			return Directory.Exists(path);
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
				if (string.IsNullOrWhiteSpace(catalog.Path))
				{
					return catalog;
				}

				if (Directory.Exists(catalog.Path))
				{
					catalog.Size = Directory.GetFiles(catalog.Path, "*", SearchOption.AllDirectories).Select(filePath => new FileInfo(filePath)).Sum(file => file.Length);
				}

				return catalog;
			});
		}

		/// <summary>
		/// Deletes the specified storage.
		/// </summary>
		/// <param name="storage">The storage.</param>
		/// <returns>
		/// The deleted instance of <see cref="Storage"/>.
		/// </returns>
		public async Task<Storage> DeleteStorage(Storage storage)
		{
			await this.DeleteCatalog(storage.CatalogRoot);

			return storage;
		}

		/// <summary>
		/// Deletes the specified catalog.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		/// <returns>
		/// The deleted instance of <see cref="Catalog"/>.
		/// </returns>
		public async Task<Catalog> DeleteCatalog(Catalog catalog)
		{
			return await Task.Run(() =>
			{
				if (string.IsNullOrWhiteSpace(catalog.Path) && string.IsNullOrWhiteSpace(catalog.Name))
				{
					return catalog;
				}

				catalog.Path = catalog.Path ?? Path.Combine(this.fileSystemSettings.StorageRootPath, catalog.Name);

				if (Directory.Exists(catalog.Path))
				{
					Directory.Delete(catalog.Path, true);
				}

				return catalog;
			});
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Checks <see cref="FileSystem.StorageRootPath"/> value is not empty.
		/// </summary>
		/// <exception cref="ArgumentException">The root path to the storages is not configured.</exception>
		private void CheckStorageRoot()
		{
			if (string.IsNullOrWhiteSpace(this.fileSystemSettings.StorageRootPath) || !Directory.Exists(this.fileSystemSettings.StorageRootPath))
			{
				throw new ArgumentException("The root path for the storages is not configured or does not exist.");
			}
		}

		#endregion
	}
}
