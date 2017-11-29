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
	/// Provides methods to handle <see cref="Catalog"/> data.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.DataAccess.Services.Repositories.ICatalogRepository" />
	public class CatalogRepository : ICatalogRepository
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

		/// <summary>
		/// The <see cref="[dbo].[GetDirectoryCountByParentID]"/> stored procedure name.
		/// </summary>
		private const string GetDirectoryCountByParentIDStoredProcedure = "[dbo].[GetDirectoryCountByParentID]";

		#endregion

		#region Private Members

		/// <summary>
		/// The database context
		/// </summary>
		private readonly IDbContext context = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CatalogRepository" /> class.
		/// </summary>
		/// <param name="context">The database context.</param>
		public CatalogRepository(IDbContext context)
		{
			this.context = context;
		}

		#endregion

		#region ICatalogRepository Implementations

		/// <summary>
		/// Gets the number of entities that match the specified one.
		/// </summary>
		/// <param name="catalog">>The catalog to search by.</param>
		/// <returns>The number of entities.</returns>
		public async Task<int> GetCountAsync(Catalog catalog)
		{
			return await this.context.ExecuteScalarAsync<object, int>(
				GetDirectoryCountByParentIDStoredProcedure,
				new
				{
					@Name = string.IsNullOrWhiteSpace(catalog?.Name) ? null : catalog.Name.Trim().ToLower(),
					@ParentID = catalog?.ParentID,
				});
		}

		/// <summary>
		/// Deletes the entity by specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>The asynchronous operation.</returns>
		public async Task DeleteAsync(Guid id)
		{
			await this.context.ExecuteAsync(
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
		/// <returns>The asynchronous operation.</returns>
		public async Task DeleteByParentIDAsync(Guid? id)
		{
			await this.context.ExecuteAsync(
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
		/// The instance of <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.Catalog" />.
		/// </returns>
		public async Task<Catalog> GetAsync(Guid id)
		{
			Catalog result = (await this.context.QueryAsync<Catalog>(
				GetDirectoryByIDStoredProcedure,
				new
				{
					@ID = id
				})).FirstOrDefault();

			result?.AcceptChanges();

			return result;
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
		public async Task<IEnumerable<Catalog>> FindAsync(int offset = 0, int limit = 20)
		{
			return await Task.FromException<IEnumerable<Catalog>>(new NotSupportedException());
		}

		/// <summary>
		/// Gets the list of entities that match the specified one.
		/// </summary>
		/// <param name="catalog">The catalog to search by.</param>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to select.</param>
		/// <returns>
		/// The list of instances of <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.Catalog" />.
		/// </returns>
		public async Task<IEnumerable<Catalog>> FindAsync(Catalog catalog, int offset = 0, int limit = 20)
		{
			IEnumerable<Catalog> result = await this.context.QueryAsync<Catalog>(
				GetDirectoryByParentIDStoredProcedure,
				new
				{
					@Name = string.IsNullOrWhiteSpace(catalog?.Name) ? null : catalog.Name.Trim().ToLower(),
					@ParentID = catalog?.ParentID,
					@StartIndex = offset,
					@ChunkSize = limit
				});

			foreach (Catalog item in result)
			{
				item.AcceptChanges();
			}

			return result;
		}

		/// <summary>
		/// Saves the specified entity.
		/// </summary>
		/// <param name="entity">The entity of type <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.Catalog" />.</param>
		/// <returns>The instance of <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.Catalog" />.</returns>
		public async Task<Catalog> SaveAsync(Catalog entity)
		{
			Guid id = entity.ID == Guid.Empty ? Guid.NewGuid() : entity.ID;

			if (await this.context.ExecuteAsync(
				entity.ID == Guid.Empty ? InsertDirectoryStoredProcedure : UpdateDirectoryStoredProcedure,
				new
				{
					@ID = id,
					@ParentID = entity.ParentID,
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
