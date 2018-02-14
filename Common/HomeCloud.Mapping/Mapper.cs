namespace HomeCloud.Mapping
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Core;

	#endregion

	/// <summary>
	/// Provides common implementation of mapper to convert instance of one type to the instance of another type.
	/// </summary>
	/// <seealso cref="HomeCloud.Mapping.IMapper" />
	public class Mapper : IMapper
	{
		#region Private Members

		/// <summary>
		/// The type converter factory.
		/// </summary>
		private readonly IServiceFactory<ITypeConverter> converterFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Mapper" /> class.
		/// </summary>
		/// <param name="converterFactory">The type converter factory.</param>
		public Mapper(IServiceFactory<ITypeConverter> converterFactory)
		{
			this.converterFactory = converterFactory;
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
			if (source == null)
			{
				return target;
			}

			if (target == null)
			{
				target = (TTarget)typeof(TTarget).GetConstructor(Type.EmptyTypes)?.Invoke(null);
			}

			ITypeConverter<TSource, TTarget> converter = this.converterFactory.Get<ITypeConverter<TSource, TTarget>>() as ITypeConverter<TSource, TTarget>;
			if (converter == null)
			{
				NullReferenceException exception = new NullReferenceException($"Unable to resolve dependency for {typeof(ITypeConverter<TSource, TTarget>).FullName}");

				throw new TypeInitializationException(typeof(Mapper).FullName, exception);
			}

			return converter.Convert(source, target);
		}

		/// <summary>
		/// Converts the specified instance of type <see cref="TSource" /> to the instance of type <see cref="TTarget" /> asynchronously.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TTarget">The type of the target.</typeparam>
		/// <param name="source">The instance of <see cref="TSource" />.</param>
		/// <param name="target">The instance of <see cref="TTarget" />.</param>
		/// <returns>
		/// The mapped instance of <see cref="TTarget" />.
		/// </returns>
		public async Task<TTarget> MapAsync<TSource, TTarget>(TSource source, TTarget target)
		{
			return await Task.Run(() => this.Map<TSource, TTarget>(source, target));
		}

		#endregion
	}
}
