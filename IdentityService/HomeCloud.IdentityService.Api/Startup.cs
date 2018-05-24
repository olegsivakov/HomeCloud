namespace HomeCloud.IdentityService.Api
{
	#region Usings

	using System;

	using HomeCloud.IdentityService.Api.DependencyInjection;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;

	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	using HomeCloud.Mvc;
	using HomeCloud.Mvc.Exceptions;
	using HomeCloud.Mvc.Hypermedia;
	using HomeCloud.IdentityService.Api.Controllers;
	using HomeCloud.IdentityService.Api.Models;
	using HomeCloud.Mvc.Models;

	#endregion

	/// <summary>
	/// Represents an instance the application starts up.
	/// </summary>
	public class Startup
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Startup"/> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the configuration.
		/// </summary>
		/// <value>
		/// The configuration.
		/// </value>
		public IConfiguration Configuration { get; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Configures and adds services to the container. This method gets called by the runtime.
		/// </summary>
		/// <param name="services">The service collection of <see cref="IServiceCollection" /> type.</param>
		/// <returns>The instance of <see cref="IServiceProvider"/>.</returns>
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services
				.AddDatabases(this.Configuration)
				.AddMappings()
				.AddIdentityServices();

			services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

			services.AddCors();

			services.AddMvc()
				.Extend()
				.AddInputValidation()
				.AddHypermedia();

			return services.BuildServiceProvider();

		}

		/// <summary>
		/// Configures the HTTP request pipeline. This method gets called by the runtime.
		/// </summary>
		/// <param name="application">The application.</param>
		/// <param name="environment">The environment.</param>
		public void Configure(IApplicationBuilder application, IHostingEnvironment environment)
		{
			if (environment.IsDevelopment())
			{
				application.UseDeveloperExceptionPage();
			}

			application.UseExceptionHandling();
			application.UseHypermedia(routes =>
			{
				routes.AddRoute(nameof(ClientController.GetClientList))
						.AddRoute<PagedListViewModel<ApplicationViewModel>>("self", nameof(ClientController.GetClientList), model => new { offset = model.Offset, limit = model.Size })
						.AddRoute<PagedListViewModel<ApplicationViewModel>>("previous", nameof(ClientController.GetClientList), model => new { offset = model.Offset - model.Size, limit = model.Size }, model => model.Offset > 0)
						.AddRoute<PagedListViewModel<ApplicationViewModel>>("next", nameof(ClientController.GetClientList), model => new { offset = model.Offset + model.Size, limit = model.Size }, model => model.Offset + model.Size < model.TotalCount)
						.AddRoute<PagedListViewModel<ApplicationViewModel>>("create", nameof(ClientController.CreateClient), null)
						.AddRoute<PagedListViewModel<ApplicationViewModel>, ApplicationViewModel>("items", nameof(ClientController.GetClientByID), model => new { id = model.ID });

				routes.AddRoute(nameof(ClientController.GetClientByID))
						.AddRoute<ClientViewModel>("self", nameof(ClientController.GetClientByID), model => new { id = model.ID })
						.AddRoute<ClientViewModel>("update", nameof(ClientController.UpdateClient), model => new { id = model.ID })
						.AddRoute<ClientViewModel>("delete", nameof(ClientController.DeleteClientByID), model => new { id = model.ID })
						.AddRoute<ClientViewModel>("scopes", nameof(ClientController.GetClientScopeList), model => new { id = model.ID })
						.AddRoute<ClientViewModel>("origins", nameof(ClientController.GetClientOriginList), model => new { id = model.ID })
						.AddRoute<ClientViewModel>("secrets", nameof(ClientController.GetClientSecretList), model => new { id = model.ID })
						.AddRoute<ClientViewModel>("grants", nameof(ClientController.GetClientGrantList), model => new { id = model.ID });

				routes.AddRoute(nameof(ClientController.CreateClient))
						.AddRoute<ClientViewModel>("self", nameof(ClientController.CreateClient), null)
						.AddRoute<ClientViewModel>("get", nameof(ClientController.GetClientByID), model => new { id = model.ID });

				routes.AddRoute(nameof(ClientController.GetClientScopeList))
						.AddRoute<ApplicationDataListViewModel<string>>("self", nameof(ClientController.GetClientScopeList), model => new { id = model.ApplicationID })
						.AddRoute<ApplicationDataListViewModel<string>>("save", nameof(ClientController.SaveClientScopeList), model => new { id = model.ApplicationID });

				routes.AddRoute(nameof(ClientController.SaveClientScopeList))
						.AddRoute<ApplicationDataListViewModel<string>>("self", nameof(ClientController.SaveClientScopeList), model => new { id = model.ApplicationID })
						.AddRoute<ApplicationDataListViewModel<string>>("get", nameof(ClientController.GetClientScopeList), model => new { id = model.ApplicationID });

				routes.AddRoute(nameof(ClientController.GetClientOriginList))
						.AddRoute<ApplicationDataListViewModel<string>>("self", nameof(ClientController.GetClientOriginList), model => new { id = model.ApplicationID })
						.AddRoute<ApplicationDataListViewModel<string>>("save", nameof(ClientController.SaveClientOriginList), model => new { id = model.ApplicationID });

				routes.AddRoute(nameof(ClientController.SaveClientOriginList))
						.AddRoute<ApplicationDataListViewModel<string>>("self", nameof(ClientController.SaveClientOriginList), model => new { id = model.ApplicationID })
						.AddRoute<ApplicationDataListViewModel<string>>("get", nameof(ClientController.GetClientOriginList), model => new { id = model.ApplicationID });

				routes.AddRoute(nameof(ClientController.GetClientSecretList))
						.AddRoute<ApplicationDataListViewModel<SecretViewModel>>("self", nameof(ClientController.GetClientSecretList), model => new { id = model.ApplicationID })
						.AddRoute<ApplicationDataListViewModel<SecretViewModel>>("save", nameof(ClientController.SaveClientSecretList), model => new { id = model.ApplicationID });

				routes.AddRoute(nameof(ClientController.SaveClientOriginList))
						.AddRoute<ApplicationDataListViewModel<SecretViewModel>>("self", nameof(ClientController.SaveClientOriginList), model => new { id = model.ApplicationID })
						.AddRoute<ApplicationDataListViewModel<SecretViewModel>>("get", nameof(ClientController.GetClientOriginList), model => new { id = model.ApplicationID });

				routes.AddRoute(nameof(ClientController.GetClientGrantList))
						.AddRoute<ApplicationDataListViewModel<GrantViewModel>>("self", nameof(ClientController.GetClientGrantList), model => new { id = model.ApplicationID });

				routes.AddRoute(nameof(ResourceController.GetApiResourceList))
						.AddRoute<PagedListViewModel<ApplicationViewModel>>("self", nameof(ResourceController.GetApiResourceList), model => new { offset = model.Offset, limit = model.Size })
						.AddRoute<PagedListViewModel<ApplicationViewModel>>("previous", nameof(ResourceController.GetApiResourceList), model => new { offset = model.Offset - model.Size, limit = model.Size }, model => model.Offset > 0)
						.AddRoute<PagedListViewModel<ApplicationViewModel>>("next", nameof(ResourceController.GetApiResourceList), model => new { offset = model.Offset + model.Size, limit = model.Size }, model => model.Offset + model.Size < model.TotalCount)
						.AddRoute<PagedListViewModel<ApplicationViewModel>>("create", nameof(ResourceController.CreateApiResource), null)
						.AddRoute<PagedListViewModel<ApplicationViewModel>, ApplicationViewModel>("items", nameof(ResourceController.GetApiResourceByID), model => new { id = model.ID });

				routes.AddRoute(nameof(ResourceController.CreateApiResource))
						.AddRoute<ApiResourceViewModel>("self", nameof(ResourceController.CreateApiResource), null)
						.AddRoute<ApiResourceViewModel>("get", nameof(ResourceController.GetApiResourceByID), model => new { id = model.ID });

				routes.AddRoute(nameof(ResourceController.GetApiResourceByID))
						.AddRoute<ApiResourceViewModel>("self", nameof(ResourceController.GetApiResourceByID), model => new { id = model.ID })
						.AddRoute<ApiResourceViewModel>("update", nameof(ResourceController.UpdateApiResource), model => new { id = model.ID })
						.AddRoute<ApiResourceViewModel>("delete", nameof(ResourceController.DeleteApiResourceByID), model => new { id = model.ID })
						.AddRoute<ApiResourceViewModel>("scopes", nameof(ResourceController.GetApiResourceScopeList), model => new { id = model.ID })
						.AddRoute<ApiResourceViewModel>("claims", nameof(ResourceController.GetApiResourceClaimList), model => new { id = model.ID })
						.AddRoute<ApiResourceViewModel>("secrets", nameof(ResourceController.GetApiResourceSecretList), model => new { id = model.ID });

				routes.AddRoute(nameof(ResourceController.GetApiResourceScopeList))
						.AddRoute<ApplicationDataListViewModel<string>>("self", nameof(ResourceController.GetApiResourceScopeList), model => new { id = model.ApplicationID })
						.AddRoute<ApplicationDataListViewModel<string>>("save", nameof(ResourceController.SaveApiResourceScopeList), model => new { id = model.ApplicationID });

				routes.AddRoute(nameof(ResourceController.SaveApiResourceScopeList))
						.AddRoute<ApplicationDataListViewModel<string>>("self", nameof(ResourceController.SaveApiResourceScopeList), model => new { id = model.ApplicationID })
						.AddRoute<ApplicationDataListViewModel<string>>("get", nameof(ResourceController.GetApiResourceScopeList), model => new { id = model.ApplicationID });

				routes.AddRoute(nameof(ResourceController.GetApiResourceClaimList))
						.AddRoute<ApplicationDataListViewModel<string>>("self", nameof(ResourceController.GetApiResourceClaimList), model => new { id = model.ApplicationID })
						.AddRoute<ApplicationDataListViewModel<string>>("save", nameof(ResourceController.SaveApiResourceClaimList), model => new { id = model.ApplicationID });

				routes.AddRoute(nameof(ResourceController.SaveApiResourceClaimList))
						.AddRoute<ApplicationDataListViewModel<string>>("self", nameof(ResourceController.SaveApiResourceClaimList), model => new { id = model.ApplicationID })
						.AddRoute<ApplicationDataListViewModel<string>>("get", nameof(ResourceController.GetApiResourceClaimList), model => new { id = model.ApplicationID });

				routes.AddRoute(nameof(ResourceController.GetApiResourceSecretList))
						.AddRoute<ApplicationDataListViewModel<SecretViewModel>>("self", nameof(ResourceController.GetApiResourceSecretList), model => new { id = model.ApplicationID })
						.AddRoute<ApplicationDataListViewModel<SecretViewModel>>("save", nameof(ResourceController.SaveApiResourceSecretList), model => new { id = model.ApplicationID });

				routes.AddRoute(nameof(ResourceController.SaveApiResourceSecretList))
						.AddRoute<ApplicationDataListViewModel<SecretViewModel>>("self", nameof(ResourceController.SaveApiResourceSecretList), model => new { id = model.ApplicationID })
						.AddRoute<ApplicationDataListViewModel<SecretViewModel>>("get", nameof(ResourceController.GetApiResourceSecretList), model => new { id = model.ApplicationID });

				routes.AddRoute(nameof(GrantController.GetGrantList))
						.AddRoute<PagedListViewModel<GrantViewModel>>("self", nameof(GrantController.GetGrantList), model => new { offset = model.Offset, limit = model.Size })
						.AddRoute<PagedListViewModel<GrantViewModel>>("previous", nameof(GrantController.GetGrantList), model => new { offset = model.Offset - model.Size, limit = model.Size }, model => model.Offset > 0)
						.AddRoute<PagedListViewModel<GrantViewModel>>("next", nameof(GrantController.GetGrantList), model => new { offset = model.Offset + model.Size, limit = model.Size }, model => model.Offset + model.Size < model.TotalCount)
						.AddRoute<PagedListViewModel<GrantViewModel>, GrantViewModel>("items", nameof(GrantController.GetGrantByID), model => new { id = model.ID });

				routes.AddRoute(nameof(GrantController.GetGrantByID))
						.AddRoute<GrantViewModel>("self", nameof(GrantController.GetGrantByID), model => new { id = model.ID })
						.AddRoute<GrantViewModel>("delete", nameof(GrantController.DeleteGrantByID), model => new { id = model.ID });
			});

			application.UseCors(policyBuilder =>
			{
				policyBuilder.WithOrigins("https://homecloudweb.azurewebsites.net", "http://localhost:8080").AllowAnyHeader().WithExposedHeaders("X-Total-Count").AllowAnyMethod();
			});

			application.UseMvc();


		}

		#endregion
	}
}
