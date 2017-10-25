namespace HomeCloud.DataStorage.Business.Services.Providers
{
	#region Usings

	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	public interface IDataProvider
	{
		void CreateStorage(Storage storage);
	}
}
