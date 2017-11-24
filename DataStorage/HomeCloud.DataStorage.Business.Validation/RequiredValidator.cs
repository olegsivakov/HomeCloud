namespace HomeCloud.DataStorage.Business.Validation
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Providers;

	using HomeCloud.Validation;

	#endregion

	/// <summary>
	/// Provides methods to validate required attributes of the instance.
	/// </summary>
	/// <seealso cref="HomeCloud.Validation.Validator{System.Object}" />
	/// <seealso cref="HomeCloud.DataStorage.Business.Validation.IRequiredValidator" />
	public class RequiredValidator : Validator<object>, IRequiredValidator
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IDataProvider"/> factory.
		/// </summary>
		private readonly IServiceFactory<IDataProvider> dataProviderFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="RequiredValidator" /> class.
		/// </summary>
		/// <param name="dataProviderFactory">The <see cref="IDataProvider"/> factory.</param>
		public RequiredValidator(IServiceFactory<IDataProvider> dataProviderFactory)
			: base()
		{
			this.dataProviderFactory = dataProviderFactory;

			this.If(obj => obj is null).AddError("The instance is not defined.");
		}

		#endregion

		#region IRequiredValidator Implementations

		/// <summary>
		/// Validates the specified instance of <see cref="Storage"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(Storage instance)
		{
			this.If(obj => string.IsNullOrWhiteSpace(instance.DisplayName)).AddError("The specified storage name is empty.");

			return await this.ValidateAsync((object)instance);
		}

		/// <summary>
		/// Validates the specified instance of <see cref="Catalog"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(Catalog instance)
		{
			Catalog parentCatalog = new Catalog() { ID = (instance.Parent?.ID).GetValueOrDefault() };

			this.If(obj => string.IsNullOrWhiteSpace(instance.Name)).AddError("The specified catalog name is empty.");
			this.If(obj => parentCatalog.ID == Guid.Empty).AddError("The specified parent catalog is empty.");
			this.If(async obj => !await this.dataProviderFactory.Get<IDataStoreProvider>().CatalogExists(parentCatalog)).AddError("The specified parent catalog does not exist.");

			return await this.ValidateAsync((object)instance);
		}

		/// <summary>
		/// Validates the specified instance of <see cref="CatalogEntry"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(CatalogEntry instance)
		{
			Catalog catalog = new Catalog() { ID = (instance.Catalog?.ID).GetValueOrDefault() };

			this.If(obj => string.IsNullOrWhiteSpace(instance.Name)).AddError("The specified catalog entry name is empty.");
			this.If(obj => catalog.ID == Guid.Empty).AddError("The specified catalog is empty.");
			this.If(async obj => !await this.dataProviderFactory.Get<IDataStoreProvider>().CatalogExists(catalog)).AddError("The specified catalog does not exist.");

			return await this.ValidateAsync((object)instance);
		}

		#endregion
	}
}
