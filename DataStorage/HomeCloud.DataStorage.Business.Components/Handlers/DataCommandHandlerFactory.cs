namespace HomeCloud.DataStorage.Business.Components.Handlers
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.DataStorage.Business.Services.Handlers;
	using HomeCloud.DataStorage.Business.Services.Providers;

	using HomeCloud.DataStorage.Business.Services.Commands;

	#endregion

	public class DataCommandHandlerFactory : IDataCommandHandlerFactory
	{
		#region Private Members

		private readonly IDataProviderFactory providerFactory = null;

		private readonly IActionCommandFactory commandFactory = null;

		private readonly IDictionary<Type, Type> container = new Dictionary<Type, Type>();

		#endregion

		#region Constructors

		public DataCommandHandlerFactory(IActionCommandFactory commandFactory, IDataProviderFactory providerFactory)
		{
			this.commandFactory = commandFactory;
			this.providerFactory = providerFactory;

			container.Add(typeof(IDataStoreCommandHandler), typeof(DataStoreCommandHandler));
		}

		#endregion

		#region IDataCommandHandlerFactory Implementations

		public virtual IDataCommandHandler CreateHandler<T>() where T : IDataCommandHandler
		{
			Type type = typeof(T);

			if (!this.container.ContainsKey(type))
			{
				return default(T);
			}

			return (T)Activator.CreateInstance(this.container[type], this.commandFactory, this.providerFactory);
		}

		#endregion
	}
}
