namespace HomeCloud.Mvc.Hypermedia
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Linq;

	#endregion

	/// <summary>
	/// Provides methods to build <see cref="HATEOAS"/> links for the specified route.
	/// </summary>
	/// <seealso cref="HomeCloud.Mvc.Hypermedia.ILinkRouteBuilder" />
	public class LinkRouteBuilder : ILinkRouteBuilder
	{
		#region Private Members

		/// <summary>
		/// The <see cref="HATEOAS"/> link route map.
		/// </summary>
		private readonly ILinkRouteMap routeMap = null;

		/// <summary>
		/// The link route collection.
		/// </summary>
		private readonly IList<LinkRoute> routes = new List<LinkRoute>();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="LinkRouteBuilder"/> class.
		/// </summary>
		/// <param name="routeMap">The link route map instance.</param>
		/// <param name="routeName">The name of route to build the links for.</param>
		public LinkRouteBuilder(ILinkRouteMap routeMap, string routeName)
		{
			this.routeMap = routeMap;
			this.RouteName = routeName.ToLower();
		}

		#endregion

		#region ILinkRouteBuilder Implementations

		/// <summary>
		/// Gets the route name.
		/// </summary>
		/// <value>
		/// The route name.
		/// </value>
		public string RouteName { get; private set; }

		/// <summary>
		/// Adds the route link to the builder.
		/// </summary>
		/// <typeparam name="T">the type of model to build link for.</typeparam>
		/// <param name="linkName">The link name. According to <see cref="HATEOAS" /> principle the value corresponds to <see cref="Link.Relation" />.</param>
		/// <param name="routeName">The route name of an action the link is being generated for.</param>
		/// <param name="routeValues">The route values required by an action the link is being generated for.</param>
		/// <param name="condition">The value defining whether the link should be generated.</param>
		/// <returns>
		/// the instance of<see cref="ILinkRouteBuilder" />.
		/// </returns>
		public ILinkRouteBuilder AddRouteLink<T>(string linkName, string routeName, Func<T, object> routeValues, Func<T, bool> condition = null)
			where T : class
		{
			if (string.IsNullOrWhiteSpace(linkName))
			{
				throw new ArgumentNullException(nameof(linkName));
			}

			if (this.routes.Any(route => route.Name.ToLower() == linkName.ToLower()))
			{
				throw new DuplicateNameException(linkName);
			}

			this.routes.Add(new LinkRoute()
			{
				Name = linkName,
				RouteName = routeName,
				RouteValues = model => routeValues != null && model is T ? routeValues(model as T) : null,
				Condition = model =>
				{
					if (condition is null)
					{
						return true;
					}

					return model is T ? condition(model as T) : false;
				}
			});

			return this;
		}

		/// <summary>
		/// Maps the added <see cref="HATEOAS" /> links to the route specified by name.
		/// </summary>
		public void Map()
		{
			this.routeMap.MapRoutes(this.RouteName, this.routes);
		}

		#endregion
	}
}
