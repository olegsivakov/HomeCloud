namespace HomeCloud.Mvc
{
	#region Usings

	using System.IO;

	using Microsoft.AspNetCore.Mvc;

	using Controller = HomeCloud.Mvc.ControllerBase;
	using HomeCloud.Mvc.Models;

	#endregion

	/// <summary>
	/// Implements a contract that represents the stream result of <see cref="HTTP GET" /> method.
	/// </summary>
	/// <seealso cref="HttpGetResult" />
	public class HttpGetStreamResult : HttpGetResult
	{
		#region Contstructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpGetStreamResult" /> class.
		/// </summary>
		/// <param name="controller">The controller.</param>
		public HttpGetStreamResult(Controller controller, IFileModel value)
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
			IFileModel file = this.Value as IFileModel;
			if (file is null)
			{
				return this.Controller.NotFound();
			}

			string name = !string.IsNullOrWhiteSpace(file.FileName) ? file.FileName : (!string.IsNullOrWhiteSpace(file.Path) ? Path.GetFileName(file.Path) : string.Empty);

			return this.Controller.PhysicalFile(file.Path, file.MimeType, name);
		}

		#endregion
	}
}
