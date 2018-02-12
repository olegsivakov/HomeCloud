namespace HomeCloud.Data.MongoDB
{
	/// <summary>
	/// Defines methods to handle the data of <see cref="T" /> type stored in <see cref="MongoDB" /> database.
	/// </summary>
	/// <typeparam name="T">The type of data to handle.</typeparam>
	/// <seealso cref="HomeCloud.Data.MongoDB.IMongoDBRepository" />
	/// <seealso cref="HomeCloud.Data.IRepository{T}" />
	public interface IMongoDBRepository<T> : IMongoDBRepository, IRepository<T>
	{
	}
}
