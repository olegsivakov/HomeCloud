namespace HomeCloud.Data.IO.Repositories
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Threading.Tasks;

	using HomeCloud.Core;

	#endregion

	/// <summary>
	/// Provides methods to handle <see cref="FileInfo" /> data.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.IO.Repositories.IFileInfoRepository" />
	public class FileInfoRepository : IFileInfoRepository
	{
		#region Private Members

		/// <summary>
		/// The context
		/// </summary>
		private readonly IFileSystemContext context = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileInfoRepository" /> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public FileInfoRepository(IFileSystemContext context)
		{
			this.context = context;
		}

		#endregion

		#region IFileInfoRepository Implementations

		/// <summary>
		/// Deletes the record by specified file system path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <exception cref="NotImplementedException"></exception>
		public void Delete(string path)
		{
			this.context.Delete(path);
		}

		/// <summary>
		/// Gets the records of <see cref="!:T" /> type by specified expression.
		/// </summary>
		/// <param name="parent">The parent directory.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The <see cref="T:HomeCloud.Core.IPaginable" /> list of instances of <see cref="!:T" /> type.
		/// </returns>
		public IPaginable<FileInfo> Find(DirectoryInfo parent, int offset = 0, int limit = 20)
		{
			if (parent.Exists)
			{
				IEnumerable<FileInfo> result = parent.GetFiles();

				return new PagedList<FileInfo>(result.Skip(offset).Take(limit))
				{
					Offset = offset,
					Limit = limit,
					TotalCount = result.Count()
				};
			}

			return new PagedList<FileInfo>();
		}

		/// <summary>
		/// Gets the entity of <see cref="!:T" /> by specified file system path.
		/// </summary>
		/// <param name="path">The path to the file system instance.</param>
		/// <returns>
		/// The instance of <see cref="!:T" /> type.
		/// </returns>
		public FileInfo Get(string path)
		{
			return new FileInfo(path);
		}

		/// <summary>
		/// Gets the instance of <see cref="!:T" /> of specified <paramref name="name" /> located in <paramref name="parent" />.
		/// </summary>
		/// <param name="name">The name of the directory of file.</param>
		/// <param name="parent">The instance of <see cref="T:System.IO.DirectoryInfo" /> representing parent directory the requested by <paramref name="name" /> instance should be created in.
		/// By default the value corresponds <see cref="P:HomeCloud.Data.IO.FileSystemOptions.Root" />.</param>
		/// <returns>
		/// The instance of <see cref="!:T" />.
		/// </returns>
		public FileInfo Get(string name, DirectoryInfo parent = null)
		{
			return this.context.NewFile(name, parent);
		}

		/// <summary>
		/// Saves the specified entity of <see cref="!:T" />.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// The instance of <see cref="!:T" />.
		/// </returns>
		public FileInfo Save(FileInfo entity)
		{
			this.context.CreateFile(entity.FullName, null);

			return entity;
		}

		/// <summary>
		/// Saves the specified entity of <see cref="FileInfo" />.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="stream">The stream to write to the file.</param>
		/// <returns>
		/// The instance of <see cref="FileInfo" />.
		/// </returns>
		public FileInfo Save(FileInfo entity, Stream stream)
		{
			this.context.CreateFile(entity.FullName, stream);

			return entity;
		}

		#endregion
	}
}
