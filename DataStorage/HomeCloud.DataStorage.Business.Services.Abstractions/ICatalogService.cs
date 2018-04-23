namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Defines methods to handle catalogs.
	/// </summary>
	public interface ICatalogService
	{
		/// <summary>
		/// Creates the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="Catalog" /> type.</param>
		/// <returns>
		/// The operation result containing created instance of <see cref="Catalog"/>.
		/// </returns>
		Task<ServiceResult<Catalog>> CreateCatalogAsync(Catalog catalog);

		/// <summary>
		/// Updates the specified catalog.
		/// </summary>
		/// <param name="catalog">The instance of <see cref="Catalog" /> type.</param>
		/// <returns>
		/// The operation result containing updated instance of <see cref="Catalog"/>.
		/// </returns>
		Task<ServiceResult<Catalog>> UpdateCatalogAsync(Catalog catalog);

		/// <summary>
		/// Deletes the catalog by specified identifier.
		/// </summary>
		/// <param name="id">The catalog identifier.</param>
		/// <returns>The operation result.</returns>
		Task<ServiceResult> DeleteCatalogAsync(Guid id);

		/// <summary>
		/// Gets the catalog by specified identifier.
		/// </summary>
		/// <param name="id">The catalog identifier.</param>
		/// <returns>
		/// The operation result containing the instance of <see cref="Catalog" />.
		/// </returns>
		Task<ServiceResult<Catalog>> GetCatalogAsync(Guid id);

		/// <summary>
		/// Validates catalog asynchronously.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		/// <returns>The operation result.</returns>
		Task<ServiceResult> ValidateAsync(Catalog catalog);

		/// <summary>
		/// Gets the list of catalogs by specified parent one.
		/// </summary>
		/// <param name="parentID">The parent catalog identifier.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The operation result containing the list of instances of <see cref="Catalog" />.
		/// </returns>
		Task<ServiceResult<IPaginable<Catalog>>> GetCatalogsAsync(Guid parentID, int offset = 0, int limit = 20);

		/// <summary>
		/// Calculates and syncs the size of the specified catalog.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		/// <returns>The updated catalog.</returns>
		Task<ServiceResult<Catalog>> CalculateSize(Catalog catalog);
	}
}
