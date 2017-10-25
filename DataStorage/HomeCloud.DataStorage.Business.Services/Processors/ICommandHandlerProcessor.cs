namespace HomeCloud.DataStorage.Business.Services.Processors
{
	#region Usings

	using HomeCloud.DataStorage.Business.Services.Handlers;
	using HomeCloud.Business.Services;

	#endregion

	public interface ICommandHandlerProcessor : IDataCommandHandlerFactory
	{
		void AddHandler(ICommandHandler handler);

		void Process();
	}
}
