namespace HomeCloud.Data.IO
{
	/// <summary>
	/// Defines methods to add file system services to service collection.
	/// </summary>
	public interface IFileSystemBuilder
	{
		/// <summary>
		/// Adds default <see cref="IFileSystemContext" /> file context to the service collection.
		/// </summary>
		/// <returns>
		/// The instance of <see cref="IFileSystemBuilder" />.
		/// </returns>
		IFileSystemBuilder AddContext();

		/// <summary>
		/// Adds default <see cref="IFileSystemContextScope" /> file context scope to the service collection.
		/// </summary>
		/// <returns>
		/// The instance of <see cref="IFileSystemBuilder" />.
		/// </returns>
		IFileSystemBuilder AddContextScope();

		/// <summary>
		/// Adds specified <see cref="IFileSystemContext" /> file context to the service collection.
		/// </summary>
		/// <typeparam name="TContext">The type of the file context derived from <see cref="IFileSystemContext" />.</typeparam>
		/// <typeparam name="TImplementation">The type of <see cref="IFileSystemContext" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="IFileSystemBuilder" />.
		/// </returns>
		IFileSystemBuilder AddContext<TContext, TImplementation>()
			where TContext : class, IFileSystemContext
			where TImplementation : FileSystemContext, TContext;
	}
}
