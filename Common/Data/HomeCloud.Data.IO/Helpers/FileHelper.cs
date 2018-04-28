namespace HomeCloud.Data.IO.Helpers
{
	#region Usings

	using System;
	using System.IO;
	using System.Linq;

	#endregion

	/// <summary>
	/// Provides helper methods to file.
	/// </summary>
	internal static class FileHelper
	{
		#region Private Members

		/// <summary>
		/// The temporary folder
		/// </summary>
		private static readonly string temporaryFolder = Path.Combine(Path.GetTempPath(), "CdFileMgr");

		#endregion

		#region Public Methods

		/// <summary>
		/// Ensures that the folder that contains the temporary files exists.
		/// </summary>
		public static void EnsureTemporaryFolderExists()
		{
			if (!Directory.Exists(temporaryFolder))
			{
				Directory.CreateDirectory(temporaryFolder);
			}
		}

		/// <summary>
		/// Returns a unique temporary file name.
		/// </summary>
		/// <param name="extension"></param>
		/// <returns></returns>
		public static string GetTemporaryFileName(string extension)
		{
			return Path.Combine(temporaryFolder, Guid.NewGuid().ToString().Substring(0, 16)) + extension;
		}

		/// <summary>
		/// Gets a temporary directory .
		/// </summary>
		/// <param name="parentPath">The path to the parent directory.</param>
		/// <param name="prefix">The prefix of the directory name.</param>
		/// <returns>
		/// The path to the newly created temporary directory. The temporary directory is created automatically.
		/// </returns>
		public static string GetTemporaryDirectory(string parentPath = null, string prefix = null)
		{
			return Path.Combine(parentPath ?? temporaryFolder, prefix ?? string.Empty + Guid.NewGuid().ToString().Substring(0, 16));
		}

		/// <summary>
		/// Returns true if the given path is a directory.
		/// </summary>
		/// <param name="path">The path</param>
		/// <returns>True if the path is a directory one. Otherwise it returns false.</returns>
		public static bool IsDirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException(nameof(path));
			}
			path = path.Trim();

			if (Directory.Exists(path))
			{
				return true;
			}

			if (File.Exists(path))
			{
				return false;
			}

			if (new[] { "\\", "/" }.Any(x => path.EndsWith(x)))
			{
				return true;
			}

			return string.IsNullOrWhiteSpace(Path.GetExtension(path));
		}

		#endregion
	}
}
