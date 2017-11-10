namespace HomeCloud.Api.Http
{
	#region Usings

	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Routing;

	#endregion

	/// <summary>
	/// An <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> that returns a <see cref="No Content"/> (204) response with a Location header.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.ObjectResult" />
	public class NoContentAtActionResult : ObjectResult
	{
		#region constants

		/// <summary>
		/// The <see cref="Location"/> header name.
		/// </summary>
		private const string LocationHeaderName = "Location";

		#endregion

		#region Private Members

		/// <summary>
		/// The <see cref="Microsoft.AspNetCore.Mvc.IUrlHelper"/> used to generate URLs.
		/// </summary>
		private readonly IUrlHelper urlHelper = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="NoContentAtActionResult" /> class.
		/// </summary>
		/// <param name="urlHelper">The <see cref="Microsoft.AspNetCore.Mvc.IUrlHelper"/> used to generate URLs.</param>
		/// <param name="actionName">The name of the action.</param>
		/// <param name="controllerName">The name of the controller.</param>
		/// <param name="routeValues">The route values.</param>
		public NoContentAtActionResult(IUrlHelper urlHelper, string actionName, string controllerName, object routeValues)
			: base(null)
		{
			this.urlHelper = urlHelper;

			this.StatusCode = StatusCodes.Status204NoContent;

			this.ActionName = actionName;
			this.ControllerName = controllerName;
			this.RouteValues = new RouteValueDictionary(routeValues);
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the name of the action to use for generating the URL.
		/// </summary>
		/// <value>
		/// The name of the action.
		/// </value>
		public string ActionName { get; set; }

		/// <summary>
		/// Gets or sets the name of the controller to use for generating the URL.
		/// </summary>
		/// <value>
		/// The name of the controller.
		/// </value>
		public string ControllerName { get; set; }

		/// <summary>
		/// Gets or sets the route data to use for generating the URL.
		/// </summary>
		/// <value>
		/// The route values.
		/// </value>
		public RouteValueDictionary RouteValues { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// This method is called before the formatter writes to the output stream.
		/// </summary>
		/// <param name="context">The action context.</param>
		public override void OnFormatting(ActionContext context)
		{
			base.OnFormatting(context);

			string scheme = this.urlHelper.ActionContext.HttpContext.Request.Scheme;
			string url = this.urlHelper.Action(this.ActionName, this.ControllerName, this.RouteValues, scheme);

			context.HttpContext.Response.Headers.Add(LocationHeaderName, url);
		}

		#endregion
	}
}
