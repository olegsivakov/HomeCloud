namespace HomeCloud.Mvc
{
	#region Usings

	using Microsoft.AspNetCore.Mvc;

	using Controller = HomeCloud.Mvc.ControllerBase;

	#endregion

	/// <summary>
	/// Implements a contract that represents the result of <see cref="HTTP GET" /> method.
	/// </summary>
	/// <seealso cref="HomeCloud.Mvc.HttpMethodResult" />
	public class HttpGetResult : HttpMethodResult
	{
		#region Contstructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpGetResult" /> class.
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="value">The value to override <see cref="!:HTTP" /> body. Can be set to <see cref="T:System.Nullable" /> as default.</param>
		public HttpGetResult(Controller controller, object value = null)
			: base(controller, value)
		{
		}

		#endregion

		#region HttpMethodResult Implementations

		/// <summary>
		/// Returns the <see cref="IActionResult" /> that represents the current <see cref="HTTP" /> method.
		/// </summary>
		/// <returns>
		/// The instance of <see cref="IActionResult" />.
		/// </returns>
		public override IActionResult ToActionResult()
		{
			return this.Value == null ? (IActionResult)this.Controller.NotFound() : this.Controller.Ok(this.Value);
		}

		#endregion
	}
}
