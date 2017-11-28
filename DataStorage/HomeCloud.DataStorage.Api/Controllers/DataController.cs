namespace HomeCloud.DataStorage.Api.Controllers
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Core.Extensions;

	using HomeCloud.DataStorage.Api.Filters;
	using HomeCloud.DataStorage.Api.Models;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Services;

	using HomeCloud.Mapping;

	using Microsoft.AspNetCore.Http.Features;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.WebUtilities;

	using Microsoft.Net.Http.Headers;

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
		/// <param name="catalogEntryService">The <see cref="ICatalogEntryService"/> service.</param>
		/// <param name="mapper">The model type mapper.</param>
		public DataController(ICatalogEntryService catalogEntryService, IMapper mapper)
			: base(mapper)
		{
			this.catalogEntryService = catalogEntryService;
		}

		#endregion

		/// <summary>
		/// Gets the data model specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the instance of <see cref="DataViewModel" />.
		/// </returns>
		[HttpGet("v1/[controller]/{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			return await this.HttpGet(
					id,
					async () =>
					{
						ServiceResult<CatalogEntry> result = await this.catalogEntryService.GetEntryAsync(id);

						return await this.HttpGetStreamResult<CatalogEntry, FileViewModel>(result);
					});
		}

		/// <summary>
		/// Creates the specified data model.
		/// </summary>
		/// <param name="catalogID">The catalog model identifier.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the instance of <see cref="DataViewModel" />.
		/// </returns>
		//[HttpPost("v1/[controller]/{catalogID}")]
		//[DisableFormValueModelBinding]
		//public async Task<IActionResult> Post(Guid catalogID)
		//{
		//	if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
		//	{
		//		return this.BadRequest($"Expected a multipart request, but got {Request.ContentType}");
		//	}

		//	DataViewModel model = new DataViewModel()
		//	{
		//		ID = catalogID,
		//		IsCatalog = true
		//	};

		//	return await this.HttpPost(
		//		model,
		//		async () =>
		//		{
		//			string fileName = null;

		//			string boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType), DefaultFormOptions.MultipartBoundaryLengthLimit);
		//			MultipartReader reader = new MultipartReader(boundary, HttpContext.Request.Body);

		//			MultipartSection section = await reader.ReadNextSectionAsync();
		//			if (section != null)
		//			{
		//				ContentDispositionHeaderValue contentDisposition = null;
		//				if (ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition) && MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
		//				{
		//					fileName = HeaderUtilities.RemoveQuotes(contentDisposition.FileName).ToString();
		//				}
		//			}

		//			CatalogEntryStream stream = new CatalogEntryStream(
		//				new CatalogEntry()
		//				{
		//					Catalog = await this.Mapper.MapNewAsync<DataViewModel, Catalog>(model),
		//					Name = fileName
		//				},
		//				section.Body);

		//			ServiceResult<CatalogEntry> result = await this.catalogEntryService.CreateEntryAsync(stream);

		//			return await this.HttpPostResult<CatalogEntry, DataViewModel>(this.Get, result);
		//		});
		//}

		/// <summary>
		/// Checks whether the resource specified by identifier exists.
		/// </summary>
		/// <param name="id">The resource identifier.</param>
		/// <returns><see cref="ControllerBase.Ok"/> if resource exists. Otherwise <see cref="ControllerBase.NotFound"/></returns>
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

		[HttpGet("v1/[controller]")]
		public async Task<IActionResult> GetAll()
		{
			return await this.HttpGet(
				Guid.NewGuid(),
				async () =>
				{
					return await this.HttpGetResult<Catalog, DataViewModel>(new ServiceResult<Catalog>(new Catalog() { Size = 2056 }));
				});
		}
	}
}
