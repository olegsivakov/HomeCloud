namespace HomeCloud.DataStorage.DataAccess.Components.Repositories
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;

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
		public void Delete(Guid id)
		{
			this.context.Execute(
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
		public void DeleteByDirectoryID(Guid id)
		{
			this.context.Execute(
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
		public IEnumerable<File> Find(int offset = 0, int limit = 20)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Gets the entity by specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.File" />.
		/// </returns>
		public File Get(Guid id)
		{
			return this.context.Query<File>(
				GetFileByIDStoredProcedure,
				new
				{
					@ID = id
				}).FirstOrDefault();
		}

		/// <summary>
		/// Gets the list of entities by specified identifier of entity of <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.Catalog" /> type the list belongs to.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to select.</param>
		/// <returns>
		/// The list of instances of <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.File" />.
		/// </returns>
		public IEnumerable<File> GetByDirectoryID(Guid id, int offset = 0, int limit = 20)
		{
			return this.context.Query<File>(
				GetFileByDirectoryIDStoredProcedure,
				new
				{
					@ParentID = id,
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
		public File Save(File entity)
		{
			Guid id = entity.ID == Guid.Empty ? Guid.NewGuid() : entity.ID;

			if (this.context.Execute(
				entity.ID == Guid.Empty ? InsertFileStoredProcedure : UpdateFileStoredProcedure,
				new
				{
					@ID = id,
					@DirectoryID = entity.DirectoryID,
					@Name = entity.Name,
					@Extension = entity.Extension
				}) > 0)
			{
				return this.Get(id);
			}

			return entity;
		}

		#endregion
	}
}
