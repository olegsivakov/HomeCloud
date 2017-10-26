namespace HomeCloud.DataStorage.Business.Components.Commands
{
	#region Usings

	using System;

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Business.Services.Commands;
	using HomeCloud.DataStorage.Business.Services.Providers;

	#endregion

	/// <summary>
	/// Provides methods to create the instance of <see cref="ICommand"/> that executes the specified action.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Services.Commands.IActionCommandFactory" />
	public class ActionCommandFactory : IActionCommandFactory
	{
		#region Private Members

		/// <summary>
		/// The data provider factory.
		/// </summary>
		private readonly IDataProviderFactory dataProviderFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionCommandFactory"/> class.
		/// </summary>
		/// <param name="dataProviderFactory">The data provider factory.</param>
		public ActionCommandFactory(IDataProviderFactory dataProviderFactory)
		{
			this.dataProviderFactory = dataProviderFactory;
		}

		#endregion

		#region IDataCommandFactory Implementations

		/// <summary>
		/// Creates the command that executes the specified action.
		/// </summary>
		/// <param name="executeAction">The action to execute.</param>
		/// <param name="undoAction">The action to revert command execution result.</param>
		/// <returns>
		/// The command of <see cref="T:HomeCloud.Core.ICommand" />.
		/// </returns>
		public virtual ICommand CreateCommand(Action executeAction, Action undoAction)
		{
			return new ActionCommand(executeAction, undoAction);
		}

		/// <summary>
		/// Creates the command that executes the specified action against the data provided by the <see cref="T:HomeCloud.DataStorage.Business.Services.Providers.IDataProvider" /> provider.
		/// </summary>
		/// <param name="provider">The data provider instance of <see cref="T:HomeCloud.DataStorage.Business.Services.Providers.IDataProvider" />.</param>
		/// <param name="executeAction">The action to execute.</param>
		/// <param name="undoAction">The action to revert command execution result.</param>
		/// <returns>
		/// The command of <see cref="T:HomeCloud.Core.ICommand" />.
		/// </returns>
		public virtual ICommand CreateCommand(IDataProvider provider, Action<IDataProvider> executeAction, Action<IDataProvider> undoAction)
		{
			return new DataCommand(provider, executeAction, undoAction);
		}

		/// <summary>
		/// Creates the command that executes the specified action against the data provided by the data provider of the specified <see cref="T:HomeCloud.DataStorage.Business.Services.Providers.IDataProvider" /> type.
		/// </summary>
		/// <typeparam name="TProvider">The type of the data provider derived from <see cref="T:HomeCloud.DataStorage.Business.Services.Providers.IDataProvider" />.</typeparam>
		/// <param name="executeAction">The action to execute.</param>
		/// <param name="undoAction">The action to revert command execution result.</param>
		/// <returns>
		/// The command of <see cref="T:HomeCloud.Core.ICommand" />.
		/// </returns>
		public virtual ICommand CreateCommand<TProvider>(Action<IDataProvider> executeAction, Action<IDataProvider> undoAction)
			where TProvider : IDataProvider
		{
			IDataProvider provider = this.dataProviderFactory.GetProvider<TProvider>();

			return this.CreateCommand(provider, executeAction, undoAction);
		}

		#endregion
	}
}
