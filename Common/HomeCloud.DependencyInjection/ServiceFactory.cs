namespace HomeCloud.DependencyInjection
{
	#region Usings

	using System;

	using HomeCloud.Core;
	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// Provides a factory for the services of <see cref="T"/> type.
	/// </summary>
	/// <typeparam name="T">The type of the service.</typeparam>
	/// <seealso cref="HomeCloud.Core.IServiceFactory{T}" />
	internal class ServiceFactory<T> : IServiceFactory<T>
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IServiceProvider"/> provider.
		/// </summary>
		private readonly IServiceProvider provider = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceFactory{T}"/> class.
		/// </summary>
		/// <param name="provider">The service provider.</param>
		internal ServiceFactory(IServiceProvider provider)
		{
			this.provider = provider;
		}

		#endregion

		#region IServiceFactory<T> Implementations

		/// <summary>
		/// Gets the specified service which type is derived from the type specified in the factory.
		/// </summary>
		/// <typeparam name="TService">The type of the service derived from <see cref="!:T" />.</typeparam>
		/// <returns>
		/// The instance of <see cref="!:T" />.
		/// </returns>
		public T Get<TService>() where TService : T
		{
			return provider.GetService<TService>();
		}

		#endregion
	}
}
