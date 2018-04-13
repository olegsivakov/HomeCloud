namespace HomeCloud.DataStorage.Api.Controllers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
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
	public class CatalogController : Controller
	{
		#region Private Members

		/// <summary>
		/// The <see cref="ICatalogService"/> service.
		/// </summary>
		private readonly ICatalogService catalogService = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CatalogController" /> class.
		/// </summary>
		/// <param name="mapper">The model type mapper.</param>
		/// <param name="catalogService">The <see cref="ICatalogService" /> service.</param>
		public CatalogController(IMapper mapper, ICatalogService catalogService)
			: base(mapper)
		{
			this.catalogService = catalogService;
		}

		#endregion

		/// <summary>
		/// Gets the list of data models by specified catalog identifier.
		/// </summary>
		/// <param name="id">The catalog identifier.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="limit">The limit.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the list of instances of <see cref="CatalogViewModel" />.
		/// </returns>
		[HttpGet("v1/[controller]s/{id}/data", Name = nameof(CatalogController.GetCatalogDataList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetCatalogDataList(
			[RequireNonDefault(ErrorMessage = "The catalog identifier is empty")] Guid id,
			[Range(0, int.MaxValue, ErrorMessage = "The offset parameter should be positive number.")] int offset,
			[Range(1, int.MaxValue, ErrorMessage = "The limit parameter cannot be less or equal zero.")] int limit)
		{
			ServiceResult<IPaginable<Catalog>> result = await this.catalogService.GetCatalogsAsync(id, offset, limit);
			IEnumerable<DataViewModel> data = result.Data != null ? this.Mapper.MapNew<Catalog, DataViewModel>(result.Data) : null;

			return this.HttpResult(
				new DataListViewModel(data, id)
				{
					Offset = result.Data?.Offset ?? offset,
					Size = result.Data?.Limit ?? limit,
					TotalCount = result.Data?.TotalCount ?? 0
				},
				result.Errors);
		}

		/// <summary>
		/// Gets the catalog model specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the instance of <see cref="CatalogViewModel" />.
		/// </returns>
		[HttpGet("v1/[controller]s/{id}", Name = nameof(CatalogController.GetCatalogByID))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetCatalogByID(
			[RequireNonDefault(ErrorMessage = "The catalog identifier is empty")] Guid id)
		{
			ServiceResult<Catalog> result = await this.catalogService.GetCatalogAsync(id);
			CatalogViewModel data = result.Data != null ? this.Mapper.MapNew<Catalog, CatalogViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}

		/// <summary>
		/// Creates the specified catalog model.
		/// </summary>
		/// <param name="catalogID">The catalog model identifier.</param>
		/// <param name="model">The model of <see cref="CatalogViewModel" />.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the instance of <see cref="CatalogViewModel" />.
		/// </returns>
		[HttpPost("v1/[controller]s/{catalogID}", Name = nameof(CatalogController.CreateCatalog))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> CreateCatalog(
			[RequireNonDefault(ErrorMessage = "The catalog identifier is empty")] Guid catalogID,
			[Required(ErrorMessage = "The model is undefined")] [FromBody] CatalogViewModel model)
		{
			Catalog entity = this.Mapper.MapNew<CatalogViewModel, Catalog>(model);
			entity.Parent.ID = catalogID;

			ServiceResult<Catalog> result = await this.catalogService.CreateCatalogAsync(entity);
			CatalogViewModel data = result.Data != null ? this.Mapper.MapNew<Catalog, CatalogViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}

		/// <summary>
		/// Updates the existing catalog model by the specified identifier.
		/// </summary>
		/// <param name="id">The catalog unique identifier.</param>
		/// <param name="model">The model of <see cref="CatalogViewModel" />.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the instance of <see cref="CatalogViewModel" />.
		/// </returns>
		[HttpPut("v1/[controller]s/{id}", Name = nameof(CatalogController.UpdateCatalog))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> UpdateCatalog(
			[RequireNonDefault(ErrorMessage = "The catalog identifier is empty")] Guid id,
			[Required(ErrorMessage = "The model is undefined")] [FromBody] CatalogViewModel model)
		{
			Catalog entity = this.Mapper.MapNew<CatalogViewModel, Catalog>(model);
			entity.ID = id;

			ServiceResult<Catalog> result = await this.catalogService.UpdateCatalogAsync(entity);
			CatalogViewModel data = result.Data != null ? this.Mapper.MapNew<Catalog, CatalogViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}

		/// <summary>
		/// Deletes the existing catalog model by specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/>.</returns>
		[HttpDelete("v1/[controller]s/{id}", Name = nameof(CatalogController.DeleteCatalog))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> DeleteCatalog(
			[RequireNonDefault(ErrorMessage = "The catalog identifier is empty")] Guid id)
		{
			ServiceResult result = await this.catalogService.DeleteCatalogAsync(id);

			return this.HttpResult(null, result.Errors);
		}
	}
}
