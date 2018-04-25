namespace HomeCloud.IO.Operations
{
	#region Usings

	using System.IO;

	using HomeCloud.IO.Extensions;
	using HomeCloud.IO.Helpers;

	#endregion

	/// <summary>
	///  Moves a specified file or directory to a new location, providing the option to specify a path.
	/// </summary>
	public sealed class MoveOperation : Operation
	{
		#region Private Members

		/// <summary>
		/// The destination path
		/// </summary>
		private readonly string destinationPath = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MoveOperation" /> class.
		/// </summary>
		/// <param name="sourcePath">The source path.</param>
		/// <param name="destinationPath">The destination path.</param>
		public MoveOperation(string sourcePath, string destinationPath)
			: base(sourcePath)
		{
			this.destinationPath = destinationPath;
		}

		#endregion

		#region Operation Implementations

		/// <summary>
		/// Executes the operation.
		/// </summary>
		public override void Execute()
		{
			this.Backup();

			if (FileHelper.IsDirectory(this.Path))
			{
				DirectoryInfo directory = new DirectoryInfo(this.Path);
				directory.Move(this.destinationPath);
			}
			else
			{
				File.Move(this.Path, this.destinationPath);
			}
		}

		/// <summary>
		/// Rolls back the operation and restores the original state.
		/// </summary>
		public override void Rollback()
		{
			this.Restore();

			if (FileHelper.IsDirectory(this.destinationPath))
			{
				if (Directory.Exists(this.destinationPath))
				{
					Directory.Delete(this.destinationPath, true);
				}
			}
			else
			{
				if (File.Exists(this.destinationPath))
				{
					File.Delete(this.destinationPath);
				}
			}

			
		}

		#endregion
	}
}
