namespace HomeCloud.Data.SqlServer
{
	#region Usings

	using System.Collections.Generic;
	using System.Threading.Tasks;

	#endregion

	/// <summary>
	/// Represents methods to handle data of <see cref="T" /> stored in <see cref="SqlServer" /> database.
	/// </summary>
	/// <typeparam name="T">The type of data to handle.</typeparam>
	/// <seealso cref="HomeCloud.Data.SqlServer.ISqlServerDBRepository" />
	/// <seealso cref="HomeCloud.Data.IRepository{T}" />
	public interface ISqlServerDBRepository<T> : ISqlServerDBRepository, IRepository<T>
	{
		/// <summary>
		/// Searches for all records of <see cref="T" /> type.
		/// </summary>
		/// <param name="offset">The index of the first record that should appear in the list.</param>
		/// <param name="limit">The number of records to select.</param>
		/// <returns>
		/// The list of instances of <see cref="T" /> type.
		/// </returns>
		Task<IEnumerable<T>> FindAsync(int offset = 0, int limit = 20);
	}
}
