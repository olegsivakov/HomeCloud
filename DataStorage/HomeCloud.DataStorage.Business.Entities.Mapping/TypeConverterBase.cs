namespace HomeCloud.DataStorage.Business.Entities.Mapping
{
	/// <summary>
	/// Provides common methods for type converters.
	/// </summary>
	public abstract class TypeConverterBase
	{
		/// <summary>
		/// Validates the specified source and target.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TTarget">The type of the target.</typeparam>
		/// <param name="source">The source.</param>
		/// <param name="target">The target.</param>
		/// <returns>The instance of <see cref="TTarget"/>.</returns>
		protected TTarget Validate<TSource, TTarget>(TSource source, TTarget target)
			where TTarget : new()
		{
			if (source == null)
			{
				return default(TTarget);
			}

			if (target == null)
			{
				target = new TTarget();
			}

			return target;
		}
	}
}
