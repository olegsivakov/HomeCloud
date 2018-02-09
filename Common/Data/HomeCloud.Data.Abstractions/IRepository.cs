namespace HomeCloud.Data
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	#endregion

	/// <summary>
	/// Defines common methods to handle data.
	/// </summary>
	public interface IRepository
	{
		/// <summary>
		/// Deletes the record by specified unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>The asynchronous operation.</returns>
		Task DeleteAsync(Guid id);
	}
}
