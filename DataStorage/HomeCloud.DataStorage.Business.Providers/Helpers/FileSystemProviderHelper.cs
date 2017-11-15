namespace HomeCloud.DataStorage.Business.Providers.Helpers
{
	#region Usings

	using System;
	using System.IO;

	using HomeCloud.DataStorage.Api.Configuration;

	using HomeCloud.DataStorage.Business.Entities;

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
			ValidateStorageRootSetting(settings);

			if (string.IsNullOrWhiteSpace(storage.Path) && string.IsNullOrWhiteSpace(storage.Name))
			{
				throw new ArgumentException("Storage path or name is empty.");
			}
		}

		/// <summary>
		/// Validates <see cref="Catalog.Parent.Path" /> value not to be empty.
		/// </summary>
		/// <param name="catalog">The catalog which parent catalog should be validated for.</param>
		/// <exception cref="ArgumentException">The parent catalog path is empty.</exception>
		/// <exception cref="DirectoryNotFoundException">The parent catalog does not exist by specified path.</exception>
		public static void ValidateParentCatalogPath(this Catalog catalog)
		{
			if (string.IsNullOrWhiteSpace(catalog.Parent?.Path))
			{
				throw new ArgumentException("The parent catalog path is empty.");
			}

			if (!Directory.Exists(catalog.Parent.Path))
			{
				throw new DirectoryNotFoundException("The parent catalog does not exist by specified path.");
			}
		}

		/// <summary>
		/// Validates catalog <see cref="CatalogRoot.Path" /> and <see cref="CatalogRoot.Name" /> properties values not to be empty either.
		/// </summary>
		/// <param name="catalog">The catalog to validate.</param>
		/// <exception cref="ArgumentException">Catalog path or name is empty.</exception>
		/// <exception cref="DirectoryNotFoundException">The parent catalog does not exist by specified path.</exception>
		public static void ValidateCatalogPath(this Catalog catalog)
		{
			ValidateParentCatalogPath(catalog);

			if (string.IsNullOrWhiteSpace(catalog.Path) && string.IsNullOrWhiteSpace(catalog.Name))
			{
				throw new ArgumentException("Catalog path or name is empty.");
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
		/// <returns>The absolute path to the storage catalog.</returns>
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
	}
}
