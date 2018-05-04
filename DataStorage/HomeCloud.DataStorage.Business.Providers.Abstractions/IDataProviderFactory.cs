namespace HomeCloud.DataStorage.Business.Providers
{
	/// <summary>
	/// Defines factory methods to provide data.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Providers.IDataProvider" />
	public interface IDataProviderFactory : IDataProvider
	{
		/// <summary>
		/// Gets the instance of <see cref="T"/> derived from <see cref="IDataProvider"/>.
		/// </summary>
		/// <typeparam name="T">The type of provider.</typeparam>
		/// <returns>The instance of <see cref="T"/>.</returns>
		IDataProvider GetProvider<T>() where T : IDataProvider;
	}
}
