namespace HomeCloud.Mvc.Hypermedia
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Defines methods to build <see cref="HATEOAS"/> links for the specified route.
	/// </summary>
	public interface ILinkRouteBuilder
	{
		/// <summary>
		/// Gets the route name.
		/// </summary>
		/// <value>
		/// The route name.
		/// </value>
		string RouteName { get; }

		/// <summary>
		/// Adds the route link to the builder.
		/// </summary>
		/// <typeparam name="T">the type of model to build link for.</typeparam>
		/// <param name="linkName">The link name. According to <see cref="HATEOAS"/> principle the value corresponds to <see cref="Link.Relation"/>.</param>
		/// <param name="routeName">The route name of an action the link is being generated for.</param>
		/// <param name="routeValues">The route values required by an action the link is being generated for.</param>
		/// <param name="condition">The value defining whether the link should be generated.</param>
		/// <returns>the instance of<see cref="ILinkRouteBuilder"/>.</returns>
		ILinkRouteBuilder AddRouteLink<T>(string linkName, string routeName, Func<T, object> routeValues, Func<T, bool> condition = null)
			where T : class;

		/// <summary>
		/// Maps the added <see cref="HATEOAS"/> links to the route specified by name.
		/// </summary>
		void Map();
	}
}
