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
		/// Deletes the list of entities by specified identifier of entity of <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.Directory" /> type the list belongs to.
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
		/// Gets the list of entities by specified identifier of entity of <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.Directory" /> type the list belongs to.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <param name="startIndex">The index of the first record that should appear in the list.</param>
		/// <param name="chunkSize">The number of records to select.</param>
		/// <returns>
		/// The list of instances of <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.File" />.
		/// </returns>
		public IEnumerable<File> GetByDirectoryID(Guid id, int startIndex, int chunkSize)
		{
			return this.context.Query<File>(
				GetFileByDirectoryIDStoredProcedure,
				new
				{
					@ParentID = id,
					@StartIndex = startIndex,
					@ChunkSize = chunkSize
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
			return this.context.Query<File>(entity.ID == Guid.Empty ? InsertFileStoredProcedure : UpdateFileStoredProcedure, entity).FirstOrDefault();
		}

		#endregion
	}
}
