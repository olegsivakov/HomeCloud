namespace HomeCloud.Mvc.Hypermedia.Routing
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Defines mapping table for the route specified by route name and the collection of related <see cref="HATEOAS"/> routes.
	/// </summary>
	public interface IRouteMap
	{
		/// <summary>
		/// Creates the route builder for the route specified by name.
		/// </summary>
		/// <param name="routeName">the route name.</param>
		/// <returns>the instance of <see cref="IRouteBuilder"/></returns>
		IRouteBuilder AddRoute(string routeName);

		/// <summary>
		/// Adds mapping of the route collection and route specified by name to the current instance.
		/// </summary>
		/// <param name="routeName">The route name.</param>
		/// <param name="routes">The collection of <see cref="RelationRoute"/>.</param>
		void MapRoutes(string routeName, IEnumerable<RelationRoute> routes);

		/// <summary>
		/// Gets the routes mapped to route specified by name.
		/// </summary>
		/// <param name="routeName">The route name.</param>
		/// <returns>The list of <see cref="RelationRoute"/>.</returns>
		IEnumerable<RelationRoute> GetRoutes(string routeName);

		/// <summary>
		/// Builds the route map.
		/// </summary>
		void Build();
	}
}
