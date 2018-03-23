namespace HomeCloud.Mvc.Hypermedia
{
	#region Usings

	using System.Collections.Generic;

	using HomeCloud.Mvc.Hypermedia.Relations;

	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Filters;

	#endregion

	/// <summary>
	/// Generates <see cref="HATEOAS"/> links for the specified <see cref="HTTP"/> response result.
	/// </summary>
	public class HypermediaFilter : ActionFilterAttribute
	{
		#region Constants

		/// <summary>
		/// The applicable content type
		/// </summary>
		private const string contentType = "application/json";

		#endregion

		#region Private Members

		/// <summary>
		/// The relation service
		/// </summary>
		private readonly IRelationService relationService = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HypermediaFilter"/> class.
		/// </summary>
		/// <param name="relationService">The relation service.</param>
		public HypermediaFilter(IRelationService relationService)
		{
			this.relationService = relationService;
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
			if (result != null && result.Value != null && result.ContentTypes.Contains(contentType))
			{
				IEnumerable<IRelation> relations = this.relationService.GetRelations(result.Value, context.ActionDescriptor.AttributeRouteInfo.Name);
				result.Value = new HypermediaResponse(result.Value)
				{
					Relations = relations
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
