namespace HomeCloud.DataStorage.Business.Services.Commands
{
	#region Usings

	using System;

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Services.Providers;

	#endregion

	public interface IActionCommandFactory
	{
		ICommand CreateCommand(Action executeAction, Action undoAction);

		ICommand CreateCommand(IDataProvider provider, Action<IDataProvider> executeAction, Action<IDataProvider> undoAction);

		ICommand CreateCommand<TProvider>(Action<IDataProvider> executeAction, Action<IDataProvider> undoAction)
			where TProvider : IDataProvider;
	}
}
