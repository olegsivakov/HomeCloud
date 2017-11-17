namespace HomeCloud.DataStorage.Business.Validation
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Providers;

	using HomeCloud.Exceptions;
	using HomeCloud.Validation;

	#endregion

	/// <summary>
	/// Provides methods to validate whether the specified instance already exists.
	/// </summary>
	/// <seealso cref="HomeCloud.Validation.Validator{System.Guid}" />
	/// <seealso cref="HomeCloud.DataStorage.Business.Validation.IPresenceValidator" />
	public class PresenceValidator : Validator<Guid>, IPresenceValidator
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IDataProvider"/> factory.
		/// </summary>
		private readonly IServiceFactory<IDataProvider> dataProviderFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PresenceValidator" /> class.
		/// </summary>
		/// <param name="dataProviderFactory">The <see cref="IDataProvider"/> factory.</param>
		public PresenceValidator(IServiceFactory<IDataProvider> dataProviderFactory)
			: base()
		{
			this.dataProviderFactory = dataProviderFactory;

			this.If(id => id == Guid.Empty).AddError("The unique identifier is empty.");
		}

		#endregion

		#region IPresenceValidator Implementations

		/// <summary>
		/// Validates the specified instance of <see cref="Storage"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(Storage instance)
		{
			this.If(async id => !await this.dataProviderFactory.Get<IDataStoreProvider>().StorageExists(instance)).AddError(new NotFoundException("Specified storage does not exist."));
			this.If(async id =>
			{
				Storage storage = instance.Clone() as Storage;

				if (await this.dataProviderFactory.Get<IDataStoreProvider>().StorageExists(instance))
				{
					storage = await this.dataProviderFactory.Get<IDataStoreProvider>().GetStorage(storage);
					storage = await this.dataProviderFactory.Get<IAggregationDataProvider>().GetStorage(storage);

					return !await this.dataProviderFactory.Get<IFileSystemProvider>().StorageExists(storage);
				}

				return false;
			}).AddError(new NotFoundException("Storage with specified name does not exist."));

			return await this.ValidateAsync(instance.ID);
		}

		/// <summary>
		/// Validates the specified instance of <see cref="Catalog"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(Catalog instance)
		{
			this.If(async id => !await this.dataProviderFactory.Get<IDataStoreProvider>().CatalogExists(instance)).AddError(new NotFoundException("Specified catalog does not exist."));
			this.If(async id =>
			{
				Catalog catalog = instance.Clone() as Catalog;

				catalog.Parent = await this.dataProviderFactory.Get<IDataStoreProvider>().GetCatalog(instance.Parent as Catalog);
				catalog.Parent = await this.dataProviderFactory.Get<IAggregationDataProvider>().GetCatalog(instance.Parent as Catalog);

				return !await this.dataProviderFactory.Get<IFileSystemProvider>().CatalogExists(instance);
			}).AddError(new NotFoundException("Catalog with specified name does not exist in parent catalog."));

			return await this.ValidateAsync(instance.ID);
		}

		#endregion
	}
}
