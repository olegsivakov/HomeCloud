namespace HomeCloud.DataStorage.Api.Controllers
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;

	using HomeCloud.DataStorage.Api.Models;
	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Provides base <see cref="RESTful API"/> methods with view model support.
	/// </summary>
	public abstract class ControllerBase : Controller
	{
		#region Public Methods

		/// <summary>
		/// Executes <see cref="HttpGet" /> method against the entry.
		/// </summary>
		/// <typeparam name="T">The type of model derived from <see cref="ViewModelBase" />.</typeparam>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return..</param>
		/// <param name="action">The action to execute against entry.</param>
		/// <returns>
		/// The asynchronous operation of <see cref="IActionResult" />.
		/// </returns>
		protected async virtual Task<IActionResult> HttpGet<T>(int offset, int limit, Func<Task<IEnumerable<T>>> action)
			where T : ViewModelBase
		{
			if (offset < 0)
			{
				return this.BadRequest("The offset parameter should be positive number.");
			}

			if (limit <= 0)
			{
				return this.BadRequest("The limit parameter cannot be less or equal zero.");
			}

			IEnumerable<T> result = await action();

			return this.Ok(result);
		}

		/// <summary>
		/// Executes <see cref="HttpGet" /> method against the entry.
		/// </summary>
		/// <typeparam name="T">The type of model derived from <see cref="ViewModelBase" />.</typeparam>
		/// <param name="id">The unique identifier.</param>
		/// <param name="action">The action to execute against entry.</param>
		/// <returns>
		/// The asynchronous operation of <see cref="IActionResult" />.
		/// </returns>
		protected async virtual Task<IActionResult> HttpGet<T>(Guid id, Func<Task<T>> action)
			where T : ViewModelBase
		{
			if (id == Guid.Empty)
			{
				return this.BadRequest("The specified unique identifier is empty");
			}

			T result = await action();

			return this.Ok(result);
		}

		/// <summary>
		/// Executes <see cref="HttpPost"/> method against the entry.
		/// </summary>
		/// <typeparam name="T">The type of model derived from <see cref="ViewModelBase"/>.</typeparam>
		/// <param name="model">The model.</param>
		/// <param name="action">The action to execute against entry.</param>
		/// <returns>The asynchronous operation of <see cref="IActionResult"/>.</returns>
		protected async virtual Task<IActionResult> HttpPost<T>(T model, Func<Task<T>> action)
			where T : ViewModelBase
		{
			if (model == null)
			{
				return this.BadRequest();
			}

			T result = await action();

			return this.Ok(result);
		}

		/// <summary>
		/// Executes <see cref="HttpPut"/> method against the entry.
		/// </summary>
		/// <typeparam name="T">The type of model derived from <see cref="ViewModelBase"/>.</typeparam>
		/// <param name="id">The entry unique identifier.</param>
		/// <param name="model">The entry model.</param>
		/// <param name="action">The action to execute against entry.</param>
		/// <returns>The asynchronous operation of <see cref="IActionResult"/>.</returns>
		protected async virtual Task<IActionResult> HttpPut<T>(Guid id, T model, Func<Task<T>> action)
			where T : ViewModelBase
		{
			if (model == null || model.ID != id)
			{
				return this.BadRequest();
			}

			await action();

			return this.NoContent();
		}

		/// <summary>
		/// Executes <see cref="HttpDelete"/> method against the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="action">The action to execute against entry.</param>
		/// <returns>The asynchronous operation of <see cref="IActionResult"/>.</returns>
		protected async virtual Task<IActionResult> Delete(Guid id, Func<Guid, Task> action)
		{
			await action(id);

			return this.NoContent();
		}

		#endregion
	}
}
