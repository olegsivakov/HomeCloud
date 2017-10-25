namespace HomeCloud.DataStorage.Business.Services.Commands
{
	#region Usings

	using System;

	using HomeCloud.DataStorage.Business.Services.Providers;
	using HomeCloud.Business.Services;

	#endregion

	public interface IDataCommandFactory
	{
		ICommand CreateCommand<TProvider>(Action<IDataProvider> executeAction, Action<IDataProvider> undoAction)
			where TProvider : IDataProvider;
	}
}
