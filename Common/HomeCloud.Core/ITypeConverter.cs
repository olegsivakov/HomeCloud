namespace HomeCloud.Core
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Defines delegate to convert instance of type <see cref="TSource"/> to the instance of type <see cref="TTarget"/>
	/// </summary>
	/// <typeparam name="TSource">The type of the source.</typeparam>
	/// <typeparam name="TTarget">The type of the target.</typeparam>
	public interface ITypeConverter<TSource, TTarget>
	{
		/// <summary>
		/// Gets the type converter delegate.
		/// </summary>
		/// <value>
		/// The converter delegate.
		/// </value>
		Func<TSource, TTarget> Converter { get; }
	}
}
