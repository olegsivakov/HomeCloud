namespace HomeCloud.DataStorage.Business.Components.Processors
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Business.Services.Commands;
	using HomeCloud.DataStorage.Business.Services.Handlers;
	using HomeCloud.DataStorage.Business.Services.Processors;

	#endregion

	/// <summary>
	/// Provides methods to process execution of the set of attached command handlers.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Components.Handlers.DataCommandHandlerFactory" />
	/// <seealso cref="HomeCloud.DataStorage.Business.Services.Processors.ICommandHandlerProcessor" />
	public class CommandHandlerProcessor : ICommandHandlerProcessor
	{
		#region Private Members

		/// <summary>
		/// The list of command handlers.
		/// </summary>
		private readonly IList<ICommandHandler> handlers = new List<ICommandHandler>();

		/// <summary>
		/// The command handler factory
		/// </summary>
		private readonly IServiceFactory<IDataCommandHandler> commandHandlerFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandHandlerProcessor" /> class.
		/// </summary>
		/// <param name="commandFactory">The action command factory.</param>
		/// <param name="commandHandlerFactory">The command handler factory.</param>
		public CommandHandlerProcessor(IActionCommandFactory commandFactory, IServiceFactory<IDataCommandHandler> commandHandlerFactory)
		{
			this.commandHandlerFactory = commandHandlerFactory;
		}

		#endregion

		#region ICommandHandlerProcessor Implementations

		/// <summary>
		/// Attaches the handler to the processor.
		/// </summary>
		/// <param name="handler">The command handler instance of <see cref="T:HomeCloud.Core.ICommandHandler" />.</param>
		public void AddHandler(ICommandHandler handler)
		{
			this.handlers.Add(handler);
		}

		/// <summary>
		/// Creates and attaches the instance of <see cref="T:HomeCloud.DataStorage.Business.Services.Handlers.IDataCommandHandler" /> type to execute data command.
		/// </summary>
		/// <typeparam name="T">The type of the handler derived from <see cref="T:HomeCloud.DataStorage.Business.Services.Handlers.IDataCommandHandler" />.</typeparam>
		/// <returns>
		/// The instance of <see cref="IDataCommandHandler" />.
		/// </returns>
		public IDataCommandHandler CreateDataHandler<T>() where T : IDataCommandHandler
		{
			IDataCommandHandler handler = this.commandHandlerFactory.Get<T>();

			this.AddHandler(handler);

			return handler;
		}

		/// <summary>
		/// Processes the execution of attached command handlers..
		/// </summary>
		public void Process()
		{
			ICommandHandler current = null;

			try
			{
				foreach (ICommandHandler handler in this.handlers)
				{
					current = handler;

					handler.Execute();
				}
			}
			catch (Exception exception)
			{
				int currentIndex = this.handlers.IndexOf(current);

				for (int index = currentIndex - 1; index >= 0; --index)
				{
					current = this.handlers[index];

					current.Undo();
				}

				throw exception;
			}
		}

		#endregion
	}
}
