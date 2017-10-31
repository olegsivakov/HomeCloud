namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using System.IO;
	using System.Linq;

	using HomeCloud.DataStorage.Api.Configuration;
	using HomeCloud.DataStorage.Business.Entities;

	using Microsoft.Extensions.Options;

	#endregion

	/// <summary>
	///  Provides methods to manage data from file system.
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

		#region IDataStoreProvider Implementations

		/// <summary>
		/// Creates the storage.
		/// </summary>
		/// <param name="storage">The storage.</param>
		public void CreateStorage(Storage storage)
		{
			string path = Path.Combine(this.fileSystemSettings.StoragePath, storage.CatalogRoot.Name);
			if (Directory.Exists(path))
			{
				DirectoryInfo result = Directory.CreateDirectory(path);

				storage.CatalogRoot.Path = result.FullName;
				storage.CatalogRoot.Size = result.EnumerateFiles(null, SearchOption.AllDirectories).Sum(file => (long)file.Length);
			}
		}

		public void DeleteStorage(Storage storage)
		{
		}

		public void SetStorageQuota(Storage storage, long quota)
		{
		}

		#endregion
	}
}
