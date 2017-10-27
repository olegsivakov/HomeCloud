namespace HomeCloud.Core
{
	#region Usings

	using System;
	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Provides common implementation of mapper to convert instance of one type to the instance of another type.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.IMapper" />
	public class Mapper : IMapper
	{
		#region Private Members

		/// <summary>
		/// The type converter container.
		/// </summary>
		private readonly IDictionary<Type, ITypeConverter> container = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Mapper"/> class.
		/// </summary>
		/// <param name="registrar">The action to register type converters.</param>
		public Mapper(Action<IDictionary<Type, ITypeConverter>> registrar = null)
		{
			registrar?.Invoke(this.container);
		}

		#endregion

		#region IMapper Implementations

		/// <summary>
		/// Converts the specified instance of type <see cref="TSource" /> to the instance of type <see cref="TTarget" />.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TTarget">The type of the target.</typeparam>
		/// <param name="source">The instance of <see cref="TSource" />.</param>
		/// <param name="target">The instance of <see cref="TTarget" />.</param>
		/// <returns>
		/// The mapped instance of <see cref="TTarget" />.
		/// </returns>
		public TTarget Map<TSource, TTarget>(TSource source, TTarget target)
		{
			Type type = typeof(ITypeConverter<TSource, TTarget>);

			if (!this.container.ContainsKey(type))
			{
				return default(TTarget);
			}

			ITypeConverter<TSource, TTarget> typeConverter = (this.container[type] as ITypeConverter<TSource, TTarget>);

			return typeConverter.Convert(source, target);
		}

		#endregion
	}
}
