namespace HomeCloud.Api.Http
{
	#region Usings

	using HomeCloud.Api.Mvc;
	using Microsoft.AspNetCore.Mvc;

	using ControllerBase = HomeCloud.Api.Mvc.ControllerBase;

	#endregion

	public class HttpPostResult<T> : HttpMethodResult<T>
		where T : IViewModel
	{
		private string createdActionName = null;

		public HttpPostResult(ControllerBase controller)
			: base(controller)
		{
		}

		public T Data { get; set; }

		public override IActionResult ToActionResult()
		{
			return this.HandleError() ?? ((this.Data == null) ? (IActionResult)this.Controller.NotFound() : this.Controller.CreatedAtAction(
				this.createdActionName,
				this.Controller.GetType().Name,
				new { id = this.Data.ID },
				this.Data));
		}
	}
}
