namespace HomeCloud.DataStorage.Api.Models
{
	#region Usings

	using System.Collections.Generic;

	using HomeCloud.Http;

	using Newtonsoft.Json;
	using System.Linq;

	#endregion

	/// <summary>
	/// Represents the list of view model items.
	/// </summary>
	/// <typeparam name="T">The model type.</typeparam>
	/// <seealso cref="System.Collections.Generic.List{T}" />
	public class PagedListViewModel<T> : List<T>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PagedListViewModel{T}"/> class.
		/// </summary>
		public PagedListViewModel()
			: base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PagedListViewModel{T}"/> class.
		/// </summary>
		/// <param name="items">The items.</param>
		public PagedListViewModel(IEnumerable<T> items)
			: base(items ?? Enumerable.Empty<T>())
		{
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the offset.
		/// </summary>
		/// <value>
		/// The offset.
		/// </value>
		[JsonIgnore]
		public int Offset { get; set; }

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>
		/// The size.
		/// </value>
		[JsonIgnore]
		public int Size { get; set; }

		/// <summary>
		/// Gets or sets the total count.
		/// </summary>
		/// <value>
		/// The total count.
		/// </value>
		[HttpHeader("X-Total-Count")]
		public int TotalCount { get; set; }

		#endregion
	}
}
