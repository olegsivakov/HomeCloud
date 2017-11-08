namespace HomeCloud.Api.Mvc
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Api.Http;

	using Microsoft.AspNetCore.Mvc;

	using ControllerBase = Microsoft.AspNetCore.Mvc.Controller;
	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Provides common <see cref="RESTful API"/> methods with view model support.
	/// </summary>
	public abstract class Controller : ControllerBase
	{
		#region Public Methods

		/// <summary>
		/// Creates an <see cref="HomeCloud.Api.Http.UnprocessableEntityResult" /> object that produces an Microsoft.AspNetCore.Http.StatusCodes.Status422UnprocessableEntity response.
		/// </summary>
		/// <param name="value">The <see cref="ErrorViewModel" /> value to format in the entity body.</param>
		/// <returns>
		/// The created <see cref="HomeCloud.Api.Http.UnprocessableEntityResult" /> for the response.
		/// </returns>
		public virtual UnprocessableEntityResult UnprocessableEntity(ErrorViewModel value)
		{
			UnprocessableEntityResult result = new UnprocessableEntityResult(value);
			value.StatusCode = result.StatusCode.GetValueOrDefault();

			return result;
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
		protected async virtual Task<IActionResult> HttpGet<TModel>(int offset, int limit, Func<Task<HttpMethodResult<IEnumerable<TModel>>>> action)
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

			HttpMethodResult<IEnumerable<TModel>> result = await action();
			if (result.HasErrors)
			{
				return this.UnprocessableEntity(new ErrorViewModel()
				{
					Errors = result.Errors
				});
			}

			return this.Ok(result.Data);
		}

		/// <summary>
		/// Executes <see cref="HttpGet" /> method against the entry.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <param name="action">The action to execute against entry.</param>
		/// <returns>
		/// The asynchronous operation of <see cref="IActionResult" />.
		/// </returns>
		protected async virtual Task<IActionResult> HttpGet<TModel>(Guid id, Func<Task<HttpMethodResult<TModel>>> action)
			where TModel : IViewModel
		{
			if (id == Guid.Empty)
			{
				return this.BadRequest("The specified unique identifier is empty");
			}

			HttpMethodResult<TModel> result = await action();
			if (result.HasErrors)
			{
				return this.UnprocessableEntity(new ErrorViewModel()
				{
					Errors = result.Errors
				});
			}

			if (result.Data == null)
			{
				return this.NotFound();
			}

			return this.Ok(result.Data);
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
		protected async virtual Task<IActionResult> HttpPut<T>(Guid id, T model, Func<Task<T>> action)
			where T : IViewModel
		{
			if (model == null || model.ID != id)
			{
				return this.BadRequest();
			}

			await action();

			return this.NoContent();
		}

		/// <summary>
		/// Executes <see cref="HttpPost" /> method against the entry.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="model">The model.</param>
		/// <param name="action">The action to execute against entry.</param>
		/// <returns>
		/// The asynchronous operation of <see cref="IActionResult" />.
		/// </returns>
		protected async virtual Task<IActionResult> HttpPost<TModel>(TModel model, Func<Task<HttpMethodResult<TModel>>> action)
			where TModel : IViewModel
		{
			if (model == null)
			{
				return this.BadRequest();
			}

			HttpMethodResult<TModel> result = await action();
		}

		/// <summary>
		/// Executes <see cref="HttpDelete" /> method against the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="action">The action to execute against entry.</param>
		/// <returns>
		/// The asynchronous operation of <see cref="IActionResult" />.
		/// </returns>
		protected async virtual Task<IActionResult> Delete(Guid id, Func<Guid, Task> action)
		{
			await action(id);

			return this.NoContent();
		}

		#endregion
	}
}
