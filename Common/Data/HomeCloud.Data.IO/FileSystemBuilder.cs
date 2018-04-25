namespace HomeCloud.Data.IO
{
	#region Usings

	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// Implements methods to add file system services to service collection.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.IO.IFileSystemBuilder" />
	public class FileSystemBuilder : IFileSystemBuilder
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IServiceCollection"/>.
		/// </summary>
		private readonly IServiceCollection services = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemBuilder"/> class.
		/// </summary>
		/// <param name="services">The services.</param>
		public FileSystemBuilder(IServiceCollection services)
		{
			this.services = services;
		}

		#endregion

		#region IFileSystemBuilder Implementations

		/// <summary>
		/// Adds default <see cref="IFileSystemContext" /> file context to the service collection.
		/// </summary>
		/// <returns>
		/// The instance of <see cref="IFileSystemBuilder" />.
		/// </returns>
		public IFileSystemBuilder AddContext()
		{
			this.services.AddScoped<IFileSystemContext, FileSystemContext>();

			return this;
		}

		/// <summary>
		/// Adds default <see cref="IFileSystemContextScope" /> file context scope to the service collection.
		/// </summary>
		/// <returns>
		/// The instance of <see cref="IFileSystemBuilder" />.
		/// </returns>
		public IFileSystemBuilder AddContextScope()
		{
			this.services.AddScoped<IFileSystemContextScope, FileSystemContextScope>();

			return this;
		}

		/// <summary>
		/// Adds specified <see cref="IFileSystemContext" /> file context to the service collection.
		/// </summary>
		/// <typeparam name="TContext">The type of the file context derived from <see cref="IFileSystemContext" />.</typeparam>
		/// <typeparam name="TImplementation">The type of <see cref="IFileSystemContext" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="IFileSystemBuilder" />.
		/// </returns>
		public IFileSystemBuilder AddContext<TContext, TImplementation>()
			where TContext : class, IFileSystemContext
			where TImplementation : FileSystemContext, TContext
		{
			this.services.AddScoped<TContext, TImplementation>();

			return this;
		}

		/// <summary>
		/// Adds specified <see cref="IFileSystemContextScope" /> file context to the service collection.
		/// </summary>
		/// <typeparam name="TContextScope">The type of the file context scope derived from <see cref="IFileSystemContextScope" />.</typeparam>
		/// <typeparam name="TImplementation">The type of <see cref="IFileSystemContextScope" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="IFileSystemBuilder" />.
		/// </returns>
		public IFileSystemBuilder AddContextScope<TContextScope, TImplementation>()
			where TContextScope : class, IFileSystemContextScope
			where TImplementation : FileSystemContextScope, TContextScope
		{
			this.services.AddScoped<TContextScope, TImplementation>();

			return this;
		}

		/// <summary>
		/// Adds the specified <see cref="IFileSystemRepository" /> repository to the service collection.
		/// </summary>
		/// <typeparam name="TRepository">The type of the repository derived from <see cref="IFileSystemRepository" />.</typeparam>
		/// <typeparam name="TImplementation">The type of the <see cref="IFileSystemRepository" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="IFileSystemBuilder" />.
		/// </returns>
		public IFileSystemBuilder AddRepository<TRepository, TImplementation>()
			where TRepository : class, IFileSystemRepository
			where TImplementation : class, TRepository
		{
			this.services.AddScoped<TRepository, TImplementation>();

			return this;
		}

		#endregion
	}
}
