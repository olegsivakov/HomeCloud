namespace HomeCloud.Scheduling
{
	#region Usings

	using System;
	using System.Linq;
	using System.Reflection;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Options;

	#endregion

	/// <summary>
	/// Provides methods to build the scheduler from the series of <see cref="IScheduledTask"/> tasks.
	/// </summary>
	/// <seealso cref="Sabaka.Core.Scheduling.ISchedulerBuilder" />
	internal class SchedulerBuilder : ISchedulerBuilder
	{
		#region Private Members

		/// <summary>
		/// The service collection container used as a scheduler to register <see cref="IScheduledTask"/> instances.
		/// </summary>
		private readonly IServiceCollection services = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SchedulerBuilder"/> class.
		/// </summary>
		/// <param name="services">The services.</param>
		internal SchedulerBuilder(IServiceCollection services)
		{
			this.services = services;
		}

		#endregion

		#region ISchedulerBuilder Implementations

		/// <summary>
		/// Adds the task of <see cref="TService"/> type to the scheduler.
		/// </summary>
		/// <typeparam name="TService">The type of the <see cref="IScheduledTask" /> service.</typeparam>
		/// <typeparam name="TImplementation">The type of the <see cref="IScheduledTask" /> implementation.</typeparam>
		/// <param name="schedule">The time period the task is executed.</param>
		/// <returns>
		/// The instance of <see cref="ISchedulerBuilder" />.
		/// </returns>
		public ISchedulerBuilder AddTask<TService, TImplementation>(TimeSpan? schedule = null)
			where TService : class, IScheduledTask
			where TImplementation : class, TService
		{
			services.AddSingleton<IHostedService, TImplementation>(provider =>
			{
				ConstructorInfo constructor = typeof(TImplementation).GetConstructors().FirstOrDefault();
				if (constructor == null)
				{
					throw new TypeInitializationException(typeof(TImplementation).FullName, new Exception($"No constructor defined for '{typeof(TImplementation).FullName}'"));
				}

				object[] parameters = constructor.GetParameters().Select(parameter =>
				{
					return provider.GetRequiredService(parameter.ParameterType);
				}).ToArray();

				TImplementation instance = constructor.Invoke(parameters) as TImplementation;

				IOptions<SchedulerOptions> options = provider.GetRequiredService<IOptions<SchedulerOptions>>();

				instance.Schedule = schedule ?? options.Value.Schedule ?? throw new TypeInitializationException(typeof(TService).FullName, new Exception("Schedule period not defined for the task either in options."));

				if (options.Value.UnobservedTaskExceptionHandler != null)
				{
					instance.UnobservedTaskException += options.Value.UnobservedTaskExceptionHandler;
				}

				return instance;
			});

			return this;
		}

		#endregion
	}
}
