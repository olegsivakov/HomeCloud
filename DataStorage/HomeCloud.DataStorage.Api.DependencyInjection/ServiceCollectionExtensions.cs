namespace HomeCloud.DataStorage.Api.DependencyInjection
{
	#region Usings

	using HomeCloud.DataAccess.Components.Factories;
	using HomeCloud.DataAccess.Services.Factories;

	using HomeCloud.DataStorage.Api.Configuration;
	using HomeCloud.DataStorage.DataAccess.Components.Factories;

	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// Configures the application dependencies.
	/// </summary>
	public static partial class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds the dependencies to <see cref="IServiceCollection"/>.
		/// </summary>
		/// <param name="services">The collection of services.</param>
		public static void AddDependencies(this IServiceCollection services)
		{
			services.AddSingleton<IDbRepositoryFactory, DataStorageRepositoryFactory>();
			services.AddSingleton<IRepositoryFactory, RepositoryFactory>();

			services.AddSingleton<IDataContextScopeFactory, DataContextScopeFactory>();
		}

		/// <summary>
		/// Sets application configuration settings to <see cref="IServiceCollection"/>.
		/// </summary>
		/// <param name="services">The collection of services.</param>
		/// <param name="configuration">The configuration.</param>
		public static void Configure(this IServiceCollection services, IConfigurationRoot configuration)
		{
			services.Configure<ConnectionStrings>(configuration.GetSection(nameof(ConnectionStrings)));

			services.Configure<Database>(configuration.GetSection(nameof(Database)));
			services.Configure<FileSystem>(configuration.GetSection(nameof(FileSystem)));
		}
	}
}
