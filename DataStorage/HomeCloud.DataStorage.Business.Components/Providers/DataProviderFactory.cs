namespace HomeCloud.DataStorage.Business.Components.Providers
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.DataStorage.Business.Services.Providers;

	#endregion

	/// <summary>
	/// Provides methods to provide data providers of <see cref="IDataProvider"/> type.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Services.Providers.IDataProviderFactory" />
	public class DataProviderFactory : IDataProviderFactory
	{
		#region Private Members

		/// <summary>
		/// The provider container.
		/// </summary>
		private readonly IDictionary<Type, object> container = new Dictionary<Type, object>();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DataProviderFactory"/> class.
		/// </summary>
		/// <param name="dataStoreProvider">The <see cref="IDataStoreProvider"/> provider.</param>
		public DataProviderFactory(IDataStoreProvider dataStoreProvider)
		{
			this.container.Add(typeof(IDataStoreProvider), dataStoreProvider);
		}

		#endregion

		#region IDataProviderFactory Implementations

		/// <summary>
		/// Gets the data provider.
		/// </summary>
		/// <typeparam name="T">the type of data provider derived from <see cref="T:HomeCloud.DataStorage.Business.Services.Providers.IDataProvider" />.</typeparam>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataStorage.Business.Services.Providers.IDataProvider" />.
		/// </returns>
		public virtual IDataProvider GetProvider<T>() where T : IDataProvider
		{
			Type type = typeof(T);

			if (!this.container.ContainsKey(type))
			{
				return default(T);
			}

			return this.container[type] as IDataProvider;
		}

		#endregion
	}
}
