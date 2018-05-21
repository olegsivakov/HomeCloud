﻿namespace HomeCloud.IdentityService.Api
{
	#region Usings

	using System;

	using HomeCloud.IdentityService.Api.DependencyInjection;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;

	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	using HomeCloud.Mvc.Exceptions;
	using HomeCloud.Mvc;

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
				.AddInputValidation();

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

			application.UseCors(policyBuilder =>
			{
				policyBuilder.WithOrigins("https://homecloudweb.azurewebsites.net", "http://localhost:8080").AllowAnyHeader().WithExposedHeaders("X-Total-Count").AllowAnyMethod();
			});

			application.UseMvc();


		}

		#endregion
	}
}
