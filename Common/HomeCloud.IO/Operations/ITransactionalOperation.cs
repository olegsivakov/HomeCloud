namespace HomeCloud.IO
{
	/// <summary>
	/// Represents a transactional file operation.
	/// </summary>
	public interface ITransactionalOperation
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
