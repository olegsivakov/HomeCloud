namespace HomeCloud.Mvc.Hypermedia.Routing
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents the route.
	/// </summary>
	public class Route
	{
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
		public virtual Func<object, object> RouteValues { get; set; }

		/// <summary>
		/// Gets or sets the delegate indicating whether the current instance is reliable.
		/// </summary>
		/// <value>
		/// The delegate.
		/// </value>
		public virtual Func<object, bool> Condition { get; set; }
	}
}
