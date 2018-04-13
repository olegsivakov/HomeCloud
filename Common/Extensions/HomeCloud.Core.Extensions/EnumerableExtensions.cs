namespace HomeCloud.Core.Extensions
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	#endregion

	/// <summary>
	/// Provides extension methods for <see cref="IEnumerable"/> collections.
	/// </summary>
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Performs the specified action on each element of <see cref="IEnumerable{T}"/> by chunks of the specified size asynchronously.
		/// Within a portion of elements the action against each element is being performed in parallel.
		/// </summary>
		/// <typeparam name="T">The type of elements in the collection.</typeparam>
		/// <param name="source">The source collection.</param>
		/// <param name="action">The action to perform against the chunked collection.</param>
		/// <param name="size">The chunk size.</param>
		/// <returns>The asynchronous operation.</returns>
		[Obsolete("Use built-in TPL framework")]
		public static void ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> action, int size)
		{
			int count = source.Count();

			ForEachAsync(
				(offset, limit) => new PagedList<T>(source.Skip(offset).Take(limit))
				{
					Offset = offset,
					Limit = limit,
					TotalCount = count
				},
				action,
				size);
		}

		/// <summary>
		/// Projects each element of a sequence into a new form asynchronously.
		/// </summary>
		/// <typeparam name="T">The type of elements in the source collection.</typeparam>
		/// <typeparam name="TResult">The type of the value returned by selector.</typeparam>
		/// <param name="source">A sequence of values to invoke a transform function on.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <returns>The instance of <see cref="IEnumerable{T}"/> whose elements are the result of  invoking the transform function on each element of source.</returns>
		public static IEnumerable<TResult> SelectAsync<T, TResult>(this IEnumerable<T> source, Func<T, Task<TResult>> selector)
		{
			return source.AsParallel().Select(item =>
			{
				Task<TResult> task = selector(item);
				task.Wait();

				return task.Result;
			});
		}

		/// <summary>
		/// Performs the specified action on each selector element by chunks of the specified size asynchronously.
		/// Within a portion of elements the action against each element is being performed in parallel.
		/// </summary>
		/// <typeparam name="T">The type of elements in the collection.</typeparam>
		/// <param name="selector">The action that defines a single chunk operation. The parameters specified in selector meets current offset index and chunk size value.</param>
		/// <param name="action">The action to perform against the chunked collection.</param>
		/// <param name="size">The chunk size.</param>
		/// <returns>The asynchronous operation.</returns>
		[Obsolete("Use built-in TPL framework")]
		private static void ForEachAsync<T>(Func<int, int, IPaginable<T>> selector, Func<T, Task> action, int size)
		{
			IPaginable<T> chunks = null;

			do
			{
				int offset = (chunks?.Limit).GetValueOrDefault() + chunks?.Offset ?? 0;
				int limit = chunks?.Limit ?? size;

				chunks = selector(offset, limit);

				chunks.AsParallel().ForAll(async chunk => await action(chunk));
			}
			while (chunks.Offset < chunks.TotalCount);
		}
	}
}
