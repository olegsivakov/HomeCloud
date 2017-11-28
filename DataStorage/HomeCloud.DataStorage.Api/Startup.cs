namespace HomeCloud.DataStorage.Api
{
	#region Usings

	using System;
	using System.Buffers;

	using HomeCloud.DataStorage.Api.DependencyInjection;
	using HomeCloud.Exceptions;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc.Formatters;

	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	using HomeCloudJsonOutputFormatter = HomeCloud.Api.Formatters.JsonOutputFormatter;

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
			services.AddDependencies();
			services.Configure(this.Configuration);

			services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

			services.AddMvc(options =>
			{
				options.OutputFormatters.RemoveType<JsonOutputFormatter>();
				options.OutputFormatters.Insert(
					0,
					new HomeCloudJsonOutputFormatter(
						JsonSerializerSettingsProvider.CreateSerializerSettings(),
						ArrayPool<char>.Shared));
			});

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

			application.UseExceptionHandlerMiddleware();

			application.UseMvc();
		}

		#endregion
	}
}
