namespace HomeCloud.IO.Operations
{
	#region Usings

	using System.IO;

	#endregion

	/// <summary>
	/// Creates or overwrites the specified file, specifying a buffer size and a System.IO.FileOptions value that describes how to create or overwrite the file.
	/// </summary>
	/// <seealso cref="HomeCloud.IO.Operations.Operation" />
	public sealed class CreateFileOperation : Operation
	{
		#region Private Members

		/// <summary>
		/// The source stream
		/// </summary>
		private Stream stream = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CreateFileOperation" /> class.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="stream">The source stream to create file from.</param>
		public CreateFileOperation(string path, Stream stream)
			: base(path)
		{
			this.stream = stream;
		}

		#endregion

		#region Operation Overrides

		/// <summary>
		/// Executes the operation.
		/// </summary>
		public override void Execute()
		{
			if (File.Exists(this.Path))
			{
				this.Backup();
			}

			using (FileStream file = File.Create(this.Path, 1024, FileOptions.WriteThrough))
			{
				stream.CopyTo(file);
			}
		}

		/// <summary>
		/// Rolls back the operation and restores the original state.
		/// </summary>
		public override void Rollback()
		{
			if (File.Exists(this.Path))
			{
				File.Delete(this.Path);
			}

			if (!string.IsNullOrWhiteSpace(this.BackupPath))
			{
				this.Restore();
			}
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public override void Dispose()
		{
			this.stream = null;
			base.Dispose();
		}

		#endregion
	}
}
