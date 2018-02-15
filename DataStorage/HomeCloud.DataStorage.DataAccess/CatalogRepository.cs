namespace HomeCloud.DataStorage.DataAccess
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.Core.Extensions;

	using HomeCloud.Data.SqlServer;
	using HomeCloud.DataStorage.DataAccess.Objects;

	#endregion

	/// <summary>
	/// Provides methods to handle <see cref="Catalog" /> data.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.SqlServer.SqlServerDBRepository{HomeCloud.DataStorage.DataAccess.Objects.Catalog}" />
	/// <seealso cref="HomeCloud.DataStorage.DataAccess.ICatalogRepository" />
	public sealed class CatalogRepository : SqlServerDBRepository<Catalog>, ICatalogRepository
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

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CatalogRepository"/> class.
		/// </summary>
		/// <param name="context">The data context.</param>
		public CatalogRepository(ISqlServerDBContext context)
			: base(context)
		{
		}

		#endregion

		#region ICatalogRepository Implementations

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
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public async Task DeleteByParentIDAsync(Guid? id)
		{
			await this.Context.ExecuteAsync(
				DeleteDirectoryByParentIDStoredProcedure,
				new
				{
					@ParentID = id
				});
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
		public async Task<IPaginable<Catalog>> FindAsync(Catalog catalog, int offset = 0, int limit = 20)
		{
			IEnumerable<Catalog> result = await this.Context.QueryAsync<Catalog>(
				GetDirectoryByParentIDStoredProcedure,
				new
				{
					@Name = string.IsNullOrWhiteSpace(catalog?.Name) ? null : catalog.Name.Trim().ToLower(),
					@ParentID = catalog?.ParentID,
					@StartIndex = offset,
					@ChunkSize = limit
				});

			await result.ForEachAsync(async item => await Task.Run(() => item.AcceptChanges()));
			int count = await this.GetCountAsync(catalog);

			return new PagedList<Catalog>(result)
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
		public override async Task<IPaginable<Catalog>> FindAsync(int offset = 0, int limit = 20)
		{
			return await this.FindAsync(offset, limit);
		}

		/// <summary>
		/// Gets the entity of <see cref="!:T" /> by specified unique identifier asynchronously.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>
		/// The instance of <see cref="!:T" /> type.
		/// </returns>
		public override async Task<Catalog> GetAsync(Guid id)
		{
			Catalog result = (await this.Context.QueryAsync<Catalog>(
				GetDirectoryByIDStoredProcedure,
				new
				{
					@ID = id
				})).FirstOrDefault();

			result?.AcceptChanges();

			return result;
		}

		/// <summary>
		/// Gets the number of entities that match the specified one.
		/// </summary>
		/// <param name="catalog">&gt;The catalog to search by.</param>
		/// <returns>
		/// The number of entities.
		/// </returns>
		public async Task<int> GetCountAsync(Catalog catalog)
		{
			return await this.Context.ExecuteScalarAsync<object, int>(
				GetDirectoryCountByParentIDStoredProcedure,
				new
				{
					@Name = string.IsNullOrWhiteSpace(catalog?.Name) ? null : catalog.Name.Trim().ToLower(),
					@ParentID = catalog?.ParentID,
				});
		}

		/// <summary>
		/// Saves the specified entity of <see cref="!:T" /> asynchronously.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// The instance of <see cref="!:T" />.
		/// </returns>
		public override async Task<Catalog> SaveAsync(Catalog entity)
		{
			Guid id = entity.ID == Guid.Empty ? Guid.NewGuid() : entity.ID;

			if (await this.Context.ExecuteAsync(
				entity.ID == Guid.Empty || (await this.GetAsync(id)) is null ? InsertDirectoryStoredProcedure : UpdateDirectoryStoredProcedure,
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
