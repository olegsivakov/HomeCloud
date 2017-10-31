namespace HomeCloud.DataStorage.Business.Commands
{
	#region Usings

	using System;

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
		private readonly Action executeAction = null;

		/// <summary>
		/// The action to revert command execution result.
		/// </summary>
		private readonly Action undoAction = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionCommand"/> class.
		/// </summary>
		/// <param name="executeAction">The action to execute.</param>
		/// <param name="undoAction">The action to revert command execution result.</param>
		public ActionCommand(Action executeAction, Action undoAction)
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
		public virtual void Execute()
		{
			if (this.executeAction != null)
			{
				this.executeAction();

				this.IsCompleted = true;
			}
		}

		/// <summary>
		/// Reverts the command results to previous state.
		/// </summary>
		public virtual void Undo()
		{
			if (this.undoAction != null)
			{
				this.undoAction();

				this.IsCompleted = false;
			}
		}

		#endregion

		#endregion
	}
}
