namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// 
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
		/// Gets the list of catalogs by specified parent one.
		/// </summary>
		/// <param name="parent">The parent catalog identifier.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The operation result containing the list of instances of <see cref="Catalog" />.
		/// </returns>
		Task<ServiceResult<IEnumerable<Catalog>>> GetCatalogsAsync(Guid parentID, int offset = 0, int limit = 20);
	}
}



//Create Storage
//Input:
//	Name:

//Validation:
//	- validate id does not exist
//	- validate name is not empty
//	- generate path by name and validate physical catalog does not exist

//Update Storage
//Input:
//	ID, Name, Size, quota

//Validation:
//	- Validate id exists
//	- Validate Name is not empty



//Create catalog/catalog entry
//Input:
//	Name, Parent ID

//Prepare:
//	Get parent catalog by parent id

//Validation:
//	- validate id does not exist
//	- validate name is not empty
//	- validate parent catalog not empty
//	- generate path by name and validate physical catalog does not exist

//Update Catalog
//Input:
//	ID, Name, Parent ID


//Preparation:
//	- Get parent catalog by parent id

//Validation:
//	- Validate id exists
//	- validate name is not empty
//	- validate parent catalog not empty
//	if parent id is different or name is different
//	- generate path by name and validate physical catalog does not exist
