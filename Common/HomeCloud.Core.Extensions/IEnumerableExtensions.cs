namespace HomeCloud.Core.Extensions
{
	#region Usings

	using System.Collections.Generic;
	using System.Linq;

	#endregion

	/// <summary>
	/// Provides extension methods for <see cref="System.Collections.IEnumerable"/> collections.
	/// </summary>
	public static class IEnumerableExtensions
	{
		/// <summary>
		/// Converts a generic <see cref="IEnumerable{out T}" /> to a generic <see cref="IPaginable{out T}"/>.
		/// </summary>
		/// <typeparam name="T">The type of items in the <see cref="IEnumerable{out T}" /> collection.</typeparam>
		/// <param name="items">The items to convert.</param>
		/// <returns></returns>
		public static IPaginable<T> AsPaginable<T>(this IEnumerable<T> items)
		{
			int count = items.Count();

			return new PagedList<T>(items)
			{
				Limit = count,
				TotalCount = count
			};
		}
	}
}
