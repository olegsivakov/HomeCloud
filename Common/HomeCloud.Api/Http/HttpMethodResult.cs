namespace HomeCloud.Api.Http
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using HomeCloud.Api.Mvc;
	using HomeCloud.Exceptions;

	using Microsoft.AspNetCore.Mvc;

	using ControllerBase = HomeCloud.Api.Mvc.ControllerBase;

	#endregion

	/// <summary>
	/// Provides common implementation of the contract to represent the result of any <see cref="HTTP"/> method.
	/// </summary>
	/// <seealso cref="HomeCloud.Api.Http.IHttpMethodResult" />
	public abstract class HttpMethodResult : IHttpMethodResult
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpMethodResult"/> class.
		/// </summary>
		/// <param name="controller">The controller.</param>
		protected HttpMethodResult(ControllerBase controller)
		{
			this.Controller = controller;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets a value indicating whether the <see cref="HTTP" /> method has errors.
		/// </summary>
		/// <value>
		/// <c>true</c> if <see cref="HTTP" /> method has errors; otherwise, <c>false</c>.
		/// </value>
		public virtual bool HasErrors => (this.Errors?.Any()).GetValueOrDefault();

		/// <summary>
		/// Gets or sets the list of errors.
		/// </summary>
		/// <value>
		/// The list of <see cref="Exception" />.
		/// </value>
		public virtual IEnumerable<Exception> Errors { get; set; }

		#endregion

		#region Protected Properties

		/// <summary>
		/// Gets the controller.
		/// </summary>
		/// <value>
		/// The instance of <see cref="ControllerBase"/>.
		/// </value>
		protected ControllerBase Controller { get; }

		#endregion

		#region IActionResult Implementations

		/// <summary>
		/// Executes the result operation of the action method asynchronously. This method is called by MVC to process
		/// the result of an action method.
		/// </summary>
		/// <param name="context">The context in which the result is executed. The context information includes
		/// information about the action that was executed and request information.</param>
		/// <returns>
		/// A task that represents the asynchronous execute operation.
		/// </returns>
		public async Task ExecuteResultAsync(ActionContext context)
		{
			await this.ToActionResult().ExecuteResultAsync(context);
		}

		#endregion

		#region IHttpMethodResult Implementations

		/// <summary>
		/// Returns the <see cref="IActionResult" /> that represents the current <see cref="HTTP" /> method.
		/// </summary>
		/// <returns>
		/// The instance of <see cref="IActionResult" />.
		/// </returns>
		public abstract IActionResult ToActionResult();

		/// <summary>
		/// Provides the action method to return the errors presented by the <see cref="Errors"/> collection 
		/// </summary>
		/// <returns>The instance of <see cref="IActionResult"/>.</returns>
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

		#endregion
	}
}
