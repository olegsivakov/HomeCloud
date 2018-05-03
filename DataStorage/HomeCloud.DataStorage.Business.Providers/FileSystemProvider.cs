namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Transactions;

	using HomeCloud.Core;

	using HomeCloud.Data.IO;
	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Provides methods to manage data from file system.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Providers.IFileSystemProvider" />
	public class FileSystemProvider : IFileSystemProvider
	{
		#region Private Members

		/// <summary>
		/// The operation collection
		/// </summary>
		private readonly IFileSystemOperation operation = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemProvider" /> class.
		/// </summary>
		/// <param name="scope">The context scope.</param>
		public FileSystemProvider(IFileSystemContext context)
		{
			this.operation = context;
		}

		#endregion

		#region IFileSystemProvider Implementations

		#region Storage Methods

		/// <summary>
		/// Gets a value indicating whether the specified storage already exists.
		/// </summary>
		/// <param name="storage">The storage.</param>
		/// <returns><c>true</c> if the storage exists. Otherwise <c>false.</c></returns>
		public async Task<bool> StorageExists(Storage storage)
		{
			if (string.IsNullOrWhiteSpace(storage.Path) && string.IsNullOrWhiteSpace(storage.Name))
			{
				throw new ArgumentException("Storage path or name is empty.");
			}

			bool result = !string.IsNullOrWhiteSpace(storage.Path) ? this.operation.DirectoryExists(storage.Path) : this.operation.GetDirectory(storage.Name).Exists;

			return await Task.FromResult(result);
		}

		/// <summary>
		/// Creates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to create.</param>
		/// <returns>The newly created instance of <see cref="Storage" /> type.</returns>
		public async Task<Storage> CreateStorage(Storage storage)
		{
			if (string.IsNullOrWhiteSpace(storage.Name))
			{
				throw new ArgumentException("Storage name is empty.");
			}

			this.ExecuteTransaction(() =>
			{

				DirectoryInfo directory = this.operation.GetDirectory(storage.Name);
				if (!directory.Exists)
				{
					directory = this.operation.CreateDirectory(directory.FullName);
				}

				storage.Path = directory.FullName;
			});

			return await Task.FromResult(storage);
		}

		/// <summary>
		/// Updates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to update.</param>
		/// <returns>The updated instance of <see cref="Storage"/> type.</returns>
		public async Task<Storage> UpdateStorage(Storage storage)
		{
			if (string.IsNullOrWhiteSpace(storage.Path) || string.IsNullOrWhiteSpace(storage.Name))
			{
				throw new ArgumentException("Storage path or name is empty.");
			}

			this.ExecuteTransaction(() =>
			{
				DirectoryInfo directory = this.operation.GetDirectory(storage.Name);
				if (!directory.Exists)
				{
					if (storage.Path != directory.FullName && this.operation.DirectoryExists(storage.Path))
					{
						this.operation.Move(storage.Path, directory.FullName);
					}
					else
					{
						directory = this.operation.CreateDirectory(directory.FullName);
					}
				}

				storage.Path = directory.FullName;
			});

			return await Task.FromResult(storage);
		}

		/// <summary>
		/// Gets the list of storages.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>The list of instances of <see cref="Storage"/> type.</returns>
		public async Task<IPaginable<Storage>> GetStorages(int offset = 0, int limit = 20)
		{
			IEnumerable<DirectoryInfo> result = this.operation.GetDirectories();

			return await Task.FromResult(new PagedList<Storage>(result.Skip(offset).Take(limit).Select(directory => new Storage()
			{
				Path = directory.FullName, Name = directory.Name
			}))
			{
				Offset = offset,
				Limit = limit,
				TotalCount = result.Count()
			});
		}

		/// <summary>
		/// Gets storage by initial instance set.
		/// </summary>
		/// <param name="storage">The initial storage set.</param>
		/// <returns>The instance of <see cref="Storage"/> type.</returns>
		public async Task<Storage> GetStorage(Storage storage)
		{
			if (string.IsNullOrWhiteSpace(storage.Path) && string.IsNullOrWhiteSpace(storage.Name))
			{
				throw new ArgumentException("Storage path or name is empty.");
			}

			DirectoryInfo directory = !string.IsNullOrWhiteSpace(storage.Path) ? new DirectoryInfo(storage.Path) : this.operation.GetDirectory(storage.Name);
			if (directory.Exists)
			{
				storage.Size = this.operation.GetFiles(directory, true).Sum(file => file.Length);
			}

			return await Task.FromResult(storage);
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
			if (string.IsNullOrWhiteSpace(storage.Path) && string.IsNullOrWhiteSpace(storage.Name))
			{
				throw new ArgumentException("Storage path or name is empty.");
			}

			this.ExecuteTransaction(() =>
			{
				DirectoryInfo directory = !string.IsNullOrWhiteSpace(storage.Path) ? new DirectoryInfo(storage.Path) : this.operation.GetDirectory(storage.Name);
				if (directory.Exists)
				{
					this.operation.Delete(directory.FullName);
				}
			});

			return await Task.FromResult(storage);
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
			if (string.IsNullOrWhiteSpace(catalog.Path) && (string.IsNullOrWhiteSpace(catalog.Name) || string.IsNullOrWhiteSpace(catalog.Parent?.Path)))
			{
				throw new ArgumentException("Catalog path, name or parent catalog are empty.");
			}

			bool result = !string.IsNullOrWhiteSpace(catalog.Path) ? this.operation.DirectoryExists(catalog.Path) : this.operation.GetDirectory(catalog.Name, new DirectoryInfo(catalog.Parent?.Path)).Exists;

			return await Task.FromResult(result);
		}

		/// <summary>
		/// Creates the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="Catalog" /> type to create.</param>
		/// <returns>The newly created instance of <see cref="Catalog" /> type.</returns>
		public async Task<Catalog> CreateCatalog(Catalog catalog)
		{
			if (string.IsNullOrWhiteSpace(catalog.Name) || string.IsNullOrWhiteSpace(catalog.Parent?.Path))
			{
				throw new ArgumentException("Catalog name or parent catalog are empty.");
			}

			this.ExecuteTransaction(() =>
			{
				DirectoryInfo directory = this.operation.GetDirectory(catalog.Name, new DirectoryInfo(catalog.Parent.Path));
				if (!directory.Exists)
				{
					directory = Directory.CreateDirectory(directory.FullName);
				}

				catalog.Path = directory.FullName;
			});

			return await Task.FromResult(catalog);
		}

		/// <summary>
		/// Updates the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="Catalog" /> type to update.</param>
		/// <returns>The updated instance of <see cref="Catalog"/> type.</returns>
		public async Task<Catalog> UpdateCatalog(Catalog catalog)
		{
			if (string.IsNullOrWhiteSpace(catalog.Path) || string.IsNullOrWhiteSpace(catalog.Name) || string.IsNullOrWhiteSpace(catalog.Parent?.Path))
			{
				throw new ArgumentException("Catalog path, name or parent catalog are empty.");
			}

			this.ExecuteTransaction(() =>
			{
				DirectoryInfo directory = this.operation.GetDirectory(catalog.Name, new DirectoryInfo(catalog.Parent.Path));
				if (!directory.Exists)
				{
					if (catalog.Path != directory.FullName && this.operation.DirectoryExists(catalog.Path))
					{
						this.operation.Move(catalog.Path, directory.FullName);
					}
					else
					{
						directory = this.operation.CreateDirectory(directory.FullName);
					}
				}

				catalog.Path = directory.FullName;
			});

			return await Task.FromResult(catalog);
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
			IEnumerable<DirectoryInfo> result = this.operation.GetDirectories(new DirectoryInfo(parent.Path));

			return await Task.FromResult(new PagedList<Catalog>(result.Skip(offset).Take(limit).Select(directory => new Catalog()
			{
				Path = directory.FullName,
				Name = directory.Name
			}))
			{
				Offset = offset,
				Limit = limit,
				TotalCount = result.Count()
			});
		}

		/// <summary>
		/// Gets the catalog by the initial instance set.
		/// </summary>
		/// <param name="catalog">The initial catalog set.</param>
		/// <returns>The instance of <see cref="Catalog"/> type.</returns>
		public async Task<Catalog> GetCatalog(Catalog catalog)
		{
			if (string.IsNullOrWhiteSpace(catalog.Path) && (string.IsNullOrWhiteSpace(catalog.Name) || string.IsNullOrWhiteSpace(catalog.Parent?.Path)))
			{
				throw new ArgumentException("Catalog path, name or parent catalog are empty.");
			}

			DirectoryInfo directory = !string.IsNullOrWhiteSpace(catalog.Path) ? new DirectoryInfo(catalog.Path) : this.operation.GetDirectory(catalog.Name, new DirectoryInfo(catalog.Parent.Path));
			if (directory.Exists)
			{
				catalog.Size = this.operation.GetFiles(directory, true).Sum(file => file.Length);
			}

			return await Task.FromResult(catalog);
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
			if (string.IsNullOrWhiteSpace(catalog.Path) && (string.IsNullOrWhiteSpace(catalog.Name) || string.IsNullOrWhiteSpace(catalog.Parent?.Path)))
			{
				throw new ArgumentException("Catalog path, name or parent catalog are empty.");
			}

			this.ExecuteTransaction(() =>
			{
				DirectoryInfo directory = !string.IsNullOrWhiteSpace(catalog.Path) ? new DirectoryInfo(catalog.Path) : this.operation.GetDirectory(catalog.Name, new DirectoryInfo(catalog.Parent.Path));
				if (directory.Exists)
				{
					this.operation.Delete(directory.FullName);
				}
			});

			return await Task.FromResult(catalog);
		}

		/// <summary>
		/// Recalculates the size of the specified catalog.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		/// <returns>The updated instance of <see cref="Catalog"/>.</returns>
		public async Task<Catalog> RecalculateSize(Catalog catalog)
		{
			return await this.GetCatalog(catalog);
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
			if (string.IsNullOrWhiteSpace(entry.Path) && (string.IsNullOrWhiteSpace(entry.Name) || string.IsNullOrWhiteSpace(entry.Catalog?.Path)))
			{
				throw new ArgumentException("File path, name or catalog is empty.");
			}

			bool result = !string.IsNullOrWhiteSpace(entry.Path) ? this.operation.FileExists(entry.Path) : this.operation.GetFile(entry.Name, new DirectoryInfo(entry.Catalog?.Path)).Exists;

			return await Task.FromResult(result);
		}

		/// <summary>
		/// Gets the catalog entry by the initial instance set.
		/// </summary>
		/// <param name="stream">The catalog entry stream to create from.</param>
		/// <returns>The instance of <see cref="CatalogEntry"/> type.</returns>
		public async Task<CatalogEntry> CreateCatalogEntry(CatalogEntryStream stream)
		{
			if (string.IsNullOrWhiteSpace(stream.Entry.Name) || string.IsNullOrWhiteSpace(stream.Entry.Catalog?.Path))
			{
				throw new ArgumentException("File name or catalog is empty.");
			}

			this.ExecuteTransaction(() =>
			{
				DirectoryInfo directory = new DirectoryInfo(stream.Entry.Catalog.Path);
				if (!directory.Exists)
				{
					directory = this.operation.CreateDirectory(directory.FullName);
				}

				FileInfo file = this.operation.GetFile(stream.Entry.Name, directory);
				if (!file.Exists)
				{
					file = this.operation.CreateFile(file.FullName, stream);
				}

				stream.Entry.Path = file.FullName;
				stream.Entry.Size = file.Length;
			});

			return await Task.FromResult(stream.Entry);
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
			IEnumerable<FileInfo> result = this.operation.GetFiles(new DirectoryInfo(catalog.Path));

			return await Task.FromResult(new PagedList<CatalogEntry>(result.Skip(offset).Take(limit).Select(file => new CatalogEntry()
			{
				Name = file.Name,
				Path = file.FullName
			}))
			{
				Offset = offset,
				Limit = limit,
				TotalCount = result.Count()
			});
		}

		/// <summary>
		/// Gets the catalog entry by the initial instance set.
		/// </summary>
		/// <param name="entry">The initial catalog entry set.</param>
		/// <returns>The instance of <see cref="CatalogEntry"/> type.</returns>
		public async Task<CatalogEntry> GetCatalogEntry(CatalogEntry entry)
		{
			if (string.IsNullOrWhiteSpace(entry.Path) && (string.IsNullOrWhiteSpace(entry.Name) || string.IsNullOrWhiteSpace(entry.Catalog?.Path)))
			{
				throw new ArgumentException("File path, name or catalog are empty.");
			}

			FileInfo file = !string.IsNullOrWhiteSpace(entry.Path) ? new FileInfo(entry.Path) : this.operation.GetFile(entry.Name, new DirectoryInfo(entry.Catalog.Path));

			entry.Path = file.FullName;
			entry.Size = file.Length;

			return await Task.FromResult(entry);
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
			if (string.IsNullOrWhiteSpace(entry.Path) && (string.IsNullOrWhiteSpace(entry.Name) || string.IsNullOrWhiteSpace(entry.Catalog?.Path)))
			{
				throw new ArgumentException("File path, name or catalog are empty.");
			}

			FileInfo file = !string.IsNullOrWhiteSpace(entry.Path) ? new FileInfo(entry.Path) : this.operation.GetFile(entry.Name, new DirectoryInfo(entry.Catalog.Path));
			if (!file.Exists)
			{
				return await Task.FromResult(new CatalogEntryStream(entry, 0));
			}

			byte[] buffer = this.operation.ReadBytes(entry.Path, offset, length);

			CatalogEntryStream stream = new CatalogEntryStream(entry, buffer.Length);
			stream.Write(buffer, 0, buffer.Length);

			return await Task.FromResult(stream);
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
			if (string.IsNullOrWhiteSpace(entry.Path) && (string.IsNullOrWhiteSpace(entry.Name) || string.IsNullOrWhiteSpace(entry.Catalog?.Path)))
			{
				throw new ArgumentException("File path, name or catalog are empty.");
			}

			this.ExecuteTransaction(() =>
			{
				FileInfo file = !string.IsNullOrWhiteSpace(entry.Path) ? new FileInfo(entry.Path) : this.operation.GetFile(entry.Name, new DirectoryInfo(entry.Catalog.Path));
				if (file.Exists)
				{
					this.operation.Delete(file.FullName);
				}
			});

			return await Task.FromResult(entry);
		}

		#endregion

		#endregion

		#region Private Methods

		/// <summary>
		/// Executes the specified action as a single transaction.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		private void ExecuteTransaction(Action action)
		{
			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
			{
				action();

				scope.Complete();
			}
		}

		#endregion
	}
}
