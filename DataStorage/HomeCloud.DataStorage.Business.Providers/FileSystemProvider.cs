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
		#region Private Members

		/// <summary>
		/// The factory of catalog validators.
		/// </summary>
		private readonly IServiceFactory<ICatalogValidator> catalogValidatorFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemProvider" /> class.
		/// </summary>
		/// <param name="fileSystemSettings">The file system settings.</param>
		public FileSystemProvider(IServiceFactory<ICatalogValidator> catalogValidatorFactory)
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
			ValidationResult validationResult = this.catalogValidatorFactory.Get<ICatalogRequiredValidator>().Validate(storage.CatalogRoot);
			if (!validationResult.IsValid)
			{
				throw new ValidationException(validationResult.Errors);
			}

			storage.CatalogRoot.Path = (!Directory.Exists(storage.CatalogRoot.Path) ? Directory.CreateDirectory(storage.CatalogRoot.Path) : new DirectoryInfo(storage.CatalogRoot.Path))?.FullName;

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
