namespace HomeCloud.DataStorage.Business.Handlers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Business.Commands;
	using HomeCloud.DataStorage.Business.Providers;

	#endregion

	/// <summary>
	/// Provides methods to handle the execution of the set of data commands.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Handlers.IDataCommandHandler" />
	public class DataCommandHandler : IDataCommandHandler
	{
		#region Private Members

		/// <summary>
		/// The list of commands.
		/// </summary>
		private readonly IList<ICommand> commands = new List<ICommand>();

		/// <summary>
		/// The <see cref="IActionCommandFactory"/> factory.
		/// </summary>
		private readonly IActionCommandFactory commandFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DataCommandHandler" /> class.
		/// </summary>
		/// <param name="commandFactory">The <see cref="IActionCommandFactory" /> factory.</param>
		public DataCommandHandler(IActionCommandFactory commandFactory)
		{
			this.commandFactory = commandFactory;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Creates the asynchronous data command and attaches to the current instance.
		/// </summary>
		/// <typeparam name="TProvider">The type of the provider.</typeparam>
		/// <param name="executeAsyncAction">The asynchronous action to execute.</param>
		/// <param name="undoAsyncAction">The asynchronous action to undo.</param>
		/// <returns>
		/// The current instance of <see cref="IDataCommandHandler"/>.
		/// </returns>
		public virtual IDataCommandHandler CreateAsyncCommand<TProvider>(Func<IDataProvider, Task> executeAsyncAction, Func<IDataProvider, Task> undoAsyncAction)
			where TProvider : IDataProvider
		{
			ICommand command = this.commandFactory.CreateCommand<TProvider>(executeAsyncAction, undoAsyncAction);

			this.SetCommand(command);

			return this;
		}

		/// <summary>
		/// Runs and executes the commands asynchronously.
		/// </summary>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public virtual async Task ExecuteAsync()
		{
			IList<Task> tasks = new List<Task>();

			foreach (ICommand command in this.commands)
			{
				tasks.Add(command.ExecuteAsync());
			}

			await Task.WhenAll(tasks);
		}

		/// <summary>
		/// Sets the command to execute.
		/// </summary>
		/// <param name="command">The command.</param>
		public virtual void SetCommand(ICommand command)
		{
			this.commands.Add(command);
		}

		/// <summary>
		/// Reverts execution result of the commands asynchronously.
		/// </summary>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public virtual async Task UndoAsync()
		{
			IList<Task> tasks = new List<Task>();

			foreach (ICommand command in this.commands)
			{
				tasks.Add(command.UndoAsync());
			}

			await Task.WhenAll(tasks);
		}

		#endregion
	}
}
