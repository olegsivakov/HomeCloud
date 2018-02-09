namespace HomeCloud.Core
{


	/// <summary>
	/// Defines methods to convert instance of type <see cref="TSource"/> to the instance of type <see cref="TTarget"/>
	/// </summary>
	/// <typeparam name="TSource">The type of the source.</typeparam>
	/// <typeparam name="TTarget">The type of the target.</typeparam>
	/// <seealso cref="HomeCloud.Core.ITypeConverter" />
	public interface ITypeConverter<TSource, TTarget> : ITypeConverter
	{
		/// <summary>
		/// Converts the instance of <see cref="TSource" /> type to the instance of <see cref="TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="TSource" />.</param>
		/// <param name="target">The instance of <see cref="TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="TTarget" />.
		/// </returns>
		TTarget Convert(TSource source, TTarget target);
	}
}
