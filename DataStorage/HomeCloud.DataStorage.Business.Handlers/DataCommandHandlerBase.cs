namespace HomeCloud.DataStorage.Business.Handlers
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Business.Commands;
	using HomeCloud.DataStorage.Business.Providers;

	#endregion

	/// <summary>
	/// Provides common implementation of methods to handle the data commands.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Handlers.IDataCommandHandler" />
	public abstract class DataCommandHandlerBase : IDataCommandHandler
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
		/// Initializes a new instance of the <see cref="DataCommandHandlerBase"/> class.
		/// </summary>
		/// <param name="provider">The data provider.</param>
		/// <param name="commandFactory">The action command factory.</param>
		protected DataCommandHandlerBase(IDataProvider provider, IActionCommandFactory commandFactory)
		{
			this.Provider = provider;
			this.commandFactory = commandFactory;
		}

		#endregion

		#region IDataCommandHandler Implementations

		#region Public Properties

		/// <summary>
		/// Gets the data provider.
		/// </summary>
		/// <value>
		/// The data provider.
		/// </value>
		public IDataProvider Provider { get; private set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Creates the asynchronous data command.
		/// </summary>
		/// <param name="executeAsyncAction">The asynchronous action to execute.</param>
		/// <param name="undoAsyncAction">The asynchronous action to undo.</param>
		/// <returns>
		/// The command of <see cref="T:HomeCloud.Core.ICommand" />.
		/// </returns>
		public virtual ICommand CreateAsyncCommand(Func<IDataProvider, Task> executeAsyncAction, Func<IDataProvider, Task> undoAsyncAction)
		{
			ICommand command = this.commandFactory.CreateCommand(this.Provider, executeAsyncAction, undoAsyncAction);

			this.SetCommand(command);

			return command;
		}

		/// <summary>
		/// Executes the command.
		/// </summary>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public virtual async Task ExecuteAsync()
		{
			await this.command.ExecuteAsync();
		}

		/// <summary>
		/// Sets the command to execute.
		/// </summary>
		/// <param name="command">The command.</param>
		public virtual void SetCommand(ICommand command)
		{
			this.command = command;
		}

		/// <summary>
		/// Reverts the result of command execution.
		/// </summary>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public virtual async Task UndoAsync()
		{
			await this.command.UndoAsync();
		}

		#endregion

		#endregion
	}
}
