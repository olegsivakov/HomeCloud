namespace HomeCloud.Scheduling
{
	#region Usings

	using System;
	using System.Threading;
	using System.Threading.Tasks;

	#endregion

	/// <summary>
	/// Provides general implementation of the single operation executed by schedule.
	/// </summary>
	/// <seealso cref="Sabaka.Core.Scheduling.IScheduledTask" />
	public abstract class ScheduledTask : IScheduledTask
	{
		#region Private Members

		/// <summary>
		/// The background task.
		/// </summary>
		private Task task = null;

		/// <summary>
		/// The cancellation token source
		/// </summary>
		private CancellationTokenSource cancellationTokenSource = null;

		#endregion

		#region IScheduledTask Implementations

		/// <summary>
		/// The event handler occured when <see cref="Exception" /> has been thrown.
		/// </summary>
		public event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException = null;

		/// <summary>
		/// Gets or sets the operation name.
		/// </summary>
		/// <value>
		/// The operation name.
		/// </value>
		public abstract string Name { get; }

		/// <summary>
		/// Gets the operation schedule.
		/// </summary>
		/// <value>
		/// The operation schedule.
		/// </value>
		public virtual TimeSpan Schedule { get; set; }

		/// <summary>
		/// Executes the scheduled task asynchronously.
		/// </summary>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public abstract Task ExecuteAsync();

		#endregion

		#region IHostedService Implementations

		/// <summary>
		/// Triggered when the application host is ready to start the service.
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns>The asynchronous operation.</returns>
		public Task StartAsync(CancellationToken cancellationToken)
		{
			this.cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

			return this.ExecuteActionAsync(() => this.ExecuteAsync(), this.cancellationTokenSource.Token);
		}

		/// <summary>
		/// Triggered when the application host is performing a graceful shutdown.
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns>The asynchronous operation</returns>
		public async Task StopAsync(CancellationToken cancellationToken)
		{
			if (this.task != null)
			{
				this.cancellationTokenSource.Cancel();

				await Task.WhenAny(this.task, Task.Delay(-1, cancellationToken));

				cancellationToken.ThrowIfCancellationRequested();
			}
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Notifies <see cref="IScheduledTask.UnobservedTaskException"/> event subscribers when <see cref="Exception"/> has been thrown.
		/// </summary>
		/// <param name="exception">The exception.</param>
		protected void OnUnobservedTaskException(Exception exception)
		{
			UnobservedTaskExceptionEventArgs arguments = new UnobservedTaskExceptionEventArgs(exception as AggregateException ?? new AggregateException(exception));

			this.UnobservedTaskException?.Invoke(this, arguments);

			if (!arguments.Observed)
			{
				throw exception;
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Executes the specified action asynchronously.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The asynchronous operation.</returns>
		private async Task ExecuteActionAsync(Func<Task> action, CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				try
				{
					await action();
				}
				catch (Exception exception)
				{
					this.OnUnobservedTaskException(exception);
				}

				await Task.Delay(this.Schedule, cancellationToken);
			}
		}

		#endregion
	}
}
