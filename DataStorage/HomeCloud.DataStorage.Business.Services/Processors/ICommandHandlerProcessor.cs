namespace HomeCloud.DataStorage.Business.Services.Processors
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Services.Handlers;

	#endregion

	/// <summary>
	/// Defines methods to process execution of the set of attached command handlers.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Services.Handlers.IDataCommandHandlerFactory" />
	public interface ICommandHandlerProcessor : IDataCommandHandlerFactory
	{
		/// <summary>
		/// Attaches the handler to the processor.
		/// </summary>
		/// <param name="handler">The command handler instance of <see cref="ICommandHandler"/>.</param>
		void AddHandler(ICommandHandler handler);

		/// <summary>
		/// Processes the execution of attached command handlers..
		/// </summary>
		void Process();
	}
}
