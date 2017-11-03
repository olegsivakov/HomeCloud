namespace HomeCloud.DataStorage.Business.Handlers
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Providers;

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
		/// Creates the asynchronous data command.
		/// </summary>
		/// <param name="executeAsyncAction">The asynchronous action to execute.</param>
		/// <param name="undoAsyncAction">The asynchronous action to undo.</param>
		/// <returns>
		/// The command of <see cref="ICommand" />.
		/// </returns>
		ICommand CreateAsyncCommand(Func<IDataProvider, Task> executeAsyncAction, Func<IDataProvider, Task> undoAsyncAction);
	}
}
