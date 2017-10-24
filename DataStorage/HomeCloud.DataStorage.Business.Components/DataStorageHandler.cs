using HomeCloud.DataAccess.Services;
using HomeCloud.DataAccess.Services.Factories;
using HomeCloud.DataStorage.Api.Configuration;
using HomeCloud.DataStorage.DataAccess.Services.Repositories;
using Microsoft.Extensions.Options;

namespace HomeCloud.DataStorage.Business.Components
{
	public class DataStorageHandler : DataProcessor
	{
		#region Private Members

		private readonly IDataContextScopeFactory dataContextScopeFactory = null;

		private readonly ConnectionStrings connectionStrings = null;

		#endregion

		#region Constructors

		public DataStorageHandler(IDataContextScopeFactory dataContextScopeFactory, IOptionsSnapshot<ConnectionStrings> connectionStrings)
			: base(null)
		{
			this.dataContextScopeFactory = dataContextScopeFactory;
			this.connectionStrings = connectionStrings?.Value;
		}

		#endregion

		#region DataProcessor Implementations

		protected override void ExecuteCreateStorage()
		{
			
		}

		#endregion
	}
}
