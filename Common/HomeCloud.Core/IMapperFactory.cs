namespace HomeCloud.Core
{
	/// <summary>
	/// Defines methods to create mapper instances of <see cref="IMapper"/>.
	/// </summary>
	public interface IMapperFactory
	{
		/// <summary>
		/// Gets the instance of <see cref="IMapper"/>.
		/// </summary>
		/// <typeparam name="T">The type of the mapper derived from <see cref="IMapper"/>.</typeparam>
		/// <returns>The instance of <see cref="IMapper"/>.</returns>
		IMapper GetMapper<T>() where T : IMapper;
	}
}
