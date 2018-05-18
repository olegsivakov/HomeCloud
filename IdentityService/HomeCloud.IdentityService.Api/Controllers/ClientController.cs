namespace HomeCloud.IdentityService.Api.Controllers
{
	using HomeCloud.Core;
	using HomeCloud.Http;
	#region Usings

	using HomeCloud.IdentityService.Api.Models;
	using HomeCloud.IdentityService.Business.Entities.Applications;
	using HomeCloud.IdentityService.Business.Services;
	using HomeCloud.Mapping;
	using HomeCloud.Mapping.Extensions;
	using HomeCloud.Mvc.ActionConstraints;
	using HomeCloud.Mvc.DataAnnotations;
	using Microsoft.AspNetCore.Mvc;
	using System;
	using System.Threading.Tasks;

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
		/// <param name="id">The client identifier.</param>
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
	}
}
