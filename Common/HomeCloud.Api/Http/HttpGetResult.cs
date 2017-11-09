namespace HomeCloud.Api.Http
{
	#region Usings

	using Microsoft.AspNetCore.Mvc;

	using ControllerBase = HomeCloud.Api.Mvc.ControllerBase;

	#endregion

	/// <summary>
	/// Implements a contract that represents the result of <see cref="HTTP GET" /> method.
	/// </summary>
	/// <typeparam name="T">The type of the data provided by <see cref="HTTP"/> method </typeparam>
	/// <seealso cref="HomeCloud.Api.Http.HttpMethodResult" />
	public sealed class HttpGetResult<T> : HttpMethodResult
	{
		#region Contstructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpGetResult{T}"/> class.
		/// </summary>
		/// <param name="controller">The controller.</param>
		public HttpGetResult(ControllerBase controller)
			: base(controller)
		{
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
			return this.HandleError() ?? ((this.Data == null) ? (IActionResult)this.Controller.NotFound() : this.Controller.Ok(this.Data));
		}

		#endregion
	}
}
