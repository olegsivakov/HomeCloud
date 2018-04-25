namespace HomeCloud.IO.Operations
{
	#region Usings

	using System.IO;

	using HomeCloud.IO.Extensions;
	using HomeCloud.IO.Helpers;

	#endregion

	/// <summary>
	/// Copies an existing file or directory to a new destination. Overwriting a file with the same name is not allowed by default.
	/// </summary>
	public sealed class CopyOperation : Operation
	{
		#region Private Members

		/// <summary>
		/// The destination path
		/// </summary>
		private readonly string sourcePath = null;

		/// <summary>
		/// Indicates whether the file with the same is allowed to overwrite.
		/// </summary>
		private readonly bool overwrite = false;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CopyOperation"/> class.
		/// </summary>
		/// <param name="sourcePath">The source path.</param>
		/// <param name="destinationPath">The destination path.</param>
		/// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
		public CopyOperation(string sourcePath, string destinationPath, bool overwrite)
			: base(destinationPath)
		{
			this.sourcePath = sourcePath;
			this.overwrite = overwrite;
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
				if (Directory.Exists(this.Path) && this.overwrite)
				{
					Directory.Delete(this.Path, true);
				}

				DirectoryInfo source = new DirectoryInfo(this.sourcePath);
				source.Copy(this.Path);
			}
			else
			{
				File.Copy(this.sourcePath, this.Path, this.overwrite);
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
