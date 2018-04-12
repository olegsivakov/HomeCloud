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
	/// Provides methods to handle <see cref="Storage" /> data.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.SqlServer.SqlServerDBRepository{HomeCloud.DataStorage.DataAccess.Objects.Storage}" />
	/// <seealso cref="HomeCloud.DataStorage.DataAccess.IStorageRepository" />
	public sealed class StorageRepository : SqlServerDBRepository<Storage>, IStorageRepository
	{
		#region Constants

		/// <summary>
		/// The <see cref="[dbo].[InsertStorage]"/> stored procedure name.
		/// </summary>
		private const string InsertStorageStoredProcedure = "[dbo].[InsertStorage]";

		/// <summary>
		/// The <see cref="[dbo].[UpdateStorage]"/> stored procedure name.
		/// </summary>
		private const string UpdateStorageStoredProcedure = "[dbo].[UpdateStorage]";

		/// <summary>
		/// The <see cref="[dbo].[DeleteStorageByID]"/> stored procedure name.
		/// </summary>
		private const string DeleteStorageByIDStoredProcedure = "[dbo].[DeleteStorageByID]";

		/// <summary>
		/// The <see cref="[dbo].[GetStorage]"/> stored procedure name.
		/// </summary>
		private const string GetStorageStoredProcedure = "[dbo].[GetStorage]";

		/// <summary>
		/// The <see cref="[dbo].[GetStorageCount]"/> stored procedure name.
		/// </summary>
		private const string GetStorageCountStoredProcedure = "[dbo].[GetStorageCount]";

		/// <summary>
		/// The <see cref="[dbo].[GetStorageByID]"/> stored procedure name.
		/// </summary>
		private const string GetStorageByIDStoredProcedure = "[dbo].[GetStorageByID]";

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StorageRepository"/> class.
		/// </summary>
		/// <param name="context">The data context.</param>
		public StorageRepository(ISqlServerDBContext context)
			: base(context)
		{
		}

		#endregion

		#region IStorageRepository Implementations

		/// <summary>
		/// Gets the number of entities that match the specified one.
		/// </summary>
		/// <param name="storage">>The storage to search by.</param>
		/// <returns>The number of entities.</returns>
		public async Task<int> GetCountAsync(Storage storage = null)
		{
			return await this.Context.ExecuteScalarAsync<object, int>(
				GetStorageCountStoredProcedure,
				new
				{
					@Name = string.IsNullOrWhiteSpace(storage?.Name) ? null : storage.Name.Trim().ToLower()
				});
		}

		/// <summary>
		/// Gets the list of entities that match the specified one.
		/// </summary>
		/// <param name="storage">The storage.</param>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to select.</param>
		/// <returns>
		/// The list of instances of <see cref="T:HomeCloud.DataStorage.DataAccess.Objects.Storage" /> type.
		/// </returns>
		public async Task<IPaginable<Storage>> FindAsync(Storage storage, int offset = 0, int limit = 20)
		{
			IEnumerable<Storage> result = await this.Context.QueryAsync<Storage>(
				GetStorageStoredProcedure,
				new
				{
					@Name = string.IsNullOrWhiteSpace(storage?.Name) ? null : storage.Name.Trim().ToLower(),
					@StartIndex = offset,
					@ChunkSize = limit
				});

			Parallel.ForEach(result, item => item.AcceptChanges());
			int count = await this.GetCountAsync(storage);

			return new PagedList<Storage>(result)
			{
				Offset = offset,
				Limit = limit,
				TotalCount = count
			};
		}

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
				DeleteStorageByIDStoredProcedure,
				new
				{
					@ID = id
				});
		}

		/// <summary>
		/// Searches for all records of <see cref="!:T" /> type.
		/// </summary>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The list of instances of <see cref="!:T" /> type.
		/// </returns>
		public override async Task<IPaginable<Storage>> FindAsync(int offset = 0, int limit = 20)
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
		public override async Task<Storage> GetAsync(Guid id)
		{
			Storage result = (await this.Context.QueryAsync<Storage>(
				GetStorageByIDStoredProcedure,
				new
				{
					@ID = id
				})).FirstOrDefault();

			result?.AcceptChanges();

			return result;
		}

		/// <summary>
		/// Saves the specified entity of <see cref="!:T" /> asynchronously.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// The instance of <see cref="!:T" />.
		/// </returns>
		public override async Task<Storage> SaveAsync(Storage entity)
		{
			Guid id = entity.ID == Guid.Empty ? Guid.NewGuid() : entity.ID;

			string sqlQuery = entity.ID == Guid.Empty || (await this.GetAsync(id)) is null ? InsertStorageStoredProcedure : UpdateStorageStoredProcedure;

			if (await this.Context.ExecuteAsync(
				sqlQuery,
				new
				{
					@ID = id,
					@Name = entity.Name,
					@Quota = entity.Quota
				}) > 0)
			{
				return await this.GetAsync(id);
			}

			return entity;
		}

		#endregion
	}
}
