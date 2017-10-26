namespace HomeCloud.DataStorage.Business.Components.Handlers
{
	#region Usings

	using System;

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Business.Services.Commands;
	using HomeCloud.DataStorage.Business.Services.Handlers;
	using HomeCloud.DataStorage.Business.Services.Providers;

	#endregion

	public class DataStoreCommandHandler : IDataStoreCommandHandler
	{
		#region Private Members

		private ICommand command = null;

		private readonly IActionCommandFactory commandFactory = null;

		#endregion

		#region Constructors

		public DataStoreCommandHandler(IActionCommandFactory commandFactory, IDataProviderFactory providerFactory)
		{
			this.commandFactory = commandFactory;
			this.Provider = providerFactory.GetProvider<IDataStoreProvider>();
		}

		#endregion

		#region IDataStoreCommandHandler Implementations

		public IDataProvider Provider { get; }

		public void SetCommand(ICommand command)
		{
			this.command = command;
		}

		public ICommand CreateCommand(Action<IDataProvider> executeAction, Action<IDataProvider> undoAction)
		{
			ICommand command = this.commandFactory.CreateCommand(this.Provider, executeAction, undoAction);

			this.SetCommand(command);

			return command;
		}

		public void Execute()
		{
			this.command.Execute();
		}

		public void Undo()
		{
			this.command.Undo();
		}

		#endregion
	}
}
