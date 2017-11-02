﻿namespace HomeCloud.DataStorage.Business.Validation
{
	/// <summary>
	/// Defines methods to validate whether the specified instance is unique.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Validation.IStorageValidator" />
	/// <seealso cref="HomeCloud.DataStorage.Business.Validation.ICatalogValidator" />
	public interface IUniqueValidator : IStorageValidator, ICatalogValidator
	{
	}
}
