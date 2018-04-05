namespace HomeCloud.Mvc.Hypermedia
{
	#region Usings

	using System.Linq;
	using System.Collections;
	using System.Collections.Generic;

	using HomeCloud.Mvc.ActionConstraints;
	using HomeCloud.Mvc.Hypermedia.Relations;
	using HomeCloud.Mvc.Hypermedia.Routing;

	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Abstractions;
	using Microsoft.AspNetCore.Mvc.ActionConstraints;
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

			IEnumerable<RelationRoute> routes = this.routes.GetRoutes(routeName);

			result.AddRange(routes.Where(relationRoute => (relationRoute.Routes.FirstOrDefault()?.Condition(model)).GetValueOrDefault()).Select(relationRoute =>
			{
				Route route = relationRoute.Routes.FirstOrDefault();
				IEnumerable<IActionConstraintMetadata> constraints = this.GetActionConstraints(route);

				return new Relation()
				{
					Name = relationRoute.Name,
					Link = this.CreateLink(route, constraints, model)
				};
			}));

			if (model is IEnumerable)
			{
				IEnumerable<object> items = (model as IEnumerable).Cast<object>();

				result.AddRange(routes.Where(relationRoute => items.Any(item => relationRoute.Routes.Any(route => route.Condition(item)))).Select(relationRoute =>
				{
					List<Link> links = new List<Link>();
					foreach (object item in items)
					{
						links.AddRange(relationRoute.Routes.Where(route => route.Condition(item)).Select(itemRoute =>
						{
							IEnumerable<IActionConstraintMetadata> constraints = this.GetActionConstraints(itemRoute);

							return this.CreateLink(itemRoute, constraints, item);
						}));
					}

					return new RelationList()
					{
						Name = relationRoute.Name,
						Links = links
					};
				}).Where(relation => relation.Links.Any()));
			}

			return result;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Gets the action constraints of the specified route.
		/// </summary>
		/// <param name="route">The route.</param>
		/// <returns>The list of instances of type <see cref="IActionConstraintMetadata"/>.</returns>
		private IEnumerable<IActionConstraintMetadata> GetActionConstraints(Route route)
		{
			return this.actionDescriptors.FirstOrDefault(item => item.AttributeRouteInfo.Name == route.RouteName)?.ActionConstraints ?? Enumerable.Empty<IActionConstraintMetadata>();
		}

		/// <summary>
		/// Creates the link for specified model based on route and route action constraints.
		/// </summary>
		/// <param name="route">The route.</param>
		/// <param name="constraints">The constraints.</param>
		/// <param name="model">The model.</param>
		/// <returns>the instance of <see cref="Link"/>.</returns>
		private Link CreateLink(Route route, IEnumerable<IActionConstraintMetadata> constraints, object model)
		{
			return new Link()
			{
				Href = this.urlHelper.Link(route.RouteName, route.RouteValues(model))?.ToLower(),
				Method = constraints.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods.FirstOrDefault()?.ToLower(),
				ContentType = constraints.OfType<ContentTypeAttribute>().FirstOrDefault()?.ContentType?.ToLower()
			};
		}

		#endregion
	}
}
