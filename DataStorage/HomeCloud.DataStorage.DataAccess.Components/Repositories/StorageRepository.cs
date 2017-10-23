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
	/// Provides methods to handle <see cref="Storage"/> data.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.DataAccess.Services.Repositories.IStorageRepository" />
	public class StorageRepository : IStorageRepository
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
		/// The <see cref="[dbo].[GetStorageByID]"/> stored procedure name.
		/// </summary>
		private const string GetStorageByIDStoredProcedure = "[dbo].[GetStorageByID]";

		#endregion

		#region Private Members

		/// <summary>
		/// The database context
		/// </summary>
		private readonly IDbContext context = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StorageRepository" /> class.
		/// </summary>
		/// <param name="context">The database context.</param>
		public StorageRepository(IDbContext context)
		{
			this.context = context;
		}

		#endregion

		#region IStorageRepository Implementations

		/// <summary>
		/// Deletes the entity by specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		public void Delete(Guid id)
		{
			this.context.Execute(
				DeleteStorageByIDStoredProcedure,
				new
				{
					@ID = id
				});
		}

		/// <summary>
		/// Gets the entity by specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.Storage" />.
		/// </returns>
		public Storage Get(Guid id)
		{
			return this.context.Query<Storage>(
				GetStorageByIDStoredProcedure,
				new
				{
					@ID = id
				}).FirstOrDefault();
		}

		/// <summary>
		/// Looks for all records of <see cref="T" /> type.
		/// </summary>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to select.</param>
		/// <returns>
		/// The list of instances of <see cref="T" /> type.
		/// </returns>
		public IEnumerable<Storage> Find(int offset = 0, int limit = 20)
		{
			return this.context.Query<Storage>(
				GetStorageStoredProcedure,
				new
				{
					@StartIndex = offset,
					@ChunkSize = limit
				});
		}

		/// <summary>
		/// Saves the specified entity.
		/// </summary>
		/// <param name="entity">The entity of type <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.Storage" />.</param>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.Storage" />.
		/// </returns>
		public Storage Save(Storage entity)
		{
			return this.context.Query<Storage>(entity.ID == Guid.Empty ? InsertStorageStoredProcedure : UpdateStorageStoredProcedure, entity).FirstOrDefault();
		}

		#endregion
	}
}
