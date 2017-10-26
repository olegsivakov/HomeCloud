﻿namespace HomeCloud.Core
{
	/// <summary>
	/// Defines common interface to convert instance of one type to the instance of another type.
	/// </summary>
	public interface IMapper
	{
		/// <summary>
		/// Converts the specified instance of type <see cref="TSource"/> to the instance of type <see cref="TTarget"/>.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TTarget">The type of the target.</typeparam>
		/// <param name="source">The instance of <see cref="TSource"/>.</param>
		/// <returns>The instance of <see cref="TTarget"/>.</returns>
		TTarget Map<TSource, TTarget>(TSource source);
	}
}