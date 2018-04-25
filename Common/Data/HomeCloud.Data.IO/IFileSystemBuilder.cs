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

		/// <summary>
		/// Adds specified <see cref="IFileSystemContextScope" /> file context to the service collection.
		/// </summary>
		/// <typeparam name="TContextScope">The type of the file context scope derived from <see cref="IFileSystemContextScope" />.</typeparam>
		/// <typeparam name="TImplementation">The type of <see cref="IFileSystemContextScope" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="IFileSystemBuilder" />.
		/// </returns>
		IFileSystemBuilder AddContextScope<TContextScope, TImplementation>()
			where TContextScope : class, IFileSystemContextScope
			where TImplementation : FileSystemContextScope, TContextScope;

		/// <summary>
		/// Adds the specified <see cref="IFileSystemRepository" /> repository to the service collection.
		/// </summary>
		/// <typeparam name="TRepository">The type of the repository derived from <see cref="IFileSystemRepository" />.</typeparam>
		/// <typeparam name="TImplementation">The type of the <see cref="IFileSystemRepository" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="IFileSystemBuilder" />.
		/// </returns>
		IFileSystemBuilder AddRepository<TRepository, TImplementation>()
			where TRepository : class, IFileSystemRepository
			where TImplementation : class, TRepository;
	}
}
