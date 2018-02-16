﻿namespace HomeCloud.Data.MongoDB
{
	#region Usings

	using System;

	using HomeCloud.DependencyInjection;

	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// Provides extension methods to set up <see cref="MongoDB"/> database services to <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.
	/// </summary>
	public static class MongoDBServiceCollectionExtensions
	{
		/// <summary>
		/// Adds the <see cref="MongoDB"/> database services to the service collection.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="setupAction">The setup action.</param>
		/// <returns>The instance of <see cref="IMongoDBBuilder"/>.</returns>
		public static IMongoDBBuilder AddMongoDB(this IServiceCollection services, Action<MongoDBOptions> setupAction)
		{
			services.AddFactory<IMongoDBRepository>();

			if (setupAction is null)
			{
				services.Configure<MongoDBOptions>(options => { });
			}
			else
			{
				services.Configure(setupAction);
			}

			return new MongoDBBuilder(services);
		}
	}
}