namespace HomeCloud.Scheduling
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	#endregion

	/// <summary>
	/// Represents Scheduler options.
	/// </summary>
	public class SchedulerOptions
	{
		/// <summary>
		/// Gets or sets the time interval the scheduled task shoul be triggered..
		/// </summary>
		/// <value>
		/// The time interval.
		/// </value>
		public TimeSpan? Schedule { get; set; }

		/// <summary>
		/// Gets or sets the event handler to notify subscribers about an <see cref="Exception"/> thrown.
		/// </summary>
		/// <value>
		/// The event handler.
		/// </value>
		public EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskExceptionHandler { get; set; }
	}
}
