namespace HomeCloud.DataStorage.Business.Providers.Helpers
{
	#region Usings

	using System;
	using System.IO;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Configuration;

	#endregion

	/// <summary>
	/// Provides helper methods to implement <see cref="IFileSystemProvider"/>.
	/// </summary>
	internal static class FileSystemProviderHelper
	{
		/// <summary>
		/// Validates <see cref="FileSystem.StorageRootPath" /> settings value not to be empty.
		/// </summary>
		/// <param name="settings">The settings.</param>
		/// <exception cref="ArgumentException">The root path to the storages is not configured or does not exist.</exception>
		/// <exception cref="DirectoryNotFoundException">The root path for storages does not exist.</exception>
		public static void ValidateStorageRootSetting(this FileSystem settings)
		{
			if (string.IsNullOrWhiteSpace(settings.StorageRootPath))
			{
				throw new ArgumentException("The root path for storages is not configured.");
			}

			if (!Directory.Exists(settings.StorageRootPath))
			{
				throw new DirectoryNotFoundException("The root path for storages does not exist.");
			}
		}

		/// <summary>
		/// Validates storage <see cref="CatalogRoot.Path" /> and <see cref="CatalogRoot.Name" /> properties values not to be empty either.
		/// </summary>
		/// <param name="storage">The storage to validate.</param>
		/// <param name="settings">The settings.</param>
		/// <exception cref="ArgumentException">Storage path or name is empty.</exception>
		public static void ValidateStoragePath(this Storage storage, FileSystem settings)
		{
			if (string.IsNullOrWhiteSpace(storage.Path))
			{
				ValidateStorageRootSetting(settings);
			}

			if (string.IsNullOrWhiteSpace(storage.Path) && string.IsNullOrWhiteSpace(storage.Name))
			{
				throw new ArgumentException("Storage path or name is empty.");
			}
		}

		/// <summary>
		/// Validates catalog <see cref="CatalogRoot.Path" /> and <see cref="CatalogRoot.Name" /> properties values not to be empty either.
		/// </summary>
		/// <param name="catalog">The catalog to validate.</param>
		/// <exception cref="ArgumentException">Catalog path or name is empty.</exception>
		public static void ValidatePath(this Catalog catalog)
		{
			if (string.IsNullOrWhiteSpace(catalog.Path))
			{
				ValidateParentPath(catalog.Parent);
			}

			if (string.IsNullOrWhiteSpace(catalog.Path) && string.IsNullOrWhiteSpace(catalog.Name))
			{
				throw new ArgumentException("Catalog entry path or name is empty.");
			}
		}

		/// <summary>
		/// Validates catalog entry <see cref="CatalogEntry.Path" /> and <see cref="CatalogEntry.Name" /> properties values not to be empty either.
		/// </summary>
		/// <param name="entry">The catalog entry to validate.</param>
		/// <exception cref="ArgumentException">Catalog entry path or name is empty.</exception>
		public static void ValidatePath(this CatalogEntry entry)
		{
			if (string.IsNullOrWhiteSpace(entry.Path))
			{
				ValidateParentPath(entry.Catalog);
			}

			if (string.IsNullOrWhiteSpace(entry.Path) && string.IsNullOrWhiteSpace(entry.Name))
			{
				throw new ArgumentException("Catalog entry path or name is empty.");
			}
		}

		/// <summary>
		/// Returns <see cref="CatalogRoot.Path" /> in case if it's not empty or forcibly generates the absolute path to the catalog of the specified storage.
		/// </summary>
		/// <param name="storage">The storage.</param>
		/// <param name="settings">The settings.</param>
		/// <param name="force">A value indicating whether the absolute path will be generated unless the <see cref="CatalogRoot.Path"/> is not empty.</param>
		/// <returns>
		/// The absolute path to the storage catalog.
		/// </returns>
		/// <exception cref="ArgumentException">The storage catalog name is empty</exception>
		public static string GeneratePath(this Storage storage, FileSystem settings, bool force = false)
		{
			if (!force && !string.IsNullOrWhiteSpace(storage.Path))
			{
				return storage.Path;
			}

			if (string.IsNullOrWhiteSpace(storage.Name))
			{
				throw new ArgumentException("The storage catalog name is empty");
			}

			return Path.Combine(settings.StorageRootPath, storage.Name);
		}

		/// <summary>
		/// Returns <see cref="CatalogRoot.Path" /> in case if it's not empty or forcibly generates the absolute path to the specified catalog.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		/// <param name="force">A value indicating whether the absolute path will be generated unless the <see cref="CatalogRoot.Path"/> is not empty.</param>
		/// <returns>The absolute path to the catalog.</returns>
		public static string GeneratePath(this Catalog catalog, bool force = false)
		{
			if (!force && !string.IsNullOrWhiteSpace(catalog.Path))
			{
				return catalog.Path;
			}

			if (string.IsNullOrWhiteSpace(catalog.Name))
			{
				throw new ArgumentException("The catalog name is empty");
			}

			return Path.Combine(catalog.Parent.Path, catalog.Name);
		}

		/// <summary>
		/// Returns <see cref="CatalogEntry.Path" /> in case if it's not empty or forcibly generates the absolute path to the specified catalog entry.
		/// </summary>
		/// <param name="entry">The catalog entry.</param>
		/// <param name="force">A value indicating whether the absolute path will be generated unless the <see cref="CatalogEntry.Path"/> is not empty.</param>
		/// <returns>The absolute path to the catalog entry.</returns>
		public static string GeneratePath(this CatalogEntry entry, bool force = false)
		{
			if (!force && !string.IsNullOrWhiteSpace(entry.Path))
			{
				return entry.Path;
			}

			if (string.IsNullOrWhiteSpace(entry.Name))
			{
				throw new ArgumentException("The catalog entry name is empty");
			}

			return Path.Combine(entry.Catalog.Path, entry.Name);
		}

		/// <summary>
		/// Validates <see cref="Catalog.Parent.Path" /> or <see cref="CatalogEntry.Catalog.Path"/> value not to be empty.
		/// </summary>
		/// <param name="parent">The parent catalog which path should be validated.</param>
		/// <exception cref="ArgumentException">The parent catalog path is empty.</exception>
		/// <exception cref="DirectoryNotFoundException">The parent catalog does not exist by specified path.</exception>
		private static void ValidateParentPath(CatalogRoot parent)
		{
			if (string.IsNullOrWhiteSpace(parent?.Path))
			{
				throw new ArgumentException("The parent catalog path is empty.");
			}

			if (!Directory.Exists(parent.Path))
			{
				throw new DirectoryNotFoundException("The parent catalog does not exist by specified path.");
			}
		}
	}
}
