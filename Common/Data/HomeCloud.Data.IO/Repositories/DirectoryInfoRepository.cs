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
	/// Provides methods to handle <see cref="DirectoryInfo" /> data.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.IO.Repositories.IDirectoryInfoRepository" />
	public class DirectoryInfoRepository : IDirectoryInfoRepository
	{
		#region Private Members

		/// <summary>
		/// The context
		/// </summary>
		private readonly IFileSystemContext context = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DirectoryInfoRepository" /> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public DirectoryInfoRepository(IFileSystemContext context)
		{
			this.context = context;
		}

		#endregion

		#region IDirectoryInfoRepository Implementations

		/// <summary>
		/// Creates the new instance of <see cref="DirectoryInfo"/> in <paramref name="parent"/>. The method doesn't create new directory in file system.
		/// </summary>
		/// <param name="name">The name of the directory.</param>
		/// <param name="parent">The instance of <see cref="DirectoryInfo"/> representing parent directory the requested by <paramref name="name"/> instance should be created in.
		/// By default the value corresponds <see cref="FileSystemOptions.Root"/>.</param>
		/// <returns>The instance of <see cref="DirectoryInfo"/></returns>
		public DirectoryInfo Get(string name, DirectoryInfo parent = null)
		{
			return this.context.NewDirectory(name, parent);
		}

		/// <summary>
		/// Deletes the record by specified file system path.
		/// </summary>
		/// <param name="path">The path.</param>
		public void Delete(string path)
		{
			if (this.context.DirectoryExists(path))
			{
				this.context.Delete(path);
			}
		}

		/// <summary>
		/// Gets the records of <see cref="T" /> type by specified expression.
		/// </summary>
		/// <param name="parent">The parent directory.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The <see cref="IPaginable" /> list of instances of <see cref="T" /> type.
		/// </returns>
		public IPaginable<DirectoryInfo> Find(DirectoryInfo parent, int offset = 0, int limit = 20)
		{
			if (parent.Exists)
			{
				IEnumerable<DirectoryInfo> result = parent.GetDirectories();

				return new PagedList<DirectoryInfo>(result.Skip(offset).Take(limit))
				{
					Offset = offset,
					Limit = limit,
					TotalCount = result.Count()
				};
			}

			return new PagedList<DirectoryInfo>();
		}

		/// <summary>
		/// Gets the entity of <see cref="T"/> by specified file system path.
		/// </summary>
		/// <param name="path">The path to the file system instance.</param>
		/// <returns>The instance of <see cref="T"/> type.</returns>
		public DirectoryInfo Get(string path)
		{
			return new DirectoryInfo(path);
		}

		/// <summary>
		/// Saves the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns><The instance of <see cref="DirectoryInfo"/>.</returns>
		public DirectoryInfo Save(DirectoryInfo entity)
		{
			if (!entity.Exists)
			{
				this.context.CreateDirectory(entity.FullName);

				return new DirectoryInfo(entity.FullName);
			}

			return entity;
		}

		#endregion
	}
}
