namespace HomeCloud.IO
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Transactions;

	using HomeCloud.IO.Extensions;
	using HomeCloud.IO.Helpers;
	using HomeCloud.IO.Operations;

	#endregion

	/// <summary>
	/// Implements <see cref="IFileSystemClient"/> client that performs the actions against file system.
	/// </summary>
	public class FileSystemClient : IFileSystemClient
	{
		#region Private Members

		/// <summary>
		/// The enlistment container
		/// </summary>
		[ThreadStatic]
		private static IDictionary<string, TransactionEnlistment> container = null;

		/// <summary>
		/// The synchronization object
		/// </summary>
		private static readonly object synchronizationObject = new object();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemClient"/> class.
		/// </summary>
		public FileSystemClient()
		{
			FileHelper.EnsureTemporaryFolderExists();
		}

		#endregion

		#region IFileSystemClient Implementations

		#region Public Properties

		/// <summary>
		/// Gets a value indicating whether this instance use transaction.
		/// </summary>
		/// <value>
		///   <c>True</c> if this instance use transaction; otherwise, <c>false</c>.
		/// </value>
		public bool IsTransactional { get => Transaction.Current != null; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Determines whether the specified path refers to a directory that exists on disk.
		/// </summary>
		/// <param name="path">The directory to determine.</param>
		/// <returns>True if the directory exists. Otherwise it returns false.</returns>
		public bool DirectoryExists(string path)
		{
			return Directory.Exists(path);
		}

		/// <summary>
		/// Determines whether the specified fpath refers to a file that exists on disk.
		/// </summary>
		/// <param name="path">The file to determine.</param>
		/// <returns>True if the file exists. Otherwise it returns false.</returns>
		public bool FileExists(string path)
		{
			return File.Exists(path);
		}

		/// <summary>
		/// Gets the files in the specified directory.
		/// </summary>
		/// <param name="path">The directory to get files.</param>
		/// <param name="handler">The <see cref="FileEventHandler" /> object to call on each file found.</param>
		/// <param name="recursive">if set to <c>true</c>, the method searches for files sub-directories recursively.</param>
		public void GetFiles(string path, FileEventHandler handler, bool recursive)
		{
			string[] files = Directory.GetFiles(path);
			foreach (string file in files)
			{
				bool cancel = false;
				handler(file, ref cancel);
				if (cancel)
				{
					return;
				}
			}

			if (recursive)
			{
				foreach (string directory in Directory.GetDirectories(path))
				{
					this.GetFiles(directory, handler, recursive);
				}
			}
		}

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

		#region IFileOperation Implementations

		/// <summary>
		/// Appends the specified string to the file, creating the file if it doesn't already exist.
		/// </summary>
		/// <param name="path">The file to append the string to.</param>
		/// <param name="contents">The string to append to the file.</param>
		public void AppendAllText(string path, string content)
		{
			if (this.IsTransactional)
			{
				EnlistOperation(new AppendAllTextOperation(path, content));
			}
			else
			{
				File.AppendAllText(path, content);
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
			if (this.IsTransactional)
			{
				EnlistOperation(new CopyOperation(sourcePath, destinationPath, overwrite));
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

		/// <summary>
		/// Creates directory by the specified path.
		/// </summary>
		/// <param name="path">The directory path to create.</param>
		public void CreateDirectory(string path)
		{
			if (this.IsTransactional)
			{
				EnlistOperation(new CreateDirectoryOperation(path));
			}
			else
			{
				Directory.CreateDirectory(path);
			}
		}

		/// <summary>
		/// Creates the file by specified path and from the specified <see cref="Stream" />.
		/// </summary>
		/// <param name="path">The file path to create.</param>
		/// <param name="stream">The stream containing file data.</param>
		public void CreateFile(string path, Stream stream)
		{
			if (this.IsTransactional)
			{
				EnlistOperation(new CreateFileOperation(path, stream));
			}
			else
			{
				using (FileStream file = File.Create(path, 1024, FileOptions.WriteThrough))
				{
					stream.CopyTo(file);
				}
			}
		}

		/// <summary>
		/// Deletes the specified file or directory. An exception is not thrown if the file does not exist.
		/// </summary>
		/// <param name="path">The path to the file or directory to delete.</param>
		public void Delete(string path)
		{
			if (this.IsTransactional)
			{
				EnlistOperation(new DeleteOperation(path));
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

		/// <summary>
		/// Moves the specified file or directory to the new location specified by <paramref name="destinationPath"/>.
		/// </summary>
		/// <param name="sourcePath">The source path to the file or directory to move.</param>
		/// <param name="destinationPath">The destination path to move to.</param>
		public void Move(string sourcePath, string destinationPath)
		{
			if (this.IsTransactional)
			{
				EnlistOperation(new MoveOperation(sourcePath, destinationPath));
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

		/// <summary>
		/// Takes a snapshot of the file or directory specified by path. Used to rollback the file or directory later.
		/// </summary>
		/// <param name="path">The file or directory path to take a snapshot for.</param>
		public void Snapshot(string path)
		{
			if (this.IsTransactional)
			{
				EnlistOperation(new SnapshotOperation(path));
			}
		}

		/// <summary>
		/// Creates a file, writes the specified <paramref name="content"/> to the file.
		/// </summary>
		/// <param name="path">The file to write <paramref name="content"/> to.</param>
		/// <param name="content">The array of bytes to write to the file.</param>
		public void WriteAllBytes(string path, byte[] content)
		{
			if (this.IsTransactional)
			{
				EnlistOperation(new WriteAllBytesOperation(path, content));
			}
			else
			{
				File.WriteAllBytes(path, content);
			}
		}

		#endregion

		#endregion

		#region Private Methods

		/// <summary>
		/// Enlists the specified operation.
		/// </summary>
		/// <param name="operation">The operation.</param>
		private static void EnlistOperation(ITransactionalOperation operation)
		{
			Transaction transaction = Transaction.Current;
			TransactionEnlistment enlistment = null;

			if (container is null)
			{
				lock (synchronizationObject)
				{
					if (container == null)
					{
						container = new Dictionary<string, TransactionEnlistment>();
					}

					string id = transaction.TransactionInformation.LocalIdentifier;
					if (!container.TryGetValue(id, out enlistment))
					{
						enlistment = new TransactionEnlistment(transaction);

						container.Add(id, enlistment);
					}

					enlistment.EnlistOperation(operation);
				}
			}
		}

		#endregion
	}
}
