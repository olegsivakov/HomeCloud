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
	/// Provides methods to validate whether the specified instance is unique.
	/// </summary>
	/// <seealso cref="HomeCloud.Validation.Validator{System.Guid}" />
	/// <seealso cref="HomeCloud.DataStorage.Business.Validation.IUniqueValidator" />
	public class UniqueValidator : Validator<Guid>, IUniqueValidator
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
		public UniqueValidator(IServiceFactory<IDataProvider> dataProviderFactory)
			: base()
		{
			this.dataProviderFactory = dataProviderFactory;
		}

		#endregion

		#region IUniqueValidator Implementations

		/// <summary>
		/// Validates the specified instance of <see cref="Storage"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(Storage instance)
		{
			this.If(async id => id != Guid.Empty && await this.dataProviderFactory.Get<IDataStoreProvider>().StorageExists(instance)).AddError(new AlreadyExistsException("The storage already exists."));
			this.If(async id => await this.dataProviderFactory.Get<IFileSystemProvider>().StorageExists(instance)).AddError(new AlreadyExistsException("The storage with provided name already exists."));

			return await this.ValidateAsync(instance.ID);
		}

		/// <summary>
		/// Validates the specified instance of <see cref="Catalog"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(Catalog instance)
		{
			Catalog catalog = await this.dataProviderFactory.Get<IDataStoreProvider>().GetCatalog(instance);

			this.If(async id => id != Guid.Empty && await this.dataProviderFactory.Get<IDataStoreProvider>().CatalogExists(instance)).AddError(new AlreadyExistsException("The catalog already exists."));
			this.If(async id =>
			{
				if (catalog.Parent?.ID == instance.Parent?.ID && catalog.Name == instance.Name)
				{
					return false;
				}

				return await this.dataProviderFactory.Get<IFileSystemProvider>().CatalogExists(instance);
			}).AddError(new AlreadyExistsException("The catalog with the provided name already exists in parent catalog."));

			return await this.ValidateAsync(instance.ID);
		}

		#endregion
	}
}
