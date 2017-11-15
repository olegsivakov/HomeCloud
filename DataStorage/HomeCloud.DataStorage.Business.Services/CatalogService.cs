namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Provides methods to handle catalogs.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Services.ICatalogService" />
	public class CatalogService : ICatalogService
	{
		/// <summary>
		/// Creates the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.Catalog" /> type.</param>
		/// <returns>
		/// The operation result containing created instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.Catalog" />.
		/// </returns>
		public Task<ServiceResult<Catalog>> CreateCatalogAsync(Catalog catalog)
		{
			
		}

		/// <summary>
		/// Deletes the catalog by specified identifier.
		/// </summary>
		/// <param name="id">The catalog identifier.</param>
		/// <returns>
		/// The operation result.
		/// </returns>
		public Task<ServiceResult> DeleteCatalogAsync(Guid id)
		{
		}

		/// <summary>
		/// Gets the catalog by specified identifier.
		/// </summary>
		/// <param name="id">The catalog identifier.</param>
		/// <returns>
		/// The operation result containing the instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.Catalog" />.
		/// </returns>
		public Task<ServiceResult<Catalog>> GetCatalogAsync(Guid id)
		{
		}

		/// <summary>
		/// Gets the list of catalogs by specified parent one.
		/// </summary>
		/// <param name="parent">The parent catalog.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The operation result containing the list of instances of <see cref="T:HomeCloud.DataStorage.Business.Entities.Catalog" />.
		/// </returns>
		public Task<ServiceResult<IEnumerable<Catalog>>> GetCatalogsAsync(Catalog parent, int offset = 0, int limit = 20)
		{
		}

		/// <summary>
		/// Updates the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.Catalog" /> type.</param>
		/// <returns>
		/// The operation result containing updated instance of <see cref="T:HomeCloud.DataStorage.Business.Entities.Catalog" />.
		/// </returns>
		public Task<ServiceResult<Catalog>> UpdateCatalogAsync(Catalog catalog)
		{
		}
	}
}
