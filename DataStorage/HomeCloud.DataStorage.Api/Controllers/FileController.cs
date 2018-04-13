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
	using HomeCloud.Mvc.ActionConstraints;
	using HomeCloud.Mvc.DataAnnotations;

	using Microsoft.AspNetCore.Http.Features;
	using Microsoft.AspNetCore.Mvc;

	#endregion

	/// <summary>
	/// Provides <see cref="RESTful API" /> with <see cref="DataViewModel" /> support.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Api.Controllers.Controller" />
	public class FileController : Controller
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
		/// Initializes a new instance of the <see cref="FileController" /> class.
		/// </summary>
		/// <param name="mapper">The model type mapper.</param>
		/// <param name="catalogEntryService">The <see cref="ICatalogEntryService" /> service.</param>
		public FileController(IMapper mapper, ICatalogEntryService catalogEntryService)
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
		[HttpGet("v1/[controller]/{id}", Name = nameof(FileController.GetFileByID))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetFileByID(
			[RequireNonDefault(ErrorMessage = "The unique identifier is empty")] Guid id)
		{
			ServiceResult<CatalogEntry> result = await this.catalogEntryService.GetEntryAsync(id);
			FileViewModel data = result.Data != null ? this.Mapper.MapNew<CatalogEntry, FileViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}

		/// <summary>
		/// Downloads the data by specified identifie
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the instance of <see cref="DataViewModel" /> or <see cref="FileViewModel" /> stream.
		/// </returns>
		[HttpGet("v1/[controller]/{id}/download", Name = nameof(FileController.DownloadFileByID))]
		[ContentType(MimeTypes.Application.OctetStream)]
		public async Task<IActionResult> DownloadFileByID(
			[RequireNonDefault(ErrorMessage = "The unique identifier is empty")] Guid id)
		{
			ServiceResult<CatalogEntry> result = await this.catalogEntryService.GetEntryAsync(id);
			FileStreamViewModel data = result.Data != null ? this.Mapper.MapNew<CatalogEntry, FileStreamViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}

		/// <summary>
		/// Creates the specified data model.
		/// </summary>
		/// <param name="catalogID">The catalog model identifier.</param>
		/// <param name="model">The stream model.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the instance of <see cref="FileStreamViewModel" />.
		/// </returns>
		[HttpPost("v1/catalogs/{catalogID}/[controller]", Name = nameof(FileController.CreateFile))]
		[ContentType(MimeTypes.Multipart.FormData)]
		[DisableFormValueModelBinding]
		public async Task<IActionResult> CreateFile(
			[RequireNonDefault(ErrorMessage = "The catalog identifier is empty")] Guid catalogID,
			[Required(ErrorMessage = "The model is undefined")] [FromBody] FileStreamViewModel model)
		{
			CatalogEntry entry = this.Mapper.MapNew<FileStreamViewModel, CatalogEntry>(model);
			entry.Catalog.ID = catalogID;

			CatalogEntryStream stream = new CatalogEntryStream(entry, model.Stream);

			ServiceResult<CatalogEntry> result = await this.catalogEntryService.CreateEntryAsync(stream);
			FileStreamViewModel data = result.Data != null ? this.Mapper.MapNew<CatalogEntry, FileStreamViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}

		/// <summary>
		/// Deletes the existing data model by specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" />.
		/// </returns>
		[HttpDelete("v1/[controller]/{id}", Name = nameof(FileController.DeleteFile))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> DeleteFile(
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
		[HttpHead("v1/[controller]/{id}", Name = nameof(FileController.DataExists))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> DataExists(
			[RequireNonDefault(ErrorMessage = "The unique identifier is empty")] Guid id)
		{
			ServiceResult<CatalogEntry> result = await this.catalogEntryService.GetEntryAsync(id);
			FileStreamViewModel data = result.Data != null ? this.Mapper.MapNew<CatalogEntry, FileStreamViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}
	}
}
