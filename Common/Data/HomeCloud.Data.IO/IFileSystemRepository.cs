namespace HomeCloud.Data.IO
{
	/// <summary>
	/// Marks the repository implementation to be <see cref="IFileSystemRepository" />.
	/// </summary>
	public interface IFileSystemRepository
	{
		/// <summary>
		/// Deletes the record by specified file system path.
		/// </summary>
		/// <param name="path">The path.</param>
		void Delete(string path);
	}
}
