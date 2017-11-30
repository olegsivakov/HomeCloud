namespace HomeCloud.DataStorage.Api.Controllers
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.Core.Extensions;

	using HomeCloud.DataStorage.Api.Filters;
	using HomeCloud.DataStorage.Api.Models;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Services;

	using HomeCloud.Mapping;

	using Microsoft.AspNetCore.Http.Features;
	using Microsoft.AspNetCore.Mvc;

	#endregion

	/// <summary>
	/// Provides <see cref="RESTful API" /> with <see cref="DataViewModel" /> support.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Api.Controllers.Controller" />
	public class DataController : Controller
	{
		#region Constants

		/// <summary>
		/// The "<see cref="application/octet-stream"/>" content type
		/// </summary>
		private const string ApplicationOctetStreamContentType = "application/octet-stream";

		#endregion

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
		/// <param name="catalogEntryService">The <see cref="ICatalogEntryService"/> service.</param>
		/// <param name="mapper">The model type mapper.</param>
		public DataController(ICatalogEntryService catalogEntryService, IMapper mapper)
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
		[HttpGet("v1/[controller]/{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			return await this.HttpGet(
					id,
					async () =>
					{
						ServiceResult<CatalogEntry> result = await this.catalogEntryService.GetEntryAsync(id);

						if (this.HttpContext.Request.ContentType == ApplicationOctetStreamContentType)
						{
							return await this.HttpGetStreamResult<CatalogEntry, FileViewModel>(result);
						}

						return await this.HttpGetResult<CatalogEntry, DataViewModel>(result);
					});
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
		[HttpGet("v1/catalogs/{catalogID}/[controller]")]
		public async Task<IActionResult> Get(Guid catalogID, int offset, int limit)
		{
			return await this.HttpGet(
				offset,
				limit,
				async () =>
				{
					ServiceResult<IPaginable<CatalogEntry>> catalogResult = await this.catalogEntryService.GetEntriesAsync(catalogID, offset, limit);

					return await this.HttpGetResult<CatalogEntry, DataViewModel>(catalogResult);
				});
		}

		/// <summary>
		/// Creates the specified data model.
		/// </summary>
		/// <param name="catalogID">The catalog model identifier.</param>
		/// <param name="model">The stream model.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the instance of <see cref="FileStreamViewModel" />.
		/// </returns>
		[HttpPost("v1/catalogs/{catalogID}/[controller]")]
		[DisableFormValueModelBinding]
		public async Task<IActionResult> Post(Guid catalogID, [FromBody] FileStreamViewModel model)
		{
			return await this.HttpPost(
				model,
				async () =>
				{
					CatalogEntry entry = await this.Mapper.MapNewAsync<FileViewModel, CatalogEntry>(model);
					entry.Catalog.ID = catalogID;

					CatalogEntryStream stream = new CatalogEntryStream(entry, model.Stream);

					ServiceResult<CatalogEntry> result = await this.catalogEntryService.CreateEntryAsync(stream);

					return await this.HttpPostResult<CatalogEntry, DataViewModel>(this.Get, result);
				});
		}

		/// <summary>
		/// Deletes the existing data model by specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" />.
		/// </returns>
		[HttpDelete("v1/[controller]/{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			return await this.HttpDelete(
				id,
				async () =>
				{
					ServiceResult result = await this.catalogEntryService.DeleteEntryAsync(id);

					return await this.HttpDeleteResult(result);
				});
		}

		/// <summary>
		/// Checks whether the data resource specified by identifier exists.
		/// </summary>
		/// <param name="id">The data resource identifier.</param>
		/// <returns>
		///   <see cref="ControllerBase.Ok" /> if resource exists. Otherwise <see cref="ControllerBase.NotFound" />
		/// </returns>
		[HttpHead("v1/[controller]/{id}")]
		public async Task<IActionResult> Exists(Guid id)
		{
			return await this.HttpHead(
				id,
				async () =>
				{
					ServiceResult<CatalogEntry> result = await this.catalogEntryService.GetEntryAsync(id);

					return await this.HttpHeadResult<CatalogEntry, FileViewModel>(result);
				});
		}
	}
}
