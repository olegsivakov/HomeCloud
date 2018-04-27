namespace HomeCloud.Data.IO
{
	#region Usings

	using System;
	using System.IO;

	using HomeCloud.IO;

	using Microsoft.Extensions.Options;

	#endregion

	/// <summary>
	/// Provides methods to query data from file system.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.IO.IFileSystemContext" />
	public class FileSystemContext : IFileSystemContext
	{
		#region Private Members

		/// <summary>
		/// The file system client.
		/// </summary>
		private IFileSystemClient client = null;

		/// <summary>
		/// The configuration options.
		/// </summary>
		private readonly FileSystemOptions options = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemContext" /> class.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="accessor">The configuration options accessor.</param>
		/// <exception cref="System.ArgumentNullException">accessor or <see cref="IOptionsSnapshot{FileSystemOptions}.Value"/> or <see cref="FileSystemOptions.Root"/>.</exception>
		public FileSystemContext(IFileSystemClient client, IOptionsSnapshot<FileSystemOptions> accessor)
		{
			if (accessor is null)
			{
				throw new ArgumentNullException(nameof(accessor));
			}

			if (accessor.Value is null)
			{
				throw new ArgumentNullException(nameof(accessor.Value));
			}

			if (string.IsNullOrWhiteSpace(accessor.Value.Root))
			{
				throw new ArgumentNullException(nameof(accessor.Value.Root));
			}

			this.options = accessor.Value;
			this.client = client;
		}

		#endregion

		#region IFileSystemContext Implementations

		/// <summary>
		/// Copies the specified <paramref name="sourceFileName"/> to <paramref name="destFileName"/>.
		/// </summary>
		/// <param name="sourcePath">The file or directory path to copy from.</param>
		/// <param name="destFileName">The destination file or directory path to copy to.</param>
		/// <param name="overwrite">Indicates whether the destination file should be overwritten, otherwise false.</param>
		public void Copy(string sourcePath, string destinationPath, bool overwrite)
		{
			this.client.Copy(sourcePath, destinationPath, overwrite);
		}

		/// <summary>
		/// Creates directory by the specified path.
		/// </summary>
		/// <param name="path">The directory path to create.</param>
		public void CreateDirectory(string path)
		{
			this.client.CreateDirectory(path);
		}

		/// <summary>
		/// Creates the file by specified path and from the specified <see cref="Stream" />.
		/// </summary>
		/// <param name="path">The file path to create.</param>
		/// <param name="stream">The stream containing file data.</param>
		public void CreateFile(string path, Stream stream)
		{
			this.client.CreateFile(path, stream);
		}

		/// <summary>
		/// Deletes the specified file or directory. An exception is not thrown if the file does not exist.
		/// </summary>
		/// <param name="path">The path to the file or directory to delete.</param>
		public void Delete(string path)
		{
			this.client.Delete(path);
		}

		/// <summary>
		/// Moves the specified file or directory to the new location specified by <paramref name="destinationPath"/>.
		/// </summary>
		/// <param name="sourcePath">The source path to the file or directory to move.</param>
		/// <param name="destinationPath">The destination path to move to.</param>
		public void Move(string sourcePath, string destinationPath)
		{
			this.client.Move(sourcePath, destinationPath);
		}

		/// <summary>
		/// Determines whether the specified path refers to a directory that exists on disk.
		/// </summary>
		/// <param name="path">The directory to determine.</param>
		/// <returns>True if the directory exists. Otherwise it returns false.</returns>
		public bool DirectoryExists(string path)
		{
			return this.client.DirectoryExists(path);
		}

		/// <summary>
		/// Determines whether the specified fpath refers to a file that exists on disk.
		/// </summary>
		/// <param name="path">The file to determine.</param>
		/// <returns>True if the file exists. Otherwise it returns false.</returns>
		public bool FileExists(string path)
		{
			return this.client.FileExists(path);
		}

		#endregion

		#region IDisposable Implementations

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			this.client.Dispose();
		}

		#endregion
	}
}
