namespace HomeCloud.Data.IO
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Threading.Tasks;
	using System.Transactions;

	using HomeCloud.Core.Extensions;

	using HomeCloud.Data.IO.Helpers;
	using HomeCloud.Data.IO.Operations;

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
		/// The synchronization object
		/// </summary>
		private readonly object synchronizationObject = new object();

		/// <summary>
		/// The enlistment container
		/// </summary>
		private readonly IDictionary<string, TransactionEnlistment> container = new Dictionary<string, TransactionEnlistment>();

		/// <summary>
		/// The configuration options.
		/// </summary>
		private readonly FileSystemOptions options = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemContext" /> class.
		/// </summary>
		/// <param name="accessor">The configuration options accessor.</param>
		/// <exception cref="System.ArgumentNullException">accessor or <see cref="IOptionsSnapshot{FileSystemOptions}.Value"/> or <see cref="FileSystemOptions.Root"/>.</exception>
		public FileSystemContext(IOptionsSnapshot<FileSystemOptions> accessor)
			: base()
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

			FileHelper.EnsureTemporaryFolderExists();

			this.options = accessor.Value;
		}

		#endregion

		#region IFileSystemClient Implementations

		#region IFileSystemContext Methods

		/// <summary>
		/// Gets a value indicating whether this instance use transaction.
		/// </summary>
		/// <value>
		///   <c>True</c> if this instance use transaction; otherwise, <c>false</c>.
		/// </value>
		public bool IsTransactional { get => Transaction.Current != null; }

		/// <summary>
		/// Creates a temporary file with the given extension.
		/// </summary>
		/// <param name="extension">File extension. Default value is '<see cref=".tmp"/>'.</param>
		/// <returns>The path to the temporary file.</returns>
		public string CreateTemporaryFile(string extension = ".tmp")
		{
			string path = FileHelper.GetTemporaryFileName(extension);

			this.Snapshot(path);

			return path;
		}

		/// <summary>
		/// Creates the temporary directory with the given directory prefix.
		/// </summary>
		/// <param name="prefix">The prefix of the directory name. Default value is <see cref="Nullable"/></param>
		/// <returns>
		/// The path to the newly created temporary directory.
		/// </returns>
		public string CreateTemporaryDirectory(string prefix = null)
		{
			string path = FileHelper.GetTemporaryDirectory(Path.GetTempPath(), prefix);

			this.CreateDirectory(path);

			return path;
		}

		#endregion

		#region IFileSystemOperation Implementations

		/// <summary>
		/// Gets the instance of <see cref="DirectoryInfo" /> located in <paramref name="parent" />.
		/// </summary>
		/// <param name="name">The name of the directory.</param>
		/// <param name="parent">The instance of <see cref="DirectoryInfo" /> representing parent directory the requested by <paramref name="name" /> instance should be created in.
		/// The value corresponds to <see cref="FileSystemOptions.Root" /> by default.</param>
		/// <returns>
		/// The instance of <see cref="DirectoryInfo" />
		/// </returns>
		public DirectoryInfo GetDirectory(string name, DirectoryInfo parent = null)
		{
			string parentPath = (parent?.FullName ?? this.options.Root);

			return new DirectoryInfo(Path.Combine(parentPath, name));
		}

		/// <summary>
		/// Gets the instance of <see cref="FileInfo" /> located in <paramref name="parent" />.
		/// </summary>
		/// <param name="name">The name of the file containing extension.</param>
		/// <param name="parent">The instance of <see cref="DirectoryInfo" /> representing parent directory the requested by <paramref name="name" /> instance should be created in.
		/// The value corresponds to <see cref="FileSystemOptions.Root" /> by default.</param>
		/// <returns>
		/// The instance of <see cref="FileInfo" />
		/// </returns>
		public FileInfo GetFile(string name, DirectoryInfo parent = null)
		{
			string parentPath = (parent?.FullName ?? this.options.Root);

			return new FileInfo(Path.Combine(parentPath, name));
		}

		/// <summary>
		/// Gets the list of directories located in specified <paramref name="directory" />.
		/// </summary>
		/// <param name="directory">The directory to search in. The value corresponds to <see cref="FileSystemOptions.Root" /> by default.</param>
		/// <param name="recursive">Indicates whether the sub directories should be included in result output. By default the value is false.</param>
		/// <returns>
		/// The list of instances if <see cref="DirectoryInfo" />
		/// </returns>
		public IEnumerable<DirectoryInfo> GetDirectories(DirectoryInfo directory = null, bool recursive = false)
		{
			return (directory ?? new DirectoryInfo(this.options.Root)).GetDirectories("*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
		}

		/// <summary>
		/// Gets the list files located in specified <paramref name="directory" />.
		/// </summary>
		/// <param name="directory">The directory to search in. The value corresponds to <see cref="FileSystemOptions.Root" /> by default.</param>
		/// <param name="recursive">Indicates whether the sub directories should be included in result output. By default the value is false.</param>
		/// <returns>
		/// The list of instances if <see cref="FileInfo" />
		/// </returns>
		public IEnumerable<FileInfo> GetFiles(DirectoryInfo directory = null, bool recursive = false)
		{
			return (directory ?? new DirectoryInfo(this.options.Root)).GetFiles("*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
		}

		/// <summary>
		/// Determines whether the specified path refers to a directory that exists on disk.
		/// </summary>
		/// <param name="path">The directory to determine.</param>
		/// <returns>True if the directory exists. Otherwise it returns false.</returns>
		public bool DirectoryExists(string path)
		{
			return FileHelper.IsDirectory(path) && Directory.Exists(path);
		}

		/// <summary>
		/// Determines whether the specified fpath refers to a file that exists on disk.
		/// </summary>
		/// <param name="path">The file to determine.</param>
		/// <returns>True if the file exists. Otherwise it returns false.</returns>
		public bool FileExists(string path)
		{
			return !FileHelper.IsDirectory(path) && File.Exists(path);
		}

		/// <summary>
		/// Appends the specified string to the file, creating the file if it doesn't already exist.
		/// </summary>
		/// <param name="path">The file to append the string to.</param>
		/// <param name="contents">The string to append to the file.</param>
		public void AppendAllText(string path, string content)
		{
			lock (this.synchronizationObject)
			{
				if (this.IsTransactional)
				{
					this.EnlistOperation(new AppendAllTextOperation(path, content));
				}
				else
				{
					File.AppendAllText(path, content);
				}
			}
		}

		/// <summary>
		/// Copies the specified <paramref name="sourceFileName"/> to <paramref name="destFileName"/>.
		/// </summary>
		/// <param name="sourcePath">The file or directory path to copy from.</param>
		/// <param name="destFileName">The destination file or directory path to copy to.</param>
		/// <param name="overwrite">Indicates whether the destination file should be overwritten, otherwise false.</param>
		public void Copy(string sourcePath, string destinationPath, bool overwrite)
		{
			lock (synchronizationObject)
			{

				if (this.IsTransactional)
				{
					this.EnlistOperation(new CopyOperation(sourcePath, destinationPath, overwrite));
				}
				else
				{
					if (FileHelper.IsDirectory(sourcePath))
					{
						DirectoryInfo directory = new DirectoryInfo(sourcePath);
						directory.Copy(destinationPath);
					}
					else
					{
						File.Copy(sourcePath, destinationPath, overwrite);
					}
				}
			}
		}

		/// <summary>
		/// Creates directory by the specified path.
		/// </summary>
		/// <param name="path">The directory path to create.</param>
		/// <returns>The newly created instance of <see cref="DirectoryInfo"/>.</returns>
		public DirectoryInfo CreateDirectory(string path)
		{
			lock (synchronizationObject)
			{
				if (this.IsTransactional)
				{
					this.EnlistOperation(new CreateDirectoryOperation(path));
				}
				else
				{
					Directory.CreateDirectory(path);
				}
			}

			return new DirectoryInfo(path);
		}

		/// <summary>
		/// Creates the file by specified path and from the specified <see cref="Stream" />.
		/// </summary>
		/// <param name="path">The file path to create.</param>
		/// <param name="stream">The stream containing file data.</param>
		/// <returns>The newly created instance of <see cref="FileInfo"/>.</returns>
		public FileInfo CreateFile(string path, Stream stream)
		{
			lock (synchronizationObject)
			{
				if (this.IsTransactional)
				{
					this.EnlistOperation(new CreateFileOperation(path, stream));
				}
				else
				{
					using (FileStream file = File.Create(path, 1024, FileOptions.WriteThrough))
					{
						stream.CopyTo(file);
					}
				}
			}

			return new FileInfo(path);
		}

		/// <summary>
		/// Deletes the specified file or directory. An exception is not thrown if the file does not exist.
		/// </summary>
		/// <param name="path">The path to the file or directory to delete.</param>
		public void Delete(string path)
		{
			lock (synchronizationObject)
			{
				if (this.IsTransactional)
				{
					this.EnlistOperation(new DeleteOperation(path));
				}
				else
				{
					if (FileHelper.IsDirectory(path))
					{
						Directory.Delete(path);
					}
					else
					{
						File.Delete(path);
					}
				}
			}
		}

		/// <summary>
		/// Moves the specified file or directory to the new location specified by <paramref name="destinationPath"/>.
		/// </summary>
		/// <param name="sourcePath">The source path to the file or directory to move.</param>
		/// <param name="destinationPath">The destination path to move to.</param>
		public void Move(string sourcePath, string destinationPath)
		{
			lock (synchronizationObject)
			{
				if (this.IsTransactional)
				{
					this.EnlistOperation(new MoveOperation(sourcePath, destinationPath));
				}
				else
				{
					if (FileHelper.IsDirectory(sourcePath))
					{
						DirectoryInfo directory = new DirectoryInfo(sourcePath);
						directory.Move(destinationPath);
					}
					else
					{
						File.Move(sourcePath, destinationPath);
					}
				}
			}
		}

		/// <summary>
		/// Takes a snapshot of the file or directory specified by path. Used to rollback the file or directory later.
		/// </summary>
		/// <param name="path">The file or directory path to take a snapshot for.</param>
		public void Snapshot(string path)
		{
			if (this.IsTransactional)
			{
				this.EnlistOperation(new SnapshotOperation(path));
			}
		}

		/// <summary>
		/// Creates a file, writes the specified <paramref name="content"/> to the file.
		/// </summary>
		/// <param name="path">The file to write <paramref name="content"/> to.</param>
		/// <param name="content">The array of bytes to write to the file.</param>
		public void WriteAllBytes(string path, byte[] content)
		{
			lock (this.synchronizationObject)
			{
				if (this.IsTransactional)
				{
					this.EnlistOperation(new WriteAllBytesOperation(path, content));
				}
				else
				{
					File.WriteAllBytes(path, content);
				}
			}
		}

		/// <summary>
		/// Reads the byte portion from th efile specified by <paramref name="path" />.
		/// </summary>
		/// <param name="path">The file to write <paramref name="content" /> to.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="length">The number of bytes from byte array to return.</param>
		/// <returns>The byte array.</returns>
		public byte[] ReadBytes(string path, int offset = 0, int length = 0)
		{
			using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				long count = length == 0 ? stream.Length : length;

				byte[] buffer = new byte[count];
				stream.Read(buffer, offset, (int)count);

				return buffer;
			}
		}

		#endregion

		#region IDIsposable Implementations

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public virtual void Dispose()
		{
			if (this.container.Count > 0)
			{
				lock (this.synchronizationObject)
				{
					if (this.container.Count > 0)
					{
						Parallel.ForEach(this.container.Values, enlistment => enlistment.Dispose());
						this.container.Clear();
					}
				}
			}
		}

		#endregion

		#endregion

		#region Private Methods

		/// <summary>
		/// Enlists the specified operation.
		/// </summary>
		/// <param name="operation">The operation.</param>
		private void EnlistOperation(IScopedOperation operation)
		{
			Transaction transaction = Transaction.Current;
			string id = transaction.TransactionInformation.LocalIdentifier;

			TransactionEnlistment enlistment = null;
			lock (this.synchronizationObject)
			{
				if (!this.container.TryGetValue(id, out enlistment))
				{
					enlistment = new TransactionEnlistment(transaction);
					this.container.Add(id, enlistment);
				}

				enlistment.EnlistOperation(operation);
			}
		}

		#endregion
	}
}
