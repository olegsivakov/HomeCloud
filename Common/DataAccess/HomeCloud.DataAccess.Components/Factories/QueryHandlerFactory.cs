namespace HomeCloud.DataAccess.Components.Factories
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.DataAccess.Services.Factories;

	#endregion

	/// <summary>
	/// Defines methods to distribute factories of <see cref="IQueryHandlerFactory" /> type.
	/// </summary>
	/// <seealso cref="HomeCloud.DataAccess.Services.Factories.IQueryHandlerFactory" />
	public sealed class QueryHandlerFactory : IQueryHandlerFactory
	{
		#region Private Members

		/// <summary>
		/// The factory container.
		/// </summary>
		private readonly IDictionary<Type, object> container = new Dictionary<Type, object>();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="QueryHandlerFactory" /> class.
		/// </summary>
		/// <param name="databaseQueryHandlerFactory">The <see cref="IDbQueryHandlerFactory" /> factory.</param>
		public QueryHandlerFactory(IDbQueryHandlerFactory databaseQueryHandlerFactory = null)
		{
			if (databaseQueryHandlerFactory != null)
			{
				this.container.Add(typeof(IDbQueryHandlerFactory), databaseQueryHandlerFactory);
			}
		}

		#endregion

		#region ICommandHandlerFactory Implementations

		/// <summary>
		/// Gets the query handler factory.
		/// </summary>
		/// <typeparam name="T">The type of factory derived from <see cref="T:HomeCloud.DataAccess.Services.Factories.IQueryHandlerFactory" />.</typeparam>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataAccess.Services.Factories.IQueryHandlerFactory" />.
		/// </returns>
		public T GetFactory<T>() where T : IQueryHandlerFactory
		{
			if (!this.container.ContainsKey(typeof(T)))
			{
				return default(T);
			}

			return (T)this.container[typeof(T)];
		}

		#endregion
	}
}
