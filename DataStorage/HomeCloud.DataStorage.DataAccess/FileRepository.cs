namespace HomeCloud.DataStorage.DataAccess
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.Data.SqlServer;
	using HomeCloud.DataStorage.DataAccess.Objects;

	#endregion

	/// <summary>
	/// Provides methods to handle <see cref="File" /> data.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.SqlServer.SqlServerDBRepository{HomeCloud.DataStorage.DataAccess.Objects.File}" />
	/// <seealso cref="HomeCloud.DataStorage.DataAccess.IFileRepository" />
	public sealed class FileRepository : SqlServerDBRepository<File>, IFileRepository
	{
		#region Constants

		/// <summary>
		/// The <see cref="[dbo].[InsertFile]"/> stored procedure name.
		/// </summary>
		private const string InsertFileStoredProcedure = "[dbo].[InsertFile]";

		/// <summary>
		/// The <see cref="[dbo].[UpdateFile]"/> stored procedure name.
		/// </summary>
		private const string UpdateFileStoredProcedure = "[dbo].[UpdateFile]";

		/// <summary>
		/// The <see cref="[dbo].[DeleteFileByID]"/> stored procedure name.
		/// </summary>
		private const string DeleteFileByIDStoredProcedure = "[dbo].[DeleteFileByID]";

		/// <summary>
		/// The <see cref="[dbo].[DeleteFileByDirectoryID]"/> stored procedure name.
		/// </summary>
		private const string DeleteFileByDirectoryIDStoredProcedure = "[dbo].[DeleteFileByDirectoryID]";

		/// <summary>
		/// The <see cref="[dbo].[GetFileByID]"/> stored procedure name.
		/// </summary>
		private const string GetFileByIDStoredProcedure = "[dbo].[GetFileByID]";

		/// <summary>
		/// The <see cref="[dbo].[GetFileByDirectoryID]"/> stored procedure name.
		/// </summary>
		private const string GetFileByDirectoryIDStoredProcedure = "[dbo].[GetFileByDirectoryID]";

		/// <summary>
		/// The <see cref="[dbo].[GetFileCountByDirectoryID]"/> stored procedure name.
		/// </summary>
		private const string GetFileCountByDirectoryIDStoredProcedure = "[dbo].[GetFileCountByDirectoryID]";

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileRepository"/> class.
		/// </summary>
		/// <param name="context">The data context.</param>
		public FileRepository(ISqlServerDBContext context)
			: base(context)
		{
		}

		#endregion

		#region IFileRepository Implementations

		/// <summary>
		/// Deletes the record by specified unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public override async Task DeleteAsync(Guid id)
		{
			await this.Context.ExecuteAsync(
				DeleteFileByIDStoredProcedure,
				new
				{
					@ID = id
				});
		}

		/// <summary>
		/// Deletes the list of entities by specified identifier of entity of <see cref="T:HomeCloud.DataStorage.DataAccess.Objects.Catalog" /> type the list belongs to.
		/// </summary>
		/// <param name="id">The parent entity unique identifier.</param>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public async Task DeleteByDirectoryIDAsync(Guid id)
		{
			await this.Context.ExecuteAsync(
				DeleteFileByDirectoryIDStoredProcedure,
				new
				{
					@DirectoryID = id
				});
		}

		/// <summary>
		/// Gets the list of entities that match the specified one.
		/// </summary>
		/// <param name="file">The file to search by.</param>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to select.</param>
		/// <returns>
		/// The list of instances of <see cref="T:HomeCloud.DataStorage.DataAccess.Objects.File" />.
		/// </returns>
		public async Task<IPaginable<File>> FindAsync(File file, int offset = 0, int limit = 20)
		{
			IEnumerable<File> result =  await this.Context.QueryAsync<File>(
				GetFileByDirectoryIDStoredProcedure,
				new
				{
					@Name = string.IsNullOrWhiteSpace(file?.Name) ? null : file.Name.Trim().ToLower(),
					@DirectoryID = file.DirectoryID,
					@StartIndex = offset,
					@ChunkSize = limit
				});

			int count = await this.GetCountAsync(file);

			return new PagedList<File>(result)
			{
				Offset = offset,
				Limit = limit,
				TotalCount = count
			};
		}

		/// <summary>
		/// Searches for all records of <see cref="!:T" /> type.
		/// </summary>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The list of instances of <see cref="!:T" /> type.
		/// </returns>
		public override async Task<IPaginable<File>> FindAsync(int offset = 0, int limit = 20)
		{
			return await this.FindAsync(null, offset, limit);
		}

		/// <summary>
		/// Gets the entity of <see cref="!:T" /> by specified unique identifier asynchronously.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>
		/// The instance of <see cref="!:T" /> type.
		/// </returns>
		public override async Task<File> GetAsync(Guid id)
		{
			IEnumerable<File> result = await this.Context.QueryAsync<File>(
				GetFileByIDStoredProcedure,
				new
				{
					@ID = id
				});

			return result.FirstOrDefault();
		}

		/// <summary>
		/// Gets the number of entities that match the specified one.
		/// </summary>
		/// <param name="file">&gt;The file to search by.</param>
		/// <returns>
		/// The number of entities.
		/// </returns>
		public async Task<int> GetCountAsync(File file)
		{
			return await this.Context.ExecuteScalarAsync<object, int>(
				GetFileCountByDirectoryIDStoredProcedure,
				new
				{
					@Name = string.IsNullOrWhiteSpace(file?.Name) ? null : file.Name.Trim().ToLower(),
					@DirectoryID = file.DirectoryID
				});
		}

		/// <summary>
		/// Saves the specified entity of <see cref="!:T" /> asynchronously.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// The instance of <see cref="!:T" />.
		/// </returns>
		public override async Task<File> SaveAsync(File entity)
		{
			Guid id = entity.ID == Guid.Empty ? Guid.NewGuid() : entity.ID;

			if (await this.Context.ExecuteAsync(
				entity.ID == Guid.Empty || (await this.GetAsync(id)) is null ? InsertFileStoredProcedure : UpdateFileStoredProcedure,
				new
				{
					@ID = id,
					@DirectoryID = entity.DirectoryID,
					@Name = entity.Name
				}) > 0)
			{
				return await this.GetAsync(id);
			}

			return entity;
		}

		#endregion
	}
}
