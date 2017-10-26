namespace HomeCloud.DataStorage.Business.Components.Commands
{
	#region Usings

	using System;

	using HomeCloud.DataStorage.Business.Services.Commands;
	using HomeCloud.DataStorage.Business.Services.Providers;

	using HomeCloud.Business.Services;

	#endregion

	public class ActionCommandFactory : IActionCommandFactory
	{
		#region Private Members

		private readonly IDataProviderFactory dataProviderFactory = null;

		#endregion

		#region Constructors

		public ActionCommandFactory(IDataProviderFactory dataProviderFactory)
		{
			this.dataProviderFactory = dataProviderFactory;
		}

		#endregion

		#region IDataCommandFactory Implementations

		public virtual ICommand CreateCommand(Action executeAction, Action undoAction)
		{
			return new ActionCommand(executeAction, undoAction);
		}

		public virtual ICommand CreateCommand(IDataProvider provider, Action<IDataProvider> executeAction, Action<IDataProvider> undoAction)
		{
			return new DataCommand(provider, executeAction, undoAction);
		}

		public virtual ICommand CreateCommand<TProvider>(Action<IDataProvider> executeAction, Action<IDataProvider> undoAction)
			where TProvider : IDataProvider
		{
			IDataProvider provider = this.dataProviderFactory.GetProvider<TProvider>();

			return this.CreateCommand(provider, executeAction, undoAction);
		}

		#endregion
	}
}
