namespace HomeCloud.DataStorage.Business.Components.Commands
{
	#region Usings

	using System;

	using HomeCloud.DataStorage.Business.Services.Providers;
	using HomeCloud.Business.Services;

	#endregion

	public class ActionCommand : ICommand
	{
		#region Private Members

		private readonly Action executeAction = null;

		private readonly Action undoAction = null;

		#endregion

		#region Constructors

		public ActionCommand(Action executeAction, Action undoAction)
		{
			this.executeAction = executeAction;
			this.undoAction = undoAction;
		}

		#endregion

		#region IDataCommand<TProvider> Implementations

		#region Public Properties

		public bool IsCompleted { get; private set; }

		#endregion

		#region Public Methods

		public virtual void Execute()
		{
			if (this.executeAction != null)
			{
				this.executeAction();

				this.IsCompleted = true;
			}
		}

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
