namespace HomeCloud.DataStorage.Business.Services.Providers
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
		void CreateStorage(Storage storage);
	}
}
