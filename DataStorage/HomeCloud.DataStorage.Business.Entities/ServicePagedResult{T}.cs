namespace HomeCloud.DataStorage.Business.Entities
{
	#region Usings

	using System.Collections.Generic;
	using System.Linq;

	#endregion

	/// <summary>
	/// Represents the <see cref="IEnumerable{T}"/> paged result of service operation.
	/// </summary>
	/// <typeparam name="T">The type of the data in the result list.</typeparam>
	/// <seealso cref="HomeCloud.DataStorage.Business.Entities.ServiceResult{System.Collections.Generic.IEnumerable{T}}" />
	public class ServicePagedResult<T> : ServiceResult<IEnumerable<T>>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ServicePagedResult{T}"/> class.
		/// </summary>
		/// <param name="data">The result list of <see cref="!:T" /> type.</param>
		public ServicePagedResult(IEnumerable<T> data)
			: base(data)
		{
			this.Limit = this.Data.Count();
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the offset index.
		/// </summary>
		/// <value>
		/// The offset index.
		/// </value>
		public int Offset { get; set; }

		/// <summary>
		/// Gets or sets the number of items to return.
		/// </summary>
		/// <value>
		/// The number of records to return.
		/// </value>
		public int Limit { get; set; }

		/// <summary>
		/// Gets or sets the total number of items.
		/// </summary>
		/// <value>
		/// The total number of items.
		/// </value>
		public int TotalCount { get; set; }

		#endregion
	}
}
