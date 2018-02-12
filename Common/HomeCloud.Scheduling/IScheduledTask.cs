namespace HomeCloud.Scheduling
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using Microsoft.Extensions.Hosting;

	#endregion

	/// <summary>
	/// Defines a single operation executed by schedule.
	/// </summary>
	public interface IScheduledTask : IHostedService
	{
		/// <summary>
		/// The event handler occured when <see cref="Exception"/> has been thrown.
		/// </summary>
		event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException;

		/// <summary>
		/// Gets or sets the operation name.
		/// </summary>
		/// <value>
		/// The operation name.
		/// </value>
		string Name { get; }

		/// <summary>
		/// Gets the operation schedule.
		/// </summary>
		/// <value>
		/// The operation schedule.
		/// </value>
		TimeSpan Schedule { get; set; }

		/// <summary>
		/// Executes the scheduled task asynchronously.
		/// </summary>
		/// <returns>The asynchronous operation.</returns>
		Task ExecuteAsync();
	}
}
