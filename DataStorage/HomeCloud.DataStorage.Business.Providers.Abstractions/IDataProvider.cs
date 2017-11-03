namespace HomeCloud.DataStorage.Business.Providers
{
	#region Usings

	using System.Threading.Tasks;

	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Defines methods to provide data.
	/// </summary>
	public interface IDataProvider
	{
		/// <summary>
		/// Creates the specified storage.
		/// </summary>
		/// <param name="storage">The instance of <see cref="Storage" /> type to create.</param>
		/// <returns>The newly created instance of <see cref="Storage" /> type.</returns>
		Task<Storage> CreateStorage(Storage storage);
	}
}
