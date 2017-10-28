namespace HomeCloud.DataStorage.Business.Entities.Mapping
{
	#region Usings

	using HomeCloud.Core;

	#endregion

	/// <summary>
	/// Provides common implementation of mapper to convert instance of one type to the instance of another type.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.IMapper" />
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
			ITypeConverter<TSource, TTarget> converter = this.converterFactory.Get<ITypeConverter<TSource, TTarget>>() as ITypeConverter<TSource, TTarget>;

			return converter != null ? converter.Convert(source, target) : default(TTarget);
		}

		#endregion
	}
}
