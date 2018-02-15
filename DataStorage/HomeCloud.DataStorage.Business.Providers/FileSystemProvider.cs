namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using System;
	using System.IO;
	using System.Linq;
	using System.Threading.Tasks;

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Providers.Helpers;
	using HomeCloud.DataStorage.Configuration;

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
		/// The file access synchronization object
		/// </summary>
		private static readonly object FileAccessSyncObject = new object();

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

		#region IDataStoreProvider Implementations

		#region Storage Methods

		/// <summary>
		/// Gets a value indicating whether the specified storage already exists.
		/// </summary>
		/// <param name="storage">The storage.</param>
		/// <returns><c>true</c> if the storage exists. Otherwise <c>false.</c></returns>
		public async Task<bool> StorageExists(Storage storage)
		{
			return await Task.Run(() =>
			{
				storage.ValidateStoragePath(this.fileSystemSettings);

				string path = storage.GeneratePath(this.fileSystemSettings);

				return Directory.Exists(path);
			});
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
				storage.ValidateStoragePath(this.fileSystemSettings);

				storage.Path = storage.GeneratePath(this.fileSystemSettings, true);
				if (!Directory.Exists(storage.Path))
				{
					storage.Path = Directory.CreateDirectory(storage.Path).FullName;
				}

				return storage;
			});
		}

		/// <summary>
		/// Updates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to update.</param>
		/// <returns>The updated instance of <see cref="Storage"/> type.</returns>
		public async Task<Storage> UpdateStorage(Storage storage)
		{
			return await Task.Run(() =>
			{
				storage.ValidateStoragePath(this.fileSystemSettings);

				storage.Path = storage.GeneratePath(this.fileSystemSettings);
				if (!Directory.Exists(storage.Path))
				{
					storage.Path = Directory.CreateDirectory(storage.Path).FullName;
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
		public async Task<IPaginable<Storage>> GetStorages(int offset = 0, int limit = 20)
		{
			return await Task.FromException<IPaginable<Storage>>(new NotSupportedException());
		}

		/// <summary>
		/// Gets storage by initial instance set.
		/// </summary>
		/// <param name="storage">The initial storage set.</param>
		/// <returns>The instance of <see cref="Storage"/> type.</returns>
		public async Task<Storage> GetStorage(Storage storage)
		{
			return await Task.Run(() =>
			{
				storage.ValidateStoragePath(this.fileSystemSettings);

				storage.Path = storage.GeneratePath(this.fileSystemSettings);
				if (Directory.Exists(storage.Path))
				{
					storage.Size = Directory.GetFiles(storage.Path, "*", SearchOption.AllDirectories).Select(filePath => new FileInfo(filePath)).Sum(file => file.Length);
				}

				return storage;
			});
		}

		/// <summary>
		/// Deletes the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to delete.</param>
		/// <returns>
		/// The deleted instance of <see cref="Storage"/> type.
		/// </returns>
		public async Task<Storage> DeleteStorage(Storage storage)
		{
			return await Task.Run(() =>
			{
				storage.ValidateStoragePath(this.fileSystemSettings);

				storage.Path = storage.GeneratePath(this.fileSystemSettings);
				if (Directory.Exists(storage.Path))
				{
					Directory.Delete(storage.Path, true);
				}

				return storage;
			});
		}

		#endregion

		#region Catalog Methods

		/// <summary>
		/// Gets a value indicating whether the specified catalog already exists.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		/// <returns><c>true</c> if the catalog exists. Otherwise <c>false.</c></returns>
		public async Task<bool> CatalogExists(Catalog catalog)
		{
			return await Task.Run(() =>
			{
				catalog.ValidatePath();

				string path = catalog.GeneratePath();

				return Directory.Exists(path);
			});
		}

		/// <summary>
		/// Creates the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="Catalog" /> type to create.</param>
		/// <returns>The newly created instance of <see cref="Catalog" /> type.</returns>
		public async Task<Catalog> CreateCatalog(Catalog catalog)
		{
			return await Task.Run(() =>
			{
				catalog.ValidatePath();

				catalog.Path = catalog.GeneratePath(true);
				if (!Directory.Exists(catalog.Path))
				{
					catalog.Path = Directory.CreateDirectory(catalog.Path).FullName;
				}

				return catalog;
			});
		}

		/// <summary>
		/// Updates the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="Catalog" /> type to update.</param>
		/// <returns>The updated instance of <see cref="Catalog"/> type.</returns>
		public async Task<Catalog> UpdateCatalog(Catalog catalog)
		{
			return await Task.Run(() =>
			{
				catalog.ValidatePath();

				string path = catalog.GeneratePath(true);
				if (!Directory.Exists(path))
				{
					if (catalog.Path != path && Directory.Exists(catalog.Path))
					{
						Directory.Move(catalog.Path, path);
					}
					else
					{
						catalog.Path = Directory.CreateDirectory(path).FullName;
					}
				}

				catalog.Path = path;

				return catalog;
			});
		}

		/// <summary>
		/// Gets the list of catalogs located in specified parent catalog.
		/// </summary>
		/// <param name="parent">The parent catalog of <see cref="CatalogRoot"/> type.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The list of instances of <see cref="Catalog" /> type.
		/// </returns>
		public async Task<IPaginable<Catalog>> GetCatalogs(CatalogRoot parent, int offset = 0, int limit = 20)
		{
			return await Task.FromException<IPaginable<Catalog>>(new NotSupportedException());
		}

		/// <summary>
		/// Gets the catalog by the initial instance set.
		/// </summary>
		/// <param name="catalog">The initial catalog set.</param>
		/// <returns>The instance of <see cref="Catalog"/> type.</returns>
		public async Task<Catalog> GetCatalog(Catalog catalog)
		{
			return await Task.Run(() =>
			{
				catalog.ValidatePath();

				catalog.Path = catalog.GeneratePath();
				if (Directory.Exists(catalog.Path))
				{
					catalog.Size = Directory.GetFiles(catalog.Path, "*", SearchOption.AllDirectories).Select(filePath => new FileInfo(filePath)).Sum(file => file.Length);
				}

				return catalog;
			});
		}

		/// <summary>
		/// Deletes the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="Catalog" /> type to delete.</param>
		/// <returns>
		/// The deleted instance of <see cref="Catalog"/> type.
		/// </returns>
		public async Task<Catalog> DeleteCatalog(Catalog catalog)
		{
			return await Task.Run(() =>
			{
				catalog.ValidatePath();

				catalog.Path = catalog.GeneratePath();
				if (Directory.Exists(catalog.Path))
				{
					Directory.Delete(catalog.Path, true);
				}

				return catalog;
			});
		}

		#endregion

		#region CatalogEntry Methods

		/// <summary>
		/// Gets a value indicating whether the specified catalog entry already exists.
		/// </summary>
		/// <param name="entry">The catalog entry.</param>
		/// <returns><c>true</c> if the catalog entry exists. Otherwise <c>false.</c></returns>
		public async Task<bool> CatalogEntryExists(CatalogEntry entry)
		{
			return await Task.Run(() =>
			{
				entry.ValidatePath();

				string path = entry.GeneratePath();

				return File.Exists(path);
			});
		}

		/// <summary>
		/// Gets the catalog entry by the initial instance set.
		/// </summary>
		/// <param name="stream">The catalog entry stream to create from.</param>
		/// <returns>The instance of <see cref="CatalogEntry"/> type.</returns>
		public async Task<CatalogEntry> CreateCatalogEntry(CatalogEntryStream stream)
		{
			return await Task.Run(() =>
			{
				stream.Entry.ValidatePath();

				stream.Entry.Path = stream.Entry.GeneratePath(true);
				if (!File.Exists(stream.Entry.Path))
				{
					lock (FileAccessSyncObject)
					{
						if (!File.Exists(stream.Entry.Path))
						{
							using (FileStream fileStream = File.Create(stream.Entry.Path, 1024, FileOptions.WriteThrough))
							{
								stream.CopyTo(fileStream);
							}
						}
					}
				}

				stream.Entry.Size = new FileInfo(stream.Entry.Path).Length;

				return stream.Entry;
			});
		}

		/// <summary>
		/// Gets the list of catalog entries located in specified catalog.
		/// </summary>
		/// <param name="catalog">The catalog of <see cref="CatalogRoot"/> type.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The list of instances of <see cref="CatalogEntry" /> type.
		/// </returns>
		public async Task<IPaginable<CatalogEntry>> GetCatalogEntries(CatalogRoot catalog, int offset = 0, int limit = 20)
		{
			return await Task.FromException<IPaginable<CatalogEntry>>(new NotSupportedException());
		}

		/// <summary>
		/// Gets the catalog entry by the initial instance set.
		/// </summary>
		/// <param name="entry">The initial catalog entry set.</param>
		/// <returns>The instance of <see cref="CatalogEntry"/> type.</returns>
		public async Task<CatalogEntry> GetCatalogEntry(CatalogEntry entry)
		{
			return await Task.Run(() =>
			{
				entry.ValidatePath();

				entry.Path = entry.GeneratePath();
				if (File.Exists(entry.Path))
				{
					entry.Size = new FileInfo(entry.Path).Length;
				}

				return entry;
			});
		}

		/// <summary>
		/// Gets the catalog entry by the initial instance set.
		/// </summary>
		/// <param name="entry">The initial catalog entry set.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="length">The number of bytes from byte array to return.</param>
		/// <returns>
		/// The instance of <see cref="CatalogEntry" /> type.
		/// </returns>
		public async Task<CatalogEntryStream> GetCatalogEntryStream(CatalogEntry entry, int offset = 0, int length = 0)
		{
			entry = await this.GetCatalogEntry(entry);

			if (File.Exists(entry.Path))
			{
				lock (FileAccessSyncObject)
				{
					if (File.Exists(entry.Path))
					{
						using (FileStream stream = new FileStream(entry.Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
						{
							long count = length == 0 ? stream.Length : length;

							byte[] buffer = new byte[count];
							stream.Read(buffer, offset, (int)count);

							CatalogEntryStream result = new CatalogEntryStream(entry, count);

							result.Write(buffer, 0, (int)count);
						}
					}
				}
			}

			return new CatalogEntryStream(entry, 0);
		}

		/// <summary>
		/// Deletes the specified catalog entry.
		/// </summary>
		/// <param name="entry">The instance of <see cref="CatalogEntry" /> type to delete.</param>
		/// <returns>
		/// The deleted instance of <see cref="CatalogEntry"/> type.
		/// </returns>
		public async Task<CatalogEntry> DeleteCatalogEntry(CatalogEntry entry)
		{
			return await Task.Run(() =>
			{
				entry.ValidatePath();

				entry.Path = entry.GeneratePath();
				if (File.Exists(entry.Path))
				{
					lock (FileAccessSyncObject)
					{
						if (File.Exists(entry.Path))
						{
							File.Delete(entry.Path);
						}
					}
				}

				return entry;
			});
		}

		#endregion

		#endregion
	}
}
