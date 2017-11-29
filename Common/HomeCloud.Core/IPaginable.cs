namespace HomeCloud.Core
{
	#region Usings

	using System.Collections;

	#endregion

	/// <summary>
	/// Exposes the enumerator that support a simple iteration over the subset of a non-generic collection.
	/// </summary>
	/// <seealso cref="System.Collections.IEnumerable" />
	public interface IPaginable : IEnumerable
	{
		/// <summary>
		/// Gets or sets the offset index.
		/// </summary>
		/// <value>
		/// The offset index.
		/// </value>
		int Offset { get; set; }

		/// <summary>
		/// Gets or sets the number of items to be presented in the collection.
		/// </summary>
		/// <value>
		/// The number of items to be presented in the collection.
		/// </value>
		int Limit { get; set; }

		/// <summary>
		/// Gets or sets the total number of items.
		/// </summary>
		/// <value>
		/// The total count.
		/// </value>
		int TotalCount { get; set; }
	}
}
