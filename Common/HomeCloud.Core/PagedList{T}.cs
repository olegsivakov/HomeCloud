namespace HomeCloud.Core
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Represents a strongly-typed list of object subset that can be accessed by index.
	/// </summary>
	/// <typeparam name="T">The type of object.</typeparam>
	/// <seealso cref="System.Collections.Generic.List{T}" />
	/// <seealso cref="HomeCloud.Core.IPaginable" />
	public class PagedList<T> : List<T>, IPaginable<T>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PagedList{T}"/> class.
		/// </summary>
		public PagedList()
			: base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PagedList{T}"/> class.
		/// </summary>
		/// <param name="items">The items.</param>
		public PagedList(IEnumerable<T> items)
			: base(items)
		{
		}

		#endregion

		#region IPaginable Implemantations

		/// <summary>
		/// Gets or sets the offset index.
		/// </summary>
		/// <value>
		/// The offset index.
		/// </value>
		public int Offset { get; set; }

		/// <summary>
		/// Gets or sets the number of items to be presented in the collection.
		/// </summary>
		/// <value>
		/// The number of items to be presented in the collection.
		/// </value>
		public int Limit { get; set; }

		/// <summary>
		/// Gets or sets the total number of items.
		/// </summary>
		/// <value>
		/// The total count.
		/// </value>
		public int TotalCount { get; set; }

		#endregion
	}
}
