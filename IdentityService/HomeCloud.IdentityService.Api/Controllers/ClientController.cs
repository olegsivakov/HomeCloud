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

	using HomeCloud.IdentityService.Business.Entities;
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
	/// Provides <see cref="RESTful API" /> with <see cref="ClientViewModel" /> support.
	/// </summary>
	/// <seealso cref="HomeCloud.IdentityService.Api.Controllers.Controller" />
	public class ClientController : Controller
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IClientService"/> service.
		/// </summary>
		private readonly IClientService clientService = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ClientController" /> class.
		/// </summary>
		/// <param name="mapper">The model type mapper.</param>
		/// <param name="clientService">The <see cref="IClientService" /> service.</param>
		public ClientController(
			IMapper mapper,
			IClientService clientService)
			: base(mapper)
		{
			this.clientService = clientService;
		}

		#endregion

		/// <summary>
		/// Gets the client application by specified identifier.
		/// </summary>
		/// <param name="id">The client application identifier.</param>
		/// <returns>The instance of <see cref="ClientViewModel"/>.</returns>
		[HttpGet("v1/[controller]s/{id}", Name = nameof(ClientController.GetClientByID))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetClientByID(
			[RequireNonDefault(ErrorMessage = "The client application identifier is empty")] Guid id)
		{
			ServiceResult<Client> result = await this.clientService.GetApplicationAsync(id);
			ClientViewModel data = result.Data != null ? this.Mapper.MapNew<Client, ClientViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}

		/// <summary>
		/// Gets the grant list of the client application.
		/// </summary>
		/// <param name="id">The client application identifier.</param>
		/// <returns>The instance of <see cref="IEnumerable{GrantViewModel}"/>.</returns>
		[HttpGet("v1/[controller]s/{id}/grants", Name = nameof(ClientController.GetClientGrantList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetClientGrantList(
			[RequireNonDefault(ErrorMessage = "The client application identifier is empty")] Guid id)
		{
			ServiceResult<IEnumerable<Grant>> result = await this.clientService.GetGrantsAsync(id);
			IEnumerable<GrantViewModel> data = result.Data != null ? this.Mapper.MapNew<Grant, GrantViewModel>(result.Data) : null;

			return this.HttpResult(new ApplicationDataListViewModel<GrantViewModel>(data, id)
			{
				Size = result.Data.Count(),
				TotalCount = result.Data.Count()
			}, result.Errors);
		}

		/// <summary>
		/// Gets the secret list of the client application.
		/// </summary>
		/// <param name="id">The client application identifier.</param>
		/// <returns>The instance of <see cref="IEnumerable{SecretViewModel}"/>.</returns>
		[HttpGet("v1/[controller]s/{id}/secrets", Name = nameof(ClientController.GetClientSecretList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetClientSecretList(
			[RequireNonDefault(ErrorMessage = "The client application identifier is empty")] Guid id)
		{
			ServiceResult<IEnumerable<Secret>> result = await this.clientService.GetSecretsAsync(id);
			IEnumerable<SecretViewModel> data = result.Data != null ? this.Mapper.MapNew<Secret, SecretViewModel>(result.Data) : null;

			return this.HttpResult(new ApplicationDataListViewModel<SecretViewModel>(data, id)
			{
				Size = result.Data.Count(),
				TotalCount = result.Data.Count()
			}, result.Errors);
		}

		/// <summary>
		/// Gets the scope list of the client application.
		/// </summary>
		/// <param name="id">The client application identifier.</param>
		/// <returns>The instance of <see cref="IEnumerable{string}"/>.</returns>
		[HttpGet("v1/[controller]s/{id}/scopes", Name = nameof(ClientController.GetClientScopeList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetClientScopeList(
			[RequireNonDefault(ErrorMessage = "The client application identifier is empty")] Guid id)
		{
			ServiceResult<IEnumerable<string>> result = await this.clientService.GetScopesAsync(id);

			return this.HttpResult(new ApplicationDataListViewModel<string>(result.Data, id)
			{
				Size = result.Data.Count(),
				TotalCount = result.Data.Count()
			}, result.Errors);
		}

		/// <summary>
		/// Gets the origin list of the client application.
		/// </summary>
		/// <param name="id">The client application identifier.</param>
		/// <returns>The instance of <see cref="IEnumerable{string}"/>.</returns>
		[HttpGet("v1/[controller]s/{id}/origins", Name = nameof(ClientController.GetClientOriginList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetClientOriginList(
			[RequireNonDefault(ErrorMessage = "The client application identifier is empty")] Guid id)
		{
			ServiceResult<IEnumerable<string>> result = await this.clientService.GetOriginsAsync(id);

			return this.HttpResult(new ApplicationDataListViewModel<string>(result.Data, id)
			{
				Size = result.Data.Count(),
				TotalCount = result.Data.Count()
			}, result.Errors);
		}

		/// <summary>
		/// Gets the client list.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The list of instance of <see cref="GrantViewModel" />.
		/// </returns>
		[HttpGet("v1/[controller]s", Name = nameof(ClientController.GetClientList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> GetClientList(
			[Range(0, int.MaxValue, ErrorMessage = "The offset parameter should be positive number.")] int offset,
			[Range(1, int.MaxValue, ErrorMessage = "The limit parameter cannot be less or equal zero.")] int limit)
		{
			ServiceResult<IPaginable<Client>> result = await this.clientService.FindApplicationsAsync(null, offset, limit);
			IEnumerable<ApplicationViewModel> data = result.Data != null ? this.Mapper.MapNew<Application, ApplicationViewModel>(result.Data.Cast<Application>()) : null;

			return this.HttpResult(new PagedListViewModel<ApplicationViewModel>(data?.OrderBy(item => item.Name))
			{
				Offset = result.Data?.Offset ?? offset,
				Size = result.Data?.Limit ?? limit,
				TotalCount = result.Data?.TotalCount ?? 0
			}, result.Errors);
		}

		/// <summary>
		/// Validates the client application model being created.
		/// </summary>
		/// <returns>The result of validation operation.</returns>
		[HttpPut("v1/[controller]s/validate", Name = nameof(ClientController.ValidateClient))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> ValidateClient(
			[Required(ErrorMessage = "The model is undefined")] [FromBody] ClientViewModel model)
		{
			Client entity = this.Mapper.MapNew<ClientViewModel, Client>(model);
			ServiceResult result = await this.clientService.ValidateAsync(entity);

			return this.HttpResult(null, result.Errors);
		}


		/// <summary>
		/// Deletes the client application by specified client application identifier.
		/// </summary>
		/// <param name="id">The client application identifier.</param>
		/// <returns>The result of the operation.</returns>
		[HttpDelete("v1/[controller]s/{id}", Name = nameof(ClientController.DeleteClientByID))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> DeleteClientByID(
			[RequireNonDefault(ErrorMessage = "The client application identifier is empty")] Guid id)
		{
			ServiceResult result = await this.clientService.DeleteApplicationAsync(id);

			return this.HttpResult(null, result.Errors);
		}

		/// <summary>
		/// Creates the specified client application model.
		/// </summary>
		/// <param name="model">The model of <see cref="ClientViewModel"/>.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/> containing the instance of <see cref="ClientViewModel"/>.</returns>
		[HttpPost("v1/[controller]s", Name = nameof(ClientController.CreateClient))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> CreateClient(
			[Required(ErrorMessage = "The model is undefined")] [FromBody] ClientViewModel model)
		{
			Client entity = this.Mapper.MapNew<ClientViewModel, Client>(model);
			ServiceResult<Client> result = await this.clientService.CreateApplicationAsync(entity);

			ClientViewModel data = result.Data != null ? this.Mapper.MapNew<Client, ClientViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}

		/// <summary>
		/// Updates the existing cliet model with the specified identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <param name="model">The model of <see cref="ClientViewModel"/>.</param>
		/// <returns>The asynchronous result of <see cref="IActionResult"/> containing the instance of <see cref="ClientViewModel"/>.</returns>
		[HttpPut("v1/[controller]s/{id}", Name = nameof(ClientController.UpdateClient))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> UpdateClient(
			[RequireNonDefault(ErrorMessage = "The client application identifier is empty")] Guid id,
			[Required(ErrorMessage = "The model is undefined")] [FromBody] ClientViewModel model)
		{
			model.ID = id;

			Client entity = this.Mapper.MapNew<ClientViewModel, Client>(model);
			ServiceResult<Client> result = await this.clientService.UpdateApplicationAsync(entity);

			ClientViewModel data = result.Data != null ? this.Mapper.MapNew<Client, ClientViewModel>(result.Data) : null;

			return this.HttpResult(data, result.Errors);
		}

		/// <summary>
		/// Gets the secret list of the client application.
		/// </summary>
		/// <param name="id">The client application identifier.</param>
		/// <returns>The instance of <see cref="IEnumerable{SecretViewModel}"/>.</returns>
		[HttpPut("v1/[controller]s/{id}/secrets", Name = nameof(ClientController.SaveClientSecretList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> SaveClientSecretList(
			[RequireNonDefault(ErrorMessage = "The client application identifier is empty")] Guid id,
			[FromBody] IEnumerable<SecretViewModel> model)
		{
			IEnumerable<Secret> entities = this.Mapper.MapNew<SecretViewModel, Secret>(model ?? Enumerable.Empty<SecretViewModel>());

			ServiceResult<IEnumerable<Secret>> result = await this.clientService.SaveSecretsAsync(id, entities);
			IEnumerable<SecretViewModel> data = result.Data != null ? this.Mapper.MapNew<Secret, SecretViewModel>(result.Data) : null;

			return this.HttpResult(new ApplicationDataListViewModel<SecretViewModel>(data, id)
			{
				Size = data.Count(),
				TotalCount = data.Count()
			}, result.Errors);
		}

		/// <summary>
		/// Gets the scope list of the client application.
		/// </summary>
		/// <param name="id">The client application identifier.</param>
		/// <returns>The instance of <see cref="IEnumerable{string}"/>.</returns>
		[HttpPut("v1/[controller]s/{id}/scopes", Name = nameof(ClientController.SaveClientScopeList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> SaveClientScopeList(
			[RequireNonDefault(ErrorMessage = "The client application identifier is empty")] Guid id,
			[FromBody] IEnumerable<string> model)
		{
			ServiceResult<IEnumerable<string>> result = await this.clientService.SaveScopesAsync(id, model);

			return this.HttpResult(new ApplicationDataListViewModel<string>(result.Data, id)
			{
				Size = result.Data.Count(),
				TotalCount = result.Data.Count()
			}, result.Errors);
		}

		/// <summary>
		/// Gets the origin list of the client application.
		/// </summary>
		/// <param name="id">The client application identifier.</param>
		/// <returns>The instance of <see cref="IEnumerable{string}"/>.</returns>
		[HttpPut("v1/[controller]s/{id}/origins", Name = nameof(ClientController.SaveClientOriginList))]
		[ContentType(MimeTypes.Application.Json)]
		public async Task<IActionResult> SaveClientOriginList(
			[RequireNonDefault(ErrorMessage = "The client application identifier is empty")] Guid id,
			[FromBody] IEnumerable<string> model)
		{
			ServiceResult<IEnumerable<string>> result = await this.clientService.SaveOriginsAsync(id, model);

			return this.HttpResult(new ApplicationDataListViewModel<string>(result.Data, id)
			{
				Size = result.Data.Count(),
				TotalCount = result.Data.Count()
			}, result.Errors);
		}
	}
}
