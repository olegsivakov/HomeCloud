namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using System.IO;
	using System.Linq;

	using HomeCloud.DataStorage.Api.Configuration;
	using HomeCloud.DataStorage.Business.Entities;

	using Microsoft.Extensions.Options;
	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Validation.Abstractions;
	using HomeCloud.Validation;
	using HomeCloud.Exceptions;

	#endregion

	/// <summary>
	///  Provides methods to manage data from file system.
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
			this.catalogValidatorFactory = catalogValidatorFactory;
		}

		#endregion

		#region IDataStoreProvider Implementations

		/// <summary>
		/// Creates the storage.
		/// </summary>
		/// <param name="storage">The storage.</param>
		public Storage CreateStorage(Storage storage)
		{
			storage.CatalogRoot.Path = Directory.CreateDirectory(storage.CatalogRoot.Path).FullName;

			return storage;
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
