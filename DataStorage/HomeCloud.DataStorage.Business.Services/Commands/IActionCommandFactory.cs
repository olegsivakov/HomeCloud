namespace HomeCloud.DataStorage.Business.Services.Commands
{
	#region Usings

	using System;

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Services.Providers;

	#endregion

	/// <summary>
	/// Defines methods to create the instance of <see cref="ICommand"/> that executes the specified action.
	/// </summary>
	public interface IActionCommandFactory
	{
		/// <summary>
		/// Creates the command that executes the specified action.
		/// </summary>
		/// <param name="executeAction">The action to execute.</param>
		/// <param name="undoAction">The action to revert command execution result.</param>
		/// <returns>The command of <see cref="ICommand"/>.</returns>
		ICommand CreateCommand(Action executeAction, Action undoAction);

		/// <summary>
		/// Creates the command that executes the specified action against the data provided by the <see cref="IDataProvider"/> provider.
		/// </summary>
		/// <param name="provider">The data provider instance of <see cref="IDataProvider"/>.</param>
		/// <param name="executeAction">The action to execute.</param>
		/// <param name="undoAction">The action to revert command execution result.</param>
		/// <returns>The command of <see cref="ICommand"/>.</returns>
		ICommand CreateCommand(IDataProvider provider, Action<IDataProvider> executeAction, Action<IDataProvider> undoAction);

		/// <summary>
		/// Creates the command that executes the specified action against the data provided by the data provider of the specified <see cref="IDataProvider"/> type.
		/// </summary>
		/// <typeparam name="TProvider">The type of the data provider derived from <see cref="IDataProvider"/>.</typeparam>
		/// <param name="executeAction">The action to execute.</param>
		/// <param name="undoAction">The action to revert command execution result.</param>
		/// <returns>The command of <see cref="ICommand"/>.</returns>
		ICommand CreateCommand<TProvider>(Action<IDataProvider> executeAction, Action<IDataProvider> undoAction)
			where TProvider : IDataProvider;
	}
}
