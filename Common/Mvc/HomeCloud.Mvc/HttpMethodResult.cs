namespace HomeCloud.Mvc
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;

	using HomeCloud.Exceptions;
	using HomeCloud.Http;
	using HomeCloud.Mvc.Exceptions;

	using Controller = HomeCloud.Mvc.ControllerBase;
	using HomeCloud.Mvc.Models;

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
		protected HttpMethodResult(Controller controller, object value = null)
			: base(value)
		{
			this.Controller = controller;
			this.ContentTypes.Add(this.Controller.Request.ContentType);
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
		/// The instance of <see cref="Controller"/>.
		/// </value>
		protected Controller Controller { get; }

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
		public static IHttpMethodResult Create(Controller controller, object value = null, IEnumerable<Exception> errors = null)
		{
			string method = controller.Request.Method?.ToUpper();

			switch (method)
			{
				case HttpMethods.Get:
					{
						if (value is IFileModel)
						{
							return new HttpGetStreamResult(controller, value as IFileModel)
							{
								Errors = errors
							};
						}

						return new HttpGetResult(controller, value)
						{
							Errors = errors
						};
					}

				case HttpMethods.Head:
					{
						return new HttpHeadResult(controller, value)
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
						throw new NotSupportedException($"HTTP method {method} is not supported.");
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

		/// <summary>
		/// Overrides default behavior of the current instance of <see cref="IHttpMethodResult" /> to forcibly respond as a <see cref="JSON" />.
		/// </summary>
		/// <returns>
		/// The instance of <see cref="IHttpMethodResult" />.
		/// </returns>
		public IHttpMethodResult AsJson()
		{
			this.ContentTypes.Clear();
			this.ContentTypes.Add(MimeTypes.Application.Json);

			return this;
		}

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
					HttpExceptionResponse model = new HttpExceptionResponse()
					{
						Messages = notFoundExceptions.Select(error => error.Message)
					};

					return this.Controller.NotFound(model);
				}

				IEnumerable<AlreadyExistsException> alreadyExistsExceptions = this.Errors.OfType<AlreadyExistsException>();
				if (alreadyExistsExceptions.Any())
				{
					HttpExceptionResponse model = new HttpExceptionResponse()
					{
						Messages = alreadyExistsExceptions.Select(error => error.Message)
					};

					return this.Controller.Conflict(model);
				}

				IEnumerable<ValidationException> validationExceptions = this.Errors.OfType<ValidationException>();
				if (validationExceptions.Any())
				{
					HttpExceptionResponse model = new HttpExceptionResponse()
					{
						Messages = validationExceptions.Select(error => error.Message)
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
