namespace HomeCloud.DataStorage.Business.Handlers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Commands;

	#endregion

	/// <summary>
	/// Provides methods to process execution of the set of attached command handlers.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Handlers.ICommandHandlerProcessor" />
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
		private readonly IServiceFactory<ICommandHandler> commandHandlerFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandHandlerProcessor" /> class.
		/// </summary>
		/// <param name="commandFactory">The action command factory.</param>
		/// <param name="commandHandlerFactory">The command handler factory.</param>
		public CommandHandlerProcessor(IActionCommandFactory commandFactory, IServiceFactory<ICommandHandler> commandHandlerFactory)
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
		/// Creates and attaches the instance of <see cref="T:HomeCloud.DataStorage.Business.Handlers.IDataCommandHandler" /> type to execute data command.
		/// </summary>
		/// <typeparam name="T">The type of the handler derived from <see cref="T:HomeCloud.DataStorage.Business.Handlers.IDataCommandHandler" />.</typeparam>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataStorage.Business.Handlers.IDataCommandHandler" />.
		/// </returns>
		public IDataCommandHandler CreateDataHandler<T>() where T : IDataCommandHandler
		{
			IDataCommandHandler handler = this.commandHandlerFactory.Get<T>() as IDataCommandHandler;
			if (handler != null)
			{
				this.AddHandler(handler);
			}

			return handler;
		}

		/// <summary>
		/// Removes all attached handlers.
		/// </summary>
		public void RemoveHandlers()
		{
			this.handlers.Clear();
		}

		/// <summary>
		/// Processes the execution of attached command handlers..
		/// </summary>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public async Task ProcessAsync()
		{
			ICommandHandler current = null;

			try
			{
				foreach (ICommandHandler handler in this.handlers)
				{
					current = handler;

					await handler.ExecuteAsync();
				}
			}
			catch (Exception exception)
			{
				int currentIndex = this.handlers.IndexOf(current);

				for (int index = currentIndex - 1; index >= 0; --index)
				{
					current = this.handlers[index];

					await current.UndoAsync();
				}

				await Task.FromException(exception);
			}
		}

		#endregion
	}
}
