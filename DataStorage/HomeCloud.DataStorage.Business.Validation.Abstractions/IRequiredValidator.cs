namespace HomeCloud.DataStorage.Business.Validation
{
	/// <summary>
	/// Defines methods to validate required attributes of the instance.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Validation.IStorageValidator" />
	/// <seealso cref="HomeCloud.DataStorage.Business.Validation.ICatalogValidator" />
	/// <seealso cref="HomeCloud.DataStorage.Business.Validation.ICatalogEntryValidator" />
	public interface IRequiredValidator : IStorageValidator, ICatalogValidator, ICatalogEntryValidator
	{
	}
}
