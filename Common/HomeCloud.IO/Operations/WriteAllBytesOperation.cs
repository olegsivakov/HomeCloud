namespace HomeCloud.IO.Operations
{
	#region Usings

	using System.IO;

	#endregion

	/// <summary>
	/// Creates a new file, writes the specified byte array to the file, and then closes the file. If the target file already exists, it is overwritten.
	/// </summary>
	public sealed class WriteAllBytesOperation : Operation
	{
		#region Private Members

		/// <summary>
		/// The byte content
		/// </summary>
		private readonly byte[] content = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="WriteAllBytesOperation"/> class.
		/// </summary>
		/// <param name="path">The file path.</param>
		/// <param name="content">The byte content to write.</param>
		public WriteAllBytesOperation(string path, byte[] content)
			: base(path)
		{
			this.content = content ?? new byte[0];
		}

		#endregion

		#region Operation Overrides

		/// <summary>
		/// Executes the operation.
		/// </summary>
		public override void Execute()
		{
			this.Backup();

			File.WriteAllBytes(this.Path, this.content);
		}

		/// <summary>
		/// Rolls back the operation and restores the original state.
		/// </summary>
		public override void Rollback()
		{
			this.Restore();
		}

		#endregion
	}
}