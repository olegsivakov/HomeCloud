namespace HomeCloud.Core
{
	#region Usings

	using System.Threading.Tasks;

	#endregion

	/// <summary>
	/// Defines methods to handle command of <see cref="ICommand"/>.
	/// </summary>
	public interface ICommandHandler
	{
		/// <summary>
		/// Sets the command to execute.
		/// </summary>
		/// <param name="command">The command.</param>
		void SetCommand(ICommand command);

		/// <summary>
		/// Executes the command.
		/// </summary>
		/// <returns>The asynchronous operation.</returns>
		Task ExecuteAsync();

		/// <summary>
		/// Reverts the result of command execution.
		/// </summary>
		/// <returns>The asynchronous operation.</returns>
		Task UndoAsync();
	}
}
