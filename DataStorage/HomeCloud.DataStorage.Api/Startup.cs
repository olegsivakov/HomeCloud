namespace HomeCloud.DataStorage.Api
{
	#region Usings

	using System;

	using HomeCloud.DataStorage.Api.DependencyInjection;
	using HomeCloud.DataStorage.Api.Models;
	using HomeCloud.DataStorage.Api.Controllers;

	using HomeCloud.Mvc;
	using HomeCloud.Mvc.Exceptions;
	using HomeCloud.Mvc.Hypermedia;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;

	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

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

			application.UseCors(builder =>
			{
				builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
			});

			application.UseExceptionHandling();
			application.UseHypermedia(routes =>
			{
				routes.AddRoute(nameof(StorageController.GetStorageList))
						.AddRoute<PagedListViewModel<StorageViewModel>>("self", nameof(StorageController.GetStorageList), model => new { offset = model.Offset, limit = model.Size })
						.AddRoute<PagedListViewModel<StorageViewModel>>("previous", nameof(StorageController.GetStorageList), model => new { offset = model.Offset - model.Size, limit = model.Size }, model => model.Offset > 0)
						.AddRoute<PagedListViewModel<StorageViewModel>>("next", nameof(StorageController.GetStorageList), model => new { offset = model.Offset + model.Size, limit = model.Size }, model => model.Offset + model.Size < model.TotalCount)
						.AddRoute<PagedListViewModel<StorageViewModel>>("create", nameof(StorageController.CreateStorage), null)
						.AddRoute<PagedListViewModel<StorageViewModel>, StorageViewModel>("items", nameof(StorageController.GetStorageByID), model => new { id = model.ID });

				routes.AddRoute(nameof(StorageController.GetStorageByID))
						.AddRoute<StorageViewModel>("self", nameof(StorageController.GetStorageByID), model => new { id = model.ID })
						.AddRoute<StorageViewModel>("update", nameof(StorageController.UpdateStorage), model => new { id = model.ID })
						.AddRoute<StorageViewModel>("delete", nameof(StorageController.DeleteStorage), model => new { id = model.ID })
						.AddRoute<StorageViewModel>("catalog", nameof(CatalogController.GetCatalogByID), model => new { id = model.ID });

				routes.AddRoute(nameof(StorageController.CreateStorage))
						.AddRoute<StorageViewModel>("self", nameof(StorageController.CreateStorage), null)
						.AddRoute<StorageViewModel>("get", nameof(StorageController.GetStorageByID), model => new { id = model.ID });

				routes.AddRoute(nameof(StorageController.UpdateStorage))
						.AddRoute<StorageViewModel>("self", nameof(StorageController.UpdateStorage), model => new { id = model.ID })
						.AddRoute<StorageViewModel>("get", nameof(StorageController.GetStorageByID), model => new { id = model.ID });

				routes.AddRoute(nameof(CatalogController.GetCatalogDataList))
						.AddRoute<DataListViewModel>("self", nameof(CatalogController.GetCatalogDataList), model => new { id = model.CatalogID, offset = model.Offset, limit = model.Size })
						.AddRoute<DataListViewModel>("previous", nameof(CatalogController.GetCatalogDataList), model => new { id = model.CatalogID, offset = model.Offset - model.Size, limit = model.Size }, model => model.Offset > 0)
						.AddRoute<DataListViewModel>("next", nameof(CatalogController.GetCatalogDataList), model => new { id = model.CatalogID, offset = model.Offset + model.Size, limit = model.Size }, model => model.Offset + model.Size < model.TotalCount)
						.AddRoute<DataListViewModel>("createCatalog", nameof(CatalogController.CreateCatalog), model => new { catalogID = model.CatalogID })
						.AddRoute<DataListViewModel>("createFile", nameof(FileController.CreateFile), model => new { catalogID = model.CatalogID })
						.AddRoute<DataListViewModel, DataViewModel>("items", nameof(CatalogController.GetCatalogByID), model => new { id = model.ID }, model => model.IsCatalog)
						.AddRoute<DataListViewModel, DataViewModel>("items", nameof(FileController.GetFileByID), model => new { id = model.ID }, model => !model.IsCatalog);

				routes.AddRoute(nameof(CatalogController.GetCatalogByID))
						.AddRoute<CatalogViewModel>("self", nameof(CatalogController.GetCatalogByID), model => new { id = model.ID })
						.AddRoute<CatalogViewModel>("update", nameof(CatalogController.UpdateCatalog), model => new { id = model.ID })
						.AddRoute<CatalogViewModel>("delete", nameof(CatalogController.DeleteCatalog), model => new { id = model.ID })
						.AddRoute<CatalogViewModel>("data", nameof(CatalogController.GetCatalogDataList), model => new { id = model.ID, offset = 0, limit = 20 }); ;

				routes.AddRoute(nameof(CatalogController.CreateCatalog))
						.AddRoute<CatalogViewModel>("self", nameof(CatalogController.CreateCatalog), model => new { id = model.ID })
						.AddRoute<CatalogViewModel>("get", nameof(CatalogController.GetCatalogByID), model => new { id = model.ID });

				routes.AddRoute(nameof(CatalogController.UpdateCatalog))
						.AddRoute<CatalogViewModel>("self", nameof(CatalogController.UpdateCatalog), model => new { id = model.ID })
						.AddRoute<CatalogViewModel>("get", nameof(CatalogController.GetCatalogByID), model => new { id = model.ID });

				routes.AddRoute(nameof(FileController.GetFileByID))
						.AddRoute<DataViewModel>("self", nameof(FileController.GetFileByID), model => new { id = model.ID })
						.AddRoute<DataViewModel>("download", nameof(FileController.DownloadFileByID), model => new { id = model.ID })
						.AddRoute<DataViewModel>("delete", nameof(FileController.DeleteFile), model => new { id = model.ID });

				routes.AddRoute(nameof(FileController.CreateFile))
						.AddRoute<DataViewModel>("self", nameof(FileController.CreateFile), model => new { id = model.ID })
						.AddRoute<DataViewModel>("get", nameof(FileController.GetFileByID), model => new { id = model.ID });
			});

			application.UseMvc();
		}

		#endregion
	}
}
