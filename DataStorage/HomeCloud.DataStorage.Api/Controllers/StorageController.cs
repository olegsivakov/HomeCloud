namespace HomeCloud.DataStorage.Api.Controllers
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.Core.Extensions;

	using HomeCloud.DataStorage.Api.Models;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Services;

	using HomeCloud.Mapping;

	using Microsoft.AspNetCore.Mvc;

	#endregion

	/// <summary>
	/// Provides <see cref="RESTful API"/> with <see cref="StorageViewModel"/> support.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Api.Controllers.Controller" />
	public class StorageController : Controller
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IStorageService"/> service.
		/// </summary>
		private readonly IStorageService storageService = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StorageController" /> class.
		/// </summary>
		/// <param name="storageService">The <see cref="IStorageService" /> service.</param>
		/// <param name="mapper">The model type mapper.</param>
		public StorageController(IStorageService storageService, IMapper mapper)
			: base(mapper)
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
		public async Task<IActionResult> Get(int offset, int limit)
		{
			return await this.HttpGet(
				offset,
				limit,
				async () =>
				{
					ServiceResult<IPaginable<Storage>> result = await this.storageService.GetStoragesAsync(offset, limit);

					return await this.HttpGetResult<Storage, StorageViewModel>(result);
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
			return await this.HttpGet(
				id,
				async () =>
				{
					ServiceResult<Storage> result = await this.storageService.GetStorageAsync(id);

					return await this.HttpGetResult<Storage, StorageViewModel>(result);
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
			return await this.HttpPost(
				model,
				async () =>
				{
					Storage entity = await this.Mapper.MapNewAsync<StorageViewModel, Storage>(model);
					ServiceResult<Storage> result = await this.storageService.CreateStorageAsync(entity);

					return await this.HttpPostResult<Storage, StorageViewModel>(this.Get, result);
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
			return await this.HttpPut(
				id,
				model,
				async () =>
				{
					Storage entity = await this.Mapper.MapNewAsync<StorageViewModel, Storage>(model);
					ServiceResult<Storage> result = await this.storageService.UpdateStorageAsync(entity);

					return await this.HttpPutResult<Storage, StorageViewModel>(this.Get, result);
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
			return await this.HttpDelete(
				id,
				async () =>
				{
					ServiceResult result = await this.storageService.DeleteStorageAsync(id);

					return await this.HttpDeleteResult(result);
				});
		}
	}
}
