namespace HomeCloud.Data.IO
{
	/// <summary>
	/// Defines execution of operations against the file system in a single scope.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.IDataContextScope" />
	public interface IFileSystemContextScope : IDataContextScope
	{
		/// <summary>
		/// Gets the <see cref="IFileSystemRepository" /> repository.
		/// </summary>
		/// <typeparam name="T">The type of the repository derived from <see cref="IFileSystemRepository" />.</typeparam>
		/// <returns>The instance of <see cref="!:T" />.</returns>
		T GetRepository<T>() where T : IFileSystemRepository;
	}
}
