namespace HomeCloud.Business.Contracts
{
	/// <summary>
	/// Defines the command.
	/// </summary>
	public interface ICommand
	{
		/// <summary>
		/// Gets a value indicating whether the command is completed.
		/// </summary>
		/// <value>
		///   <c>true</c> if the command is completed; otherwise, it returns <c>false</c>.
		/// </value>
		bool IsCompleted { get; }

		/// <summary>
		/// Executes the command.
		/// </summary>
		void Execute();

		/// <summary>
		/// Reverts the command results to previous state.
		/// </summary>
		void Undo();
	}
}
