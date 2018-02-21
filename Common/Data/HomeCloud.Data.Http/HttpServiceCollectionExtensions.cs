namespace HomeCloud.Data.Http
{
	#region Usings

	using System;

	using HomeCloud.DependencyInjection;

	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// Provides extension methods to set up <see cref="Http"/> resource access services to <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.
	/// </summary>
	public static class HttpServiceCollectionExtensions
	{
		/// <summary>
		/// Adds the <see cref="Http"/> resource access services to the service collection.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="setupAction">The setup action.</param>
		/// <returns>The instance of <see cref="IHttpBuilder"/>.</returns>
		public static IHttpBuilder AddHttp(this IServiceCollection services, Action<HttpOptions> setupAction)
		{
			services.AddFactory<IHttpRepository>();

			if (setupAction is null)
			{
				services.Configure<HttpOptions>(options => { });
			}
			else
			{
				services.Configure(setupAction);
			}

			return new HttpBuilder(services);
		}
	}
}
