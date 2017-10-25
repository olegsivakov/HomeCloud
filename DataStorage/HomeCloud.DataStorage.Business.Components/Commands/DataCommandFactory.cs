namespace HomeCloud.DataStorage.Business.Components.Commands
{
	#region Usings

	using System;

	using HomeCloud.DataStorage.Business.Services.Commands;
	using HomeCloud.DataStorage.Business.Services.Providers;

	using HomeCloud.Business.Services;

	#endregion

	public class DataCommandFactory : IDataCommandFactory
	{
		#region Private Members

		private readonly IDataProviderFactory dataProviderFactory = null;

		#endregion

		#region Constructors

		public DataCommandFactory(IDataProviderFactory dataProviderFactory)
		{
			this.dataProviderFactory = dataProviderFactory;
		}

		#endregion

		#region IDataCommandFactory Implementations

		public ICommand CreateCommand<TProvider>(Action<IDataProvider> executeAction, Action<IDataProvider> undoAction)
			where TProvider : IDataProvider
		{
			IDataProvider provider = this.dataProviderFactory.GetProvider<TProvider>();

			return new DataCommand(provider, executeAction, undoAction);
		}

		#endregion
	}
}
