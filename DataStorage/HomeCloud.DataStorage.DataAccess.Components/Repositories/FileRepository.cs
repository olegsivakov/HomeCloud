namespace HomeCloud.DataStorage.DataAccess.Components.Repositories
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using HomeCloud.DataAccess.Contracts;

	using HomeCloud.DataStorage.DataAccess.Contracts;
	using HomeCloud.DataStorage.DataAccess.Services.Repositories;

	#endregion

	/// <summary>
	/// Provides methods to handle <see cref="File"/> data.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.DataAccess.Services.Repositories.IFileRepository" />
	public class FileRepository : IFileRepository
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

		#endregion

		#region Private Members

		/// <summary>
		/// The database context
		/// </summary>
		private readonly IDbContext context = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileRepository" /> class.
		/// </summary>
		/// <param name="context">The database context.</param>
		public FileRepository(IDbContext context)
		{
			this.context = context;
		}

		#endregion

		#region IFileRepository Implementations

		/// <summary>
		/// Deletes the entity by specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>The asynchronous operation.</returns>
		public async Task DeleteAsync(Guid id)
		{
			await this.context.ExecuteAsync(
				DeleteFileByIDStoredProcedure,
				new
				{
					@ID = id
				});
		}

		/// <summary>
		/// Deletes the list of entities by specified identifier of entity of <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.Catalog" /> type the list belongs to.
		/// </summary>
		/// <param name="id">The parent entity unique identifier.</param>
		/// <returns>The asynchronous operation.</returns>
		public async Task DeleteByDirectoryIDAsync(Guid id)
		{
			await this.context.ExecuteAsync(
				DeleteFileByDirectoryIDStoredProcedure,
				new
				{
					@DirectoryID = id
				});
		}

		/// <summary>
		/// Looks for all records of <see cref="!:T" /> type.
		/// </summary>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to select.</param>
		/// <returns>
		/// The list of instances of <see cref="!:T" /> type.
		/// </returns>
		/// <exception cref="NotSupportedException">Not supported as data requires the relationship for <see cref="Catalog"/>.</exception>
		public async Task<IEnumerable<File>> FindAsync(int offset = 0, int limit = 20)
		{
			return await Task.FromException<IEnumerable<File>>(new NotSupportedException());
		}

		/// <summary>
		/// Gets the entity by specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.File" />.
		/// </returns>
		public async Task<File> GetAsync(Guid id)
		{
			IEnumerable<File> result = await this.context.QueryAsync<File>(
				GetFileByIDStoredProcedure,
				new
				{
					@ID = id
				});

			return result.FirstOrDefault();
		}

		/// <summary>
		/// Gets the list of entities that match the specified one.
		/// </summary>
		/// <param name="file">The file to search by.</param>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to select.</param>
		/// <returns>
		/// The list of instances of <see cref="File" />.
		/// </returns>
		public async Task<IEnumerable<File>> FindAsync(File file, int offset = 0, int limit = 20)
		{
			return await this.context.QueryAsync<File>(
				GetFileByDirectoryIDStoredProcedure,
				new
				{
					@Name = string.IsNullOrWhiteSpace(file?.Name) ? null : file.Name.Trim().ToLower(),
					@Extension = string.IsNullOrWhiteSpace(file?.Extension) ? null : file.Extension.Trim().ToLower(),
					@DirectoryID = file.DirectoryID,
					@StartIndex = offset,
					@ChunkSize = limit
				});
		}

		/// <summary>
		/// Saves the specified entity.
		/// </summary>
		/// <param name="entity">The entity of type <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.File" />.</param>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.File" />.
		/// </returns>
		public async Task<File> SaveAsync(File entity)
		{
			Guid id = entity.ID == Guid.Empty ? Guid.NewGuid() : entity.ID;

			if (await this.context.ExecuteAsync(
				entity.ID == Guid.Empty ? InsertFileStoredProcedure : UpdateFileStoredProcedure,
				new
				{
					@ID = id,
					@DirectoryID = entity.DirectoryID,
					@Name = entity.Name,
					@Extension = entity.Extension
				}) > 0)
			{
				return await this.GetAsync(id);
			}

			return entity;
		}

		#endregion
	}
}
