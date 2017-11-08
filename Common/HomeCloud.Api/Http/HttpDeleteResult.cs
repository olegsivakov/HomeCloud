namespace HomeCloud.Api.Http
{
	#region Usings

	using Microsoft.AspNetCore.Mvc;

	using ControllerBase = HomeCloud.Api.Mvc.ControllerBase;

	#endregion

	public class HttpDeleteResult : HttpMethodResult<object>
	{
		public HttpDeleteResult(ControllerBase controller)
			: base(controller)
		{
		}

		public override IActionResult ToActionResult()
		{
			return this.HandleError() ?? this.Controller.NoContent();
		}
	}
}
