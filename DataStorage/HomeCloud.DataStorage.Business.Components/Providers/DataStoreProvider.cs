namespace HomeCloud.DataStorage.Business.Components.Providers
{
	#region Usings

	using HomeCloud.DataAccess.Services.Factories;
	using HomeCloud.DataStorage.Api.Configuration;
	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Services.Providers;

	using Microsoft.Extensions.Options;

	#endregion

	public class DataStoreProvider : IDataStoreProvider
	{
		#region Private Members

		private readonly IDataContextScopeFactory dataContextScopeFactory = null;

		private readonly ConnectionStrings connectionStrings = null;

		#endregion

		#region Constructors

		public DataStoreProvider(IDataContextScopeFactory dataContextScopeFactory, IOptionsSnapshot<ConnectionStrings> connectionStrings)
		{
			this.dataContextScopeFactory = dataContextScopeFactory;
			this.connectionStrings = connectionStrings?.Value;
		}

		#endregion

		#region IDataStoreProvider Implementations

		public void CreateStorage(Storage storage)
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}
