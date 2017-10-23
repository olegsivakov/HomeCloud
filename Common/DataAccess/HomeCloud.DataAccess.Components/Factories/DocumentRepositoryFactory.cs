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
	/// Provides factory methods to create document repositories.
	/// </summary>
	/// <seealso cref="HomeCloud.DataAccess.Services.Factories.IDocumentRepositoryFactory" />
	public class DocumentRepositoryFactory : IDocumentRepositoryFactory
	{
		#region Private Members

		/// <summary>
		/// The factory container.
		/// </summary>
		private readonly IDictionary<Type, Type> container = new Dictionary<Type, Type>();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentRepositoryFactory"/> class.
		/// </summary>
		/// <param name="registrar">The registrar of factories.</param>
		public DocumentRepositoryFactory(Action<IDictionary<Type, Type>> registrar = null)
		{
			registrar?.Invoke(this.container);
		}

		#endregion

		#region IDocumentRepositoryFactory Implementations

		/// <summary>
		/// Gets the document-specific repository for specified data context.
		/// </summary>
		/// <typeparam name="T">The type of the repository derived from <see cref="T:HomeCloud.DataAccess.Services.IDocumentRepository" />.</typeparam>
		/// <param name="context">The document context.</param>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataAccess.Services.IDocumentRepository" /> type.
		/// </returns>
		public virtual T GetRepository<T>(IDocumentContext context) where T : IDocumentRepository
		{
			Type type = typeof(T);

			if (!this.container.ContainsKey(type))
			{
				return default(T);
			}

			return (T)Activator.CreateInstance(this.container[type], context);
		}

		/// <summary>
		/// Gets the repository factory.
		/// </summary>
		/// <typeparam name="T">The type of factory derived from <see cref="T:HomeCloud.DataAccess.Services.Factories.IRepositoryFactory" />.</typeparam>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataAccess.Services.Factories.IRepositoryFactory" />.
		/// </returns>
		public virtual T GetFactory<T>() where T : IRepositoryFactory
		{
			return default(T);
		}

		#endregion
	}
}
