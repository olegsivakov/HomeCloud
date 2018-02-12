namespace HomeCloud.Scheduling
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Defines methods to build the scheduler from the series of <see cref="IScheduledTask"/> tasks.
	/// </summary>
	public interface ISchedulerBuilder
	{
		/// <summary>
		/// Adds the task of <see cref="TService"/> type to the scheduler.
		/// </summary>
		/// <typeparam name="TService">The type of the <see cref="IScheduledTask"/> service.</typeparam>
		/// <typeparam name="TImplementation">The type of the <see cref="IScheduledTask"/> implementation.</typeparam>
		/// <param name="schedule">The time period the task is executed.</param>
		/// <returns>The instance of <see cref="ISchedulerBuilder"/>.</returns>
		ISchedulerBuilder AddTask<TService, TImplementation>(TimeSpan? schedule = null)
			where TService : class, IScheduledTask
			where TImplementation : class, TService;
	}
}
