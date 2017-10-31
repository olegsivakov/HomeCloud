namespace HomeCloud.Core.Extensions
{
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
		public static TTarget MapNew<TSource, TTarget>(this IMapper mapper, TSource source)
		{
			return mapper.Map(source, default(TTarget));
		}
	}
}
