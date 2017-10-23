namespace HomeCloud.DataStorage.DataAccess.Services.Repositories
{
	#region Usings

	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataStorage.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Defines methods to handle <see cref="Storage"/> data.
	/// </summary>
	public interface IStorageRepository : IDbRepository<Storage>
	{
	}
}
