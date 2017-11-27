namespace HomeCloud.Api.Http
{
	#region Usings

	using System.IO;

	using HomeCloud.Api.Mvc;
	using Microsoft.AspNetCore.Mvc;

	using ControllerBase = HomeCloud.Api.Mvc.ControllerBase;

	#endregion

	/// <summary>
	/// Implements a contract that represents the result of <see cref="HTTP GET" /> method.
	/// </summary>
	/// <typeparam name="T">The type of the data provided by <see cref="HTTP"/> method </typeparam>
	/// <seealso cref="HttpGetResult{T}" />
	public class HttpGetStreamResult<T> : HttpGetResult<T>
		where T : IFileViewModel
	{
		#region Contstructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpGetStreamResult{T}"/> class.
		/// </summary>
		/// <param name="controller">The controller.</param>
		public HttpGetStreamResult(ControllerBase controller)
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
			string name = !string.IsNullOrWhiteSpace(this.Data.Name) ? this.Data.Name : (!string.IsNullOrWhiteSpace(this.Data.Path) ? Path.GetFileName(this.Data.Path) : string.Empty);

			return this.HandleError() ?? ((this.Data == null) ? (IActionResult)this.Controller.NotFound() : this.Controller.PhysicalFile(this.Data.Path, this.Data.MimeType, name));
		}

		#endregion
	}
}
