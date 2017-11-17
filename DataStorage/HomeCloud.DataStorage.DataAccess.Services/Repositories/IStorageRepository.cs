namespace HomeCloud.DataStorage.DataAccess.Services.Repositories
{
	#region Usings

	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataStorage.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Defines methods to handle <see cref="Storage"/> data.
	/// </summary>
	public interface IStorageRepository : IDbRepository<Storage>
	{
		/// <summary>
		/// Looks for all records of <see cref="Storage" /> that have <see cref="Storage.Name"/> value matched to specified name.
		/// </summary>
		/// <param name="name">The storage name.</param>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to select.</param>
		/// <returns>
		/// The list of instances of <see cref="Storage" /> type.
		/// </returns>
		Task<IEnumerable<Storage>> FindAsync(string name, int offset = 0, int limit = 20);
	}
}
