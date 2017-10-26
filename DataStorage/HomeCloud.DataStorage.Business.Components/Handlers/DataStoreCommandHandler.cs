namespace HomeCloud.DataStorage.Business.Components.Handlers
{
	#region Usings

	using System;

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Business.Services.Commands;
	using HomeCloud.DataStorage.Business.Services.Handlers;
	using HomeCloud.DataStorage.Business.Services.Providers;

	#endregion

	/// <summary>
	/// Provides methods to handle the command that executes the specified action against the data provided by <see cref="IDataStoreCommandHandler"/>.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Services.Handlers.IDataStoreCommandHandler" />
	public class DataStoreCommandHandler : IDataStoreCommandHandler
	{
		#region Private Members

		/// <summary>
		/// The command to execute.
		/// </summary>
		private ICommand command = null;

		/// <summary>
		/// The action command factory
		/// </summary>
		private readonly IActionCommandFactory commandFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DataStoreCommandHandler"/> class.
		/// </summary>
		/// <param name="commandFactory">The command factory.</param>
		/// <param name="providerFactory">The provider factory.</param>
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
