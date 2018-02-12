namespace HomeCloud.Scheduling
{
	#region Usings

	using System;

	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// Provides extension methods to add scheduler services in service collection.
	/// </summary>
	public static class SchedulerServiceCollectionExtensions
	{
		/// <summary>
		/// Adds the scheduler services to the service collection.
		/// </summary>
		/// <param name="services">The service collection container.</param>
		/// <param name="setupAction">The action used to configure scheduler options.</param>
		/// <returns>The instance of <see cref="ISchedulerBuilder"/>.</returns>
		public static ISchedulerBuilder AddScheduler(this IServiceCollection services, Action<SchedulerOptions> setupAction = null)
		{
			if (setupAction is null)
			{
				services.Configure<SchedulerOptions>(options => { });
			}
			else
			{
				services.Configure(setupAction);
			}

			return new SchedulerBuilder(services);
		}
	}
}
