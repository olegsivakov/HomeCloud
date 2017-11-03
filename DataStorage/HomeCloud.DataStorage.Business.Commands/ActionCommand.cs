namespace HomeCloud.DataStorage.Business.Commands
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Core;

	#endregion

	/// <summary>
	/// Represents the command that executes the specified action.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ICommand" />
	public class ActionCommand : ICommand
	{
		#region Private Members

		/// <summary>
		/// The action to execute.
		/// </summary>
		private readonly Func<Task> executeAction = null;

		/// <summary>
		/// The action to revert command execution result.
		/// </summary>
		private readonly Func<Task> undoAction = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionCommand"/> class.
		/// </summary>
		/// <param name="executeAction">The asynchronous action to execute.</param>
		/// <param name="undoAction">The asynchronous action to revert command execution result.</param>
		public ActionCommand(Func<Task> executeAction, Func<Task> undoAction)
		{
			this.executeAction = executeAction;
			this.undoAction = undoAction;
		}

		#endregion

		#region ICommand Implementations

		#region Public Properties

		/// <summary>
		/// Gets a value indicating whether the command is completed.
		/// </summary>
		/// <value>
		/// <c>true</c> if the command is completed; otherwise, it returns <c>false</c>.
		/// </value>
		public virtual bool IsCompleted { get; private set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Executes the command.
		/// </summary>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public virtual async Task ExecuteAsync()
		{
			if (this.executeAction != null)
			{
				await this.executeAction();

				this.IsCompleted = true;
			}
		}

		/// <summary>
		/// Reverts the command results to previous state.
		/// </summary>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public virtual async Task UndoAsync()
		{
			if (this.undoAction != null)
			{
				await this.undoAction();

				this.IsCompleted = false;
			}
		}

		#endregion

		#endregion
	}
}
