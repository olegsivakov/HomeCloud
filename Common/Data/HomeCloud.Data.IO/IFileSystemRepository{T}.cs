namespace HomeCloud.Data.IO
{
	/// <summary>
	/// Defines methods to handle data of <see cref="T" /> stored in file system.
	/// </summary>
	/// <typeparam name="T">The type of data to handle.</typeparam>
	/// <seealso cref="HomeCloud.Data.IO.IFileSystemRepository" />
	/// <seealso cref="HomeCloud.Data.IRepository{T}" />
	public interface IFileSystemRepository<T> : IFileSystemRepository, IRepository<T>
	{
	}
}
