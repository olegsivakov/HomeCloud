namespace HomeCloud.DataStorage.Business.Services.Providers
{
	/// <summary>
	/// Defines methods to provide data providers of <see cref="IDataProvider"/> type.
	/// </summary>
	public interface IDataProviderFactory
	{
		/// <summary>
		/// Gets the data provider instance of <see cref="IDataProvider"/> type.
		/// </summary>
		/// <typeparam name="T">the type of data provider derived from <see cref="IDataProvider"/>.</typeparam>
		/// <returns>The instance of <see cref="IDataProvider"/>.</returns>
		IDataProvider GetProvider<T>() where T : IDataProvider;
	}
}
