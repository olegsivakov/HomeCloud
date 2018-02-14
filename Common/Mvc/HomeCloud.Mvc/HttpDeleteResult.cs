namespace HomeCloud.Mvc
{
	#region Usings

	using Microsoft.AspNetCore.Mvc;

	using Controller = HomeCloud.Mvc.ControllerBase;

	#endregion

	/// <summary>
	/// Implements a contract that represents the result of <see cref="HTTP DELETE"/> method.
	/// </summary>
	/// <seealso cref="HomeCloud.Mvc.HttpMethodResult" />
	public sealed class HttpDeleteResult : HttpMethodResult
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpDeleteResult"/> class.
		/// </summary>
		/// <param name="controller">The controller.</param>
		public HttpDeleteResult(Controller controller)
			: base(controller)
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
			return this.Controller.NoContent();
		}

		#endregion
	}
}
