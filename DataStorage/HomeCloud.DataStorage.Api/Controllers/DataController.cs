namespace HomeCloud.DataStorage.Api.Controllers
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;

	using HomeCloud.DataStorage.Api.Models;

	using ControllerBase = HomeCloud.Api.Mvc.ControllerBase;

	#endregion

	/// <summary>
	/// Provides <see cref="RESTful API"/> with <see cref="DataViewModel"/> support.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Api.Controllers.ControllerBase" />
	public class DataController : ControllerBase
	{
		/// <summary>
		/// Gets the list of data models.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <param name="limit">The limit.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/> containing the list of instances of <see cref="DataViewModel"/>.</returns>
		[HttpGet("v1/storages/{storageID}/[controller]")]
		public async Task<IActionResult> Get(Guid storageID, int offset = 0, int limit = 20)
		{
			return this.Ok(System.Linq.Enumerable.Empty<DataViewModel>());
		}

		/// <summary>
		/// Gets the data model specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/> containing the instance of <see cref="DataViewModel"/>.</returns>
		[HttpGet("v1/storages/{storageID}/[controller]/{id}")]
		public async Task<IActionResult> Get(Guid storageID, Guid id)
		{
			return await this.HttpGet(id, async () =>
			{
				return new DataViewModel() { ID = id };
			});
		}

		/// <summary>
		/// Creates the specified storage model.
		/// </summary>
		/// <param name="model">The model of <see cref="DataViewModel"/>.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/> containing the instance of <see cref="DataViewModel"/>.</returns>
		[HttpPost("v1/storages/{storageID}/[controller]")]
		public async Task<IActionResult> Post(Guid storageID, DataViewModel model)
		{
			return await this.HttpPost(model, async () =>
			{
				return model;
			});
		}

		/// <summary>
		/// Updates the existing storage model with the specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <param name="model">The model of <see cref="DataViewModel"/>.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/> containing the instance of <see cref="DataViewModel"/>.</returns>
		[HttpPut("v1/storages/{storageID}/[controller]/{id}")]
		public async Task<IActionResult> Put(Guid storageID, Guid id, [FromBody] DataViewModel model)
		{
			return await this.HttpPut(id, model, async () =>
			{
				return model;
			});
		}

		/// <summary>
		/// Deletes the existing storage model ин specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/>.</returns>
		[HttpDelete("v1/[controller]/{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			return this.NoContent();
		}
	}
}
