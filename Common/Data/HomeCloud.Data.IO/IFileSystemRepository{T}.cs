namespace HomeCloud.Data.IO
{
	#region Usings

	using System.IO;

	using HomeCloud.Core;

	#endregion

	/// <summary>
	/// Defines methods to handle data of <see cref="T" /> stored in file system.
	/// </summary>
	/// <typeparam name="T">The type of data to handle.</typeparam>
	/// <seealso cref="HomeCloud.Data.IO.IFileSystemRepository" />
	public interface IFileSystemRepository<T> : IFileSystemRepository
		where T : FileSystemInfo
	{
		/// <summary>
		/// Gets the instance of <see cref="T"/> of specified <paramref name="name"/> located in <paramref name="parent"/>.
		/// </summary>
		/// <param name="name">The name of the directory of file.</param>
		/// <param name="parent">The instance of <see cref="DirectoryInfo"/> representing parent directory the requested by <paramref name="name"/> instance should be created in.
		/// By default the value corresponds <see cref="FileSystemOptions.Root"/>.</param>
		/// <returns>The instance of <see cref="T"/>.</returns>
		T Get(string name, DirectoryInfo parent = null);

		/// <summary>
		/// Gets the records of <see cref="T" /> type by specified expression.
		/// </summary>
		/// <param name="parent">The parent directory.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The <see cref="IPaginable" /> list of instances of <see cref="T" /> type.
		/// </returns>
		IPaginable<T> Find(DirectoryInfo parent, int offset = 0, int limit = 20);

		/// <summary>
		/// Gets the entity of <see cref="T"/> by specified file system path.
		/// </summary>
		/// <param name="path">The path to the file system instance.</param>
		/// <returns>The instance of <see cref="T"/> type.</returns>
		T Get(string path);

		/// <summary>
		/// Saves the specified entity of <see cref="T"/>.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>The instance of <see cref="T"/>.</returns>
		T Save(T entity);
	}
}
