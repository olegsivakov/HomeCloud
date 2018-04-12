namespace HomeCloud.Core.Extensions
{
	#region Usings

	using System;
	using System.Linq;
	using System.Threading.Tasks;

	#endregion

	/// <summary>
	/// Provides extension methods for parallel task execution.
	/// </summary>
	public static class ParallelExtensions
	{
		/// <summary>
		/// Invokes asynchronous actions in parallel.
		/// </summary>
		/// <param name="actions">The actions.</param>
		public static void InvokeAsync(params Func<Task>[] actions)
		{
			var invocations = actions.Select(action => new Action(() =>
			{
				Task task = action();
				task.Wait();
			})).ToArray();

			Parallel.Invoke(invocations);
		}
	}
}
