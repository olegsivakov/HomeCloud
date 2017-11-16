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
		/// Initializes a new instance of the <see cref="RequiredValidator"/> class.
		/// </summary>
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
			this.If(obj => string.IsNullOrWhiteSpace(instance.DisplayName)).AddError("The storage name is empty.");

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

			this.If(obj => string.IsNullOrWhiteSpace(instance.Name)).AddError("The catalog name is empty.");
			this.If(obj => parentCatalog.ID == Guid.Empty).AddError("The parent catalog is empty.");
			this.If(async obj => await this.dataProviderFactory.Get<IDataStoreProvider>().CatalogExists(parentCatalog)).AddError("The parent catalog with the specified identifier does not exist.");
			this.If(async obj =>
			{
				IDataProvider provider = this.dataProviderFactory.Get<IDataStoreProvider>();
				if (await provider.CatalogExists(parentCatalog))
				{
					parentCatalog = await provider.GetCatalog(parentCatalog);
					parentCatalog = await this.dataProviderFactory.Get<IAggregationDataProvider>().GetCatalog(parentCatalog);
					if (string.IsNullOrWhiteSpace(parentCatalog.Path))
					{
						return true;
					}

					return !(await this.dataProviderFactory.Get<IFileSystemProvider>().CatalogExists(parentCatalog));
				}

				return true;
			}).AddError("The specified parent catalog does not exist.");

			return await this.ValidateAsync((object)instance);
		}

		#endregion
	}
}
