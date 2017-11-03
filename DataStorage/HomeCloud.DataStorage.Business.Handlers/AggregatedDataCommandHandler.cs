namespace HomeCloud.DataStorage.Business.Handlers
{
	#region Usings

	using HomeCloud.Core;

	using HomeCloud.DataStorage.Business.Commands;
	using HomeCloud.DataStorage.Business.Providers;

	#endregion

	/// <summary>
	/// Provides methods to handle the command that executes the specified action against the data provided by <see cref="IAggregatedDataCommandHandler" />.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Handlers.DataCommandHandlerBase" />
	/// <seealso cref="HomeCloud.DataStorage.Business.Handlers.IAggregatedDataCommandHandler" />
	public class AggregatedDataCommandHandler : DataCommandHandlerBase, IAggregatedDataCommandHandler
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AggregatedDataCommandHandler" /> class.
		/// </summary>
		/// <param name="providerFactory">The data provider factory.</param>
		/// <param name="commandFactory">The action command factory.</param>
		public AggregatedDataCommandHandler(IServiceFactory<IDataProvider> providerFactory, IActionCommandFactory commandFactory)
			: base(providerFactory.Get<IAggregationDataProvider>(), commandFactory)
		{
		}

		#endregion
	}
}
