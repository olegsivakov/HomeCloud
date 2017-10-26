namespace HomeCloud.DataStorage.Business.Services.Handlers
{
	/// <summary>
	/// Defines methods to create handlers that execute data commands.
	/// </summary>
	public interface IDataCommandHandlerFactory
	{
		/// <summary>
		/// Creates the instance of <see cref="IDataCommandHandler"/> type to execute data command.
		/// </summary>
		/// <typeparam name="T">The type of the handler derived from <see cref="IDataCommandHandler"/>.</typeparam>
		/// <returns></returns>
		IDataCommandHandler CreateHandler<T>() where T : IDataCommandHandler;
	}
}
