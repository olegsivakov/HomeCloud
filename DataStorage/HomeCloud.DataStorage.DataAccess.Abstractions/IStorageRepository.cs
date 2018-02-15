namespace HomeCloud.DataStorage.DataAccess
{
	#region Usings

	using System.Threading.Tasks;

	using HomeCloud.Core;

	using HomeCloud.Data.SqlServer;
	using HomeCloud.DataStorage.DataAccess.Objects;

	#endregion

	/// <summary>
	/// Defines methods to handle <see cref="Storage"/> data.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.SqlServer.ISqlServerDBRepository{HomeCloud.DataStorage.DataAccess.Objects.Storage}" />
	public interface IStorageRepository : ISqlServerDBRepository<Storage>
	{
		/// <summary>
		/// Gets the number of entities that match the specified one.
		/// </summary>
		/// <param name="storage">>The storage to search by.</param>
		/// <returns>The number of entities.</returns>
		Task<int> GetCountAsync(Storage storage = null);

		/// <summary>
		/// Gets the list of entities that match the specified one.
		/// </summary>
		/// <param name="storage">The storage.</param>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to select.</param>
		/// <returns>
		/// The list of instances of <see cref="Storage" /> type.
		/// </returns>
		Task<IPaginable<Storage>> FindAsync(Storage storage, int offset = 0, int limit = 20);
	}
}
