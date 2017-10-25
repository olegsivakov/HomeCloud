namespace HomeCloud.DataStorage.Business.Services.Providers
{
	public interface IDataProviderFactory
	{
		IDataProvider GetProvider<T>()
			where T : IDataProvider;
	}
}
