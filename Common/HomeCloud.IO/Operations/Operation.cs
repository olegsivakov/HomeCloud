namespace HomeCloud.IO.Operations
{
	#region Usings

	using System;
	using System.IO;

	using HomeCloud.IO.Extensions;
	using HomeCloud.IO.Helpers;

	using SystemPath = System.IO.Path;

	#endregion

	/// <summary>
	/// Contains common methods for those transactional file operations that need to backup a single file and restore it when <see cref="ITransactionalOperation.Rollback"/> is called.
	/// </summary>
	public abstract class Operation : ITransactionalOperation, IDisposable
	{
		#region Private Members

		/// <summary>
		/// The member tracking whether the current instance is disposed.
		/// </summary>
		private bool disposed = false;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Operation"/> class.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <exception cref="System.ArgumentNullException">path</exception>
		public Operation(string path)
		{
			if (path is null)
			{
				throw new ArgumentNullException(nameof(path));
			}

			this.Path = path;
		}

		#endregion

		#region Protected Properties

		/// <summary>
		/// Gets the path.
		/// </summary>
		/// <value>
		/// The path.
		/// </value>
		protected string Path { get; private set; }

		/// <summary>
		/// Gets or sets the backup path.
		/// </summary>
		/// <value>
		/// The backup path.
		/// </value>
		protected string BackupPath { get; set; }

		#endregion

		#region ITransactionalOperation Implementations

		/// <summary>
		/// Executes the operation.
		/// </summary>
		public abstract void Execute();

		/// <summary>
		/// Rolls back the operation and restores the original state.
		/// </summary>
		public abstract void Rollback();

		#endregion

		#region Protected Methods

		/// <summary>
		/// Backups th file or directory specified by <see cref="Path"/> to the file or directory correspondingly located in <see cref="BackupPath"/>.
		/// </summary>
		protected void Backup()
		{
			if (FileHelper.IsDirectory(this.Path))
			{
				DirectoryInfo source = new DirectoryInfo(this.Path);
				if (source.Exists)
				{
					string path = FileHelper.GetTemporaryFileName(String.Empty);
					this.BackupPath = source.Copy(path).FullName;
				}
			}
			else if (File.Exists(this.Path))
			{
				string path = FileHelper.GetTemporaryFileName(SystemPath.GetExtension(this.Path));
				File.Copy(this.Path, path, true);

				this.BackupPath = path;
			}
		}

		/// <summary>
		/// Restores the file or directory from the <see cref="BackupPath"/> to the <see cref="Path"/>.
		/// </summary>
		protected void Restore()
		{
			if (!string.IsNullOrWhiteSpace(this.BackupPath))
			{
				if (FileHelper.IsDirectory(this.BackupPath))
				{
					DirectoryInfo source = new DirectoryInfo(this.BackupPath);
					if (source.Exists)
					{
						if (Directory.Exists(this.Path))
						{
							Directory.Delete(this.Path, true);
						}

						source.Copy(this.Path);
					}
				}
				else if (File.Exists(this.BackupPath))
				{
					string directory = SystemPath.GetDirectoryName(this.Path);
					if (!Directory.Exists(directory))
					{
						Directory.CreateDirectory(directory);
					}

					File.Copy(this.BackupPath, this.Path, true);
				}
			}
		}

		#endregion

		#region IDisposable Implementations

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public virtual void Dispose()
		{
			InnerDispose();
			GC.SuppressFinalize(this);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Disposes the resources of the current instance.
		/// </summary>
		private void InnerDispose()
		{
			if (!this.disposed && this.BackupPath != null)
			{
				if (FileHelper.IsDirectory(this.BackupPath))
				{
					if (Directory.Exists(this.BackupPath))
					{
						Directory.Delete(this.BackupPath, true);
					}
				}
				else if(File.Exists(this.BackupPath))
				{
					FileInfo file = new FileInfo(this.BackupPath);
					if (file.IsReadOnly)
					{
						file.Attributes = FileAttributes.Normal;
					}

					File.Delete(this.BackupPath);
				}

				this.disposed = true;
			}
		}

		#endregion
	}
}
