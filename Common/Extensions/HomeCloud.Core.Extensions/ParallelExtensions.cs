namespace HomeCloud.Core.Extensions
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	#endregion

	/// <summary>
	/// Provides extension methods for parallel task execution.
	/// </summary>
	public static class ParallelExtensions
	{
		#region Public Methods

		/// <summary>
		/// Invokes asynchronous actions in parallel.
		/// </summary>
		/// <param name="actions">The actions.</param>
		public static void InvokeAsync(params Func<Task>[] actions)
		{
			IEnumerable<Action> invocations = actions.Select(action => new Action(() =>
			{
				Task task = action();
				task.Wait();
			}));

			Parallel.Invoke(invocations.ToArray());
		}

		#endregion
	}
}
