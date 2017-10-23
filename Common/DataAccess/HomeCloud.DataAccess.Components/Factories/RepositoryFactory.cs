namespace HomeCloud.DataAccess.Components.Factories
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.DataAccess.Services.Factories;

	#endregion

	/// <summary>
	/// Distributes factories of <see cref="IRepositoryFactory" /> type.
	/// </summary>
	/// <seealso cref="HomeCloud.DataAccess.Services.Factories.IRepositoryFactory" />
	public sealed class RepositoryFactory : IRepositoryFactory
	{
		#region Private Members

		/// <summary>
		/// The factory container.
		/// </summary>
		private readonly IDictionary<Type, object> container = new Dictionary<Type, object>();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="RepositoryFactory" /> class.
		/// </summary>
		/// <param name="databaseRepositoryFactory">The <see cref="IDbRepositoryFactory" /> factory.</param>
		/// <param name="documentRepositoryFactory">>The <see cref="IDocumentRepositoryFactory" /> factory.</param>
		public RepositoryFactory(IDbRepositoryFactory databaseRepositoryFactory = null, IDocumentRepositoryFactory documentRepositoryFactory = null)
		{
			if (databaseRepositoryFactory != null)
			{
				this.container.Add(typeof(IDbRepositoryFactory), databaseRepositoryFactory);
			}

			if (documentRepositoryFactory != null)
			{
				this.container.Add(typeof(IDocumentRepositoryFactory), documentRepositoryFactory);
			}
		}

		#endregion

		#region IRepositoryFactory Implementations

		/// <summary>
		/// Gets the repository factory.
		/// </summary>
		/// <typeparam name="T">The type of factory derived from <see cref="T:HomeCloud.DataAccess.Services.Factories.IRepositoryFactory" />.</typeparam>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataAccess.Services.Factories.IRepositoryFactory" />.
		/// </returns>
		public T GetFactory<T>() where T : IRepositoryFactory
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
