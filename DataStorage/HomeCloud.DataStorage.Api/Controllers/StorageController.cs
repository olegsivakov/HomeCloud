namespace HomeCloud.DataStorage.Api.Controllers
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;

	using HomeCloud.DataStorage.Api.Models;
	using HomeCloud.DataStorage.Business.Services;

	#endregion

	/// <summary>
	/// Provides <see cref="RESTful API"/> with <see cref="StorageViewModel"/> support.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Api.Controllers.ControllerBase" />
	public class StorageController : ControllerBase
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IStorageService"/> service.
		/// </summary>
		private readonly IStorageService storageService = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StorageController"/> class.
		/// </summary>
		/// <param name="storageService">The <see cref="IStorageService"/> service.</param>
		public StorageController(IStorageService storageService)
		{
			this.storageService = storageService;
		}

		#endregion

		/// <summary>
		/// Gets the list of storage models.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <param name="limit">The limit.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/> containing the list of instances of <see cref="StorageViewModel"/>.</returns>
		[HttpGet("v1/[controller]s")]
		public async Task<IActionResult> Get(int offset = 0, int limit = 20)
		{
			return this.Ok(System.Linq.Enumerable.Empty<StorageViewModel>());
		}

		/// <summary>
		/// Gets the storage model specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/> containing the instance of <see cref="StorageViewModel"/>.</returns>
		[HttpGet("v1/[controller]s/{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			return await this.HttpGet(id, async () =>
			{
				return new StorageViewModel() { ID = id };
			});
		}

		/// <summary>
		/// Creates the specified storage model.
		/// </summary>
		/// <param name="model">The model of <see cref="StorageViewModel"/>.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/> containing the instance of <see cref="StorageViewModel"/>.</returns>
		[HttpPost("v1/[controller]s")]
		public async Task<IActionResult> Post(StorageViewModel model)
		{
			return await this.HttpPost(model, async () =>
			{
				var storage = new Business.Entities.Storage()
				{
					Name = "Aleh Sivakou's storage"
				};

				await this.storageService.CreateStorageAsync(storage);

				return new StorageViewModel()
				{
					ID = storage.ID,
					Name = storage.Name,
					Quota = storage.Quota.ToString(),
					CreationDate = storage.CreationDate
					
				};
			});
		}

		/// <summary>
		/// Updates the existing storage model with the specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <param name="model">The model of <see cref="StorageViewModel"/>.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/> containing the instance of <see cref="StorageViewModel"/>.</returns>
		[HttpPut("v1/[controller]s/{id}")]
		public async Task<IActionResult> Put(Guid id, [FromBody] StorageViewModel model)
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
		[HttpDelete("v1/[controller]s/{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			return this.NoContent();
		}
	}
}
