namespace HomeCloud.DataStorage.DataAccess
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Core;

	using HomeCloud.Data.SqlServer;
	using HomeCloud.DataStorage.DataAccess.Objects;

	#endregion

	/// <summary>
	/// Defines methods to handle <see cref="Catalog" /> data.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.SqlServer.ISqlServerDBRepository{Catalog}" />
	public interface ICatalogRepository : ISqlServerDBRepository<Catalog>
	{
		/// <summary>
		/// Gets the number of entities that match the specified one.
		/// </summary>
		/// <param name="catalog">>The catalog to search by.</param>
		/// <returns>The number of entities.</returns>
		Task<int> GetCountAsync(Catalog catalog);

		/// <summary>
		/// Deletes the list of entities by specified identifier of parent entity the list belongs to.
		/// </summary>
		/// <param name="id">The parent entity unique identifier.</param>
		/// <returns>The asynchronous operation.</returns>
		Task DeleteByParentIDAsync(Guid? id);

		/// <summary>
		/// Gets the list of entities that match the specified one.
		/// </summary>
		/// <param name="catalog">The catalog to search by.</param>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to select.</param>
		/// <returns>
		/// The list of instances of <see cref="T:HomeCloud.DataStorage.DataAccess.Contracts.Catalog" />.
		/// </returns>
		Task<IPaginable<Catalog>> FindAsync(Catalog catalog, int offset = 0, int limit = 20);
	}
}
