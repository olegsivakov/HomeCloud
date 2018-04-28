namespace HomeCloud.Data.IO.Operations
{
	#region Usings

	using System.IO;
	using HomeCloud.Data.IO.Helpers;

	#endregion

	/// <summary>
	/// Deletes the directory or file by specified path and any sub-directories and files in the directory.
	/// </summary>
	internal sealed class DeleteOperation : Operation
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DeleteOperation"/> class.
		/// </summary>
		/// <param name="path">The path to the directory or file to delete.</param>
		public DeleteOperation(string path)
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

			if (FileHelper.IsDirectory(this.Path))
			{
				Directory.Delete(this.Path);
			}
			else
			{
				File.Delete(this.Path);
			}
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
