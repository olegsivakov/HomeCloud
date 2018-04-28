namespace HomeCloud.Data.IO
{
	/// <summary>
	/// Defines execution of operations against the file system in a single scope.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.IDataContextScope" />
	public interface IFileSystemContextScope : IDataContextScope
	{
		/// <summary>
		/// Gets the interface that provides the file system management methods.
		/// </summary>
		/// <returns>The instance of <see cref="IFileSystemOperation" />.</returns>
		IFileSystemOperation GetOperationCollection();
	}
}
