namespace HomeCloud.IdentityService.Api.Controllers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.Http;

	using HomeCloud.IdentityService.Api.Models;

	using HomeCloud.IdentityService.Business.Entities.Applications;
	using HomeCloud.IdentityService.Business.Services;

	using HomeCloud.Mapping;
	using HomeCloud.Mapping.Extensions;

	using HomeCloud.Mvc.ActionConstraints;
	using HomeCloud.Mvc.DataAnnotations;
	using HomeCloud.Mvc.Models;

	using Microsoft.AspNetCore.Mvc;

	#endregion

	/// <summary>
	/// Provides <see cref="RESTful API" /> with <see cref="ApiResourceViewModel" /> support.
	/// </summary>
	/// <seealso cref="HomeCloud.IdentityService.Api.Controllers.Controller" />
	public class ResourceController : Controller
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IResourceService"/> service.
		/// </summary>
		private readonly IResourceService resourceService = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ResourceController" /> class.
		/// </summary>
		/// <param name="mapper">The model type mapper.</param>
		/// <param name="resourceService">The <see cref="IResourceService" /> service.</param>
		public ResourceController(
			IMapper mapper,
			IResourceService resourceService)
			: base(mapper)
		{
			this.resourceService = resourceService;
		}

		#endregion

		/// <summary>
		/// Gets the api resource application by specified identifier.
		/// </summary>
		/// <param name="id">The api resource application identifier.</param>
		/// <returns>The instance of <see cref="ApiResourceViewModel"/>.</returns>
		[HttpGet("v1/[controller]s/{id}", Name = nameof(ResourceController.GetApiResourceByID))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetApiResourceByID(
			[RequireNonDefault(ErrorMessage = "The api resource application identifier is empty")] Guid id)
		{
			ServiceResult<ApiResource> result = await this.resourceService.GetApplicationAsync(id);
			ApiResourceViewModel data = result.Data != null ? this.Mapper.MapNew<ApiResource, ApiResourceViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}

		/// <summary>
		/// Gets the grant list of the api resource application.
		/// </summary>
		/// <param name="id">The api resource application identifier.</param>
		/// <returns>The instance of <see cref="IEnumerable{GrantViewModel}"/>.</returns>
		[HttpGet("v1/[controller]s/{id}/claims", Name = nameof(ResourceController.GetApiResourceClaimList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetApiResourceClaimList(
			[RequireNonDefault(ErrorMessage = "The api resource application identifier is empty")] Guid id)
		{
			ServiceResult<IEnumerable<string>> result = await this.resourceService.GetClaimsAsync(id);

			return this.HttpResult(new ApplicationDataListViewModel<string>(result.Data, id)
			{
				Size = result.Data.Count(),
				TotalCount = result.Data.Count()
			}, result.Errors);
		}

		/// <summary>
		/// Gets the secret list of the api resource application.
		/// </summary>
		/// <param name="id">The api resource application identifier.</param>
		/// <returns>The instance of <see cref="IEnumerable{SecretViewModel}"/>.</returns>
		[HttpGet("v1/[controller]s/{id}/secrets", Name = nameof(ResourceController.GetApiResourceSecretList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetApiResourceSecretList(
			[RequireNonDefault(ErrorMessage = "The api resource application identifier is empty")] Guid id)
		{
			ServiceResult<IEnumerable<Secret>> result = await this.resourceService.GetSecretsAsync(id);
			IEnumerable<SecretViewModel> data = result.Data != null ? this.Mapper.MapNew<Secret, SecretViewModel>(result.Data) : null;

			return this.HttpResult(new ApplicationDataListViewModel<SecretViewModel>(data, id)
			{
				Size = result.Data.Count(),
				TotalCount = result.Data.Count()
			}, result.Errors);
		}

		/// <summary>
		/// Gets the scope list of the api resource application.
		/// </summary>
		/// <param name="id">The api resource application identifier.</param>
		/// <returns>The instance of <see cref="IEnumerable{string}"/>.</returns>
		[HttpGet("v1/[controller]s/{id}/scopes", Name = nameof(ResourceController.GetApiResourceScopeList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetApiResourceScopeList(
			[RequireNonDefault(ErrorMessage = "The api resource application identifier is empty")] Guid id)
		{
			ServiceResult<IEnumerable<string>> result = await this.resourceService.GetScopesAsync(id);

			return this.HttpResult(new ApplicationDataListViewModel<string>(result.Data, id)
			{
				Size = result.Data.Count(),
				TotalCount = result.Data.Count()
			}, result.Errors);
		}

		/// <summary>
		/// Gets the api resource list.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The list of instance of <see cref="GrantViewModel" />.
		/// </returns>
		[HttpGet("v1/[controller]s", Name = nameof(ResourceController.GetApiResourceList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetApiResourceList(
			[Range(0, int.MaxValue, ErrorMessage = "The offset parameter should be positive number.")] int offset,
			[Range(1, int.MaxValue, ErrorMessage = "The limit parameter cannot be less or equal zero.")] int limit)
		{
			ServiceResult<IPaginable<ApiResource>> result = await this.resourceService.FindApplicationsAsync(null, offset, limit);
			IEnumerable<ApplicationViewModel> data = result.Data != null ? this.Mapper.MapNew<Application, ApplicationViewModel>(result.Data.Cast<Application>()) : null;

			return this.HttpResult(new PagedListViewModel<ApplicationViewModel>(data?.OrderBy(item => item.Name))
			{
				Offset = result.Data?.Offset ?? offset,
				Size = result.Data?.Limit ?? limit,
				TotalCount = result.Data?.TotalCount ?? 0
			}, result.Errors);
		}

		/// <summary>
		/// Deletes the api resource application by specified api resource application identifier.
		/// </summary>
		/// <param name="id">The api resource application identifier.</param>
		/// <returns>The result of the operation.</returns>
		[HttpDelete("v1/[controller]s/{id}", Name = nameof(ResourceController.DeleteApiResourceByID))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> DeleteApiResourceByID(
			[RequireNonDefault(ErrorMessage = "The api resource application identifier is empty")] Guid id)
		{
			ServiceResult result = await this.resourceService.DeleteApplicationAsync(id);

			return this.HttpResult(null, result.Errors);
		}

		/// <summary>
		/// Creates the specified api resource application model.
		/// </summary>
		/// <param name="model">The model of <see cref="ClientViewModel"/>.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/> containing the instance of <see cref="ClientViewModel"/>.</returns>
		[HttpPost("v1/[controller]s", Name = nameof(ResourceController.CreateApiResource))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> CreateApiResource(
			[Required(ErrorMessage = "The model is undefined")] [FromBody] ApiResourceViewModel model)
		{
			ApiResource entity = this.Mapper.MapNew<ApiResourceViewModel, ApiResource>(model);
			ServiceResult<ApiResource> result = await this.resourceService.CreateApplicationAsync(entity);

			ApiResourceViewModel data = result.Data != null ? this.Mapper.MapNew<ApiResource, ApiResourceViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}

		/// <summary>
		/// Updates the existing api resource model with the specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <param name="model">The model of <see cref="ApiResourceViewModel"/>.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/> containing the instance of <see cref="ClientViewModel"/>.</returns>
		[HttpPut("v1/[controller]s/{id}", Name = nameof(ResourceController.UpdateApiResource))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> UpdateApiResource(
			[RequireNonDefault(ErrorMessage = "The api resource application identifier is empty")] Guid id,
			[Required(ErrorMessage = "The model is undefined")] [FromBody] ApiResourceViewModel model)
		{
			model.ID = id;

			ApiResource entity = this.Mapper.MapNew<ApiResourceViewModel, ApiResource>(model);
			ServiceResult<ApiResource> result = await this.resourceService.UpdateApplicationAsync(entity);

			ApiResourceViewModel data = result.Data != null ? this.Mapper.MapNew<ApiResource, ApiResourceViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}

		/// <summary>
		/// Gets the origin list of the api resource application.
		/// </summary>
		/// <param name="id">The api resource application identifier.</param>
		/// <returns>The instance of <see cref="IEnumerable{string}"/>.</returns>
		[HttpPut("v1/[controller]s/{id}/claims", Name = nameof(ResourceController.SaveApiResourceClaimList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> SaveApiResourceClaimList(
			[RequireNonDefault(ErrorMessage = "The api resource application identifier is empty")] Guid id, IEnumerable<string> model)
		{
			ServiceResult<IEnumerable<string>> result = await this.resourceService.SaveClaimsAsync(id, model);

			return this.HttpResult(new ApplicationDataListViewModel<string>(result.Data, id)
			{
				Size = result.Data.Count(),
				TotalCount = result.Data.Count()
			}, result.Errors);
		}

		/// <summary>
		/// Gets the secret list of the api resource application.
		/// </summary>
		/// <param name="id">The api resource application identifier.</param>
		/// <returns>The instance of <see cref="IEnumerable{SecretViewModel}"/>.</returns>
		[HttpPut("v1/[controller]s/{id}/secrets", Name = nameof(ResourceController.SaveApiResourceSecretList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> SaveApiResourceSecretList(
			[RequireNonDefault(ErrorMessage = "The api resource application identifier is empty")] Guid id, IEnumerable<SecretViewModel> model)
		{
			IEnumerable<Secret> entities = this.Mapper.MapNew<SecretViewModel, Secret>(model ?? Enumerable.Empty<SecretViewModel>());

			ServiceResult<IEnumerable<Secret>> result = await this.resourceService.SaveSecretsAsync(id, entities);
			IEnumerable<SecretViewModel> data = result.Data != null ? this.Mapper.MapNew<Secret, SecretViewModel>(result.Data) : null;

			return this.HttpResult(new ApplicationDataListViewModel<SecretViewModel>(data, id)
			{
				Size = data.Count(),
				TotalCount = data.Count()
			}, result.Errors);
		}

		/// <summary>
		/// Gets the scope list of the api resource application.
		/// </summary>
		/// <param name="id">The api resource application identifier.</param>
		/// <returns>The instance of <see cref="IEnumerable{string}"/>.</returns>
		[HttpPut("v1/[controller]s/{id}/scopes", Name = nameof(ResourceController.SaveApiResourceScopeList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> SaveApiResourceScopeList(
			[RequireNonDefault(ErrorMessage = "The api resource application identifier is empty")] Guid id, IEnumerable<string> model)
		{
			ServiceResult<IEnumerable<string>> result = await this.resourceService.SaveScopesAsync(id, model);

			return this.HttpResult(new ApplicationDataListViewModel<string>(result.Data, id)
			{
				Size = result.Data.Count(),
				TotalCount = result.Data.Count()
			}, result.Errors);
		}
	}
}
