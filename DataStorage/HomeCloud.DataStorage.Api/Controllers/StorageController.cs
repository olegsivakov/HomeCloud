namespace HomeCloud.DataStorage.Api.Controllers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;

	using HomeCloud.DataStorage.Api.Models;
	using HomeCloud.DataStorage.Business.Services;
	using HomeCloud.Mapping;
	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.Core.Extensions;

	using ControllerBase = HomeCloud.Api.Mvc.ControllerBase;
	using HomeCloud.Api.Mvc;

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

		/// <summary>
		/// The <see cref="IMapper"/> mapper.
		/// </summary>
		private readonly IMapper mapper = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StorageController"/> class.
		/// </summary>
		/// <param name="storageService">The <see cref="IStorageService"/> service.</param>
		public StorageController(IStorageService storageService, IMapper mapper)
		{
			this.storageService = storageService;
			this.mapper = mapper;
		}

		#endregion

		/// <summary>
		/// Gets the list of storage models.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <param name="limit">The limit.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/> containing the list of instances of <see cref="StorageViewModel"/>.</returns>
		[HttpGet("v1/[controller]s")]
		public async Task<IActionResult> Get(int offset, int limit)
		{
			return await HttpGet(offset, limit, async () =>
			{
				ServiceResult<IEnumerable<Storage>> result = await this.storageService.GetStorages(offset, limit);
				if (!result.IsSuccess)
				{
					ErrorViewModel error = await this.mapper.MapNewAsync<ServiceResult, ErrorViewModel>(result);

					return this.UnprocessableEntity(error);
				}

				IEnumerable<Task<StorageViewModel>> tasks = result.Data.Select(async item => await this.mapper.MapNewAsync<Storage, StorageViewModel>(item));

				return this.Ok(await Task.WhenAll(tasks));
			});
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
				return this.Ok(new StorageViewModel() { ID = id });
			});
		}

		/// <summary>
		/// Creates the specified storage model.
		/// </summary>
		/// <param name="model">The model of <see cref="StorageViewModel"/>.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/> containing the instance of <see cref="StorageViewModel"/>.</returns>
		[HttpPost("v1/[controller]s")]
		public async Task<IActionResult> Post([FromBody] StorageViewModel model)
		{
			return await this.HttpPost< StorageViewModel, ObjectResult>(model, async () =>
			{
				Storage entity = await this.mapper.MapNewAsync<StorageViewModel, Storage>(model);

				ServiceResult result = await this.storageService.CreateStorageAsync(entity);
				if (!result.IsSuccess)
				{
					ErrorViewModel error = await this.mapper.MapNewAsync<ServiceResult, ErrorViewModel>(result);

					return this.UnprocessableEntity(error);
				}

				return this.Ok(await this.mapper.MapAsync(entity, model));
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
