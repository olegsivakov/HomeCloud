namespace HomeCloud.Data.IO
{
	#region Usings

	using System;

	using HomeCloud.DependencyInjection;

	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// Provides extension methods to set up file system services to <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.
	/// </summary>
	public static class FileSystemServiceCollectionExtensions
	{
		/// <summary>
		/// Adds the file system services to the service collection.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="setupAction">The setup action.</param>
		/// <returns>The instance of <see cref="IFileSystemBuilder"/>.</returns>
		public static IFileSystemBuilder AddFileSystem(this IServiceCollection services, Action<FileSystemOptions> setupAction)
		{
			if (setupAction is null)
			{
				services.Configure<FileSystemOptions>(options => { });
			}
			else
			{
				services.Configure(setupAction);
			}

			return new FileSystemBuilder(services);
		}
	}
}
