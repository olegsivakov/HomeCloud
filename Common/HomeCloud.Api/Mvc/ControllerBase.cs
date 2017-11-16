namespace HomeCloud.Api.Mvc
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Api.Http;

	using Microsoft.AspNetCore.Mvc;

	#endregion

	/// <summary>
	/// Provides base methods for <see cref="RESTful API" />.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
	public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
	{
		#region Public Methods

		/// <summary>
		/// Creates an <see cref="Microsoft.AspNetCore.Mvc.NotFoundObjectResult"/> that produces a <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound"/> response.
		/// </summary>
		/// <param name="value">The value to format in the entity body.</param>
		/// <returns>
		/// The created <see cref="Microsoft.AspNetCore.Mvc.NotFoundObjectResult" /> for the response.
		/// </returns>
		[NonAction]
		public virtual NotFoundObjectResult NotFound(ErrorViewModel value)
		{
			NotFoundObjectResult result = this.NotFound((object)value);
			value.StatusCode = result.StatusCode.GetValueOrDefault();

			return result;
		}

		/// <summary>
		/// Creates an <see cref="HomeCloud.Api.Http.UnprocessableEntityResult" /> object that produces an <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status422UnprocessableEntity"/> response.
		/// </summary>
		/// <param name="value">The <see cref="ErrorViewModel" /> value to format in the entity body.</param>
		/// <returns>
		/// The created <see cref="HomeCloud.Api.Http.UnprocessableEntityResult" /> for the response.
		/// </returns>
		[NonAction]
		public virtual UnprocessableEntityResult UnprocessableEntity(ErrorViewModel value)
		{
			UnprocessableEntityResult result = new UnprocessableEntityResult(value);
			value.StatusCode = result.StatusCode.GetValueOrDefault();

			return result;
		}

		/// <summary>
		/// Creates an <see cref="HomeCloud.Api.Http.ConflictResult" /> object that produces an <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status409Conflict"/> response.
		/// </summary>
		/// <param name="value">The <see cref="ErrorViewModel" /> value to format in the entity body.</param>
		/// <returns>
		/// The created <see cref="HomeCloud.Api.Http.ConflictResult" /> for the response.
		/// </returns>
		[NonAction]
		public virtual ConflictResult Conflict(ErrorViewModel value)
		{
			ConflictResult result = new ConflictResult(value);
			value.StatusCode = result.StatusCode.GetValueOrDefault();

			return result;
		}

		/// <summary>
		/// Creates a <see cref="HomeCloud.Api.Http.NoContentAtActionResult" /> object that produces a <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent" /> response.
		/// </summary>
		/// <param name="actionName">The name of the action to use for generating the URL.</param>
		/// <param name="controllerName">The name of the controller to use for generating the URL.</param>
		/// <param name="routeValues">The route data to use for generating the URL.</param>
		/// <returns>The created <see cref="HomeCloud.Api.Http.NoContentAtActionResult"/> for the response.</returns>
		[NonAction]
		public virtual NoContentAtActionResult NoContentAtAction(string actionName, string controllerName, object routeValues)
		{
			return new NoContentAtActionResult(this.Url, actionName, controllerName, routeValues);
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Executes <see cref="HttpGet" /> method against the entry.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return..</param>
		/// <param name="action">The action to execute against entry.</param>
		/// <returns>
		/// The asynchronous operation of <see cref="IActionResult" />.
		/// </returns>
		protected async virtual Task<IActionResult> HttpGet(int offset, int limit, Func<Task<IHttpMethodResult>> action)
		{
			if (offset < 0)
			{
				return this.BadRequest("The offset parameter should be positive number.");
			}

			if (limit <= 0)
			{
				return this.BadRequest("The limit parameter cannot be less or equal zero.");
			}

			return await action();
		}

		/// <summary>
		/// Executes <see cref="HttpGet" /> method against the entry.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <param name="action">The action to execute against entry.</param>
		/// <returns>
		/// The asynchronous operation of <see cref="IActionResult" />.
		/// </returns>
		protected async virtual Task<IActionResult> HttpGet(Guid id, Func<Task<IHttpMethodResult>> action)
		{
			if (id == Guid.Empty)
			{
				return this.BadRequest("The specified unique identifier is undefined.");
			}

			return await action();
		}

		/// <summary>
		/// Executes <see cref="HttpPost" /> method against the entry.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="action">The action to execute against entry.</param>
		/// <returns>
		/// The asynchronous operation of <see cref="IActionResult" />.
		/// </returns>
		protected async virtual Task<IActionResult> HttpPost(IViewModel model, Func<Task<IHttpMethodResult>> action)
		{
			if (model == null)
			{
				return this.BadRequest("The request body is undefined.");
			}

			return await action();
		}

		/// <summary>
		/// Executes <see cref="HttpPut" /> method against the entry.
		/// </summary>
		/// <param name="id">The entry unique identifier.</param>
		/// <param name="model">The entry model.</param>
		/// <param name="action">The action to execute against entry.</param>
		/// <returns>
		/// The asynchronous operation of <see cref="IActionResult" />.
		/// </returns>
		protected async virtual Task<IActionResult> HttpPut(Guid id, IViewModel model, Func<Task<IHttpMethodResult>> action)
		{
			if (model == null)
			{
				return this.BadRequest("The request body is undefined.");
			}

			if (model.ID != id)
			{
				return this.BadRequest("The specified unique identifier does not match one defined in the request body.");
			}

			return (await action()).ToActionResult();
		}

		/// <summary>
		/// Executes <see cref="HttpDelete" /> method against the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="action">The action to execute against entry.</param>
		/// <returns>
		/// The asynchronous operation of <see cref="IActionResult" />.
		/// </returns>
		protected async virtual Task<IActionResult> HttpDelete(Guid id, Func<Task<IHttpMethodResult>> action)
		{
			if (id == Guid.Empty)
			{
				return this.BadRequest("The specified unique identifier is undefined.");
			}

			return await action();
		}

		#endregion
	}
}
