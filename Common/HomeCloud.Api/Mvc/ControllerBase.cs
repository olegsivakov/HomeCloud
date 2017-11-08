namespace HomeCloud.Api.Mvc
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.Api.Http;

	using Microsoft.AspNetCore.Mvc;

	#endregion

	/// <summary>
	/// Provides base <see cref="RESTful API"/> methods with view model support.
	/// </summary>
	public abstract class ControllerBase : Controller
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
			NotFoundObjectResult result = this.NotFound(value);
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
			return new NoContentAtActionResult(actionName, controllerName, routeValues);
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Executes <see cref="HttpGet" /> method against the entry.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return..</param>
		/// <param name="action">The action to execute against entry.</param>
		/// <returns>
		/// The asynchronous operation of <see cref="IActionResult" />.
		/// </returns>
		protected async virtual Task<IActionResult> HttpGet<TModel>(int offset, int limit, Func<Task<HttpGetResult<IEnumerable<TModel>>>> action)
			where TModel : IViewModel
		{
			if (offset < 0)
			{
				return this.BadRequest("The offset parameter should be positive number.");
			}

			if (limit <= 0)
			{
				return this.BadRequest("The limit parameter cannot be less or equal zero.");
			}

			return (await action()).ToActionResult();
		}

		/// <summary>
		/// Executes <see cref="HttpGet" /> method against the entry.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <param name="action">The action to execute against entry.</param>
		/// <returns>
		/// The asynchronous operation of <see cref="IActionResult" />.
		/// </returns>
		protected async virtual Task<IActionResult> HttpGet<TModel>(Guid id, Func<Task<HttpGetResult<TModel>>> action)
			where TModel : IViewModel
		{
			if (id == Guid.Empty)
			{
				return this.BadRequest("The specified unique identifier is empty");
			}

			return (await action()).ToActionResult();
		}

		/// <summary>
		/// Executes <see cref="HttpPost" /> method against the entry.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="model">The model.</param>
		/// <param name="action">The action to execute against entry.</param>
		/// <returns>
		/// The asynchronous operation of <see cref="IActionResult" />.
		/// </returns>
		protected async virtual Task<IActionResult> HttpPost<TModel>(TModel model, Func<Task<HttpPostResult<TModel>>> action)
			where TModel : IViewModel
		{
			if (model == null)
			{
				return this.BadRequest();
			}

			return (await action()).ToActionResult();
		}

		/// <summary>
		/// Executes <see cref="HttpPut" /> method against the entry.
		/// </summary>
		/// <typeparam name="T">The type of model derived from <see cref="ViewModelBase" />.</typeparam>
		/// <param name="id">The entry unique identifier.</param>
		/// <param name="model">The entry model.</param>
		/// <param name="action">The action to execute against entry.</param>
		/// <returns>
		/// The asynchronous operation of <see cref="IActionResult" />.
		/// </returns>
		protected async virtual Task<IActionResult> HttpPut<TModel>(Guid id, TModel model, Func<Task<HttpPutResult<TModel>>> action)
			where TModel : IViewModel
		{
			if (model == null || model.ID != id)
			{
				return this.BadRequest();
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
		protected async virtual Task<IActionResult> Delete(Guid id, Func<Task<HttpDeleteResult>> action)
		{
			if (id == Guid.Empty)
			{
				return this.BadRequest("The specified unique identifier is empty");
			}

			return (await action()).ToActionResult();
		}

		#endregion
	}
}
