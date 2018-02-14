namespace HomeCloud.Mvc
{
	#region Usings

	using Microsoft.AspNetCore.Mvc;
	using HomeCloud.Mvc.Hypermedia;
	using HomeCloud.Mvc.Models;

	using ControllerBase = HomeCloud.Mvc.ControllerBase;

	#endregion

	/// <summary>
	/// Implements a contract that represents the result of <see cref="HTTP POST" /> method.
	/// </summary>
	/// <seealso cref="HomeCloud.Mvc.HttpMethodResult" />
	public sealed class HttpPostResult : HttpMethodResult
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpPostResult{T}" /> class.
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="value">The value to override <see cref="!:HTTP" /> body. Can be set to <see cref="T:System.Nullable" /> as default.</param>
		public HttpPostResult(ControllerBase controller, object value = null)
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
			LocationViewModel model = this.Value as LocationViewModel ?? (this.Value as HttpPostResult)?.Value as LocationViewModel ?? (this.Value as HypermediaResponse)?.Data as LocationViewModel;
			if (model is null || string.IsNullOrWhiteSpace(model.LocationUrl))
			{
				throw new System.MissingMemberException($"The instance of {this.Value?.GetType()} does not contain the location url property.");
			}

			return this.Value == null ? (IActionResult)this.Controller.NotFound() : this.Controller.Created(model.LocationUrl, this.Value);
		}

		#endregion
	}
}
