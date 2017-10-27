namespace HomeCloud.DataStorage.Business.Components.Handlers
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.DataStorage.Business.Services.Commands;
	using HomeCloud.DataStorage.Business.Services.Handlers;
	using HomeCloud.DataStorage.Business.Services.Providers;

	#endregion

	/// <summary>
	/// Provides methods to create handlers that execute data commands.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Services.Handlers.IDataCommandHandlerFactory" />
	public class DataCommandHandlerFactory : IDataCommandHandlerFactory
	{
		#region Private Members

		/// <summary>
		/// The data provider factory
		/// </summary>
		private readonly IDataProviderFactory providerFactory = null;

		/// <summary>
		/// The action command factory
		/// </summary>
		private readonly IActionCommandFactory commandFactory = null;

		/// <summary>
		/// The handler container
		/// </summary>
		private readonly IDictionary<Type, Type> container = new Dictionary<Type, Type>();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DataCommandHandlerFactory"/> class.
		/// </summary>
		/// <param name="commandFactory">The action command factory.</param>
		/// <param name="providerFactory">The data provider factory.</param>
		public DataCommandHandlerFactory(IActionCommandFactory commandFactory, IDataProviderFactory providerFactory)
		{
			this.commandFactory = commandFactory;
			this.providerFactory = providerFactory;

			this.container.Add(typeof(IDataStoreCommandHandler), typeof(DataStoreCommandHandler));
		}

		#endregion

		#region IDataCommandHandlerFactory Implementations

		/// <summary>
		/// Creates the instance of <see cref="T:HomeCloud.DataStorage.Business.Services.Handlers.IDataCommandHandler" /> type to execute data command.
		/// </summary>
		/// <typeparam name="T">The type of the handler derived from <see cref="T:HomeCloud.DataStorage.Business.Services.Handlers.IDataCommandHandler" />.</typeparam>
		/// <returns>The instance of <see cref="IDataCommandHandler"/>.</returns>
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
