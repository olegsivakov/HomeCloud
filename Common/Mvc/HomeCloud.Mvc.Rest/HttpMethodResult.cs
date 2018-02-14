namespace HomeCloud.Mvc
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;

	using Sabaka.Exceptions;
	using Sabaka.Http;
	using HomeCloud.Mvc.Models;

	using ControllerBase = HomeCloud.Mvc.ControllerBase;

	#endregion

	/// <summary>
	/// Provides common implementation of the contract to represent the result of any <see cref="HTTP"/> method.
	/// </summary>
	/// <seealso cref="HomeCloud.Mvc.IHttpMethodResult" />
	public abstract class HttpMethodResult : ObjectResult, IHttpMethodResult
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpMethodResult" /> class.
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="value">The value to override <see cref="HTTP"/> body. Can be set to <see cref="Nullable"/> as default.</param>
		protected HttpMethodResult(ControllerBase controller, object value = null)
			: base(value)
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

		#region Public Methods

		/// <summary>
		/// Creates the action result of <see cref="IHttpMethodResult" /> type according to the request <see cref="HTTP" /> method.
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="value">The value.</param>
		/// <param name="errors">The errors.</param>
		/// <returns>the instance of <see cref="IHttpMethodResult"/>.</returns>
		/// <exception cref="NotSupportedException">The provided <see cref="HTTP"/> method is not supported.</exception>
		public static IHttpMethodResult Create(ControllerBase controller, object value = null, IEnumerable<Exception> errors = null)
		{
			switch (controller.Request.Method?.ToUpper())
			{
				case HttpMethods.Get:
					{
						return new HttpGetResult(controller, value)
						{
							Errors = errors
						};
					}

				case HttpMethods.Head:
					{
						return new HttpHeadResult(controller)
						{
							Errors = errors
						};
					}

				case HttpMethods.Post:
					{
						return new HttpPostResult(controller, value)
						{
							Errors = errors
						};
					}

				case HttpMethods.Put:
					{
						return new HttpPutResult(controller, value)
						{
							Errors = errors
						};
					}

				case HttpMethods.Delete:
					{
						return new HttpDeleteResult(controller)
						{
							Errors = errors
						};
					}

				default:
					{
						throw new NotSupportedException($"HTTP method {controller.Request.Method} is not supported.");
					}
			}

		}

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
		public override async Task ExecuteResultAsync(ActionContext context)
		{
			IActionResult result = this.HandleErrors() ?? this.ToActionResult();

			await result.ExecuteResultAsync(context);
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

		#endregion

		#region Private Methods

		/// <summary>
		/// Provides the action method to return the errors presented by the <see cref="Errors"/> collection 
		/// </summary>
		/// <returns>The instance of <see cref="IActionResult"/>.</returns>
		private IActionResult HandleErrors()
		{
			if (this.HasErrors)
			{
				IEnumerable<NotFoundException> notFoundExceptions = this.Errors.OfType<NotFoundException>();
				if (notFoundExceptions.Any())
				{
					ErrorViewModel model = new ErrorViewModel()
					{
						Errors = notFoundExceptions.Select(error => error.Message)
					};

					return this.Controller.NotFound(model);
				}

				IEnumerable<AlreadyExistsException> alreadyExistsExceptions = this.Errors.OfType<AlreadyExistsException>();
				if (alreadyExistsExceptions.Any())
				{
					ErrorViewModel model = new ErrorViewModel()
					{
						Errors = alreadyExistsExceptions.Select(error => error.Message)
					};

					return this.Controller.Conflict(model);
				}

				IEnumerable<ValidationException> validationExceptions = this.Errors.OfType<ValidationException>();
				if (validationExceptions.Any())
				{
					ErrorViewModel model = new ErrorViewModel()
					{
						Errors = validationExceptions.Select(error => error.Message)
					};

					return this.Controller.UnprocessableEntity(model);
				}

				return new BadRequestResult();
			}

			return null;
		}

		#endregion
	}
}
