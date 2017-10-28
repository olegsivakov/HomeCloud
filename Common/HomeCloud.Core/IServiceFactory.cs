namespace HomeCloud.Core
{
	/// <summary>
	/// Defines a factory for the services of <see cref="T" /> type.
	/// </summary>
	/// <typeparam name="T">The type of the service.</typeparam>
	public interface IServiceFactory<T>
	{
		/// <summary>
		/// Gets the specified service which type is derived from the type specified in the factory.
		/// </summary>
		/// <typeparam name="TService">The type of the service derived from <see cref="T"/>.</typeparam>
		/// <returns>The instance of <see cref="T"/>.</returns>
		T Get<TService>() where TService : T;
	}
}
