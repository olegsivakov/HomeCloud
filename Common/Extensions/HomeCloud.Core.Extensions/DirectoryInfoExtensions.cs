namespace HomeCloud.Core.Extensions
{
	#region Usings

	using System.IO;

	#endregion

	/// <summary>
	/// Provides extension methods for <see cref="DirectoryInfo"/> instances.
	/// </summary>
	public static class DirectoryInfoExtensions
	{
		/// <summary>
		/// Copies the directory recursively to the specified path.
		/// </summary>
		/// <param name="directory">The directory.</param>
		/// <param name="path">The destination path.</param>
		/// <returns>The destination directory.</returns>
		public static DirectoryInfo Copy(this DirectoryInfo directory, string path)
		{
			DirectoryInfo destination = Directory.Exists(path) ? new DirectoryInfo(path) : Directory.CreateDirectory(path);

			foreach (FileInfo sourceFile in directory.GetFiles())
			{
				sourceFile.CopyTo(Path.Combine(destination.FullName, sourceFile.Name));
			}

			foreach (DirectoryInfo sourceSubDirectory in directory.GetDirectories())
			{
				string destinationSubDirectoryPath = Path.Combine(destination.FullName, sourceSubDirectory.Name);
				sourceSubDirectory.Copy(destinationSubDirectoryPath);
			}

			return destination;
		}

		/// <summary>
		/// Moves a directory, recursively, from one path to another. The method supports moving of directories across the volumes.
		/// </summary>
		/// <param name="directory">The directory.</param>
		/// <param name="path">The destination path.</param>
		public static void Move(this DirectoryInfo directory, string path)
		{
			if (directory.Root.FullName == Directory.GetDirectoryRoot(path))
			{
				directory.MoveTo(path);
			}
			else
			{
				directory.Copy(path);
				directory.Delete(true);
			}
		}
	}
}
