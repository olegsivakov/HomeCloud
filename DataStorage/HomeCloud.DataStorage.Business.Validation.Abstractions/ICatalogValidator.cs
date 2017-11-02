namespace HomeCloud.DataStorage.Business.Validation
{
	#region Usings

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.Validation;

	#endregion

	/// <summary>
	/// Defines methods to validate the instance of <see cref="Catalog"/>.
	/// </summary>
	/// <seealso cref="HomeCloud.Validation.IValidator{HomeCloud.DataStorage.Business.Entities.Catalog}" />
	public interface ICatalogValidator : IValidator<Catalog>
	{
	}
}
