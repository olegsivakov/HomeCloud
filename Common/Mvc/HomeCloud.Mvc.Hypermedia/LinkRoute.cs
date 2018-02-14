namespace HomeCloud.Mvc.Hypermedia
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents the link route.
	/// </summary>
	public class LinkRoute
	{
		/// <summary>
		/// Gets or sets the link name.
		/// </summary>
		/// <value>
		/// The link name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the route name for the link.
		/// </summary>
		/// <value>
		/// The route name.
		/// </value>
		public string RouteName { get; set; }

		/// <summary>
		/// Gets or sets the delegate to generate route values for the link.
		/// </summary>
		/// <value>
		/// The route values delegate.
		/// </value>
		public Func<object, object> RouteValues { get; set; }

		/// <summary>
		/// Gets or sets the delegate indicating whether the current instance is reliable.
		/// </summary>
		/// <value>
		/// The delegate.
		/// </value>
		public Func<object, bool> Condition { get; set; }
	}
}
