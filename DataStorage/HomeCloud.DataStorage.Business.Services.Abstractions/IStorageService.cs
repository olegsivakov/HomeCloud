namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Defines methods to handle storages.
	/// </summary>
	public interface IStorageService
	{
		/// <summary>
		/// Creates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage"/> type.</param>
		void CreateStorage(Storage storage);
	}
}
