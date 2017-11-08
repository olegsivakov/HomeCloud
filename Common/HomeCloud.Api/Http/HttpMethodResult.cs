namespace HomeCloud.Api.Http
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;

	using HomeCloud.Api.Mvc;
	using HomeCloud.Exceptions;

	using Microsoft.AspNetCore.Mvc;

	using ControllerBase = HomeCloud.Api.Mvc.ControllerBase;

	#endregion

	public abstract class HttpMethodResult<T>
	{
		protected HttpMethodResult(ControllerBase controller)
		{
			this.Controller = controller;
		}

		protected ControllerBase Controller { get; }

		public bool HasErrors => this.Errors?.Count() > 0;

		public IEnumerable<Exception> Errors { get; set; }

		protected virtual IActionResult HandleError()
		{
			if (this.HasErrors)
			{
				if (this.Errors.Any(error => error is NotFoundException))
				{
					string message = this.Errors.FirstOrDefault(error => error is NotFoundException).Message;
					ErrorViewModel model = new ErrorViewModel()
					{
						Errors = new List<string>() { message }
					};

					return this.Controller.NotFound(model);
				}

				if (this.Errors.Any(error => error is AlreadyExistsException))
				{
					string message = this.Errors.FirstOrDefault(error => error is AlreadyExistsException).Message;
					ErrorViewModel model = new ErrorViewModel()
					{
						Errors = new List<string>() { message }
					};

					return this.Controller.Conflict(model);
				}

				return this.Controller.UnprocessableEntity(new ErrorViewModel()
				{
					Errors = this.Errors.Where(error => error is ValidationException).Select(error => (error as ValidationException).Message)
				});
			}

			return null;
		}

		public virtual IActionResult ToActionResult()
		{
			return this.HandleError() ?? this.Controller.Ok();
		}
	}
}
