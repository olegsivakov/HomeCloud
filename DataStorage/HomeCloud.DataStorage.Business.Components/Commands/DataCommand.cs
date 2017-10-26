namespace HomeCloud.DataStorage.Business.Components.Commands
{
	#region Usings

	using System;

	using HomeCloud.DataStorage.Business.Services.Providers;

	#endregion

	public class DataCommand : ActionCommand
	{
		#region Private Members

		private readonly IDataProvider provider = null;

		#endregion

		#region Constructors

		public DataCommand(IDataProvider provider, Action<IDataProvider> executeAction, Action<IDataProvider> undoAction)
			: base(() => executeAction(provider), () => undoAction(provider))
		{
			this.provider = provider;
		}

		#endregion

		#region IDataCommand<TProvider> Implementations

		#region Public Methods

		public override void Execute()
		{
			if (this.provider != null)
			{
				base.Execute();
			}
		}

		public override void Undo()
		{
			if (this.provider != null)
			{
				base.Undo();
			}
		}

		#endregion

		#endregion
	}
}
