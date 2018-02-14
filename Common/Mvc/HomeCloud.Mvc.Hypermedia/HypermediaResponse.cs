namespace HomeCloud.Mvc.Hypermedia
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Represents the <see cref="HTTP"/> response model containing object with hypermedia links.
	/// </summary>
	public class HypermediaResponse
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HypermediaResponse"/> class.
		/// </summary>
		/// <param name="data">The data.</param>
		public HypermediaResponse(object data)
		{
			this.Data = data;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the response data.
		/// </summary>
		/// <value>
		/// The response data.
		/// </value>
		public object Data { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="HATEOAS"/> links.
		/// </summary>
		/// <value>
		/// The <see cref="HATEOAS"/> links.
		/// </value>
		public IEnumerable<Link> Links { get; set; }

		#endregion
	}
}
