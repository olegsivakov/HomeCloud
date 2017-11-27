﻿namespace HomeCloud.DataStorage.Api.Controllers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using HomeCloud.Core.Extensions;

	using HomeCloud.DataStorage.Api.Models;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Services;

	using HomeCloud.Mapping;

	using Microsoft.AspNetCore.Mvc;
	using HomeCloud.Api.Http;

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

		/// <summary>
		/// The <see cref="ICatalogEntryService"/> service.
		/// </summary>
		private readonly ICatalogEntryService catalogEntryService = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CatalogController" /> class.
		/// </summary>
		/// <param name="catalogService">The <see cref="ICatalogService" /> service.</param>
		/// <param name="catalogEntryService">The <see cref="ICatalogEntryService"/> service.</param>
		/// <param name="mapper">The model type mapper.</param>
		public CatalogController(
			ICatalogService catalogService,
			ICatalogEntryService catalogEntryService,
			IMapper mapper)
			: base(mapper)
		{
			this.catalogService = catalogService;
			this.catalogEntryService = catalogEntryService;
		}

		#endregion

		/// <summary>
		/// Gets the list of data models by their parent id.
		/// </summary>
		/// <param name="parentID">The parent model identifier.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="limit">The limit.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the list of instances of <see cref="DataViewModel" />.
		/// </returns>
		[HttpGet("v1/[controller]s/{parentID}")]
		public async Task<IActionResult> Get(Guid parentID, int offset, int limit)
		{
			return await this.HttpGet(
				offset,
				limit,
				async () =>
				{
					ServiceResult<IEnumerable<Catalog>> catalogResult = await this.catalogService.GetCatalogsAsync(parentID, offset, limit);
					HttpGetResult<IEnumerable<DataViewModel>> result = await this.HttpGetResult<Catalog, DataViewModel>(catalogResult) as HttpGetResult<IEnumerable<DataViewModel>>;

					int catalogcount = result.Data.Count();
					if (catalogcount < limit)
					{
						int entryCount = limit - catalogcount;

						ServiceResult<IEnumerable<CatalogEntry>> catalogEntryResult = await this.catalogEntryService.GetEntriesAsync(parentID, 0, limit);
						HttpGetResult<IEnumerable<DataViewModel>> entryResult = await this.HttpGetResult<CatalogEntry, DataViewModel>(catalogEntryResult) as HttpGetResult<IEnumerable<DataViewModel>>;

						result.Data.Union(entryResult.Data);
						result.Errors.Union(entryResult.Errors);
					}

					return result;
				});
		}

		/// <summary>
		/// Gets the data model specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the instance of <see cref="DataViewModel" />.
		/// </returns>
		[HttpGet("v1/[controller]s/{parentID}/{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			return await this.HttpGet(
				id,
				async () =>
				{
					ServiceResult<Catalog> result = await this.catalogService.GetCatalogAsync(id);

					return await this.HttpGetResult<Catalog, DataViewModel>(result);
				});
		}

		/// <summary>
		/// Creates the specified data model.
		/// </summary>
		/// <param name="parentID">The parent model identifier.</param>
		/// <param name="model">The model of <see cref="DataViewModel" />.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the instance of <see cref="DataViewModel" />.
		/// </returns>
		[HttpPost("v1/[controller]s/{parentID}")]
		public async Task<IActionResult> Post(Guid parentID, [FromBody] DataViewModel model)
		{
			return await this.HttpPost(
				model,
				async () =>
				{
					Catalog entity = await this.Mapper.MapNewAsync<DataViewModel, Catalog>(model);
					entity.Parent.ID = parentID;

					ServiceResult<Catalog> result = await this.catalogService.CreateCatalogAsync(entity);

					return await this.HttpPostResult<Catalog, DataViewModel>(this.Get, result);
				});
		}

		/// <summary>
		/// Updates the existing storage model with the specified identifier.
		/// </summary>
		/// <param name="parentID">The parent model identifier.</param>
		/// <param name="id">The unique identifier.</param>
		/// <param name="model">The model of <see cref="DataViewModel" />.</param>
		/// <returns>
		/// The asynchronous result of <see cref="IActionResult" /> containing the instance of <see cref="DataViewModel" />.
		/// </returns>
		[HttpPut("v1/[controller]s/{parentID}/{id}")]
		public async Task<IActionResult> Put(Guid parentID, Guid id, [FromBody] DataViewModel model)
		{
			return await this.HttpPut(
				id,
				model,
				async () =>
				{
					Catalog entity = await this.Mapper.MapNewAsync<DataViewModel, Catalog>(model);
					entity.Parent.ID = parentID;

					ServiceResult<Catalog> result = await this.catalogService.UpdateCatalogAsync(entity);

					return await this.HttpPutResult<Catalog, DataViewModel>(this.Get, result);
				});
		}

		/// <summary>
		/// Deletes the existing storage model ин specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/>.</returns>
		[HttpDelete("v1/[controller]/{id}")]
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