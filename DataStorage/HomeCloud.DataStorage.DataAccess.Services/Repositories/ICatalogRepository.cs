﻿namespace HomeCloud.DataStorage.DataAccess.Services.Repositories
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataStorage.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Defines methods to handle <see cref="Catalog" /> data.
	/// </summary>
	/// <seealso cref="HomeCloud.DataAccess.Services.IDbRepository{HomeCloud.DataStorage.DataAccess.Contracts.Catalog}" />
	public interface ICatalogRepository : IDbRepository<Catalog>
	{
		/// <summary>
		/// Deletes the list of entities by specified identifier of parent entity the list belongs to.
		/// </summary>
		/// <param name="id">The parent entity unique identifier.</param>
		/// <returns>The asynchronous operation.</returns>
		Task DeleteByParentIDAsync(Guid? id);

		/// <summary>
		/// Gets the list of entities by specified identifier of parent entity the list belongs to.
		/// </summary>
		/// <param name="storageID">The storage identifier.</param>
		/// <param name="parentID">The parent identifier.</param>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to select.</param>
		/// <returns>
		/// The list of instances of <see cref="Catalog" />.
		/// </returns>
		Task<IEnumerable<Catalog>> GetByParentIDAsync(Guid storageID, Guid? parentID, int offset = 0, int limit = 20);
	}
}