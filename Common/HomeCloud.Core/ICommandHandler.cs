namespace HomeCloud.Core
{
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
		/// Executes of specified command.
		/// </summary>
		void Execute();

		/// <summary>
		/// Reverts the result of command execution.
		/// </summary>
		void Undo();
	}
}
