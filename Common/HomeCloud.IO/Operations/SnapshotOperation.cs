namespace HomeCloud.IO.Operations
{
	/// <summary>
	/// Takes a snapshot of a file to rollback the file later if needed.
	/// </summary>
	public sealed class SnapshotOperation : Operation
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SnapshotOperation"/> class.
		/// </summary>
		/// <param name="path">The file path.</param>
		public SnapshotOperation(string path)
			: base(path)
		{
		}

		#endregion

		#region Operation Overrides

		/// <summary>
		/// Executes the operation.
		/// </summary>
		public override void Execute()
		{
			this.Backup();
		}

		/// <summary>
		/// Rolls back the operation and restores the original state.
		/// </summary>
		public override void Rollback()
		{
		}

		#endregion
	}
}
