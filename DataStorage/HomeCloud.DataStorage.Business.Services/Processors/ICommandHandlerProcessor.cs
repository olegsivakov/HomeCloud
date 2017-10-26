namespace HomeCloud.DataStorage.Business.Services.Processors
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Services.Handlers;

	#endregion

	public interface ICommandHandlerProcessor : IDataCommandHandlerFactory
	{
		void AddHandler(ICommandHandler handler);

		void Process();
	}
}
