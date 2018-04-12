using System;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCloud.Core.Extensions
{
	public static class ParallelExtensions
	{
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
