namespace HomeCloud.DataStorage.Business.Services.Handlers
{
	#region Usings

	using HomeCloud.DataStorage.Business.Services.Commands;
	using HomeCloud.Business.Services;

	#endregion

	public interface IDataCommandHandler : ICommandHandler, IDataCommandFactory
	{
	}
}
