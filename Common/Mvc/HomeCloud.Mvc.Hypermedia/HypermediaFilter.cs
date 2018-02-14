namespace HomeCloud.Mvc.Hypermedia
{
	#region Usings

	using System.Collections.Generic;

	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Filters;

	#endregion

	/// <summary>
	/// Generates <see cref="HATEOAS"/> links for the specified <see cref="HTTP"/> response result.
	/// </summary>
	public class HypermediaFilter : ActionFilterAttribute
	{
		#region Private Members

		/// <summary>
		/// The link service
		/// </summary>
		private readonly ILinkService linkService = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HypermediaFilter"/> class.
		/// </summary>
		/// <param name="linkService">The link service.</param>
		public HypermediaFilter(ILinkService linkService)
		{
			this.linkService = linkService;
			this.Order = 50;
		}

		#endregion

		#region IActionFilter Implementations

		/// <summary>
		/// Called after the action executes, before the action result.
		/// </summary>
		/// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext" />.</param>
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			ObjectResult result = context.Result as ObjectResult;
			if (result != null && result.Value != null)
			{
				IEnumerable<Link> links = this.linkService.GetLinks(result.Value, context.ActionDescriptor.AttributeRouteInfo.Name);
				result.Value = new HypermediaResponse(result.Value)
				{
					Links = links
				};

				context.Result = result;
			}
		}

		/// <summary>
		/// Called before the action executes, after model binding is complete.
		/// </summary>
		/// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext" />.</param>
		public override void OnActionExecuting(ActionExecutingContext context)
		{
		}

		#endregion
	}
}
