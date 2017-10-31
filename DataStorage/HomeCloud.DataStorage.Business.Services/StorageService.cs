namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Api.Configuration;
	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Handlers;
	using Microsoft.Extensions.Options;
	using System;
	using System.IO;

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

		/// <summary>
		/// The file system settings
		/// </summary>
		private readonly FileSystem fileSystemSettings = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StorageService" /> class.
		/// </summary>
		/// <param name="processor">The command processor.</param>
		/// <param name="commandHandlerFactory">The command handler factory.</param>
		public StorageService(ICommandHandlerProcessor processor, IServiceFactory<IDataCommandHandler> commandHandlerFactory, IOptionsSnapshot<FileSystem> fileSystemSettings)
		{
			this.processor = processor;
			this.commandHandlerFactory = commandHandlerFactory;

			this.fileSystemSettings = fileSystemSettings?.Value;
		}

		#endregion

		#region IStorageService Implementations

		public void CreateStorage(Storage storage)
		{
			storage.Name = storage.CatalogRoot.Name = Guid.NewGuid().ToString();
			storage.CatalogRoot.Path = Path.Combine(this.fileSystemSettings.StoragePath, storage.CatalogRoot.Name);

			this.processor.CreateDataHandler<IDataStoreCommandHandler>().CreateCommand(provider => storage = provider.CreateStorage(storage), null);
			this.processor.CreateDataHandler<IFileSystemCommandHandler>().CreateCommand(provider => storage = provider.CreateStorage(storage), null);
			this.processor.CreateDataHandler<IAggregatedDataCommandHandler>().CreateCommand(provider => storage = provider.CreateStorage(storage), null);

			this.processor.Process();
		}

		#endregion
	}
}
