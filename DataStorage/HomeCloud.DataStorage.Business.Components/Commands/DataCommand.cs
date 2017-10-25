namespace HomeCloud.DataStorage.Business.Components.Commands
{
	#region Usings

	using System;

	using HomeCloud.DataStorage.Business.Services.Providers;
	using HomeCloud.Business.Services;

	#endregion

	public class DataCommand : ICommand
	{
		#region Private Members

		private readonly IDataProvider provider = null;

		private readonly Action<IDataProvider> executeAction = null;

		private readonly Action<IDataProvider> undoAction = null;

		#endregion

		#region Constructors

		public DataCommand(IDataProvider provider, Action<IDataProvider> executeAction, Action<IDataProvider> undoAction)
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

		public void Execute()
		{
			if (this.provider != null && this.executeAction != null)
			{
				this.executeAction(this.provider);

				this.IsCompleted = true;
			}
		}

		public void Undo()
		{
			if (this.provider != null && this.undoAction != null)
			{
				this.undoAction(this.provider);

				this.IsCompleted = false;
			}
		}

		#endregion

		#endregion
	}
}
