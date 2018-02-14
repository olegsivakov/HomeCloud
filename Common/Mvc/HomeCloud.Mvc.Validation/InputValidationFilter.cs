namespace HomeCloud.Mvc.Validation
{
	#region Usings

	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Filters;

	using HomeCloud.Mvc.Exceptions;

	#endregion

	/// <summary>
	/// Provides validation of action arguments.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
	public class InputValidationFilter : ActionFilterAttribute
	{
		#region IActionFilter Implementations

		/// <summary>
		/// Called after the action executes, before the action result.
		/// </summary>
		/// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext" />.</param>
		public override void OnActionExecuted(ActionExecutedContext context)
		{
		}

		/// <summary>
		/// Called before the action executes, after model binding is complete.
		/// </summary>
		/// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext" />.</param>
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			ControllerBase controller = (context.Controller as ControllerBase);
			if (controller != null)
			{
				HttpExceptionResponse exception = controller.ControllerContext?.ActionDescriptor.ValidateArguments(context.ActionArguments);
				if (exception != null)
				{
					context.Result = controller.BadRequest(exception);
				}
			}
		}

		#endregion
	}
}
