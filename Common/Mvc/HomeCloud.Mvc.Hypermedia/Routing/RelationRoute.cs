namespace HomeCloud.Mvc.Hypermedia.Routing
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Represents the hypermedia route.
	/// </summary>
	public class RelationRoute
	{
		/// <summary>
		/// Gets or sets the relation name.
		/// </summary>
		/// <value>
		/// The relation name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the routes.
		/// </summary>
		/// <value>
		/// The routes.
		/// </value>
		public IList<Route> Routes { get; set; } = new List<Route>();
	}
}
