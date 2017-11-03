namespace HomeCloud.DataStorage.Business.Handlers
{
	#region Usings

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Business.Commands;
	using HomeCloud.DataStorage.Business.Providers;

	#endregion

	/// <summary>
	/// Provides methods to handle the command that executes the specified action against the data provided by <see cref="IDataStoreCommandHandler" />.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Handlers.IDataStoreCommandHandler" />
	/// <seealso cref="HomeCloud.DataStorage.Business.Handlers.DataCommandHandlerBase" />
	public class DataStoreCommandHandler : DataCommandHandlerBase, IDataStoreCommandHandler
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DataStoreCommandHandler" /> class.
		/// </summary>
		/// <param name="providerFactory">The data provider factory.</param>
		/// <param name="commandFactory">The command factory.</param>
		public DataStoreCommandHandler(IServiceFactory<IDataProvider> providerFactory, IActionCommandFactory commandFactory)
			: base(providerFactory.Get<IDataStoreProvider>(), commandFactory)
		{
		}

		#endregion
	}
}
