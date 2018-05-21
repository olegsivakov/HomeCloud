namespace HomeCloud.IdentityService.Api.Controllers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.Http;

	using HomeCloud.IdentityService.Api.Models;
	using HomeCloud.IdentityService.Business.Entities;
	using HomeCloud.IdentityService.Business.Services;

	using HomeCloud.Mapping;
	using HomeCloud.Mapping.Extensions;

	using HomeCloud.Mvc.ActionConstraints;
	using HomeCloud.Mvc.DataAnnotations;

	using Microsoft.AspNetCore.Mvc;

	#endregion

	/// <summary>
	/// Provides <see cref="RESTful API" /> with <see cref="GrantViewModel" /> support.
	/// </summary>
	/// <seealso cref="HomeCloud.IdentityService.Api.Controllers.Controller" />
	public class GrantController : Controller
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IGrantService"/> service.
		/// </summary>
		private readonly IGrantService grantService = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="GrantController" /> class.
		/// </summary>
		/// <param name="mapper">The model type mapper.</param>
		/// <param name="grantService">The <see cref="IGrantService" /> service.</param>
		public GrantController(
			IMapper mapper,
			IGrantService grantService)
			: base(mapper)
		{
			this.grantService = grantService;
		}

		#endregion

		/// <summary>
		/// Gets the grant by specified identifier.
		/// </summary>
		/// <param name="id">The grant identifier.</param>
		/// <returns>The instance of <see cref="GrantViewModel"/>.</returns>
		[HttpGet("v1/[controller]s/{id}", Name = nameof(GrantController.GetGrantByID))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetGrantByID(
			[RequireNonDefault(ErrorMessage = "The grant identifier is empty")] string id)
		{
			ServiceResult<Grant> result = await this.grantService.GetGrantAsync(id);
			GrantViewModel data = result.Data != null ? this.Mapper.MapNew<Grant, GrantViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}

		/// <summary>
		/// Gets the grant list by specified user identifier.
		/// </summary>
		/// <param name="userID">The user identifier.</param>
		/// <returns>The list of instance of <see cref="GrantViewModel"/>.</returns>
		[HttpGet("v1/[controller]s", Name = nameof(GrantController.GetGrantList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetGrantList(
			[RequireNonDefault(ErrorMessage = "The grant user identifier is empty")] [FromQuery] Guid userID)
		{
			ServiceResult<IEnumerable<Grant>> result = await this.grantService.FindGrantsAsync(new GrantSearchCriteria()
			{
				UserID = userID
			});

			IEnumerable<GrantViewModel> data = result.Data != null ? this.Mapper.MapNew<Grant, GrantViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}

		/// <summary>
		/// Gets the list of grant types.
		/// </summary>
		/// <returns>The list of key-value pairs.</returns>
		[HttpGet("v1/[controller]s/types", Name = nameof(GrantController.GetGrantTypeList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetGrantTypeList()
		{
			ServiceResult<IDictionary<int, string>> result = await this.grantService.GetGrantTypesAsync();

			return this.HttpResult(result.Data, result.Errors);
		}

		/// <summary>
		/// Deletes the list of grants by specified parameter criteria.
		/// </summary>
		/// <param name="applicationID">The client application identifier.</param>
		/// <param name="userID">The user identifier.</param>
		/// <param name="type">The grant type.</param>
		/// <returns>The result of the operation.</returns>
		[HttpDelete("v1/[controller]s/", Name = nameof(GrantController.DeleteGrantList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> DeleteGrantList(
			[RequireNonDefault(ErrorMessage = "The client application identifier is empty")] [FromQuery] Guid applicationID,
			[RequireNonDefault(ErrorMessage = "The user identifier is empty")] [FromQuery] Guid userID,
			[FromQuery] string type)
		{
			ServiceResult result = await this.grantService.DeleteGrantsAsync(new GrantSearchCriteria()
			{
				UserID = userID,
				ClientID = applicationID,
				Type = type
			});

			return this.HttpResult(null, result.Errors);
		}

		/// <summary>
		/// Deletes the grant by specified grant identifier.
		/// </summary>
		/// <param name="id">The grant identifier.</param>
		/// <returns>The result of the operation.</returns>
		[HttpDelete("v1/[controller]s/{id}", Name = nameof(GrantController.DeleteGrantByID))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> DeleteGrantByID(
			[RequireNonDefault(ErrorMessage = "The grant identifier is empty")] string id)
		{
			ServiceResult result = await this.grantService.DeleteGrantAsync(id);

			return this.HttpResult(null, result.Errors);
		}

		/// <summary>
		/// Saves the specified grant model.
		/// </summary>
		/// <param name="model">The grant model.</param>
		/// <returns>The updated instanc of <see cref="GrantViewModel"/>.</returns>
		[HttpPost("v1/[controller]s", Name = nameof(GrantController.SaveGrant))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> SaveGrant(
			[Required(ErrorMessage = "The model is undefined")] [FromBody] GrantViewModel model)
		{
			Grant entity = this.Mapper.MapNew<GrantViewModel, Grant>(model);
			ServiceResult<Grant> result = await this.grantService.SaveGrantAsync(entity);

			GrantViewModel data = result.Data != null ? this.Mapper.MapNew<Grant, GrantViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}
	}
}
