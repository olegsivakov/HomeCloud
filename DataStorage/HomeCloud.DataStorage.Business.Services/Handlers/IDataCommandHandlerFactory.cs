namespace HomeCloud.DataStorage.Business.Services.Handlers
{
	#region Usings

	using HomeCloud.Core;

	#endregion

	public interface IDataCommandHandlerFactory
	{
		IDataCommandHandler CreateHandler<T>() where T : IDataCommandHandler;
	}
}
