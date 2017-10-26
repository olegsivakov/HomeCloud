namespace HomeCloud.DataStorage.Business.Components.Providers
{
	#region Usings

	using HomeCloud.DataStorage.Api.Configuration;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Services.Providers;

	using HomeCloud.DataAccess.Services.Factories;

	using Microsoft.Extensions.Options;

	#endregion

	/// <summary>
	///  Provides methods to manage data from data store database.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Services.Providers.IDataStoreProvider" />
	public class DataStoreProvider : IDataStoreProvider
	{
		#region Private Members

		/// <summary>
		/// The data context scope factory.
		/// </summary>
		private readonly IDataContextScopeFactory dataContextScopeFactory = null;

		/// <summary>
		/// The connection strings.
		/// </summary>
		private readonly ConnectionStrings connectionStrings = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DataStoreProvider"/> class.
		/// </summary>
		/// <param name="dataContextScopeFactory">The data context scope factory.</param>
		/// <param name="connectionStrings">The connection strings.</param>
		public DataStoreProvider(IDataContextScopeFactory dataContextScopeFactory, IOptionsSnapshot<ConnectionStrings> connectionStrings)
		{
			this.dataContextScopeFactory = dataContextScopeFactory;
			this.connectionStrings = connectionStrings?.Value;
		}

		#endregion

		#region IDataStoreProvider Implementations

		/// <summary>
		/// Creates the storage.
		/// </summary>
		/// <param name="storage">The storage.</param>
		public void CreateStorage(Storage storage)
		{
		}

		#endregion
	}
}
