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
	/// <seealso cref="HomeCloud.IO.FileSystemClient" />
	/// <seealso cref="HomeCloud.Data.IO.IFileSystemContext" />
	public class FileSystemContext : FileSystemClient, IFileSystemContext
	{
		#region Private Members

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

			this.options = accessor.Value;
		}

		#endregion
	}
}
