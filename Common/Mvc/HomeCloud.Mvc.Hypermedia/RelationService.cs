namespace HomeCloud.Mvc.Hypermedia
{
	#region Usings

	using System.Linq;
	using System.Collections;
	using System.Collections.Generic;

	using HomeCloud.Mvc.Hypermedia.Relations;
	using HomeCloud.Mvc.Hypermedia.Routing;

	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Abstractions;
	using Microsoft.AspNetCore.Mvc.Infrastructure;
	using Microsoft.AspNetCore.Mvc.Internal;
	using Microsoft.AspNetCore.Mvc.Routing;

	#endregion

	/// <summary>
	/// Provides methods to handle relations.
	/// </summary>
	internal class RelationService : IRelationService
	{
		#region Private Members

		/// <summary>
		/// The action descriptor collection.
		/// </summary>
		private readonly IEnumerable<ActionDescriptor> actionDescriptors = null;

		/// <summary>
		/// The <see cref="URL"/> helper
		/// </summary>
		private readonly IUrlHelper urlHelper = null;

		/// <summary>
		/// The link route map.
		/// </summary>
		private readonly IRouteMap routes = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="RelationService"/> class.
		/// </summary>
		/// <param name="routes">The routes.</param>
		/// <param name="actionDescriptorProvider">The action descriptot provider.</param>
		/// <param name="urlHelperFactory">The <see cref="URL"/> helper factory.</param>
		/// <param name="actionContextAccessor">The action context accessor.</param>
		public RelationService(
			IRouteMap routes,
			IActionDescriptorCollectionProvider actionDescriptorProvider,
			IUrlHelperFactory urlHelperFactory,
			IActionContextAccessor actionContextAccessor)
		{
			this.routes = routes;
			this.actionDescriptors = actionDescriptorProvider?.ActionDescriptors?.Items?.AsEnumerable() ?? Enumerable.Empty<ActionDescriptor>();
			this.urlHelper = urlHelperFactory?.GetUrlHelper(actionContextAccessor.ActionContext);
		}

		#endregion

		#region IRelationService Implementations

		/// <summary>
		/// Gets the collection of relations for the specified model instance within the specified route.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="route">The name of the route.</param>
		/// <returns>the list of <see cref="IRelation"/>.</returns>
		public IEnumerable<IRelation> GetRelations(object model, string routeName)
		{
			List<IRelation> result = new List<IRelation>();

			IEnumerable<Route> routes = this.routes.GetRoutes(routeName);

			result.AddRange(routes.Where(route => route.Condition(model)).Select(route => new Relation()
			{
				Name = route.Name,
				Link = new Link()
				{
					Href = this.urlHelper.Link(route.RouteName, route.RouteValues(model))?.ToLower(),
					Method = this.actionDescriptors
										.FirstOrDefault(descriptor => descriptor.AttributeRouteInfo.Name == route.RouteName)?
										.ActionConstraints.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods.FirstOrDefault()?.ToLower()
				}
			}));

			if (model is IEnumerable)
			{
				IEnumerable<object> items = (model as IEnumerable).Cast<object>();

				result.AddRange(routes.Where(route => items.Any(item => route.Condition(item))).Select(route => new RelationList()
				{
					Name = route.Name,
					Links = items.Where(item => route.Condition(item)).Select(item => new Link()
					{
						Href = this.urlHelper.Link(route.RouteName, route.RouteValues(item))?.ToLower(),
						Method = this.actionDescriptors
									.FirstOrDefault(descriptor => descriptor.AttributeRouteInfo.Name == route.RouteName)?
									.ActionConstraints.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods.FirstOrDefault()?.ToLower()
					})
				}).Where(relation => relation.Links.Any()));
			}

			return result;
		}

		#endregion
	}
}
