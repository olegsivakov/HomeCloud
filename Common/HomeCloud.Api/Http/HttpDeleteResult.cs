namespace HomeCloud.Api.Http
{
	#region Usings

	using Microsoft.AspNetCore.Mvc;

	using ControllerBase = HomeCloud.Api.Mvc.ControllerBase;

	#endregion

	/// <summary>
	/// Implements a contract that represents the result of <see cref="HTTP DELETE"/> method.
	/// </summary>
	/// <seealso cref="HomeCloud.Api.Http.HttpMethodResult" />
	public sealed class HttpDeleteResult : HttpMethodResult
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpDeleteResult"/> class.
		/// </summary>
		/// <param name="controller">The controller.</param>
		public HttpDeleteResult(ControllerBase controller)
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
			return this.HandleError() ?? this.Controller.NoContent();
		}

		#endregion
	}
}
