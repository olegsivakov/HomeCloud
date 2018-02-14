namespace HomeCloud.Mvc
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using Microsoft.AspNetCore.Mvc;

	using HomeCloud.Mvc.Exceptions;
	using HomeCloud.Mvc.Models;

	#endregion

	/// <summary>
	/// Provides base methods for <see cref="RESTful API" />.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
	public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerBase"/> class.
		/// </summary>
		protected ControllerBase()
		{
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Creates an <see cref="Microsoft.AspNetCore.Mvc.NotFoundObjectResult"/> object that produces a <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound"/> response.
		/// </summary>
		/// <param name="model">The value to format in the entity body.</param>
		/// <returns>
		/// The created <see cref="Microsoft.AspNetCore.Mvc.NotFoundObjectResult" /> for the response.
		/// </returns>
		[NonAction]
		public virtual NotFoundObjectResult NotFound(HttpExceptionResponse model)
		{
			NotFoundObjectResult result = base.NotFound(model);
			model.StatusCode = result.StatusCode.GetValueOrDefault();

			return result;
		}

		/// <summary>
		/// Creates an <see cref="HomeCloud.Mvc.UnprocessableEntityResult" /> object that produces an <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status422UnprocessableEntity"/> response.
		/// </summary>
		/// <param name="model">The <see cref="HttpExceptionResponse" /> value to format in the entity body.</param>
		/// <returns>
		/// The created <see cref="HomeCloud.Mvc.UnprocessableEntityResult" /> for the response.
		/// </returns>
		[NonAction]
		public virtual UnprocessableEntityResult UnprocessableEntity(HttpExceptionResponse model)
		{
			UnprocessableEntityResult result = new UnprocessableEntityResult(model);
			model.StatusCode = result.StatusCode.GetValueOrDefault();

			return result;
		}

		/// <summary>
		/// Creates an <see cref="HomeCloud.Mvc.ConflictResult" /> object that produces an <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status409Conflict"/> response.
		/// </summary>
		/// <param name="model">The <see cref="HttpExceptionResponse" /> value to format in the entity body.</param>
		/// <returns>
		/// The created <see cref="HomeCloud.Mvc.ConflictResult" /> for the response.
		/// </returns>
		[NonAction]
		public virtual ConflictResult Conflict(HttpExceptionResponse model)
		{
			ConflictResult result = new ConflictResult(model);
			model.StatusCode = result.StatusCode.GetValueOrDefault();

			return result;
		}

		/// <summary>
		/// Creates a <see cref="HomeCloud.Mvc.CreatedObjectResult" /> object that produces a <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status201Created" /> response.
		/// </summary>
		/// <param name="model">The model containing location header.</param>
		/// <returns>
		/// The created <see cref="HomeCloud.Mvc.CreatedObjectResult" /> for the response.
		/// </returns>
		[NonAction]
		public virtual CreatedObjectResult Created(object model)
		{
			return new CreatedObjectResult(model);
		}

		/// <summary>
		/// Creates a <see cref="IHttpMethodResult" /> object that produces an overwritten response body.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="errors">The errors.</param>
		/// <returns>The created <see cref="IHttpMethodResult"/> for the response.</returns>
		[NonAction]
		public virtual IHttpMethodResult HttpResult(object model, IEnumerable<Exception> errors = null)
		{
			return HttpMethodResult.Create(this, model, errors);
		}

		#endregion
	}
}
