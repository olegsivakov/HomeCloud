namespace HomeCloud.DataStorage.Business.Services.Handlers
{
	#region Usings

	using System;

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Services.Providers;

	#endregion

	/// <summary>
	/// Defines methods to handle the command that executes the specified action against the data provided by <see cref="IDataProvider"/>.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ICommandHandler" />
	public interface IDataCommandHandler : ICommandHandler
	{
		/// <summary>
		/// Gets the data provider.
		/// </summary>
		/// <value>
		/// The data provider.
		/// </value>
		IDataProvider Provider { get; }

		/// <summary>
		/// Creates the data command.
		/// </summary>
		/// <param name="executeAction">The action to execute.</param>
		/// <param name="undoAction">The action to revert command execution result.</param>
		/// <returns>The command of <see cref="ICommand"/>.</returns>
		ICommand CreateCommand(Action<IDataProvider> executeAction, Action<IDataProvider> undoAction);
	}
}
