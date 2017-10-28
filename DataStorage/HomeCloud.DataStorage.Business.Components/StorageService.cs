namespace HomeCloud.DataStorage.Business.Components
{
	#region Usings

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Business.Services;
	using HomeCloud.DataStorage.Business.Services.Handlers;
	using HomeCloud.DataStorage.Business.Services.Processors;

	#endregion

	public class StorageService : IStorageService
	{
		#region Private Members

		/// <summary>
		/// The processor
		/// </summary>
		private readonly ICommandHandlerProcessor processor = null;

		/// <summary>
		/// The command handler factory
		/// </summary>
		private readonly IServiceFactory<IDataCommandHandler> commandHandlerFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StorageService" /> class.
		/// </summary>
		/// <param name="processor">The command processor.</param>
		/// <param name="commandHandlerFactory">The command handler factory.</param>
		public StorageService(ICommandHandlerProcessor processor, IServiceFactory<IDataCommandHandler> commandHandlerFactory)
		{
			this.processor = processor;
			this.commandHandlerFactory = commandHandlerFactory;
		}

		#endregion

		#region IStorageService Implementations

		public void CreateStorage()
		{
			this.processor.CreateDataHandler<IDataStoreCommandHandler>().CreateCommand(provider => provider.CreateStorage(null), provider => provider.CreateStorage(null));
		}

		#endregion
	}
}
