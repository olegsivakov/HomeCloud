namespace HomeCloud.DataStorage.Business.Services.Handlers
{
	#region Usings

	using System;

	using HomeCloud.Business.Services;
	using HomeCloud.DataStorage.Business.Services.Providers;

	#endregion

	public interface IDataCommandHandler : ICommandHandler
	{
		IDataProvider Provider { get; }

		ICommand CreateCommand(Action<IDataProvider> executeAction, Action<IDataProvider> undoAction);
	}
}
