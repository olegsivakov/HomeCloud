namespace HomeCloud.Business.Services
{
	public interface ICommandHandler
	{
		void Handle(ICommand command);
	}
}
