namespace HomeCloud.Mvc.Hypermedia
{
	#region Usings

	using System;

	using Microsoft.AspNetCore.Builder;

	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Infrastructure;

	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// Provides extension methods for hypermedia links embedding.
	/// </summary>
	public static class HypermediaExtensions
	{
		/// <summary>
		/// Adds hypermedia to <see cref="IApplicationBuilder"/> execution pipeline.
		/// </summary>
		/// <param name="application">The <see cref="IApplicationBuilder"/> application.</param>
		/// <param name="configureRoutes">A callback to configure hypermedia routes.</param>
		/// <returns>The instance of <see cref="IApplicationBuilder"/>.</returns>
		/// <exception cref="ArgumentNullException">Thrown if arguments are null.</exception>
		public static IApplicationBuilder UseHypermedia(this IApplicationBuilder application, Action<ILinkRouteMap> configureRoutes)
		{
			if (application == null)
			{
				throw new ArgumentNullException(nameof(application));
			}

			if (configureRoutes == null)
			{
				throw new ArgumentNullException(nameof(configureRoutes));
			}

			ILinkRouteMap routeMap = application.ApplicationServices.GetRequiredService<ILinkRouteMap>();

			configureRoutes(routeMap);

			routeMap.Build();

			return application;
		}

		/// <summary>
		/// Adds hypermedia for application.
		/// </summary>
		/// <param name="builder">The <see cref="IMvcBuilder"/> builder.</param>
		/// <returns>the instance of <see cref="IMvcBuilder"/>.</returns>
		public static IMvcBuilder AddHypermedia(this IMvcBuilder builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException(nameof(builder));
			}

			builder.Services.AddSingleton<ILinkRouteMap, LinkRouteMap>();
			builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
			builder.Services.AddScoped<ILinkService, LinkService>();

			builder.Services.Configure<MvcJsonOptions>(options =>
			{
				options.SerializerSettings.Converters.Clear();
				options.SerializerSettings.Converters.Add(new HypermediaJsonConverter());
			});

			builder.Services.Configure<MvcOptions>(options =>
			{
				options.Filters.Add<HypermediaFilter>(options.Filters.Count);
			});

			return builder;
		}
	}
}
