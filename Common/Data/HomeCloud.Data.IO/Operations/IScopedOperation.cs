namespace HomeCloud.Data.IO.Operations
{
	/// <summary>
	/// Represents a transactional file operation.
	/// </summary>
	public interface IScopedOperation
	{
		/// <summary>
		/// Executes the operation.
		/// </summary>
		void Execute();

		/// <summary>
		/// Rolls back the operation and restores the original state.
		/// </summary>
		void Rollback();
	}
}
