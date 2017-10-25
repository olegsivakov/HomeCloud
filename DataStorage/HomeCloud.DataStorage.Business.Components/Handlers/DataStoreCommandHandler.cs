namespace HomeCloud.DataStorage.Business.Components.Handlers
{
	#region Usings

	using HomeCloud.DataStorage.Business.Services.Handlers;
	using HomeCloud.DataStorage.Business.Services.Providers;
	using HomeCloud.DataStorage.Business.Components.Commands;

	using HomeCloud.Business.Services;
	using System;

	#endregion

	public class DataStoreCommandHandler : DataCommandFactory, IDataStoreCommandHandler
	{
		#region IDataStoreCommandHandler Implementations

		#region Public Properties

		public ICommand Command { get; private set; }

		#endregion

		#region Constructors

		public DataStoreCommandHandler(IDataProviderFactory providerFactory)
			: base(providerFactory)
		{
		}

		#endregion

		#region Public Methods

		public void Handle()
		{
			this.Command.Execute();
		}

		public void Undo()
		{
			this.Command.Undo();
		}

		#endregion

		#endregion
	}
}
