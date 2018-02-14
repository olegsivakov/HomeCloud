namespace HomeCloud.Mvc.Hypermedia
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Defines route map table for the route specified by route name and the collection of related <see cref="HATEOAS"/> link routes.
	/// </summary>
	public interface ILinkRouteMap
	{
		/// <summary>
		/// Creates the route builder for the route specified by route name.
		/// </summary>
		/// <param name="routeName">the route name.</param>
		/// <returns>the instance of <see cref="ILinkRouteBuilder"/></returns>
		ILinkRouteBuilder AddRoute(string routeName);

		/// <summary>
		/// Adds mapping of the link routes collection and route specified by name to the current instance.
		/// </summary>
		/// <param name="routeName">The route name.</param>
		/// <param name="routes">The collection of <see cref="LinkRoute"/>.</param>
		void MapRoutes(string routeName, IEnumerable<LinkRoute> routes);

		/// <summary>
		/// Gets the link routes mapped to specified route.
		/// </summary>
		/// <param name="routeName">The route name.</param>
		/// <returns>The list of <see cref="LinkRoute"/>.</returns>
		IEnumerable<LinkRoute> GetRoutes(string routeName);

		/// <summary>
		/// Builds the route map.
		/// </summary>
		void Build();
	}
}
