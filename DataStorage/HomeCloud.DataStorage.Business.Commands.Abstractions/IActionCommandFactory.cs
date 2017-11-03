namespace HomeCloud.DataStorage.Business.Commands
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Providers;

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
		ICommand CreateCommand(Func<Task> executeAction, Func<Task> undoAction);

		/// <summary>
		/// Creates the command that executes the specified action against the data provided by the <see cref="IDataProvider"/> provider.
		/// </summary>
		/// <param name="provider">The data provider instance of <see cref="IDataProvider"/>.</param>
		/// <param name="executeAction">The action to execute.</param>
		/// <param name="undoAction">The action to revert command execution result.</param>
		/// <returns>The command of <see cref="ICommand"/>.</returns>
		ICommand CreateCommand(IDataProvider provider, Func<IDataProvider, Task> executeAction, Func<IDataProvider, Task> undoAction);

		/// <summary>
		/// Creates the command that executes the specified action against the data provided by the data provider of the specified <see cref="IDataProvider"/> type.
		/// </summary>
		/// <typeparam name="TProvider">The type of the data provider derived from <see cref="IDataProvider"/>.</typeparam>
		/// <param name="executeAction">The action to execute.</param>
		/// <param name="undoAction">The action to revert command execution result.</param>
		/// <returns>The command of <see cref="ICommand"/>.</returns>
		ICommand CreateCommand<TProvider>(Func<IDataProvider, Task> executeAction, Func<IDataProvider, Task> undoAction)
			where TProvider : IDataProvider;
	}
}
