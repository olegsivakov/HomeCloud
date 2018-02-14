namespace HomeCloud.Mapping
{
	#region Usings

	using System;
	using System.Linq;

	using HomeCloud.Core;
	using HomeCloud.DependencyInjection;

	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// Provides extension methods to set up mapping services to <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.
	/// </summary>
	public static class MapperServiceCollectionExtensions
	{
		/// <summary>
		/// Adds the mapper services to the service collection.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <returns>The instance of <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.</returns>
		public static IServiceCollection AddMapper(this IServiceCollection services)
		{
			services.AddFactory<ITypeConverter>();

			services.AddSingleton<IMapper, Mapper>();

			return services;
		}

		/// <summary>
		/// Adds <see cref="ITypeConverter"/> converters to the service collection.
		/// </summary>
		/// <typeparam name="TConverter">The derived type of <see cref="ITypeConverter"/>.</typeparam>
		/// <typeparam name="TImplementation">The type of the implementation.</typeparam>
		/// <param name="services">The collection of services.</param>
		/// <returns>The instance of <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.</returns>
		public static IServiceCollection AddTypeConverter<TConverter, TImplementation>(this IServiceCollection services)
			where TConverter : class, ITypeConverter
			where TImplementation : class, TConverter
		{
			Type implementationType = typeof(TImplementation);

			ServiceDescriptor descriptor = services.FirstOrDefault(item => item.ImplementationType == implementationType && item.Lifetime == ServiceLifetime.Singleton);
			if (descriptor != null)
			{
				services.AddSingleton<TConverter>((TImplementation)descriptor.ImplementationInstance);

				return services;
			}

			if (implementationType.GetConstructor(Type.EmptyTypes) != null)
			{
				services.AddSingleton<TConverter, TImplementation>();
			}
			else
			{
				services.AddScoped<TConverter, TImplementation>();
			}

			return services;
		}
	}
}
