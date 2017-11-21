namespace HomeCloud.Core.Extensions
{
	#region Usings

	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using HomeCloud.Mapping;

	#endregion

	/// <summary>
	/// Provides extension methods for mapping one instance to another.
	/// </summary>
	public static class MapperExtensions
	{
		/// <summary>
		/// Creates the instance of <see cref="TTarget"/> and maps the instance of <see cref="TSource"/> to it.
		/// </summary>
		/// <typeparam name="TSource">The type of the source instance.</typeparam>
		/// <typeparam name="TTarget">The type of the target instance.</typeparam>
		/// <param name="mapper">The <see cref="IMapper"/> mapper.</param>
		/// <param name="source">The instance of <see cref="TSource"/>.</param>
		/// <returns>The instance of <see cref="TTarget"/>.</returns>
		public static async Task<TTarget> MapNewAsync<TSource, TTarget>(this IMapper mapper, TSource source)
			where TTarget : new()
		{
			return await mapper.MapAsync(source, new TTarget());
		}

		/// <summary>
		/// Creates the instance of <see cref="IEnumerable{T}"/> and maps the instance of <see cref="IEnumerable{TSource}"/> to them.
		/// </summary>
		/// <typeparam name="TSource">The type of the source instance.</typeparam>
		/// <typeparam name="TTarget">The type of the target instance.</typeparam>
		/// <param name="mapper">The <see cref="IMapper"/> mapper.</param>
		/// <param name="source">The list of instances of <see cref="TSource"/>.</param>
		/// <returns>The list of instances of <see cref="TTarget"/>.</returns>
		public static async Task<IEnumerable<TTarget>> MapNewAsync<TSource, TTarget>(this IMapper mapper, IEnumerable<TSource> source)
			where TTarget : new()
		{
			if (source is null || source.Count() == 0)
			{
				return Enumerable.Empty<TTarget>();
			}

			IEnumerable<Task<TTarget>> tasks = source.Select(async item => await mapper.MapNewAsync<TSource, TTarget>(item));

			return await Task.WhenAll(tasks);
		}
	}
}
