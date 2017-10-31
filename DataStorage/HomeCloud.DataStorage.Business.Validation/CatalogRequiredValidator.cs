namespace HomeCloud.DataStorage.Business.Validation
{
	#region Usings

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Validation.Abstractions;

	using HomeCloud.Validation;

	#endregion

	public class CatalogRequiredValidator : Validator<Catalog>, ICatalogRequiredValidator
	{
		#region Constructors

		public CatalogRequiredValidator()
			: base()
		{
			this.If(catalog => catalog is null).AddMessage("The catalog is not defined.");
			this.If(catalog => string.IsNullOrWhiteSpace(catalog.Name)).AddMessage("The catalog name is empty.");
			this.If(catalog => string.IsNullOrWhiteSpace(catalog.Path)).AddMessage("The catalog path is empty.");
		}

		#endregion
	}
}
