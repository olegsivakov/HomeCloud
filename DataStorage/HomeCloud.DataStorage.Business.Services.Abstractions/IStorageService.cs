namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	public interface IStorageService
	{
		void CreateStorage(Storage storage);
	}
}
