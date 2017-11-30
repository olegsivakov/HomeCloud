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
		/// <param name="catalogService">The <see cref="ICatalogService" /> service.</param>
		/// <param name="mapper">The model type mapper.</param>
		public CatalogController(
			ICatalogService catalogService,
			IMapper mapper)
			: base(mapper)
		{
			this.catalogService = catalogService;
		}

		#endregion

		/// <summary>
		/// Gets the list of catalog models by specified parent catalog identifier.
		/// </summary>
		/// <param name="parentID">The parent catalog identifier.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="limit">The limit.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the list of instances of <see cref="CatalogViewModel" />.
		/// </returns>
		[HttpGet("v1/[controller]s/{parentID}")]
		public async Task<IActionResult> Get(Guid parentID, int offset, int limit)
		{
			return await this.HttpGet(
				offset,
				limit,
				async () =>
				{
					ServiceResult<IPaginable<Catalog>> catalogResult = await this.catalogService.GetCatalogsAsync(parentID, offset, limit);

					return await this.HttpGetResult<Catalog, CatalogViewModel>(catalogResult);
				});
		}

		/// <summary>
		/// Gets the catalog model specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the instance of <see cref="CatalogViewModel" />.
		/// </returns>
		[HttpGet("v1/[controller]s/{parentID}/{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			return await this.HttpGet(
				id,
				async () =>
				{
					ServiceResult<Catalog> result = await this.catalogService.GetCatalogAsync(id);

					return await this.HttpGetResult<Catalog, CatalogViewModel>(result);
				});
		}

		/// <summary>
		/// Creates the specified catalog model.
		/// </summary>
		/// <param name="parentID">The parent catalog model identifier.</param>
		/// <param name="model">The model of <see cref="CatalogViewModel" />.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the instance of <see cref="CatalogViewModel" />.
		/// </returns>
		[HttpPost("v1/[controller]s/{parentID}")]
		public async Task<IActionResult> Post(Guid parentID, [FromBody] CatalogViewModel model)
		{
			return await this.HttpPost(
				model,
				async () =>
				{
					Catalog entity = await this.Mapper.MapNewAsync<CatalogViewModel, Catalog>(model);
					entity.Parent.ID = parentID;

					ServiceResult<Catalog> result = await this.catalogService.CreateCatalogAsync(entity);

					return await this.HttpPostResult<Catalog, CatalogViewModel>(this.Get, result);
				});
		}

		/// <summary>
		/// Updates the existing catalog model by the specified identifier.
		/// </summary>
		/// <param name="parentID">The parent catalog model identifier.</param>
		/// <param name="id">The unique identifier.</param>
		/// <param name="model">The model of <see cref="CatalogViewModel" />.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the instance of <see cref="CatalogViewModel" />.
		/// </returns>
		[HttpPut("v1/[controller]s/{parentID}/{id}")]
		public async Task<IActionResult> Put(Guid parentID, Guid id, [FromBody] CatalogViewModel model)
		{
			return await this.HttpPut(
				id,
				model,
				async () =>
				{
					Catalog entity = await this.Mapper.MapNewAsync<CatalogViewModel, Catalog>(model);
					entity.Parent.ID = parentID;

					ServiceResult<Catalog> result = await this.catalogService.UpdateCatalogAsync(entity);

					return await this.HttpPutResult<Catalog, CatalogViewModel>(this.Get, result);
				});
		}

		/// <summary>
		/// Deletes the existing catalog model by specified identifier.
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
					ServiceResult result = await this.catalogService.DeleteCatalogAsync(id);

					return await this.HttpDeleteResult(result);
				});
		}
	}
}
