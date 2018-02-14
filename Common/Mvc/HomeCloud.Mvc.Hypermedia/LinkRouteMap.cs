namespace HomeCloud.Mvc.Hypermedia
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;

	#endregion

	/// <summary>
	/// Provides route map table for the route specified by route name and the collection of related <see cref="HATEOAS"/> link routes.
	/// </summary>
	public class LinkRouteMap : ILinkRouteMap
	{
		#region Privare Members

		/// <summary>
		/// The mapping container.
		/// </summary>
		private readonly IDictionary<string, IEnumerable<LinkRoute>> container = new Dictionary<string, IEnumerable<LinkRoute>>();

		/// <summary>
		/// The builders
		/// </summary>
		private readonly IList<ILinkRouteBuilder> builders = new List<ILinkRouteBuilder>();

		#endregion

		#region ILinkRouteMap Implementations

		/// <summary>
		/// Creates the route builder for the route specified by route name.
		/// </summary>
		/// <param name="routeName">the route name.</param>
		/// <returns>
		/// the instance of <see cref="ILinkRouteBuilder" />
		/// </returns>
		public ILinkRouteBuilder AddRoute(string routeName)
		{
			if (string.IsNullOrWhiteSpace(routeName))
			{
				throw new ArgumentNullException(nameof(routeName));
			}

			ILinkRouteBuilder builder = builders.FirstOrDefault(item => item.RouteName.ToLower() == routeName?.ToLower());
			if (builder is null)
			{
				builder = new LinkRouteBuilder(this, routeName);
				this.builders.Add(builder);
			}

			return builder;
		}

		/// <summary>
		/// Adds mapping of the link routes collection and route specified by name to the current instance.
		/// </summary>
		/// <param name="routeName">The route name.</param>
		/// <param name="routes">The collection of <see cref="LinkRoute" />.</param>
		public void MapRoutes(string routeName, IEnumerable<LinkRoute> routes)
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
		/// Gets the link routes mapped to specified route.
		/// </summary>
		/// <param name="routeName">The route name.</param>
		/// <returns>
		/// The list of <see cref="LinkRoute" />.
		/// </returns>
		public IEnumerable<LinkRoute> GetRoutes(string routeName)
		{
			routeName = routeName?.ToLower();
			if (!this.container.ContainsKey(routeName))
			{
				return Enumerable.Empty<LinkRoute>();
			}

			return this.container[routeName];
		}

		/// <summary>
		/// Builds the route map.
		/// </summary>
		public void Build()
		{
			foreach (ILinkRouteBuilder builder in this.builders)
			{
				builder.Map();
			}
		}

		#endregion
	}
}
