namespace HomeCloud.Mvc.Hypermedia.Routing
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Linq;

	#endregion

	/// <summary>
	/// Provides methods to build <see cref="HATEOAS"/> route relations for the specified route.
	/// </summary>
	/// <seealso cref="HomeCloud.Mvc.Hypermedia.Routing.IRouteBuilder" />
	internal class RouteBuilder : IRouteBuilder
	{
		#region Private Members

		/// <summary>
		/// The <see cref="HATEOAS"/> link route map.
		/// </summary>
		private readonly IRouteMap routeMap = null;

		/// <summary>
		/// The link route collection.
		/// </summary>
		private readonly IList<Route> routes = new List<Route>();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="RouteBuilder"/> class.
		/// </summary>
		/// <param name="routeMap">The link route map instance.</param>
		/// <param name="routeName">The name of route to build the links for.</param>
		public RouteBuilder(IRouteMap routeMap, string routeName)
		{
			this.routeMap = routeMap;
			this.RouteName = routeName.ToLower();
		}

		#endregion

		#region IRouteMap Implementations

		/// <summary>
		/// Gets the route name.
		/// </summary>
		/// <value>
		/// The route name.
		/// </value>
		public string RouteName { get; private set; }

		/// <summary>
		/// Adds the route to the builder.
		/// </summary>
		/// <typeparam name="T">the type of model to build link for.</typeparam>
		/// <param name="relation">The link relation name. According to <see cref="!:HATEOAS" /> the value corresponds to <see cref="!:Link.Relation" />.</param>
		/// <param name="routeName">The route name of an action the link is being generated for.</param>
		/// <param name="routeValues">The route values required by an action the link is being generated for.</param>
		/// <param name="condition">The value defining whether the link should be generated.</param>
		/// <returns>
		/// The instance of<see cref="T:HomeCloud.Mvc.Hypermedia.Routing.IRouteBuilder" />.
		/// </returns>
		/// <exception cref="ArgumentNullException">relation</exception>
		/// <exception cref="DuplicateNameException">relation</exception>
		public IRouteBuilder AddRoute<T>(string relation, string routeName, Func<T, object> routeValues, Func<T, bool> condition = null)
			where T : class
		{
			if (string.IsNullOrWhiteSpace(relation))
			{
				throw new ArgumentNullException(nameof(relation));
			}

			if (this.routes.Any(route => route.Name.ToLower() == relation.ToLower()))
			{
				throw new DuplicateNameException(relation);
			}

			this.routes.Add(new Route()
			{
				Name = relation,
				RouteName = routeName,
				RouteValues = model => routeValues != null && model is T ? routeValues(model as T) : null,
				Condition = model =>
				{
					if (model is T)
					{
						return condition is null || condition(model as T);
					}

					return false;
				}
			});

			return this;
		}

		/// <summary>
		/// Adds the route to the builder.
		/// </summary>
		/// <typeparam name="TList">The type of the model for which items the links should be built.</typeparam>
		/// <typeparam name="T">the type of item to build link for.</typeparam>
		/// <param name="relation">The link relation name. According to <see cref="!:HATEOAS" /> the value corresponds to <see cref="!:Link.Relation" />.</param>
		/// <param name="routeName">The route name of an action the link is being generated for.</param>
		/// <param name="routeValues">The route values required by an action the link is being generated for.</param>
		/// <returns>
		/// The instance of<see cref="IRouteBuilder" />.
		/// </returns>
		/// <exception cref="ArgumentNullException">relation</exception>
		/// <exception cref="DuplicateNameException"></exception>
		public IRouteBuilder AddRoute<TList, T>(string relation, string routeName, Func<T, object> routeValues)
			where TList : class, IEnumerable<T>
			where T : class
		{
			if (string.IsNullOrWhiteSpace(relation))
			{
				throw new ArgumentNullException(nameof(relation));
			}

			if (this.routes.Any(route => route.Name.ToLower() == relation.ToLower()))
			{
				throw new DuplicateNameException(relation);
			}

			this.routes.Add(new Route()
			{
				Name = relation,
				RouteName = routeName,
				RouteValues = model => routeValues != null && model is T ? routeValues(model as T) : null,
				Condition = model => model is T
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
