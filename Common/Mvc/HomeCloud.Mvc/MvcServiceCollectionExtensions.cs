namespace HomeCloud.Mvc
{
	#region Usings

	using System;
	using System.Buffers;

	using HomeCloud.Mvc.Providers;
	using HomeCloud.Mvc.Validation;

	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Formatters;

	using Microsoft.Extensions.DependencyInjection;

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Provides extension methods to add services to extend <see cref="Mvc"/> to service collection.
	/// </summary>
	public static class MvcServiceCollectionExtensions
	{
		/// <summary>
		/// Extends <see cref="Aspnet.Core Mvc"/>.
		/// </summary>
		/// <param name="builder">The <see cref="Mvc"/> builder.</param>
		/// <returns>The instance of <see cref="IMvcBuilder"/></returns>
		public static IMvcBuilder Extend(this IMvcBuilder builder)
		{
			builder.Services.AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();

			return builder.UseJsonOutput();
		}

		/// <summary>
		/// Adds support of the validation of action input parameters.
		/// </summary>
		/// <param name="builder">The <see cref="Mvc" /> builder.</param>
		/// <returns>The instance of <see cref="IMvcBuilder"/></returns>
		public static IMvcBuilder AddInputValidation(this IMvcBuilder builder)
		{
			builder.Services.Configure<MvcOptions>(options =>
			{
				options.Filters.Add<InputValidationFilter>();
			});

			return builder;
		}

		/// <summary>
		/// Adds support of multipart form data requests.
		/// </summary>
		/// <param name="builder">The <see cref="Mvc" /> builder.</param>
		/// <returns>The instance of <see cref="IMvcBuilder"/></returns>
		public static IMvcBuilder AddMultipartFormData(this IMvcBuilder builder)
		{
			builder.Services.Configure<MvcOptions>(options =>
			{
				options.InputFormatters.RemoveType<Formatters.MultipartFormDataInputFormatter>();
				options.InputFormatters.Add(new Formatters.MultipartFormDataInputFormatter());
			});

			return builder;
		}

		/// <summary>
		/// Overrides default <see cref="JsonOutputFormatter"/> to support <see cref="JSON"/> content.
		/// </summary>
		/// <param name="builder">The <see cref="Mvc" /> builder.</param>
		/// <returns>The instance of <see cref="IMvcBuilder"/></returns>
		public static IMvcBuilder UseJsonOutput(this IMvcBuilder builder)
		{
			JsonSerializerSettings settings = null;
			builder.Services.Configure<MvcJsonOptions>(options =>
			{
				settings = options.SerializerSettings;
			});

			builder.Services.Configure<MvcOptions>(options =>
			{
				options.OutputFormatters.RemoveType<JsonOutputFormatter>();
				options.OutputFormatters.Add(new Formatters.JsonOutputFormatter(settings, ArrayPool<char>.Shared));
			});

			return builder;
		}
	}
}
