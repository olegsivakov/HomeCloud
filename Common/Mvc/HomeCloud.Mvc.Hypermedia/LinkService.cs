namespace HomeCloud.Mvc.Hypermedia
{
	#region Usings

	using System.Linq;
	using System.Collections.Generic;

	using Microsoft.AspNetCore.Mvc.Abstractions;
	using Microsoft.AspNetCore.Mvc.Infrastructure;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Routing;
	using Microsoft.AspNetCore.Mvc.Internal;

	#endregion

	// <summary>
	/// Provides methods to handle links.
	/// </summary>
	public class LinkService : ILinkService
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
		private readonly ILinkRouteMap linkRoutes = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="LinkService"/> class.
		/// </summary>
		/// <param name="linkRoutes">The link routes.</param>
		/// <param name="actionDescriptorProvider">The action descriptot provider.</param>
		/// <param name="urlHelperFactory">The <see cref="URL"/> helper factory.</param>
		/// <param name="actionContextAccessor">The action context accessor.</param>
		public LinkService(
			ILinkRouteMap linkRoutes,
			IActionDescriptorCollectionProvider actionDescriptorProvider,
			IUrlHelperFactory urlHelperFactory,
			IActionContextAccessor actionContextAccessor)
		{
			this.linkRoutes = linkRoutes;
			this.actionDescriptors = actionDescriptorProvider?.ActionDescriptors?.Items?.AsEnumerable() ?? Enumerable.Empty<ActionDescriptor>();
			this.urlHelper = urlHelperFactory?.GetUrlHelper(actionContextAccessor.ActionContext);
		}

		#endregion

		#region ILinkService Implementations

		/// <summary>
		/// Gets the collection of links for the specified model instance within the specified route.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="route">The name of the route.</param>
		/// <returns>the list of <see cref="Link"/>.</returns>
		public IEnumerable<Link> GetLinks(object model, string routeName)
		{
			IEnumerable<LinkRoute> routes = this.linkRoutes.GetRoutes(routeName);

			return routes.Where(route => route.Condition(model)).Select(route => new Link()
			{
				Relation = route.Name,
				Href = this.urlHelper.Link(route.RouteName, route.RouteValues(model))?.ToLower(),
				Method = this.actionDescriptors
									.FirstOrDefault(descriptor => descriptor.AttributeRouteInfo.Name == route.RouteName)?
									.ActionConstraints.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods.FirstOrDefault()?.ToLower()
			});
		}

		#endregion
	}
}
