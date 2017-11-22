namespace HomeCloud.Api.Http
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Api.Mvc;
	using Microsoft.AspNetCore.Mvc;

	using ControllerBase = HomeCloud.Api.Mvc.ControllerBase;

	#endregion

	/// <summary>
	/// Implements a contract that represents the result of <see cref="HTTP POST" /> method.
	/// </summary>
	/// <typeparam name="T">The <see cref="IViewModel"/> type of the data provided by <see cref="HTTP"/> method </typeparam>
	/// <seealso cref="HomeCloud.Api.Http.HttpMethodResult" />
	public sealed class HttpPostResult<T> : HttpMethodResult, IActionResult
		where T : IViewModel
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IActionResult" /> action method which route <see cref="URL" /> is required to be presented in the header of the <see cref="HTTP" /> method.
		/// </summary>
		private readonly Func<Guid, Task<IActionResult>> locationUrlAction = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpPostResult{T}" /> class.
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="locationUrlAction">The <see cref="IActionResult" /> action method which route <see cref="URL" /> is required to be presented in the header of the <see cref="HTTP" /> method.</param>
		public HttpPostResult(ControllerBase controller, Func<Guid, Task<IActionResult>> locationUrlAction)
			: base(controller)
		{
			this.locationUrlAction = locationUrlAction;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the data provided by <see cref="HTTP"/> method.
		/// </summary>
		/// <value>
		/// The instance of <see cref="T"/>.
		/// </value>
		public T Data { get; set; }

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
			string actionName = null;

			if (this.locationUrlAction?.Method?.GetCustomAttributes(typeof(HttpGetAttribute), false)?.Length > 0)
			{
				actionName = this.locationUrlAction.Method.Name;
			}

			return this.HandleError() ?? ((this.Data == null) ? (IActionResult)this.Controller.NotFound() : this.Controller.CreatedAtAction(
				actionName,
				this.Controller.ControllerContext.ActionDescriptor.ControllerName,
				new { id = this.Data.ID },
				this.Data));
		}

		#endregion
	}
}
