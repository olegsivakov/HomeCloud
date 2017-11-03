namespace HomeCloud.DataStorage.Business.Handlers
{
	#region Usings

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Business.Commands;
	using HomeCloud.DataStorage.Business.Providers;

	#endregion

	/// <summary>
	/// Provides methods to handle the command that executes the specified action against the data provided by <see cref="IFileSystemCommandHandler" />.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Handlers.IFileSystemCommandHandler" />
	/// <seealso cref="HomeCloud.DataStorage.Business.Handlers.DataCommandHandlerBase" />
	public class FileSystemCommandHandler : DataCommandHandlerBase, IFileSystemCommandHandler
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemCommandHandler" /> class.
		/// </summary>
		/// <param name="providerFactory">The data provider factory.</param>
		/// <param name="commandFactory">The action command factory.</param>
		public FileSystemCommandHandler(IServiceFactory<IDataProvider> providerFactory, IActionCommandFactory commandFactory)
			: base(providerFactory.Get<IFileSystemProvider>(), commandFactory)
		{
		}

		#endregion
	}
}
