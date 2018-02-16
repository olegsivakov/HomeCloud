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

	using HomeCloud.Mvc;
	using HomeCloud.Mvc.DataAnnotations;

	using Microsoft.AspNetCore.Http.Features;
	using Microsoft.AspNetCore.Mvc;

	#endregion

	/// <summary>
	/// Provides <see cref="RESTful API" /> with <see cref="DataViewModel" /> support.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Api.Controllers.Controller" />
	public class DataController : Controller
	{
		#region Private Members

		/// <summary>
		/// The default form options so that we can use them to set the default limits for
		/// request body data
		/// </summary>
		private static readonly FormOptions DefaultFormOptions = new FormOptions();

		/// <summary>
		/// The <see cref="ICatalogEntryService"/> service.
		/// </summary>
		private readonly ICatalogEntryService catalogEntryService = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DataController" /> class.
		/// </summary>
		/// <param name="mapper">The model type mapper.</param>
		/// <param name="catalogEntryService">The <see cref="ICatalogEntryService" /> service.</param>
		public DataController(IMapper mapper, ICatalogEntryService catalogEntryService)
			: base(mapper)
		{
			this.catalogEntryService = catalogEntryService;
		}

		#endregion

		/// <summary>
		/// Gets the data model by specified identifier depending on <see cref="Content-Type" /> of the request..
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the instance of <see cref="DataViewModel" /> or <see cref="FileViewModel" /> stream.
		/// </returns>
		[HttpGet("v1/[controller]/{id}", Name = nameof(DataController.GetDataByID))]
		[ContentType(
			MimeTypes.Application.Json,
			MimeTypes.Application.OctetStream)]
		public async Task<IActionResult> GetDataByID(
			[RequireNonDefault(ErrorMessage = "The unique identifier is empty")] Guid id)
		{
			ServiceResult<CatalogEntry> result = await this.catalogEntryService.GetEntryAsync(id);

			object data = null;

			switch (this.HttpContext.Request.ContentType)
			{
				case MimeTypes.Application.OctetStream:
					{
						data = result.Data != null ? await this.Mapper.MapNewAsync<CatalogEntry, FileViewModel>(result.Data) : null;

						break;
					}

				case MimeTypes.Application.Json:
					{
						data = result.Data != null ? await this.Mapper.MapNewAsync<CatalogEntry, DataViewModel>(result.Data) : null;

						break;
					}
			}

			return this.HttpResult(data, result.Errors);
		}

		/// <summary>
		/// Gets the list of data models by specified parent catalog identifier.
		/// </summary>
		/// <param name="catalogID">The parent catalog model identifier.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="limit">The limit.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the list of instances of <see cref="DataViewModel" />.
		/// </returns>
		[HttpGet("v1/catalogs/{catalogID}/[controller]", Name = nameof(DataController.GetDataList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetDataList(
			[RequireNonDefault(ErrorMessage = "The catalog identifier is empty")] Guid catalogID,
			[Range(0, int.MaxValue, ErrorMessage = "The offset parameter should be positive number.")] int offset,
			[Range(1, int.MaxValue, ErrorMessage = "The limit parameter cannot be less or equal zero.")] int limit)
		{
			ServiceResult<IPaginable<CatalogEntry>> result = await this.catalogEntryService.GetEntriesAsync(catalogID, offset, limit);
			IEnumerable<DataViewModel> data = result.Data != null ? await this.Mapper.MapNewAsync<CatalogEntry, DataViewModel>(result.Data) : null;

			return this.HttpResult(
				new PagedListViewModel<DataViewModel>(data)
				{
					Offset = result.Data?.Offset ?? offset,
					Size = result.Data?.Limit ?? limit,
					TotalCount = result.Data?.TotalCount ?? 0
				},
				result.Errors);
		}

		/// <summary>
		/// Creates the specified data model.
		/// </summary>
		/// <param name="catalogID">The catalog model identifier.</param>
		/// <param name="model">The stream model.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the instance of <see cref="FileStreamViewModel" />.
		/// </returns>
		[HttpPost("v1/catalogs/{catalogID}/[controller]", Name = nameof(DataController.CreateData))]
		[ContentType(MimeTypes.Multipart.FormData)]
		[DisableFormValueModelBinding]
		public async Task<IActionResult> CreateData(
			[RequireNonDefault(ErrorMessage = "The catalog identifier is empty")] Guid catalogID,
			[Required(ErrorMessage = "The model is undefined")] [FromBody] FileStreamViewModel model)
		{
			CatalogEntry entry = await this.Mapper.MapNewAsync<FileViewModel, CatalogEntry>(model);
			entry.Catalog.ID = catalogID;

			CatalogEntryStream stream = new CatalogEntryStream(entry, model.Stream);

			ServiceResult<CatalogEntry> result = await this.catalogEntryService.CreateEntryAsync(stream);
			DataViewModel data = result.Data != null ? await this.Mapper.MapNewAsync<CatalogEntry, DataViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}

		/// <summary>
		/// Deletes the existing data model by specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" />.
		/// </returns>
		[HttpDelete("v1/[controller]/{id}", Name = nameof(DataController.DeleteData))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> DeleteData(
			[RequireNonDefault(ErrorMessage = "The unique identifier is empty")] Guid id)
		{
			ServiceResult result = await this.catalogEntryService.DeleteEntryAsync(id);

			return this.HttpResult(null, result.Errors);
		}

		/// <summary>
		/// Checks whether the data resource specified by identifier exists.
		/// </summary>
		/// <param name="id">The data resource identifier.</param>
		/// <returns>
		///   <see cref="ControllerBase.Ok" /> if resource exists. Otherwise <see cref="ControllerBase.NotFound" />
		/// </returns>
		[HttpHead("v1/[controller]/{id}", Name = nameof(DataController.DataExists))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> DataExists(
			[RequireNonDefault(ErrorMessage = "The unique identifier is empty")] Guid id)
		{
			ServiceResult<CatalogEntry> result = await this.catalogEntryService.GetEntryAsync(id);
			DataViewModel data = result.Data != null ? await this.Mapper.MapNewAsync<CatalogEntry, DataViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}
	}
}
