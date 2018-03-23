namespace HomeCloud.Mvc.Hypermedia.Routing
{
	#region Usings

	using System;
	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Defines methods to build <see cref="HATEOAS"/> route relations for the specified route.
	/// </summary>
	public interface IRouteBuilder
	{
		/// <summary>
		/// Gets the route name.
		/// </summary>
		/// <value>
		/// The route name.
		/// </value>
		string RouteName { get; }

		/// <summary>
		/// Adds the route to the builder.
		/// </summary>
		/// <typeparam name="T">the type of model to build link for.</typeparam>
		/// <param name="relation">The link relation name. According to <see cref="HATEOAS"/> the value corresponds to <see cref="Link.Relation"/>.</param>
		/// <param name="routeName">The route name of an action the link is being generated for.</param>
		/// <param name="routeValues">The route values required by an action the link is being generated for.</param>
		/// <param name="condition">The value defining whether the link should be generated.</param>
		/// <returns>The instance of<see cref="IRouteBuilder" />.</returns>
		IRouteBuilder AddRoute<T>(string relation, string routeName, Func<T, object> routeValues, Func<T, bool> condition = null)
			where T : class;

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
		IRouteBuilder AddRoute<TList, T>(string relation, string routeName, Func<T, object> routeValues)
			where TList : class, IEnumerable<T>
			where T : class;

		/// <summary>
		/// Maps the added <see cref="HATEOAS"/> links to the route specified by name.
		/// </summary>
		void Map();
	}
}
