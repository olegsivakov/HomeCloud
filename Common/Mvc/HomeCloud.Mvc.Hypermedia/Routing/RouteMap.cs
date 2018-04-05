namespace HomeCloud.Mvc.Hypermedia.Routing
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;

	#endregion

	/// <summary>
	/// Implements mapping table for the route specified by route name and the collection of related <see cref="HATEOAS" /> routes.
	/// </summary>
	/// <seealso cref="HomeCloud.Mvc.Hypermedia.Routing.IRouteMap" />
	internal class RouteMap : IRouteMap
	{
		#region Privare Members

		/// <summary>
		/// The mapping container.
		/// </summary>
		private readonly IDictionary<string, IEnumerable<RelationRoute>> container = new Dictionary<string, IEnumerable<RelationRoute>>();

		/// <summary>
		/// The builders
		/// </summary>
		private readonly IList<IRouteBuilder> builders = new List<IRouteBuilder>();

		#endregion

		#region IRouteMap Implementations

		/// <summary>
		/// Creates the route builder for the route specified by name.
		/// </summary>
		/// <param name="routeName">the route name.</param>
		/// <returns>
		/// the instance of <see cref="T:HomeCloud.Mvc.Hypermedia.Routing.IRouteBuilder" />
		/// </returns>
		/// <exception cref="ArgumentNullException">routeName</exception>
		public IRouteBuilder AddRoute(string routeName)
		{
			if (string.IsNullOrWhiteSpace(routeName))
			{
				throw new ArgumentNullException(nameof(routeName));
			}

			IRouteBuilder builder = builders.FirstOrDefault(item => item.RouteName.ToLower() == routeName?.ToLower());
			if (builder is null)
			{
				builder = new RouteBuilder(this, routeName);
				this.builders.Add(builder);
			}

			return builder;
		}

		/// <summary>
		/// Adds mapping of the route collection and route specified by name to the current instance.
		/// </summary>
		/// <param name="routeName">The route name.</param>
		/// <param name="routes">The collection of <see cref="T:HomeCloud.Mvc.Hypermedia.Routing.RelationRoute" />.</param>
		public void MapRoutes(string routeName, IEnumerable<RelationRoute> routes)
		{
			if (routeName != null && routes.Any())
			{
				routeName = routeName.ToLower();
				if (this.container.ContainsKey(routeName))
				{
					this.container[routeName] = routes;
				}
				else
				{
					this.container.Add(routeName, routes);
				}
			}
		}

		/// <summary>
		/// Gets the routes mapped to route specified by name.
		/// </summary>
		/// <param name="routeName">The route name.</param>
		/// <returns>
		/// The list of <see cref="T:HomeCloud.Mvc.Hypermedia.Routing.RelationRoute" />.
		/// </returns>
		public IEnumerable<RelationRoute> GetRoutes(string routeName)
		{
			routeName = routeName?.ToLower();
			if (!this.container.ContainsKey(routeName))
			{
				return Enumerable.Empty<RelationRoute>();
			}

			return this.container[routeName];
		}

		/// <summary>
		/// Builds the route map.
		/// </summary>
		public void Build()
		{
			foreach (IRouteBuilder builder in this.builders)
			{
				builder.Map();
			}
		}

		#endregion
	}
}
