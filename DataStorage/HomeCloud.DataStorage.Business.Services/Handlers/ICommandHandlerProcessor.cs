namespace HomeCloud.DataStorage.Business.Services.Handlers
{
	public interface ICommandHandlerProcessor
	{
		void Process();

		void Undo();
	}
}
