namespace HomeCloud.DataAccess.Components.Factories
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.DataAccess.Contracts;

	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataAccess.Services.Factories;

	#endregion

	/// <summary>
	/// Provides factory methods to create database query handlers.
	/// </summary>
	/// <seealso cref="HomeCloud.DataAccess.Services.Factories.IDbQueryHandlerFactory" />
	public class DbQueryHandlerFactory : IDbQueryHandlerFactory
	{
		#region Private Members

		/// <summary>
		/// The factory container.
		/// </summary>
		private readonly IDictionary<Type, Type> container = new Dictionary<Type, Type>();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DbQueryHandlerFactory"/> class.
		/// </summary>
		/// <param name="registrar">The registrar of factories.</param>
		public DbQueryHandlerFactory(Action<IDictionary<Type, Type>> registrar = null)
		{
			registrar?.Invoke(this.container);
		}

		#endregion

		#region IDbQueryHandlerFactory Implementations

		/// <summary>
		/// Gets the database-specific handler for the specified data context.
		/// </summary>
		/// <typeparam name="T">The type of database handler derived from <see cref="T:HomeCloud.DataAccess.Services.IDbQueryHandler" />.</typeparam>
		/// <param name="context">The database context.</param>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataAccess.Services.IDbQueryHandler" /> type.
		/// </returns>
		public virtual T GetHandler<T>(IDbContext context) where T : IDbQueryHandler
		{
			Type type = typeof(T);

			if (!this.container.ContainsKey(type))
			{
				return default(T);
			}

			return (T)Activator.CreateInstance(this.container[type], context);
		}

		/// <summary>
		/// Gets the query handler factory.
		/// </summary>
		/// <typeparam name="T">The type of factory derived from <see cref="T:HomeCloud.DataAccess.Services.Factories.IQueryHandlerFactory" />.</typeparam>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataAccess.Services.Factories.IQueryHandlerFactory" />.
		/// </returns>
		public virtual T GetFactory<T>() where T : IQueryHandlerFactory
		{
			return default(T);
		}

		#endregion
	}
}
