namespace HomeCloud.Api.Http
{
	#region Usings

	using HomeCloud.Api.Mvc;
	using Microsoft.AspNetCore.Mvc;

	using ControllerBase = HomeCloud.Api.Mvc.ControllerBase;

	#endregion

	public class HttpPutResult<T> : HttpMethodResult<T>
		where T : IViewModel
	{
		#region Private Members

		private string actionName = null;

		#endregion

		#region Constructors

		public HttpPutResult(ControllerBase controller, string actionName)
			: base(controller)
		{
			this.actionName = actionName;
		}

		#endregion

		#region Public Properties

		public T Data { get; set; }

		#endregion

		#region Public Methods

		public override IActionResult ToActionResult()
		{
			return this.HandleError() ?? this.Controller.NoContentAtAction(actionName, this.Controller.GetType().Name, new { id = this.Data.ID });
		}

		#endregion
	}
}
