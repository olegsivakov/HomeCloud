namespace HomeCloud.DataStorage.Business.Components
{
	#region Usings

	using HomeCloud.DataStorage.Business.Services;
	using HomeCloud.DataStorage.Business.Services.Handlers;
	using HomeCloud.DataStorage.Business.Services.Processors;

	#endregion

	public class StorageService : IStorageService
	{
		#region Private Members

		private readonly ICommandHandlerProcessor processor = null;

		private readonly IDataCommandHandlerFactory commandHandlerFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StorageService"/> class.
		/// </summary>
		/// <param name="processor">The command processor.</param>
		public StorageService(ICommandHandlerProcessor processor, IDataCommandHandlerFactory commandHandlerFactory)
		{
			this.processor = processor;
			this.commandHandlerFactory = commandHandlerFactory;
		}

		#endregion

		#region IStorageService Implementations

		public void CreateStorage()
		{
			this.processor.CreateHandler<IDataStoreCommandHandler>().CreateCommand(provider => provider.CreateStorage(null), provider => provider.CreateStorage(null));
		}

		#endregion
	}
}
