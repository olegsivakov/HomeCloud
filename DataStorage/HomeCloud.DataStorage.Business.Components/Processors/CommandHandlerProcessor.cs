namespace HomeCloud.DataStorage.Business.Components.Processors
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.DataStorage.Business.Components.Handlers;
	using HomeCloud.DataStorage.Business.Services.Processors;
	using HomeCloud.DataStorage.Business.Services.Providers;

	using HomeCloud.Business.Services;

	#endregion

	public class CommandHandlerProcessor : DataCommandHandlerFactory, ICommandHandlerProcessor
	{
		#region Private Members

		private readonly IList<ICommandHandler> handlers = new List<ICommandHandler>();

		#endregion

		#region Constructors

		public CommandHandlerProcessor(IDataProviderFactory providerFactory)
			: base(providerFactory)
		{
		}

		#endregion

		#region ICommandHandlerProcessor Implementations

		public void AddHandler(ICommandHandler handler)
		{
			this.handlers.Add(handler);
		}

		public void Process()
		{
			ICommandHandler current = null;

			try
			{
				foreach (ICommandHandler handler in this.handlers)
				{
					current = handler;

					handler.Handle();
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
