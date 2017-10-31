namespace HomeCloud.DataStorage.Business.Handlers
{
	#region Usings

	using HomeCloud.Core;

	#endregion

	/// <summary>
	/// Defines methods to process execution of the set of attached command handlers.
	/// </summary>
	public interface ICommandHandlerProcessor
	{
		/// <summary>
		/// Attaches the handler to the processor.
		/// </summary>
		/// <param name="handler">The command handler instance of <see cref="ICommandHandler"/>.</param>
		void AddHandler(ICommandHandler handler);

		/// <summary>
		/// Creates and attaches the instance of <see cref="T:HomeCloud.DataStorage.Business.Handlers.IDataCommandHandler" /> type to execute data command.
		/// </summary>
		/// <typeparam name="T">The type of the handler derived from <see cref="T:HomeCloud.DataStorage.Business.Handlers.IDataCommandHandler" />.</typeparam>
		/// <returns>
		/// The instance of <see cref="IDataCommandHandler" />.
		/// </returns>
		IDataCommandHandler CreateDataHandler<T>() where T : IDataCommandHandler;

		/// <summary>
		/// Processes the execution of attached command handlers..
		/// </summary>
		void Process();
	}
}
