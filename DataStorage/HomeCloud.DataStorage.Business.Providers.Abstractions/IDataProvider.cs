namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Defines methods to provide data.
	/// </summary>
	public interface IDataProvider
	{
		/// <summary>
		/// Creates the storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="storage"/>.</param>
		Storage CreateStorage(Storage storage);

		/// <summary>
		/// Updates the storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="storage"/>.</param>
		void SetStorageQuota(Storage storage, long quota);

		/// <summary>
		/// Deletes the storage.
		/// </summary>
		/// <param name="storage">The storage.</param>
		void DeleteStorage(Storage storage);
	}
}
