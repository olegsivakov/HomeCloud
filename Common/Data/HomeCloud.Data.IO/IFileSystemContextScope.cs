namespace HomeCloud.Data.IO
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Defines execution of operations against the file system in a single scope.
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	public interface IFileSystemContextScope : IDisposable
	{
		/// <summary>
		/// Gets the <see cref="IFileSystemRepository" /> repository.
		/// </summary>
		/// <typeparam name="T">The type of the repository derived from <see cref="IFileSystemRepository" />.</typeparam>
		/// <returns>The instance of <see cref="!:T" />.</returns>
		T GetRepository<T>() where T : IFileSystemRepository;

		/// <summary>
		/// Commits the changes made in file system.
		/// </summary>
		void Commit();
	}
}
