namespace HomeCloud.Mvc.Hypermedia
{
	#region Usings

	using System.Collections.Generic;

	using HomeCloud.Mvc.Hypermedia.Relations;

	#endregion

	/// <summary>
	/// Represents the <see cref="HTTP"/> response model containing object with hypermedia links.
	/// </summary>
	internal class HypermediaResponse
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
		/// Gets or sets the <see cref="HATEOAS"/> relation links.
		/// </summary>
		/// <value>
		/// The <see cref="HATEOAS"/> relation links.
		/// </value>
		public IEnumerable<IRelation> Relations { get; set; }

		#endregion
	}
}
