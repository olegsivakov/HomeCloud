namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Handlers;

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

		public void CreateStorage(Storage storage)
		{
			this.processor.CreateDataHandler<IDataStoreCommandHandler>().CreateCommand(provider => provider.CreateStorage(storage), provider => provider.CreateStorage(null));
			this.processor.CreateDataHandler<IFileSystemCommandHandler>().CreateCommand(provider => provider.CreateStorage(storage), provider => provider.CreateStorage(null));
			this.processor.CreateDataHandler<IAggregatedDataCommandHandler>().CreateCommand(provider => provider.CreateStorage(storage), provider => provider.CreateStorage(null));

			this.processor.Process();
		}

		#endregion
	}
}
