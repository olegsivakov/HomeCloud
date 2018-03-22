namespace HomeCloud.DataStorage.Api
{
	#region Usings

	using System;
	using System.Buffers;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc.Formatters;

	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	using HomeCloud.Mvc;
	using HomeCloud.Mvc.Hypermedia;
	using HomeCloud.Mvc.Exceptions;
	using HomeCloud.DataStorage.Api.DependencyInjection;
	using HomeCloud.DataStorage.Api.Models;
	using HomeCloud.DataStorage.Api.Controllers;
	using HomeCloud.Core;

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
				.AddFileStorage(this.Configuration)
				.AddMappings()
				.AddDataStorageServices();

			services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

			services.AddMvc()
				.Extend()
				.AddInputValidation()
				.AddMultipartFormData()
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
				routes.AddRoute(nameof(StorageController.GetStorageList))
						.AddRouteLink<PagedListViewModel<StorageViewModel>>("self", nameof(StorageController.GetStorageList), model => new { offset = model.Offset, limit = model.Size })
						.AddRouteLink<PagedListViewModel<StorageViewModel>>("previous", nameof(StorageController.GetStorageList), model => new { offset = model.Offset - model.Size, limit = model.Size }, model => model.Offset > 0)
						.AddRouteLink<PagedListViewModel<StorageViewModel>>("next", nameof(StorageController.GetStorageList), model => new { offset = model.Offset + model.Size, limit = model.Size }, model => model.Offset + model.Size < model.TotalCount)
						.AddRouteLink<PagedListViewModel<StorageViewModel>>("create", nameof(StorageController.CreateStorage), null)
						.AddRouteLink<PagedListViewModel<StorageViewModel>>("get", nameof(StorageController.GetStorageList), null);

				routes.AddRoute(nameof(StorageController.GetStorageByID))
						.AddRouteLink<StorageViewModel>("self", nameof(StorageController.GetStorageByID), model => new { id = model.ID })
						.AddRouteLink<StorageViewModel>("update", nameof(StorageController.UpdateStorage), model => new { id = model.ID })
						.AddRouteLink<StorageViewModel>("delete", nameof(StorageController.DeleteStorage), model => new { id = model.ID })
						.AddRouteLink<StorageViewModel>("catalogs", nameof(CatalogController.GetCatalogList), model => new { parentID = model.ID, offset = 0, limit = 20 })
						.AddRouteLink<StorageViewModel>("data", nameof(DataController.GetDataList), model => new { catalogID = model.ID, offset = 0, limit = 20 });

				routes.AddRoute(nameof(StorageController.CreateStorage))
						.AddRouteLink<StorageViewModel>("self", nameof(StorageController.CreateStorage), null)
						.AddRouteLink<StorageViewModel>("get", nameof(StorageController.GetStorageByID), model => new { id = model.ID });

				routes.AddRoute(nameof(StorageController.UpdateStorage))
						.AddRouteLink<StorageViewModel>("self", nameof(StorageController.UpdateStorage), model => new { id = model.ID })
						.AddRouteLink<StorageViewModel>("get", nameof(StorageController.GetStorageByID), model => new { id = model.ID });

				routes.AddRoute(nameof(CatalogController.GetCatalogList))
						.AddRouteLink<CatalogListViewModel>("self", nameof(CatalogController.GetCatalogList), model => new { parentID = model.ParentID, offset = model.Offset, limit = model.Size })
						.AddRouteLink<CatalogListViewModel>("previous", nameof(CatalogController.GetCatalogList), model => new { parentID = model.ParentID, offset = model.Offset - model.Size, limit = model.Size }, model => model.Offset > 0)
						.AddRouteLink<CatalogListViewModel>("next", nameof(CatalogController.GetCatalogList), model => new { parentID = model.ParentID, offset = model.Offset + model.Size, limit = model.Size }, model => model.Offset + model.Size < model.TotalCount)
						.AddRouteLink<CatalogListViewModel>("create", nameof(CatalogController.CreateCatalog), model => new { parentID = model.ParentID })
						.AddRouteLink<CatalogListViewModel>("get", nameof(CatalogController.GetCatalogList), null);

				routes.AddRoute(nameof(CatalogController.GetCatalogByID))
						.AddRouteLink<CatalogViewModel>("self", nameof(CatalogController.GetCatalogByID), model => new { id = model.ID })
						.AddRouteLink<CatalogViewModel>("update", nameof(CatalogController.UpdateCatalog), model => new { id = model.ID })
						.AddRouteLink<CatalogViewModel>("delete", nameof(CatalogController.DeleteCatalog), model => new { id = model.ID });

				routes.AddRoute(nameof(CatalogController.CreateCatalog))
						.AddRouteLink<CatalogViewModel>("self", nameof(CatalogController.GetCatalogByID), model => new { id = model.ID })
						.AddRouteLink<CatalogViewModel>("get", nameof(CatalogController.GetCatalogByID), model => new { id = model.ID });

				routes.AddRoute(nameof(CatalogController.UpdateCatalog))
						.AddRouteLink<CatalogViewModel>("self", nameof(CatalogController.GetCatalogByID), model => new { id = model.ID })
						.AddRouteLink<CatalogViewModel>("get", nameof(CatalogController.GetCatalogByID), model => new { id = model.ID });

				routes.AddRoute(nameof(DataController.GetDataList))
						.AddRouteLink<DataListViewModel>("self", nameof(DataController.GetDataList), model => new { catalogID = model.CatalogID, offset = model.Offset, limit = model.Size })
						.AddRouteLink<DataListViewModel>("previous", nameof(DataController.GetDataList), model => new { catalogID = model.CatalogID, offset = model.Offset - model.Size, limit = model.Size }, model => model.Offset > 0)
						.AddRouteLink<DataListViewModel>("next", nameof(DataController.GetDataList), model => new { catalogID = model.CatalogID, offset = model.Offset + model.Size, limit = model.Size }, model => model.Offset + model.Size < model.TotalCount)
						.AddRouteLink<DataListViewModel>("create", nameof(DataController.CreateData), model => new { catalogID = model.CatalogID })
						.AddRouteLink<DataListViewModel>("get", nameof(DataController.GetDataList), null);

				routes.AddRoute(nameof(DataController.GetDataByID))
						.AddRouteLink<DataViewModel>("self", nameof(DataController.GetDataByID), model => new { id = model.ID })
						.AddRouteLink<DataViewModel>("delete", nameof(DataController.DeleteData), model => new { id = model.ID });

				routes.AddRoute(nameof(DataController.CreateData))
						.AddRouteLink<DataViewModel>("self", nameof(DataController.GetDataByID), model => new { id = model.ID })
						.AddRouteLink<DataViewModel>("get", nameof(DataController.GetDataByID), model => new { id = model.ID });
			});

			application.UseMvc();
		}

		#endregion
	}
}
