﻿namespace HomeCloud.Data.DependencyInjection
{
	#region Usings

	using System;

	using HomeCloud.Data.DependencyInjection.Builders;
	using HomeCloud.Data.SqlServer;
	using HomeCloud.DependencyInjection;

	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// Provides extension methods to set up <see cref="SqlServer"/> database services to <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.
	/// </summary>
	public static class SqlServerDBServiceCollectionExtensions
	{
		/// <summary>
		/// Adds the SQL server database services to the service collection.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="setupAction">The setup action.</param>
		/// <returns>The instance of <see cref="ISqlServerDBBuilder"/>.</returns>
		public static ISqlServerDBBuilder AddSqlServerDB(this IServiceCollection services, Action<SqlServerDBOptions> setupAction)
		{
			services.AddFactory<ISqlServerDBRepository>();

			services.Configure(setupAction);

			return new SqlServerDBBuilder(services);
		}
	}
}
