namespace HomeCloud.Core
{
	#region Usings

	using System.Threading.Tasks;

	#endregion

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
		/// <returns>The asynchronous operation.</returns>
		Task ExecuteAsync();

		/// <summary>
		/// Reverts the command results to previous state.
		/// </summary>
		/// <returns>The asynchronous operation.</returns>
		Task UndoAsync();
	}
}
