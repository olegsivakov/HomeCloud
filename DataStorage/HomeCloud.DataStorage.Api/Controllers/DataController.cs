namespace HomeCloud.DataStorage.Api.Controllers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.Core.Extensions;

	using HomeCloud.DataStorage.Api.Models;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Services;

	using HomeCloud.Mapping;

	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Filters;
	using Microsoft.AspNetCore.Mvc.ModelBinding;
	using System.Linq;
	using System.IO;
	using HomeCloud.DataStorage.Api.Helpers;
	using Microsoft.AspNetCore.WebUtilities;
	using HomeCloud.DataStorage.Api.Filters;
	using Microsoft.AspNetCore.Http.Features;
	using Microsoft.Net.Http.Headers;
	using System.Text;

	#endregion

	/// <summary>
	/// Provides <see cref="RESTful API" /> with <see cref="DataViewModel" /> support.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Api.Controllers.Controller" />
	public class DataController : Controller
	{
		#region Private Members

		/// <summary>
		/// The <see cref="ICatalogEntryService"/> service.
		/// </summary>
		private readonly ICatalogEntryService catalogEntryService = null;

		/// <summary>
		/// The default form options so that we can use them to set the default limits for
		// request body data
		/// </summary>
		private static readonly FormOptions DefaultFormOptions = new FormOptions();

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
					ServiceResult<Catalog> result = null;

					return await this.HttpGetResult<Catalog, DataViewModel>(result);
				});
		}

		/// <summary>
		/// Creates the specified data model.
		/// </summary>
		/// <param name="catalogID">The catalog model identifier.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the instance of <see cref="DataViewModel" />.
		/// </returns>
		[HttpPost("v1/[controller]/{catalogID}")]
		[DisableFormValueModelBinding]
		public async Task<IActionResult> Post(Guid catalogID)
		{
			if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
			{
				return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
			}

			string boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType), DefaultFormOptions.MultipartBoundaryLengthLimit);
			MultipartReader reader = new MultipartReader(boundary, HttpContext.Request.Body);

			MultipartSection section = await reader.ReadNextSectionAsync();
			while (section != null)
			{
				ContentDispositionHeaderValue contentDisposition = null;
				if (ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition) && MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
				{
					string targetFilePath = Path.Combine(@"D:\", HeaderUtilities.RemoveQuotes(contentDisposition.FileName).ToString());
					using (FileStream stream = System.IO.File.Create(targetFilePath))
					{
						stream.SetLength(section.Body.Length);

						await section.Body.CopyToAsync(stream);
					}
				}

				section = await reader.ReadNextSectionAsync();
			}

			return this.Ok();
		}
	}
}
