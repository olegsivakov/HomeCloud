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
	/// Provides methods to handle <see cref="Directory"/> data.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.DataAccess.Services.Repositories.IDirectoryRepository" />
	public class DirectoryRepository : IDirectoryRepository
	{
		#region Constants

		/// <summary>
		/// The <see cref="[dbo].[InsertDirectory]"/> stored procedure name.
		/// </summary>
		private const string InsertDirectoryStoredProcedure = "[dbo].[InsertDirectory]";

		/// <summary>
		/// The <see cref="[dbo].[UpdateDirectory]"/> stored procedure name.
		/// </summary>
		private const string UpdateDirectoryStoredProcedure = "[dbo].[UpdateDirectory]";

		/// <summary>
		/// The <see cref="[dbo].[DeleteDirectoryByID]"/> stored procedure name.
		/// </summary>
		private const string DeleteDirectoryByIDStoredProcedure = "[dbo].[DeleteDirectoryByID]";

		/// <summary>
		/// The <see cref="[dbo].[DeleteDirectoryByParentID]"/> stored procedure name.
		/// </summary>
		private const string DeleteDirectoryByParentIDStoredProcedure = "[dbo].[DeleteDirectoryByParentID]";

		/// <summary>
		/// The <see cref="[dbo].[GetDirectoryByID]"/> stored procedure name.
		/// </summary>
		private const string GetDirectoryByIDStoredProcedure = "[dbo].[GetDirectoryByID]";

		/// <summary>
		/// The <see cref="[dbo].[GetDirectoryByParentID]"/> stored procedure name.
		/// </summary>
		private const string GetDirectoryByParentIDStoredProcedure = "[dbo].[GetDirectoryByParentID]";

		#endregion

		#region Private Members

		/// <summary>
		/// The database context
		/// </summary>
		private readonly IDbContext context = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DirectoryRepository" /> class.
		/// </summary>
		/// <param name="context">The database context.</param>
		public DirectoryRepository(IDbContext context)
		{
			this.context = context;
		}

		#endregion

		#region IDirectoryRepository Implementations

		/// <summary>
		/// Deletes the entity by specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		public void Delete(Guid id)
		{
			this.context.Execute(
				DeleteDirectoryByIDStoredProcedure,
				new
				{
					@ID = id
				});
		}

		/// <summary>
		/// Deletes the list of entities by specified identifier of parent entity the list belongs to.
		/// </summary>
		/// <param name="id">The parent entity unique identifier.</param>
		public void DeleteByParentID(Guid? id)
		{
			this.context.Execute(
				DeleteDirectoryByParentIDStoredProcedure,
				new
				{
					@ParentID = id
				});
		}

		/// <summary>
		/// Gets the entity by specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.Directory" />.
		/// </returns>
		public Directory Get(Guid id)
		{
			return this.context.Query<Directory>(
				GetDirectoryByIDStoredProcedure,
				new
				{
					@ID = id
				}).FirstOrDefault();
		}

		/// <summary>
		/// Looks for all records of <see cref="!:T" /> type.
		/// </summary>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to select.</param>
		/// <returns>
		/// The list of instances of <see cref="!:T" /> type.
		/// </returns>
		/// <exception cref="NotSupportedException">Not supported as data requires the relationship for <see cref="Storage"/>.</exception>
		public IEnumerable<Directory> Find(int offset = 0, int limit = 20)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Gets the list of entities by specified identifier of parent entity the list belongs to.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to select.</param>
		/// <returns>
		/// The list of instances of <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.Directory" />.
		/// </returns>
		public IEnumerable<Directory> GetByParentID(Guid? id, int offset = 0, int limit = 20)
		{
			return this.context.Query<Directory>(
				GetDirectoryByParentIDStoredProcedure,
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
		/// <param name="entity">The entity of type <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.Directory" />.</param>
		/// <returns>The instance of <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.Directory" />.</returns>
		public Directory Save(Directory entity)
		{
			return this.context.Query<Directory>(entity.ID == Guid.Empty ? InsertDirectoryStoredProcedure : UpdateDirectoryStoredProcedure, entity).FirstOrDefault();
		}

		#endregion
	}
}
