namespace HomeCloud.IO.Operations
{
	#region Usings

	using System.IO;

	#endregion

	/// <summary>
	/// Opens a file, appends the specified string to the file, and then closes the file. If the file does not exist, this method creates a file, writes the specified string to the file, then closes the file.
	/// </summary>
	public sealed class AppendAllTextOperation : Operation
	{
		#region Private Members

		/// <summary>
		/// The content to append
		/// </summary>
		private readonly string content = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AppendAllTextOperation"/> class.
		/// </summary>
		/// <param name="path">The file path.</param>
		/// <param name="content">The content to append.</param>
		public AppendAllTextOperation(string path, string content)
			: base(path)
		{
			this.content = content;
		}

		#endregion

		#region Operation Overrides

		/// <summary>
		/// Executes the operation.
		/// </summary>
		public override void Execute()
		{
			this.Backup();

			File.AppendAllText(this.Path, content);
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
