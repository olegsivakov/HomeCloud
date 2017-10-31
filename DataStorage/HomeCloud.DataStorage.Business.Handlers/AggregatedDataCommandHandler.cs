namespace HomeCloud.DataStorage.Business.Handlers
{
	#region Usings

	using System;

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Business.Providers;
	using HomeCloud.DataStorage.Business.Commands;

	#endregion

	/// <summary>
	/// Provides methods to handle the command that executes the specified action against the data provided by <see cref="IAggregatedDataCommandHandler"/>.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Handlers.IAggregatedDataCommandHandler" />
	public class AggregatedDataCommandHandler : IAggregatedDataCommandHandler
	{
		#region Private Members

		/// <summary>
		/// The action command factory
		/// </summary>
		private readonly IActionCommandFactory commandFactory = null;

		/// <summary>
		/// The command to execute.
		/// </summary>
		private ICommand command = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AggregatedDataCommandHandler" /> class.
		/// </summary>
		/// <param name="commandFactory">The command factory.</param>
		/// <param name="providerFactory">The provider factory.</param>
		public AggregatedDataCommandHandler(IActionCommandFactory commandFactory, IServiceFactory<IDataProvider> providerFactory)
		{
			this.commandFactory = commandFactory;
			this.Provider = providerFactory.Get<IAggregationDataProvider>();
		}

		#endregion

		#region IAggregatedDataCommandHandler Implementations

		/// <summary>
		/// Gets the data provider.
		/// </summary>
		/// <value>
		/// The data provider.
		/// </value>
		public IDataProvider Provider { get; }

		/// <summary>
		/// Sets the command to execute.
		/// </summary>
		/// <param name="command">The command.</param>
		public void SetCommand(ICommand command)
		{
			this.command = command;
		}

		/// <summary>
		/// Creates the data command.
		/// </summary>
		/// <param name="executeAction">The action to execute.</param>
		/// <param name="undoAction">The action to revert command execution result.</param>
		/// <returns>The command of <see cref="ICommand"/>.</returns>
		public ICommand CreateCommand(Action<IDataProvider> executeAction, Action<IDataProvider> undoAction)
		{
			ICommand command = this.commandFactory.CreateCommand(this.Provider, executeAction, undoAction);

			this.SetCommand(command);

			return command;
		}

		/// <summary>
		/// Executes the command.
		/// </summary>
		public void Execute()
		{
			this.command.Execute();
		}

		/// <summary>
		/// Reverts the result of command execution.
		/// </summary>
		public void Undo()
		{
			this.command.Undo();
		}

		#endregion
	}
}
