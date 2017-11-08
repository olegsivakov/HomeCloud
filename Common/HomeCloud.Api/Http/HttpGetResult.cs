namespace HomeCloud.Api.Http
{
	#region Usings

	using Microsoft.AspNetCore.Mvc;

	using ControllerBase = HomeCloud.Api.Mvc.ControllerBase;

	#endregion

	public class HttpGetResult<T> : HttpMethodResult<T>
	{
		public HttpGetResult(ControllerBase controller)
			: base(controller)
		{
		}

		public T Data { get; set; }

		public override IActionResult ToActionResult()
		{
			return this.HandleError() ?? ((this.Data == null) ? (IActionResult)this.Controller.NotFound() : this.Controller.Ok(this.Data));
		}
	}
}
