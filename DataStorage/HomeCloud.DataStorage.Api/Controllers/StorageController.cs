namespace HomeCloud.DataStorage.Api.Controllers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.Threading.Tasks;

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Api.Models;
	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Services;

	using HomeCloud.Http;

	using HomeCloud.Mapping;
	using HomeCloud.Mapping.Extensions;

	using HomeCloud.Mvc.ActionConstraints;
	using HomeCloud.Mvc.DataAnnotations;

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
		/// <param name="mapper">The model type mapper.</param>
		/// <param name="storageService">The <see cref="IStorageService" /> service.</param>
		public StorageController(IMapper mapper, IStorageService storageService)
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
		[HttpGet("v1/[controller]s", Name = nameof(StorageController.GetStorageList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetStorageList(
			[Range(0, int.MaxValue, ErrorMessage = "The offset parameter should be positive number.")] int offset,
			[Range(1, int.MaxValue, ErrorMessage = "The limit parameter cannot be less or equal zero.")] int limit)
		{
			ServiceResult<IPaginable<Storage>> result = await this.storageService.GetStoragesAsync(offset, limit);
			IEnumerable<StorageViewModel> data = result.Data != null ? this.Mapper.MapNew<Storage, StorageViewModel>(result.Data) : null;

			return this.HttpResult(
				new PagedListViewModel<StorageViewModel>(data?.OrderBy(item => item.Name))
				{
					Offset = result.Data?.Offset ?? offset,
					Size = result.Data?.Limit ?? limit,
					TotalCount = result.Data?.TotalCount ?? 0
				},
				result.Errors);
		}

		/// <summary>
		/// Gets the storage model specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/> containing the instance of <see cref="StorageViewModel"/>.</returns>
		[HttpGet("v1/[controller]s/{id}", Name = nameof(StorageController.GetStorageByID))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetStorageByID(
			[RequireNonDefault(ErrorMessage = "The storage unique identifier is empty")] Guid id)
		{
			ServiceResult<Storage> result = await this.storageService.GetStorageAsync(id);
			StorageViewModel data = result.Data != null ? this.Mapper.MapNew<Storage, StorageViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}

		/// <summary>
		/// Creates the specified storage model.
		/// </summary>
		/// <param name="model">The model of <see cref="StorageViewModel"/>.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/> containing the instance of <see cref="StorageViewModel"/>.</returns>
		[HttpPost("v1/[controller]s", Name = nameof(StorageController.CreateStorage))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> CreateStorage(
			[Required(ErrorMessage = "The model is undefined")] [FromBody] StorageViewModel model)
		{
			Storage entity = this.Mapper.MapNew<StorageViewModel, Storage>(model);
			ServiceResult<Storage> result = await this.storageService.CreateStorageAsync(entity);

			StorageViewModel data = result.Data != null ? this.Mapper.MapNew<Storage, StorageViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}

		/// <summary>
		/// Updates the existing storage model with the specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <param name="model">The model of <see cref="StorageViewModel"/>.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/> containing the instance of <see cref="StorageViewModel"/>.</returns>
		[HttpPut("v1/[controller]s/{id}", Name = nameof(StorageController.UpdateStorage))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> UpdateStorage(
			[RequireNonDefault(ErrorMessage = "The storage unique identifier is empty")] Guid id,
			[Required(ErrorMessage = "The model is undefined")] [FromBody] StorageViewModel model)
		{
			model.ID = id;

			Storage entity = this.Mapper.MapNew<StorageViewModel, Storage>(model);
			ServiceResult<Storage> result = await this.storageService.UpdateStorageAsync(entity);

			StorageViewModel data = result.Data != null ? this.Mapper.MapNew<Storage, StorageViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}

		/// <summary>
		/// Deletes the existing storage model ин specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/>.</returns>
		[HttpDelete("v1/[controller]s/{id}", Name = nameof(StorageController.DeleteStorage))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> DeleteStorage(
			[RequireNonDefault(ErrorMessage = "The storage unique identifier is empty")] Guid id)
		{
			ServiceResult result = await this.storageService.DeleteStorageAsync(id);

			return this.HttpResult(null, result.Errors);
		}
	}
}
