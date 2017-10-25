namespace HomeCloud.Business.Services
{
	/// <summary>
	/// Defines methods to handle command of <see cref="ICommand"/>.
	/// </summary>
	public interface ICommandHandler
	{
		/// <summary>
		/// Gets the command to handle.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> command.
		/// </value>
		ICommand Command { get; }

		/// <summary>
		/// Handles the execution of specified command.
		/// </summary>
		void Handle();

		/// <summary>
		/// Reverts changes made by command.
		/// </summary>
		void Undo();
	}
}
