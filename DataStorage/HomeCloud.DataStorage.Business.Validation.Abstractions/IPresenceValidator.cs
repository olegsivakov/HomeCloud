namespace HomeCloud.DataStorage.Business.Validation
{
	/// <summary>
	/// Defines methods to validate whether the specified instance already exists.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Validation.IStorageValidator" />
	/// <seealso cref="HomeCloud.DataStorage.Business.Validation.ICatalogValidator" />
	/// <seealso cref="HomeCloud.DataStorage.Business.Validation.ICatalogEntryValidator" />
	public interface IPresenceValidator : IStorageValidator, ICatalogValidator, ICatalogEntryValidator
	{
	}
}
