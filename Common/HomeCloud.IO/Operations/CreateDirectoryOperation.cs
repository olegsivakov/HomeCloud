namespace HomeCloud.IO.Operations
{
	#region Usings

	using System.IO;

	#endregion

	/// <summary>
	/// Creates all directories and sub-directories in the specified path unless they already exist.
	/// </summary>
	public sealed class CreateDirectoryOperation : Operation
	{
		#region Constructors

		/// <summary>
		/// Instantiates the class.
		/// </summary>
		/// <param name="path">The directory path to create.</param>
		public CreateDirectoryOperation(string path)
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
			if (Directory.Exists(this.Path))
			{
				this.Backup();

				Directory.Delete(this.Path, true);
			}

			Directory.CreateDirectory(this.Path);
		}

		/// <summary>
		/// Rolls back the operation and restores the original state.
		/// </summary>
		public override void Rollback()
		{
			if (Directory.Exists(this.Path))
			{
				Directory.Delete(this.Path, true);
			}

			if (!string.IsNullOrWhiteSpace(this.BackupPath))
			{
				this.Restore();
			}
		}

		#endregion
	}
}
