namespace HomeCloud.Core.Extensions
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;

	#endregion

	/// <summary>
	/// Provides extension methods for <see cref="IPaginable"/> collection.
	/// </summary>
	public static class PaginableExtensions
	{
		/// <summary>
		/// Sorts the elements of a sequence in ascending order according to a key.
		/// </summary>
		/// <typeparam name="T">The type of the elements of source.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <returns>
		/// An <see cref="IPaginable" /> whose elements are sorted in ascending order according to a key.
		/// </returns>
		public static IPaginable<T> OrderAscending<T, TKey>(this IPaginable<T> source, Func<T, TKey> keySelector)
		{
			if (keySelector is null)
			{
				return source;
			}

			IEnumerable<T> result = source.OrderBy(keySelector);
			return new PagedList<T>(result)
			{
				Offset = source.Offset,
				Limit = source.Limit,
				TotalCount = source.TotalCount
			};
		}

		/// <summary>
		/// Sorts the elements of a sequence in descending order according to a key.
		/// </summary>
		/// <typeparam name="T">The type of the elements of source.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <returns>
		/// An <see cref="IPaginable" /> whose elements are sorted in descending order according to a key.
		/// </returns>
		public static IPaginable<T> OrderDescending<T, TKey>(IPaginable<T> source, Func<T, TKey> keySelector)
		{
			if (keySelector is null)
			{
				return source;
			}

			IEnumerable<T> result = source.OrderByDescending(keySelector);
			return new PagedList<T>(result)
			{
				Offset = source.Offset,
				Limit = source.Limit,
				TotalCount = source.TotalCount
			};
		}
	}
}
